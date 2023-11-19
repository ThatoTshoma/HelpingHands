using Hospital_App.CollectionViewModel;
using Hospital_App.Data;
using Hospital_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using static Hospital_App.Areas.Identity.Pages.Account.RegisterModel;


namespace Hospital_App.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var city = _context.Cities.ToList();
            return View(city);
        }
        public IActionResult AddBusiness()
        {
            var collection = new BusinessCollection
            {
                Business = new Business(),
                Cities = _context.Cities.OrderBy(c => c.CityName).ToList()

            };
            return View(collection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBusiness(BusinessCollection model)
        {
            var collection = new BusinessCollection
            {
                Business = model.Business,
                Cities = _context.Cities.OrderBy(c => c.CityName).ToList()
            };

            var business = new Business();
            business.OrganizationName = model.Business.OrganizationName;
            business.NPONumber = model.Business.NPONumber;
            business.AddressLine1 = model.Business.AddressLine1;
            business.AddressLine2 = model.Business.AddressLine2;
            business.ConatactNumber = model.Business.ConatactNumber;
            business.Email = model.Business.Email;
            business.OperatingHours = model.Business.OperatingHours;
            business.SuburbID = model.Business.SuburbID;

            _context.Businesses.Add(business);
            _context.SaveChanges();
            ViewBag.Messege = "Businesses Added";
            return RedirectToAction("ViewContract");

        }
        public IActionResult UpdateBusiness(int id)
        {
            var existingBusiness = _context.Businesses.Find(id);

            if (existingBusiness == null)
            {
                return NotFound();
            }

            var collection = new BusinessCollection
            {
                Business = existingBusiness,
                Cities = _context.Cities.OrderBy(c => c.CityName).ToList()
            };

            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateBusiness(int id, BusinessCollection model)
        {
            if (id != model.Business.OrganizationID)
            {
                return NotFound();
            }

            var business = _context.Businesses.Find(id);

            if (business == null)
            {
                return NotFound();
            }

            business.OrganizationName = model.Business.OrganizationName;
            business.NPONumber = model.Business.NPONumber;
            business.AddressLine1 = model.Business.AddressLine1;
            business.AddressLine2 = model.Business.AddressLine2;
            business.ConatactNumber = model.Business.ConatactNumber;
            business.Email = model.Business.Email;
            business.OperatingHours = model.Business.OperatingHours;
            business.SuburbID = model.Business.SuburbID;

            _context.SaveChanges();

            ViewBag.Message = "Business Updated";
            return RedirectToAction("ViewBusniness");
        }

        public IActionResult ViewBusniness()
        {
            var business = _context.Businesses.Include(c => c.Suburb).ToList();
            return View(business);
        }
        public IActionResult AddNurse()
        {
            var collection = new NurseCollection
            {
                ApplicationUser = new InputModel(),
                Nurse = new Nurse(),

            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNurse(NurseCollection model)
        {
            var user = new ApplicationUser
            {
                UserName = model.ApplicationUser.UserName,
                Email = model.ApplicationUser.Email,
                UserRole = "Nurse",
                Status = "Active",

            };

            var result = await _userManager.CreateAsync(user, model.ApplicationUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Nurse");

                var nurse = new Nurse
                {
                    FirstName = model.Nurse.FirstName,
                    Surname = model.Nurse.Surname,
                    EmailAddress = model.ApplicationUser.Email,
                    FullName = model.Nurse.FirstName + " " + model.Nurse.Surname,
                    IDNumber = model.Nurse.IDNumber,
                    ContactNumber = model.Nurse.ContactNumber,
                    Gender = model.Nurse.Gender,
                    ApplicationUserId = user.Id,

                };

                _context.Nurses.Add(nurse);
                await _context.SaveChangesAsync();

                return RedirectToAction("ListOfNurses");
            }

            return NotFound();
        }
        public async Task<IActionResult> ListOfNurses()
        {
            return View(await _context.Nurses.ToListAsync());
        }
        public IActionResult UpdateNurse(int? id)
        {
            Nurse nurseModel = new Nurse();
            if (id > 0)
                nurseModel = FetchNurseByID(id);
            return View(nurseModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateNurse(int id, [Bind("NurseID,FirstName,Surname,ContactNumber")] Nurse nurseModel)
        {
            if (ModelState.IsValid)
            {

                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("AddOrUpdateNurses", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("NurseID", nurseModel.NurseID);
                    sqlCmd.Parameters.AddWithValue("FirstName", nurseModel.FirstName);
                    sqlCmd.Parameters.AddWithValue("Surname", nurseModel.Surname);
                    sqlCmd.Parameters.AddWithValue("IDNumber", nurseModel.IDNumber);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction("ListOfNurses");
            }
            return View(nurseModel);
        }
        public Nurse FetchNurseByID(int? id)
        {
            Nurse nurseModel = new Nurse();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DataTable dtbl = new DataTable();
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("ViewNurseByID", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("NurseID", id);

                sqlDa.Fill(dtbl);
                if (dtbl.Rows.Count == 1)
                {
                    nurseModel.NurseID = Convert.ToInt32(dtbl.Rows[0]["NurseID"].ToString());
                    nurseModel.FirstName = dtbl.Rows[0]["FirstName"].ToString();
                    nurseModel.Surname = dtbl.Rows[0]["Surname"].ToString();
                    nurseModel.IDNumber =dtbl.Rows[0]["IDNumber"].ToString();

                }
                return nurseModel;
            }
        }
        public IActionResult AddOfficeManager()
        {
            var collection = new OfficeManagerCollection
            {
                ApplicationUser = new InputModel(),
                OfficeManager = new OfficeManager(),

            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOfficeManager(OfficeManagerCollection model)
        {
            var user = new ApplicationUser
            {
                UserName = model.ApplicationUser.UserName,
                Email = model.ApplicationUser.Email,
                UserRole = "OfficeManager",
                Status = "Active",
            };

            var result = await _userManager.CreateAsync(user, model.ApplicationUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "OfficeManager");

                var officeManager = new OfficeManager
                {
                    FirstName = model.OfficeManager.FirstName,
                    Surname = model.OfficeManager.Surname,
                    EmailAddress = model.ApplicationUser.Email,
                    IDNumber = model.OfficeManager.IDNumber,
                    ContactNumber = model.OfficeManager.ContactNumber,
                    Gender = model.OfficeManager.Gender,
                    ApplicationUserId = user.Id,

                };

                _context.OfficeManagers.Add(officeManager);
                await _context.SaveChangesAsync();

                return RedirectToAction("ListOfOfficeManagers");
            }

            return NotFound();
        }
        public async Task<IActionResult> ListOfOfficeManagers()
        {
            return View(await _context.OfficeManagers.ToListAsync());
        }
        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCity(City model)
        {
            if (_context.Cities.Any(c => c.CityName == model.CityName))
            {
                ModelState.AddModelError("CityName", "The name of the city already exitst!");
                return View(model);
            }

            _context.Cities.Add(model);
            _context.SaveChanges();
            return RedirectToAction("ViewCity");
        }
        public IActionResult ViewCity()
        {
            var city = _context.Cities.ToList();
            return View(city);
        }
        public IActionResult AddSuburb()
        {
            var collection = new SuburbCollection
            {
                Suburb = new Suburb(),
                Cities = _context.Cities.OrderBy(c => c.CityName).ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSuburb(SuburbCollection model)
        {
            if (!ModelState.IsValid)
            {
                var collection = new SuburbCollection
                {
                    Suburb = model.Suburb,
                    Cities = _context.Cities.OrderBy(c => c.CityName).ToList()
                };
                return View(collection);
            }

            _context.Suburbs.Add(model.Suburb);
            _context.SaveChanges();
            return RedirectToAction("ViewSuburb");
        }
        public IActionResult ViewSuburb()
        {
            var doctor = _context.Suburbs.Include(c => c.City).ToList();
            return View(doctor);
        }
        public IActionResult AddChronic()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddChronic(Chronic model)
        {
            if (_context.Chronics.Any(c => c.ChronicName == model.ChronicName))
            {
                ModelState.AddModelError("ChronicName", "The name of the city already exitst!");
                return View(model);
            }

            _context.Chronics.Add(model);
            _context.SaveChanges();
            return RedirectToAction("ViewChronic");
        }
        public IActionResult ViewChronic()
        {
            var chronic = _context.Chronics.ToList();
            return View(chronic);
        }
    }

}
