using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMobileProject.Data;
using AutoMobileProject.Models;
using AutoMobileProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileProject.Controllers
{
    [Authorize(Roles = StaticDetail.AdminEndUser)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string option = null, string search = null)
        {
            var userList = _db.Users.ToList();

            switch (option)
            {
                case "email" when search != null:
                    userList = _db.Users
                        .Where(r => r.Email.ToLower().Contains(search.ToLower()))
                        .ToList();
                    break;
                case "phone" when search != null:
                    userList = _db.Users
                        .Where(r => r.PhoneNumber.ToLower().Contains(search.ToLower()))
                        .ToList();
                    break;
                case "name" when search != null:
                    userList = _db.Users
                        .Where(r => r.FirstName.ToLower().Contains(search.ToLower()) || r.LastName.ToLower().Contains(search.ToLower()))
                        .ToList();
                    break;
            }

            return View(userList);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetail = await _db.Users.SingleOrDefaultAsync(m => m.Id == id);

            if (userDetail == null)
            {
                NotFound();
            }

            return View(userDetail);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findUserDetail = await _db.Users.SingleOrDefaultAsync(m => m.Id == id);

            if (findUserDetail == null)
            {
                NotFound();
            }

            return View(findUserDetail);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {

            if (ModelState.IsValid)
            {
                var userInDb = await _db.Users.SingleOrDefaultAsync(m => m.Id == user.Id);

                if (user == null)
                {
                    return NotFound();
                }

                userInDb.FirstName = user.FirstName;
                userInDb.LastName = user.LastName;
                userInDb.Email = user.Email;
                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.Address = user.Address;
                userInDb.City = user.City;
                userInDb.PostalCode = user.PostalCode;
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findUserDelete = await _db.Users.SingleOrDefaultAsync(m => m.Id == id);

            if (findUserDelete == null)
            {
                NotFound();
            }

            return View(findUserDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveServiceType(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findUserDelete = await _db.Users.SingleOrDefaultAsync(m => m.Id == id);

            _db.Remove(findUserDelete);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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