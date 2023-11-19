using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_App.Data;
using Hospital_App.Models;

namespace Hospital_App.Controllers
{
    public class SuburbsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuburbsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Suburbs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Suburbs.Include(s => s.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Suburbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suburb = await _context.Suburbs
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.SuburbID == id);
            if (suburb == null)
            {
                return NotFound();
            }

            return View(suburb);
        }

        // GET: Suburbs/Create
        public IActionResult Create()
        {
            ViewData["CityID"] = new SelectList(_context.Cities, "CityID", "CityID");
            return View();
        }

        // POST: Suburbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SuburbID,SuburbName,PostalCode,CityID")] Suburb suburb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suburb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityID"] = new SelectList(_context.Cities, "CityID", "CityID", suburb.CityID);
            return View(suburb);
        }

        // GET: Suburbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suburb = await _context.Suburbs.FindAsync(id);
            if (suburb == null)
            {
                return NotFound();
            }
            ViewData["CityID"] = new SelectList(_context.Cities, "CityID", "CityID", suburb.CityID);
            return View(suburb);
        }

        // POST: Suburbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SuburbID,SuburbName,PostalCode,CityID")] Suburb suburb)
        {
            if (id != suburb.SuburbID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suburb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuburbExists(suburb.SuburbID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityID"] = new SelectList(_context.Cities, "CityID", "CityID", suburb.CityID);
            return View(suburb);
        }

        // GET: Suburbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suburb = await _context.Suburbs
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.SuburbID == id);
            if (suburb == null)
            {
                return NotFound();
            }

            return View(suburb);
        }

        // POST: Suburbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suburb = await _context.Suburbs.FindAsync(id);
            _context.Suburbs.Remove(suburb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuburbExists(int id)
        {
            return _context.Suburbs.Any(e => e.SuburbID == id);
        }
    }
}
