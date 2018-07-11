using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class ServiceTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ServiceTypesController(ApplicationDbContext db)
        {
            _db = db;
        }


        //GET : ServiceTypeses
        public IActionResult Index()
        {
            var serviceTypeLists = _db.ServiceTypeses.ToList();
            return View(serviceTypeLists);
        }

        //GET: ServiceTypeses/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST : Services/Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ServiceTypes serviceType)
        {
            if (ModelState.IsValid)
            {
                serviceType.CreateBy = "Admin";
                serviceType.CreateTime = DateTime.Now;

                _db.Add(serviceType);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _db.ServiceTypeses.SingleOrDefaultAsync(m => m.Id == id);

            if (serviceType == null)
            {
                NotFound();
            }

            return View(serviceType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findServiceType = await _db.ServiceTypeses.SingleOrDefaultAsync(m => m.Id == id);

            if (findServiceType == null)
            {
                NotFound();
            }

            return View(findServiceType);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceTypes serviceType)
        {
            if (id != serviceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var serviceItem = await _db.ServiceTypeses.SingleOrDefaultAsync(r => r.Id == serviceType.Id);

                serviceItem.ServiceName = serviceType.ServiceName;
                serviceItem.LastUpdateBy = "Admin";
                serviceItem.LastUpdateTime = DateTime.Now;
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            return View(serviceType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findServiceType = await _db.ServiceTypeses.SingleOrDefaultAsync(m => m.Id == id);

            if (findServiceType == null)
            {
                NotFound();
            }

            return View(findServiceType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveServiceType(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findServiceType = await _db.ServiceTypeses.SingleOrDefaultAsync(m => m.Id == id);

            _db.Remove(findServiceType);
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