using Hospital_App.CollectionViewModel;
using Hospital_App.Data;
using Hospital_App.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel;

namespace Hospital_App.Controllers
{
    public class NurseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public NurseController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostEnvironment;
        }

        public IActionResult Index(string chronicName = null)
        {
            var chronics = _context.Chronics.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);

            var preferredSuburbs = _context.PreferedSuburbs
                .Include(ps => ps.Suburb)
                .Where(ps => ps.NurseID == nurse.NurseID)
                .Select(ps => ps.SuburbID)
                .ToList();

            var contracts = _context.Contracts
                .Include(c => c.Nurse)
                .Include(c => c.Patient)
                .ThenInclude(p => p.PatientChronics)
                .ThenInclude(pc => pc.Chronic)
                .Include(c => c.Suburb)
                .Where(c => c.Status == "New" && preferredSuburbs.Contains(c.SuburbID))
                .ToList();

            if (!string.IsNullOrEmpty(chronicName))
            {
                contracts = contracts.Where(c => c.Patient.PatientChronics.Any(pc => pc.Chronic.ChronicName == chronicName)).ToList();
            }
            ViewBag.Chronics = new SelectList(chronics, "ChronicName", "ChronicName");


            return View(contracts);
        }
        public IActionResult UpdateProfile()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);

            return View(nurse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(Nurse model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);
            nurse.FirstName = model.FirstName;
            nurse.Surname = model.Surname; 
            nurse.IDNumber = model.IDNumber;
            nurse.Gender = model.Gender;
            nurse.ContactNumber = model.ContactNumber;
            ViewBag.message = "Profile successfully updated";

            _context.SaveChanges();
            return View();
        }

        public IActionResult AddPreferredSuburb()
        {
            var collection = new PreferredSuburbCollection
            {
                PreferedSuburb = new PreferedSuburb(),
                Suburb = _context.Suburbs.OrderBy(suburb => suburb.SuburbName).ToList()
            };
            return View(collection);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPreferredSuburb(PreferredSuburbCollection model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);

            foreach (var selectedSuburbId in model.PreferedSuburb.SelectedSuburbs)
            {
                var preferedSuburb = new PreferedSuburb();
                preferedSuburb.SuburbID = selectedSuburbId;
                preferedSuburb.NurseID = nurse.NurseID;

                _context.PreferedSuburbs.Add(preferedSuburb);
            }
            _context.SaveChanges();
            ViewBag.Messege = "Succesfully added chronics";
            return RedirectToAction("ViewPreferredSuburb");
        }

        public IActionResult ViewPreferredSuburb()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);
            var preferedSuburbs = _context.PreferedSuburbs.Include(c => c.Nurse).Include(c => c.Suburb).Where(c => c.NurseID == nurse.NurseID).ToList();
            return View(preferedSuburbs);
        }
        public async Task<IActionResult> DeletePreferedSuburb(int id)
        {
          
            var suburb = await _context.PreferedSuburbs
                .FirstOrDefaultAsync(m => m.PreferedSuburbID == id);

            return View(suburb);
        }

        [HttpPost, ActionName("DeletePreferedSuburb")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suburb = await _context.PreferedSuburbs.FindAsync(id);
            _context.PreferedSuburbs.Remove(suburb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewPreferredSuburb));
        }

        public IActionResult AddVisit(int id)
        {
            var collection = new VisitCollection
            {

                Visit = new Visit { ContractID = id },
                Contracts = _context.Contracts.ToList()
            };
            return View(collection);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddVisit(VisitCollection model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var collection = new VisitCollection
            {
                Visit = model.Visit,
                Contracts = _context.Contracts.ToList()
            };
            if (model.Visit.VistDate >= DateTime.Now.Date)
            {
                var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);

                var visit = new Visit();
                visit.ContractID = model.Visit.ContractID; 
                visit.NurseID = nurse.NurseID;
                visit.VistDate = model.Visit.VistDate;
                visit.ApproxArriTime = model.Visit.ApproxArriTime;
                visit.ArriveTime = model.Visit.ArriveTime;
                visit.DepartTime = model.Visit.DepartTime;
                visit.WoundCondition = model.Visit.WoundCondition;
                visit.Note = model.Visit.Note;

                _context.Visits.Add(visit);
                _context.SaveChanges();

            }
           
                ViewBag.Message = "Please Enter the Date greater than today or equal"; 


            return RedirectToAction("ViewVisit");
        }

        public IActionResult EditVisit(int id)
        {
            var collection = new VisitCollection
            {

                Visit = _context.Visits.Single(c => c.VisitID == id),
                Contracts = _context.Contracts.ToList()

            };
            return View(collection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditVisit(int id, VisitCollection model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var collection = new VisitCollection
            {
                Visit = model.Visit,
                Contracts = _context.Contracts.ToList()

            };
            var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);
            var visit = _context.Visits.Single(c => c.VisitID == id);
            var contract = _context.Contracts.FirstOrDefault(c => c.ContractID == model.Visit.ContractID);

            visit.ContractID = contract.ContractID;
            visit.NurseID = nurse.NurseID;
            visit.VistDate = model.Visit.VistDate;
            visit.ApproxArriTime = model.Visit.ApproxArriTime;
            visit.ArriveTime = model.Visit.ArriveTime;
            visit.DepartTime = model.Visit.DepartTime;
            visit.WoundCondition = model.Visit.WoundCondition;
            visit.Note = model.Visit.Note;

            _context.SaveChanges();
            return RedirectToAction("ViewVisit");


        }

        public IActionResult ViewVisit(DateTime? startDate, DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);
            var visitQuery = _context.Visits.Include(c => c.Nurse).Include(c => c.Contract).ThenInclude(c => c.Patient).Where(c => c.Contract.NurseID == nurse.NurseID);

            if (startDate != null && endDate != null)
            {
                visitQuery = visitQuery.Where(v => v.VistDate >= startDate && v.VistDate <= endDate);
            }
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            var visit = visitQuery.ToList();
            return View(visit);
        }
        [HttpPost]
        public IActionResult GeneratePdf(DateTime? startDate, DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);
            var visitQuery = _context.Visits.Include(c => c.Nurse).Include(c => c.Contract).ThenInclude(c => c.Patient).Where(c => c.Contract.NurseID == nurse.NurseID);

            if (startDate != null && endDate != null)
            {
                visitQuery = visitQuery.Where(v => v.VistDate >= startDate && v.VistDate <= endDate);
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

            PdfPTable table = new PdfPTable(7);

            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
            table.AddCell(new PdfPCell(new Phrase("Visit Date", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Approximate Arrival Time", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Arrival Time", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Departure Time", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Wound Condition", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Note", cellFont)));
            table.AddCell(new PdfPCell(new Phrase("Patient", cellFont)));

            foreach (var visit in visits)
            {
                table.AddCell(visit.VistDate.ToString("dd/MM/yyyy"));
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
            cell.Colspan = 7; 
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


        public IActionResult AddSchedule(int id)
        {
            var collection = new ScheduleCollection
            {

                Schedule = new Schedule { ContractID = id },
                Contracts = _context.Contracts.ToList()
            };
            return View(collection);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSchedule(ScheduleCollection model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var collection = new ScheduleCollection
            {
                Schedule = model.Schedule,
                Contracts = _context.Contracts.ToList()
            };
            if (model.Schedule.ScheduleDate >= DateTime.Now.Date)
            {
                var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);

                var schedule = new Schedule();
                schedule.ContractID = model.Schedule.ContractID;
                schedule.NurseID = nurse.NurseID;
                schedule.ScheduleDate = model.Schedule.ScheduleDate;
                schedule.ApproxArriTime = model.Schedule.ApproxArriTime;
                schedule.ArriveTime = model.Schedule.ArriveTime;
                schedule.DepartTime = model.Schedule.DepartTime;
                schedule.WoundCondition = model.Schedule.WoundCondition;
                schedule.Note = model.Schedule.Note;

                _context.Schedules.Add(schedule);
                _context.SaveChanges();

            }

            ViewBag.Message = "Please Enter the Date greater than today or equal";


            return RedirectToAction("ViewSchedule");
        }
        public IActionResult ViewSchedule()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);
            var schedules = _context.Schedules.Include(c => c.Nurse).Include(c => c.Contract).ThenInclude(c => c.Patient).Where(c => c.Contract.NurseID == nurse.NurseID).ToList();
            return View(schedules);
        }
        public IActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Email(Email em)
        {
            string to = em.To;
            string subject = em.Subject;
            string body = em.Body;

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress("s221292837@mandela.ac.za");
            mail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient("smtp.office365.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("s221292837@mandela.ac.za", "Mandela22@");
            smtp.Send(mail);
            ViewBag.message = "Email has been sent to " + em.To;

            return View();
        }
        public IActionResult AssignContract(int id)
        {
            var collection = new ContractCollection
            {
                Contract = _context.Contracts.Single(c => c.ContractID == id),
                Patients = _context.Patients.ToList(),
            };
            return View(collection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignContract(int id, ContractCollection model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var collection = new ContractCollection
            {
                Contract = model.Contract,
                Patients = _context.Patients.ToList(),
            };
            var nurse = _context.Nurses.Single(c => c.ApplicationUserId.ToString() == userId);
            var contract = _context.Contracts.Single(c => c.ContractID == id);
            contract.NurseID = nurse.NurseID;
            if (model.Contract.Status == "Assigned")
            {
                contract.StartDate = DateTime.Now.Date;
            }
            contract.Status = model.Contract.Status;
            if (model.Contract.Status == "Closed")
            {
                contract.EndDate = DateTime.Now.Date; 
            }
            else
            {
                contract.EndDate = model.Contract.EndDate; 
            }

            _context.SaveChanges();

            if (model.Contract.Status == "Assigned")
            {
                return RedirectToAction("ViewAssignedContract"); 
            }
            else if (model.Contract.Status == "Closed")
            {
                return RedirectToAction("ViewClosedContract");
            }
            return View(collection);


        }
        public IActionResult ViewAllContract(string status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);
            var contract = _context.Contracts.Include(c => c.Nurse).Include(c => c.Patient).Include(c => c.Suburb).Where(c => c.NurseID == nurse.NurseID).Where(c => c.Status == status || status == null).ToList();
            ViewBag.message = "CONTACT NOT FOUND!!!!!!";

            return View(contract);
        }
        public IActionResult ViewNewContract(string chronicName = null)
        {
            var chronics = _context.Chronics.ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);

            var preferredSuburbs = _context.PreferedSuburbs
                .Include(ps => ps.Suburb)
                .Where(ps => ps.NurseID == nurse.NurseID)
                .Select(ps => ps.SuburbID)
                .ToList();

            var contracts = _context.Contracts
                .Include(c => c.Nurse)
                .Include(c => c.Patient)
                .ThenInclude(p => p.PatientChronics)
                .ThenInclude(pc => pc.Chronic)
                .Include(c => c.Suburb)
                .Where(c => c.Status == "New" && preferredSuburbs.Contains(c.SuburbID))
                .ToList();

            if (!string.IsNullOrEmpty(chronicName))
            {
                contracts = contracts.Where(c => c.Patient.PatientChronics.Any(pc => pc.Chronic.ChronicName == chronicName)).ToList();
            }
            ViewBag.Chronics = new SelectList(chronics, "ChronicName", "ChronicName");


            return View(contracts);
        }

        public IActionResult ViewAssignedContract(string chronicName = null)
        {
            var chronics = _context.Chronics.OrderBy(chronic => chronic.ChronicName).ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);
            var contract = _context.Contracts
                .Include(c => c.Nurse)
                .Include(c => c.Patient)
                .ThenInclude(p => p.PatientChronics)
                .ThenInclude(pc => pc.Chronic)
                .Include(c => c.Suburb)
                .Where(c => c.NurseID == nurse.NurseID)
                .Where(c => c.Status == "Assigned")
                .ToList();

            if (!string.IsNullOrEmpty(chronicName))
            {
                contract = contract.Where(c => c.Patient.PatientChronics.Any(pc => pc.Chronic.ChronicName == chronicName)).ToList();
            }

            ViewBag.Chronics = new SelectList(chronics, "ChronicName", "ChronicName");

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

                Paragraph title = new Paragraph("Assigned Contract Report");
                title.Alignment = Element.ALIGN_LEFT;
                document.Add(title);

                PdfPTable table = new PdfPTable(8);

                var boldTableFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);

                table.AddCell(new PdfPCell(new Phrase("Contract Date", boldTableFont)));
                table.AddCell(new PdfPCell(new Phrase("Patient Name", boldTableFont)));
                table.AddCell(new PdfPCell(new Phrase("Address Line 1", boldTableFont)));
                table.AddCell(new PdfPCell(new Phrase("Address Line 2", boldTableFont)));
                table.AddCell(new PdfPCell(new Phrase("Suburb", boldTableFont)));
                table.AddCell(new PdfPCell(new Phrase("Start Date", boldTableFont)));
                table.AddCell(new PdfPCell(new Phrase("End Date", boldTableFont)));
                table.AddCell(new PdfPCell(new Phrase("Chronic Name", boldTableFont)));

                foreach (var item in contract)
                {
                    table.AddCell(item.ContractDate.ToString("dd/MM/yyyy"));
                    table.AddCell(item.Patient.FirstName);
                    table.AddCell(item.AddressLine1);
                    table.AddCell(item.AddressLine2);
                    table.AddCell(item.Suburb.SuburbName);
                    table.AddCell(item.StartDate?.ToString("dd/MM/yyyy"));
                    table.AddCell(item.EndDate?.ToString("dd/MM/yyyy"));

                    if (item.Patient.PatientChronics != null && item.Patient.PatientChronics.Any())
                    {
                        var chronicNames = string.Join(", ", item.Patient.PatientChronics.Select(pc => pc.Chronic.ChronicName));
                        table.AddCell(chronicNames);
                    }
                    else
                    {
                        table.AddCell("No chronic conditions found.");
                    }
                }
                string subtotalLabel = "Subtotal:";
                string subtotalValue = contract.Count.ToString();
                PdfPCell cell = new PdfPCell();
                cell.Colspan = 8;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                Phrase phrase = new Phrase();
                phrase.Add(new Chunk(subtotalLabel, boldTableFont));
                phrase.Add(new Chunk(subtotalValue, boldTableFont));
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

            return View(contract);
        }
        public IActionResult ViewClosedContract()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = _context.Nurses.SingleOrDefault(c => c.ApplicationUserId.ToString() == userId);
            var contract = _context.Contracts.Include(c => c.Nurse).Include(c => c.Patient).Include(c => c.Suburb).Where(c => c.NurseID == nurse.NurseID).Where(c => c.Status == "Closed").ToList();
            return View(contract);
        }

        public IActionResult ViewPatientCondition(string chronicName = null)
        {
 
            var chronics = _context.Chronics.ToList();

            var patientChronics = _context.PatientChronics
                .Include(c => c.Patient)
                .Include(c => c.Chronic)
                .Where(c => string.IsNullOrEmpty(chronicName) || c.Chronic.ChronicName == chronicName)
                .ToList();

            ViewBag.Chronics = new SelectList(chronics, "ChronicName", "ChronicName");

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
                table.AddCell("Chronic Name");

                foreach (var item in patientChronics)
                {
                    table.AddCell(item.Patient.FirstName);
                    table.AddCell(item.Patient.EmailAddress);
                    table.AddCell(item.Patient.ContactNumber);
                    table.AddCell(item.Chronic.ChronicName);
                }

                document.Add(table);
                document.Close();

                workStream.Position = 0;
                return File(workStream, "application/pdf", "NursePreferredSuburbs.pdf");
            }

            return View(patientChronics);
        }

    }



}
