using Hospital_App.CollectionViewModel;
using Hospital_App.Data;
using Hospital_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var business = _context.Businesses.Include(c =>c.Suburb).FirstOrDefault();
            return View(business);
        }
        public JsonResult GetSuburbsByCity(int cityId)
        {

            var suburbs = _context.Suburbs.Where(s => s.CityID == cityId).Select(s => new { s.SuburbID, s.SuburbName }).ToList();
            return Json(suburbs);
        }
        public IActionResult Privacy()
        {
            ViewBag.Cities = _context.Cities.ToList();
            return View();
        }

    


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
