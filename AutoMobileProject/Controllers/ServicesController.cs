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

        public IActionResult Create(int carId)
        {
            var car = _db.Carses.FirstOrDefault(c => c.Id == carId);
            if (car != null)
            {
                var model = new CarAndServicesViewModel
                {
                    carId = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Style =  car.Style,
                    Vin = car.Vin,
                    Year = car.Year,
                    ServiceTypeses = _db.ServiceTypeses.ToList(),
                    PastServices = _db.Services.Where(s => s.CarId == carId).OrderByDescending(s => s.DateAdded).Take(5)
                };

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarAndServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NewService.CarId = model.carId;
                model.NewService.DateAdded = DateTime.Now;
                _db.Add(model.NewService);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { carId = model.carId });
            }

            var car = _db.Carses.FirstOrDefault(c => c.Id == model.carId);
            if (car != null)
            {
                var newmodel = new CarAndServicesViewModel
                {
                    carId = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Style = car.Style,
                    Vin = car.Vin,
                    Year = car.Year,
                    ServiceTypeses = _db.ServiceTypeses.ToList(),
                    PastServices = _db.Services.Where(s => s.CarId == model.carId).OrderByDescending(s => s.DateAdded).Take(5)
                };

                return View(newmodel);
            }
        }
    }
}