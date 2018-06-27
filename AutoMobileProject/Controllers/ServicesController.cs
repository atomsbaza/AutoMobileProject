using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMobileProject.Data;
using AutoMobileProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace AutoMobileProject.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ServicesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int cadId)
        {
            var model = new CarAndServicesViewModel
            {
                Cars = _db.Carses.FirstOrDefault(c => c.Id == cadId),
                ServiceTypeses = _db.ServiceTypeses.ToList(),
                PastServices = _db.Services.Where(s => s.CarId == cadId).OrderByDescending(s => s.DateAdded).Take(5)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarAndServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NewService.CarId = model.Cars.Id;
                model.NewService.DateAdded = DateTime.Now;
                _db.Add(model.NewService);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { carId = model.Cars.Id });
            }

            var newmodel = new CarAndServicesViewModel
            {
                Cars = _db.Carses.FirstOrDefault(c => c.Id == model.Cars.Id),
                ServiceTypeses = _db.ServiceTypeses.ToList(),
                PastServices = _db.Services.Where(s => s.CarId == model.Cars.Id).OrderByDescending(s => s.DateAdded).Take(5)
            };

            return View(newmodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }
    }
}