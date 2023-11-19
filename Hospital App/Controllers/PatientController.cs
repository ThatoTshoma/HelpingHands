using Hospital_App.CollectionViewModel;
using Hospital_App.Data;
using Hospital_App.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Hospital_App.Areas.Identity.Pages.Account.RegisterModel;


namespace Hospital_App.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public PatientController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProfileDetails()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        public IActionResult UpdateProfile()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.SingleOrDefault(c => c.ApplicationUserId.ToString() ==userId);

            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(Patient model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);

            if (!ValidateDateOfBirth(model.IDNumber, model.DateOfBirth))
            {
                ModelState.AddModelError("DateOfBirth", "Date of birth does not match the ID number.");
                return View();
            }

            patient.FirstName = model.FirstName;
            patient.Surname = model.Surname;
            patient.FullName = model.FirstName + " " + model.Surname;
            patient.IDNumber = model.IDNumber;
            patient.DateOfBirth = model.DateOfBirth;
            patient.EmergencyContatctPerson = model.EmergencyContatctPerson;
            patient.EmergencyNumber = model.EmergencyNumber;
            patient.Gender = model.Gender;
            patient.ContactNumber = model.ContactNumber;

            ViewBag.message = "Profile successfully updated";


            _context.SaveChanges();
            return View();
        }

        private bool ValidateDateOfBirth(string idNumber, DateTime dateOfBirth)
        {
            if (idNumber.Length != 13)
            {
                return false; 
            }

            int year = int.Parse(idNumber.Substring(0, 2));
            int month = int.Parse(idNumber.Substring(2, 2));
            int day = int.Parse(idNumber.Substring(4, 2));

            if (month > 12)
            {
                year += 2000;
                month -= 20;
            }
            else
            {
                year += 1900;
            }
            if (dateOfBirth.Year == year && dateOfBirth.Month == month && dateOfBirth.Day == day)
            {
                return true; 
            }
            else
            {
                return false; 
            }
        }


        public IActionResult LoadSuburbs(int cityId)
        {
            var suburbs = _context.Suburbs.Where(s => s.CityID == cityId).Select(s => new { suburbID = s.SuburbID, suburbName = s.SuburbName }).ToList();
            return Json(suburbs);
        }
        public JsonResult GetSuburbsByCity(int cityId)
        {

            var suburbs = _context.Suburbs.Where(s => s.CityID == cityId).Select(s => new { s.SuburbID, s.SuburbName }).OrderBy(s => s.SuburbName).ToList();
            return Json(suburbs);
        }
        public IActionResult AddContract()
        {
            var collection = new ContractCollection
            {
                Contract = new Contract(),
                Cities = _context.Cities.OrderBy(c => c.CityName).ToList()

            };
            return View(collection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddContract(ContractCollection model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var collection = new ContractCollection
            {
                Contract = model.Contract,
                Cities = _context.Cities.OrderBy(c => c.CityName).ToList()
            };

            var patient = _context.Patients.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);
            var contract = new Contract();
            contract.PatientID = patient.PatientID;
            contract.NurseID = null; 
            contract.ContractDate = DateTime.Now.Date;
            contract.StartDate = null;
            contract.EndDate = null;
            contract.AddressLine1 = model.Contract.AddressLine1;
            contract.AddressLine2 = model.Contract.AddressLine2;
            contract.SuburbID = model.Contract.SuburbID;
            contract.WoundDesc = model.Contract.WoundDesc;
            contract.Status = "New";

            _context.Contracts.Add(contract);
            _context.SaveChanges();
            ViewBag.Messege = "Contact Added";
            return RedirectToAction("ViewContract");
            //return View(collection);

        }
        public IActionResult UpdateContract(int id)
        {
            var contract = _context.Contracts.Include(c => c.Patient).SingleOrDefault(c => c.ContractID == id);
            var collection = new ContractCollection
            {
                Contract = contract,
                Cities = _context.Cities.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateContract(ContractCollection model)
        {
            var contract = _context.Contracts.Find(model.Contract.ContractID);

            if (contract != null)
            {
                contract.AddressLine1 = model.Contract.AddressLine1;
                contract.AddressLine2 = model.Contract.AddressLine2;
                contract.SuburbID = model.Contract.SuburbID;
                contract.WoundDesc = model.Contract.WoundDesc;

                _context.Contracts.Update(contract);
                _context.SaveChanges();
                ViewBag.Message = "Contract Updated";
            }
            else
            {
                ViewBag.Message = "Contract not found";
            }

            return RedirectToAction("ViewContract");
        }
        public IActionResult CancelContract(int id)
        {
            var contract = _context.Contracts.Include(c => c.Patient).SingleOrDefault(c => c.ContractID == id);
            var collection = new ContractCollection
            {
                Contract = contract,
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelContract(ContractCollection model)
        {
            var contract = _context.Contracts.Find(model.Contract.ContractID);

            if (contract != null)
            {
                contract.Status = "Canceled";
               

                _context.Contracts.Update(contract);
                _context.SaveChanges();
                ViewBag.Message = "Contract Updated";
            }
            else
            {
                ViewBag.Message = "Contract not found";
            }

            return RedirectToAction("ViewContract");
        }



        public IActionResult ViewContract(string status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.Single(c => c.ApplicationUserId.ToString() == userId);
            IQueryable<Contract> contractsQuery = _context.Contracts.Include(c => c.Patient).Include(c => c.Suburb).Where(c => c.PatientID == patient.PatientID);

            if (!string.IsNullOrEmpty(status))
            {
                contractsQuery = contractsQuery.Where(c => c.Status == status);
            }

            var contracts = contractsQuery.ToList();

            return View(contracts);
        }

        public IActionResult ViewNewContract()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.Single(c => c.ApplicationUserId.ToString() == userId);
            var visits = _context.Contracts.Include(c => c.Patient).Include(c => c.Suburb).Where(c => c.PatientID == patient.PatientID).Where(c => c.Status == "New").ToList();
            return View(visits);
        }

        public IActionResult AddChronic()
        {
            var collection = new PatientChronicCollection
            {
                PatientChronic = new PatientChronic(),
                Chronics = _context.Chronics.ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddChronic(PatientChronicCollection model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.Single(c => c.ApplicationUserId.ToString() == userId);

            foreach (var selectedChronicId in model.PatientChronic.SelectedChronics)
            {
                var patientChronic = new PatientChronic();
                patientChronic.ChronicID = selectedChronicId;
                patientChronic.PatientID = patient.PatientID;

                _context.PatientChronics.Add(patientChronic);
            }

            _context.SaveChanges();

            ViewBag.Message = "Chronic Conditions added successfully";
            return RedirectToAction("ViewChronic");
        }
        public async Task<IActionResult> DeleteChronic(int? id)
        {
           
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.Single(c => c.ApplicationUserId.ToString() == userId);
            var patChronic = await _context.PatientChronics
                .Include(s => s.Chronic)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.ChronicID == id && m.PatientID == patient.PatientID);

            return View(patChronic);
        }

        [HttpPost, ActionName("DeleteChronic")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patChronic = _context.PatientChronics.Find(id);
            if (patChronic == null)
            {
                return NotFound();
            }

            _context.PatientChronics.Remove(patChronic);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ViewChronic));
        }


        public IActionResult ViewChronic()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.Single(c => c.ApplicationUserId.ToString() == userId);
            var patientChronics = _context.PatientChronics.Include(c => c.Patient).Include(c => c.Chronic).Where(c => c.PatientID == patient.PatientID).ToList();
            return View(patientChronics);
        }

        public IActionResult ViewVisit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = _context.Patients.Single(c => c.ApplicationUserId.ToString() == userId);
            var visits = _context.Visits.Include(c => c.Nurse).Include(c => c.Contract).Where(c => c.Contract.PatientID == patient.PatientID).ToList();
            return View(visits);
        }

        public IActionResult Help()
        {
    
            return View();
        }
    }

 }


