using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMobileProject.Data;
using AutoMobileProject.Models;
using AutoMobileProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoMobileProject.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CarController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string userId = null)
        {
            if (userId == null)
            {
                // Only called when a guest user login
                userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var model = new CarAndCustomerViewModel
            {
                Carses = _db.Carses.Where(c => c.UserId == userId),
                User = _db.Users.FirstOrDefault(u => u.Id == userId)
            };
            return View(model);
        }

        public IActionResult Create(string userId)
        {
            var carObj = new Cars
            {
                Year = DateTime.Now.Year,
                UserId = userId
            };

            return View(carObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cars cars)
        {
            if (ModelState.IsValid)
            {
                _db.Add(cars);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {userId = cars.UserId});
            }

            return View(cars);
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