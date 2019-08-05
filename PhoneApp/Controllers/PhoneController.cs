using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneApp.Models;

namespace PhoneApp.Controllers
{
    public class PhoneController : Controller
    {
        private readonly AppDbContext dbContext;

        public PhoneController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var list = dbContext.Phones.ToList();
            return View(list);
        }
    }
}