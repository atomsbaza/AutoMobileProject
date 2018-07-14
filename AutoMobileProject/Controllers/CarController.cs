using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMobileProject.Data;
using AutoMobileProject.Models;
using AutoMobileProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileProject.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _db.Carses
                .Include(c => c.ApplicationUser)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (car == null)
            {
                NotFound();
            }

            return View(car);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _db.Carses
                .Include(c => c.ApplicationUser)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (car == null)
            {
                NotFound();
            }

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cars cars)
        {
            if (id != cars.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(cars);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {userId = cars.UserId});
            }

            return View(cars);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _db.Carses
                .Include(c => c.ApplicationUser)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (car == null)
            {
                NotFound();
            }

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Cars cars)
        {
            var car = await _db.Carses.SingleOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                NotFound();
            }

            _db.Carses.Remove(car);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {userId = car.UserId});
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