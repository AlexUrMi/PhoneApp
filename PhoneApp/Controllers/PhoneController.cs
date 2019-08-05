using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhoneApp.Models;
using PhoneApp.ViewModels;

namespace PhoneApp.Controllers
{
    public class PhoneController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger<PhoneController> logger;

        public PhoneController(AppDbContext dbContext, ILogger<PhoneController> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await dbContext.Phones.Include("Company").ToListAsync();
            logger.LogInformation($"PhoneController.Index method called!");
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new PhoneCreateViewModel();
            var list = await dbContext.Companies.ToListAsync();
            vm.Companies = new SelectList(list, "Id", "Name");

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                dbContext.Phones.Add(phone);
                var id = await dbContext.SaveChangesAsync();
                logger.LogInformation($"Was added product with id {id}");
            }
            else
            {
                return View(phone);
            }

            return RedirectToAction("Index");
        }
    }
}