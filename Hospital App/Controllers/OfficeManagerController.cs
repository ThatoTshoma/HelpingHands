using Hospital_App.CollectionViewModel;
using Hospital_App.Data;
using Hospital_App.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static Hospital_App.Areas.Identity.Pages.Account.RegisterModel;

namespace Hospital_App.Controllers
{
    public class OfficeManagerController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public OfficeManagerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _hostingEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var contract = _context.Contracts.Include(c => c.Nurse).Include(c => c.Patient).Include(c => c.Suburb).Where(c => c.Status == "New").ToList();
            return View(contract);
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
                    FullName = model.Nurse.FirstName + " " + model.Nurse.Surname,
                    EmailAddress = model.ApplicationUser.Email,
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
        public IActionResult ListOfNurses(string suburbName = null)
        {
            var suburbs = _context.Suburbs.ToList();

            var nurse = _context.Nurses
                .Include(p => p.PreferedSuburbs)
                .ThenInclude(p => p.Suburb)
                .ToList();

            if (!string.IsNullOrEmpty(suburbName))
            {
                nurse = nurse.Where(c => c.PreferedSuburbs.Any(pc => pc.Suburb.SuburbName == suburbName)).ToList();
            }

            ViewBag.Suburbs = new SelectList(suburbs, "SuburbName", "SuburbName");

            if (!string.IsNullOrEmpty(Request.Query["generatePdf"]))
            {
                MemoryStream workStream = new MemoryStream();
                Rectangle rectangle = new Rectangle(PageSize.A4);
                Document document = new Document(rectangle, 20, 20, 20, 20);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, workStream);
                pdfWriter.CloseStream = false;

                string imagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "logo.PNG");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                logo.ScaleToFit(150f, 150f);
                logo.Alignment = Element.ALIGN_CENTER;
                document.Open();
                document.Add(logo);

                Paragraph printedDate = new Paragraph("Printed Date: " + DateTime.Now.ToString("dd/MM/yyyy"));
                printedDate.Alignment = Element.ALIGN_LEFT;
                document.Add(printedDate);

                Paragraph title = new Paragraph("Nurse Prefered Suburbs Report");
                title.Alignment = Element.ALIGN_LEFT;
                document.Add(title);



                PdfPTable table = new PdfPTable(7);
                table.AddCell("First Name");
                table.AddCell("Last Name");
                table.AddCell("ID Number");
                table.AddCell("Contact Number");
                table.AddCell("Email");
                table.AddCell("Gender");
                table.AddCell("Preferred Suburbs");

                foreach (var nurses in nurse)
                {
                    table.AddCell(nurses.FirstName);
                    table.AddCell(nurses.Surname);
                    table.AddCell(nurses.IDNumber.ToString());
                    table.AddCell(nurses.ContactNumber);
                    table.AddCell(nurses.EmailAddress);
                    table.AddCell(nurses.Gender);

                    if (nurses.PreferedSuburbs != null && nurses.PreferedSuburbs.Any())
                    {
                        var preferredSuburbs = string.Join(", ", nurses.PreferedSuburbs.Select(ps => ps.Suburb.SuburbName));
                        table.AddCell(preferredSuburbs);
                    }
                    else
                    {
                        table.AddCell("No Preferred Suburbs found.");
                    }
                }
                string subtotalLabel = "Subtotal:";
                string subtotalValue = nurse.Count.ToString();
                PdfPCell cell = new PdfPCell();
                cell.Colspan = 7;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                Phrase phrase = new Phrase();
                phrase.Add(new Chunk(subtotalLabel));
                phrase.Add(new Chunk(subtotalValue));
                cell.Phrase = phrase;
                table.AddCell(cell);

                document.Add(table);

                PdfContentByte cb = pdfWriter.DirectContent;
                for (int page = 1; page <= pdfWriter.PageNumber; page++)
                {
                    ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, new Phrase("Page " + page + " of " + pdfWriter.PageNumber), rectangle.Width / 2, rectangle.GetBottom(30), 0);
                }

                document.Close();

                workStream.Position = 0;
                return File(workStream, "application/pdf", "AssignedContractReport.pdf");
            }

            return View(nurse); 

        }
   
        public IActionResult UpdateNurse(int id)
        {
            var nurse = _context.Nurses.Single(c => c.NurseID == id);
            return View(nurse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateNurse(int id, Nurse model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var patient = _context.Nurses.Single(c => c.NurseID == id);
            patient.FirstName = model.FirstName;
            patient.Surname = model.Surname;
            patient.IDNumber = model.IDNumber;
            patient.ContactNumber = model.ContactNumber;
            patient.Gender = model.Gender;
            _context.SaveChanges();
            return RedirectToAction("ListOfNurses");
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
                ModelState.AddModelError("ChronicName", "The name of the Chronic Conition already exitst");
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
      
       
        public IActionResult ViewAllContract(string status)
        {
           
            var contracts = _context.Contracts
                .Include(c => c.Nurse)
                .Include(c => c.Patient)
                .Include(c => c.Suburb)
                .Where(c => c.Status == status || status == null)
                .ToList();
            ViewBag.message = "CONTACT NOT FOUND!!!!!!";
            return View(contracts);

        }
        public IActionResult ViewNewContract()
        {
            var contract = _context.Contracts.Include(c => c.Nurse).Include(c => c.Patient).Include(c => c.Suburb).Where(c => c.Status == "New").ToList();
            return View(contract);
        }
        public IActionResult ViewAssignedContract()
        {
            var contract = _context.Contracts.Include(c => c.Nurse).Include(c => c.Patient).Include(c => c.Suburb).Where(c => c.Status == "Assigned").ToList();
            return View(contract);
        }
        public IActionResult AssignContract(int id)
        {
            var contract = _context.Contracts.Single(c => c.ContractID == id);
            var nurses = _context.Nurses.Include(n => n.PreferedSuburbs)
                    .Where(n => n.PreferedSuburbs.Any(ps => ps.SuburbID == contract.SuburbID))
                    .ToList();


            var collection = new ContractCollection
            {
                Contract = contract,
                Patients = _context.Patients.ToList(),
                Nurses = nurses
            };
            return View(collection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignContract(int id, ContractCollection model)
        {
            var contract = _context.Contracts.Single(c => c.ContractID == id);

            var nurses = _context.Nurses.Include(n => n.PreferedSuburbs)
                    .Where(n => n.PreferedSuburbs.Any(ps => ps.SuburbID == contract.SuburbID))
                    .ToList();

            var collection = new ContractCollection
            {
                Contract =contract,
                Patients = _context.Patients.ToList(),
                Nurses = nurses
            };
            contract.NurseID = model.Contract.NurseID;
            contract.StartDate = DateTime.Now.Date;
            contract.Status = model.Contract.Status = "Assigned";
            _context.SaveChanges();

            return RedirectToAction("ViewAssignedContract");

        }
        public IActionResult ViewNursePreferedSuburb(string suburbName = null)
        {
            var suburbs = _context.Suburbs.ToList();

            var preferedSuburbs = _context.PreferedSuburbs
                .Include(c => c.Nurse)
                .Include(c => c.Suburb)
                .Where(c => string.IsNullOrEmpty(suburbName) || c.Suburb.SuburbName == suburbName)
                .ToList();

            ViewBag.Suburbs = new SelectList(suburbs, "SuburbName", "SuburbName");

            if (!string.IsNullOrEmpty(Request.Query["generatePdf"]))
            {
                MemoryStream workStream = new MemoryStream();
                Rectangle rectangle = new Rectangle(PageSize.A4);
                Document document = new Document(rectangle, 20, 20, 20, 20);
                PdfWriter.GetInstance(document, workStream).CloseStream = false;

                document.Open();

                PdfPTable table = new PdfPTable(4);
                table.AddCell("First Name");
                table.AddCell("Email Address");
                table.AddCell("Contact Number");
                table.AddCell("Suburb Name");

                foreach (var item in preferedSuburbs)
                {
                    table.AddCell(item.Nurse.FirstName);
                    table.AddCell(item.Nurse.EmailAddress);
                    table.AddCell(item.Nurse.ContactNumber);
                    table.AddCell(item.Suburb.SuburbName);
                }

                document.Add(table);
                document.Close();

                workStream.Position = 0;
                return File(workStream, "application/pdf", "NursePreferredSuburbs.pdf");
            }

            return View(preferedSuburbs);
        }

        public IActionResult ViewVisit(DateTime? startDate, DateTime? endDate)
        {
    
            var visitQuery = _context.Visits.Include(c => c.Nurse).Include(c => c.Contract).ThenInclude(c => c.Patient).ToList();

            if (startDate != null && endDate != null)
            {
                visitQuery = visitQuery.Where(v => v.VistDate >= startDate && v.VistDate <= endDate).ToList();
            }
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            var visit = visitQuery.ToList();
            return View(visit);
        }
        [HttpPost]
        public IActionResult GeneratePdf(DateTime? startDate, DateTime? endDate)
        {

            var visitQuery = _context.Visits.Include(c => c.Nurse).Include(c => c.Contract).ThenInclude(c => c.Patient).ToList();

            if (startDate != null && endDate != null)
            {
                visitQuery = visitQuery.Where(v => v.VistDate >= startDate && v.VistDate <= endDate).ToList();
            }

            var visits = visitQuery.ToList();

            MemoryStream memoryStream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            PdfPTable headerTable = new PdfPTable(1);
            headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            string imagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "logo.PNG");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
            logo.ScaleToFit(150f, 150f);
            logo.Alignment = Element.ALIGN_CENTER;
            document.Open();
            document.Add(logo);

            PdfPCell titleCell = new PdfPCell(new Phrase("Visit Report", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.BLACK)));
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(titleCell);
            document.Add(new Paragraph(" "));

            var printedDate = DateTime.Now;
            PdfPCell dateCell = new PdfPCell(new Phrase("Printed Date: " + printedDate.ToString("dd/MM/yyyy"), new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK)));
            dateCell.HorizontalAlignment = Element.ALIGN_LEFT;
            dateCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(dateCell);

            var startDateString = startDate.HasValue ? startDate.Value.ToString("dd/MM/yyyy") : "";
            var endDateString = endDate.HasValue ? endDate.Value.ToString("dd/MM/yyyy") : "";
            PdfPCell dateRangeCell = new PdfPCell(new Phrase($"Date Range: {startDateString} - {endDateString}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK)));
            dateRangeCell.HorizontalAlignment = Element.ALIGN_LEFT;
            dateRangeCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(dateRangeCell);
            document.Add(new Paragraph(" "));


            document.Add(headerTable);

            PdfPTable table = new PdfPTable(8);

            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
            table.AddCell(new PdfPCell(new Phrase("Visit Date", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Nurse", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Approximate Arrival Time", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Arrival Time", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Departure Time", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Wound Condition", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Note", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Patient", cellFont)));

            foreach (var visit in visits)
            {
                table.AddCell(visit.VistDate.ToString("dd/MM/yyyy"));
                table.AddCell(visit.Nurse.FullName);
                table.AddCell(visit.ApproxArriTime.ToString());
                table.AddCell(visit.ArriveTime.ToString());
                table.AddCell(visit.DepartTime.ToString());
                table.AddCell(visit.WoundCondition);
                table.AddCell(visit.Note);
                table.AddCell(visit.Contract.Patient.FullName);
            }

            string subtotalLabel = "Subtotal:";
            string subtotalValue = visits.Count.ToString();
            PdfPCell cell = new PdfPCell();
            cell.Colspan = 8;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Phrase phrase = new Phrase();
            phrase.Add(new Chunk(subtotalLabel, cellFont));
            phrase.Add(new Chunk(subtotalValue, cellFont));
            cell.Phrase = phrase;
            table.AddCell(cell);


            document.Add(table);

            document.Close();

            using (var pdfReader = new PdfReader(memoryStream.ToArray()))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    using (var stamper = new PdfStamper(pdfReader, newMemoryStream))
                    {
                        int pages = pdfReader.NumberOfPages;
                        for (int i = 1; i <= pages; i++)
                        {
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, new Phrase("Page " + i + " of " + pages), 559f, 20f, 0);
                        }
                    }
                    return File(newMemoryStream.ToArray(), "application/pdf", "Visits.pdf");
                }
            }
        }


        public IActionResult GeneratePDFReport(string suburbName)
        {
            IQueryable<PreferedSuburb> preferedSuburbsQuery = _context.PreferedSuburbs
                .Include(c => c.Nurse)
                .Include(c => c.Suburb);

            if (!string.IsNullOrEmpty(suburbName))
            {
                preferedSuburbsQuery = preferedSuburbsQuery.Where(c => c.Suburb.SuburbName == suburbName);
            }

            var preferedSuburbs = preferedSuburbsQuery.ToList();

            string tempFilePath = Path.GetTempFileName();

            using (FileStream fs = new FileStream(tempFilePath, FileMode.Create))
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                PdfPTable table = new PdfPTable(4);
                table.AddCell("First Name");
                table.AddCell("Email Address");
                table.AddCell("Contact Number");
                table.AddCell("Suburb Name");

                foreach (var nurse in preferedSuburbs)
                {
                    table.AddCell(nurse.Nurse.FirstName);
                    table.AddCell(nurse.Nurse.EmailAddress);
                    table.AddCell(nurse.Nurse.ContactNumber);
                    table.AddCell(nurse.Suburb.SuburbName);
                }

                doc.Add(table);
                doc.Close();
            }

            byte[] fileContents = System.IO.File.ReadAllBytes(tempFilePath);

            System.IO.File.Delete(tempFilePath);

            return File(fileContents, "application/pdf", "NursePreferredSuburbs.pdf");
        }

    }
}
