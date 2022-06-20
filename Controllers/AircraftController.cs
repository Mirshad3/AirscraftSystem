using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AircraftSystem.Context;
using AircraftSystem.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AircraftSystem.Controllers
{
    public class AircraftController : Controller
    {
        private readonly AircraftContext _context;
        private readonly IWebHostEnvironment webHostEnvironment; 
        public AircraftController(AircraftContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

       
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aircrafts.ToListAsync());
        }
        public async Task<PartialViewResult> AircraftDetails(int id)
        {
            return PartialView("_AircraftDetails",  await _context.Aircrafts
                .FirstOrDefaultAsync(m => m.Id == id));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Make,Model,Registration,Location,DateAndTime,AircraftPicture")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(aircraft);
                _context.Add(aircraft);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(aircraft);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Make,Model,Registration,Location,DateAndTime,AircraftPicture")] Aircraft aircraft)
        {
            if (id != aircraft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aircraft.AircraftPicture != null)
                    {
                        var uniqueFileName = aircraft.Make + "_" + aircraft.Model+ ".jpg"; 
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", uniqueFileName);
                        System.IO.File.Delete(filePath);
                        ProcessUploadedFile(aircraft);
                    }
                    _context.Update(aircraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftExists(aircraft.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(aircraft);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aircraft = await _context.Aircrafts.FindAsync(id);
            _context.Aircrafts.Remove(aircraft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private string ProcessUploadedFile(Aircraft model)
        {
            string uniqueFileName = null;

            if (model.AircraftPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                //string extension = Path.GetExtension(model.AircraftPicture.FileName);
                uniqueFileName = model.Make + "_" + model.Model + ".jpg";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.AircraftPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        private bool AircraftExists(int id)
        {
            return _context.Aircrafts.Any(e => e.Id == id);
        }
    }
}
