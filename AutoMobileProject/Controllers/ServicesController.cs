using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMobileProject.Data;
using AutoMobileProject.Models;
using AutoMobileProject.Models.ViewModels;
using AutoMobileProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
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

        [Authorize]
        public IActionResult Index(int carId)
        {
            var car = _db.Carses.FirstOrDefault(c => c.Id == carId);
            var model = new CarAndServicesViewModel
            {
                CarId = car.Id,
                Make = car.Make,
                Model = car.Model,
                Style = car.Style,
                Vin = car.Vin,
                Year = car.Year,
                UserId = car.UserId,
                ServiceTypeses = _db.ServiceTypeses.ToList(),
                PastServices = _db.Services.Where(s => s.CarId == carId).OrderByDescending(s => s.DateAdded)
            };
            return View(model);
        }

        [Authorize(Roles = StaticDetail.AdminEndUser)]
        public IActionResult Create(int carId)
        {
            var car = _db.Carses.FirstOrDefault(c => c.Id == carId);
            var model = new CarAndServicesViewModel
            {
                CarId = car.Id,
                Make = car.Make,
                Model = car.Model,
                Style = car.Style,
                Vin = car.Vin,
                Year = car.Year,
                UserId = car.UserId,
                ServiceTypeses = _db.ServiceTypeses.ToList(),
                PastServices = _db.Services.Where(s => s.CarId == carId).OrderByDescending(s => s.DateAdded).Take(5)
            };
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StaticDetail.AdminEndUser)]
        public async Task<IActionResult> Create(CarAndServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NewService.CarId = model.CarId;
                model.NewService.DateAdded = DateTime.Now;
                _db.Add(model.NewService);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new { carId = model.CarId });
            }

            var car = _db.Carses.FirstOrDefault(c => c.Id == model.CarId);
            var newmodel = new CarAndServicesViewModel
            {
                CarId = car.Id,
                Make = car.Make,
                Model = car.Model,
                Style = car.Style,
                Vin = car.Vin,
                Year = car.Year,
                UserId = car.UserId,
                ServiceTypeses = _db.ServiceTypeses.ToList(),
                PastServices = _db.Services.Where(s => s.CarId == model.CarId).OrderByDescending(s => s.DateAdded).Take(5)
            };
            return View(newmodel);
        }

        [Authorize(Roles = StaticDetail.AdminEndUser)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _db.Services
                .Include(s => s.Cars)
                .Include(s => s.ServiceTypes)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = StaticDetail.AdminEndUser)]
        public async Task<IActionResult> DeleteConfirmed(Service model)
        {
            var serviceId = model.Id;
            var carId = model.CarId;

            var service = await _db.Services.SingleOrDefaultAsync(m => m.Id == serviceId);
            if (service == null)
            {
                return NotFound();
            }

            _db.Services.Remove(service);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Create), new { carId = model.CarId });
        }
    }
}