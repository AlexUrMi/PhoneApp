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

        [HttpGet]
        public async Task<IActionResult> Details (int? id)
        {
            if (id.HasValue)
            {
                var p = await dbContext.Phones.FindAsync(id.Value);
                if (p != null)
                {
                    return View(p);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                var p = await dbContext.Phones.FindAsync(id.Value);
                if (p != null)
                {
                    var vm = new PhoneCreateViewModel { Id = p.Id, Name = p.Name, Price = p.Price, CompanyId=p.CompanyId };
                    var cs = await dbContext.Companies.ToListAsync();
                    vm.Companies = new SelectList(cs, "Id", "Name");
                    var selected = vm.Companies.Where(x => x.Value == vm.CompanyId.ToString()).First();
                    selected.Selected = true;
                    return View(vm);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PhoneCreateViewModel phone) 
        {
            if(phone != null)
            {
                if (ModelState.IsValid)
                {
                    var p = new Phone { Id = phone.Id, CompanyId = phone.CompanyId, Name = phone.Name, Price = phone.Price };

                    dbContext.Phones.Update(p);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                } else
                {
                    return View(phone);
                }
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if(id != null)
            {
                var ph = await dbContext.Phones.FirstOrDefaultAsync(p => p.Id == id);
                if(ph != null)
                {
                    return View(ph);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var ph = await dbContext.Phones.FirstOrDefaultAsync(p => p.Id == id);
                if (ph != null)
                {
                    dbContext.Phones.Remove(ph);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
               
            }
            return NotFound();
        }

    }
}