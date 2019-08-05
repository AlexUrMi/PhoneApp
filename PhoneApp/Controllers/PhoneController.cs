using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneApp.Models;

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

        public IActionResult Index()
        {
            var list = dbContext.Phones.ToList();
            logger.LogInformation($"PhoneController.Index method called!");
            return View(list);
        }
    }
}