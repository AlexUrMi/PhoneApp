using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PhoneApp.Models;
using PhoneApp.ViewModels;

namespace PhoneApp.Controllers
{
    public class PhoneController : Controller
    {
        private readonly ICompanyAsyncRepository companyAsyncRepository;
        private readonly IPhoneAsyncRepository phoneAsyncRepository;
        private readonly ILogger<PhoneController> logger;

        public PhoneController( 
            ICompanyAsyncRepository companyAsyncRepository,
            IPhoneAsyncRepository phoneAsyncRepository,
            ILogger<PhoneController> logger)
        {
            this.companyAsyncRepository = companyAsyncRepository;
            this.phoneAsyncRepository = phoneAsyncRepository;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = phoneAsyncRepository.GetWithInclude(p => p.Company);

            logger.LogInformation($"PhoneController.Index method called!");
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new PhoneCreateViewModel();
            var list = await companyAsyncRepository.GetAllAsyn();
            vm.Companies = new SelectList(list, "Id", "Name");

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                var p = await phoneAsyncRepository.AddAsync(phone);
                logger.LogInformation($"Was added product with id {p.Id}");
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
                var p =  await phoneAsyncRepository.FindByIdAsync(id.Value);
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
                var p = phoneAsyncRepository.FindById(id.Value);
                if (p != null)
                {
                    var vm = new PhoneCreateViewModel { Id = p.Id, Name = p.Name, Price = p.Price, CompanyId=p.CompanyId };
                    var cs = await companyAsyncRepository.GetAllAsyn();
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

                    await phoneAsyncRepository.UpdateAsyn(p, p.Id);
                    logger.LogInformation($"Was updated product with id {p.Id}");
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
                var ph = await phoneAsyncRepository.FindAsync(p=>p.Id==id.Value);
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
                var ph = await phoneAsyncRepository.FindAsync(pp => pp.Id == id.Value);
                     if (ph != null)
                {
                   
                    await phoneAsyncRepository.DeleteAsyn(ph);
                    logger.LogInformation($"Was deleted product with id {ph.Id}");
                    return RedirectToAction("Index");
                }
               
            }
            return NotFound();
        }

    }
}