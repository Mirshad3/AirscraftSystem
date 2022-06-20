using AircraftSystem.Context;
using AircraftSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AircraftSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AircraftContext _context;
        public HomeController(ILogger<HomeController> logger, AircraftContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Aircrafts.ToListAsync()); 
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string Search)
        {
           var entities = await _context.Aircrafts.Where(m=> m.Make.Contains(Search)  || m.Model.Contains(Search) || m.Registration.Contains(Search)).ToListAsync();
            return View(entities);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
