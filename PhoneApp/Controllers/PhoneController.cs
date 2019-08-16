using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PhoneApp.Models;
using PhoneApp.ViewModels;
using System.Linq.Dynamic.Core;

namespace PhoneApp.Controllers
{
    public class DatatableParams
    {
        public string sEcho { get; set; }

        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }

        public string sSearch { get; set; }
    }
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


        Microsoft.Extensions.Primitives.StringValues GetFormValue(HttpRequest request, string key)
        {
            Microsoft.Extensions.Primitives.StringValues val = new Microsoft.Extensions.Primitives.StringValues();
            if (request.Form.TryGetValue(key, out val))
            {
                return val;
            }
            return val;
        }

        [HttpPost]
        public ActionResult GetPhoneListPost()
        {
            string test = string.Empty;
            //get form data
            var draw = GetFormValue(Request, "draw").FirstOrDefault();
            var start = GetFormValue(Request, "start").FirstOrDefault();
            var length = GetFormValue(Request, "length").FirstOrDefault();
            var sortColumn = GetFormValue(Request, "columns[" + GetFormValue(Request, "order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = GetFormValue(Request, "order[0][dir]").FirstOrDefault();
            var searchValue = GetFormValue(Request, "search[value]").FirstOrDefault();

            //Paging Size (10,20,50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            DatatableParams dt = new DatatableParams() { sSearch = searchValue, iDisplayLength = pageSize, iDisplayStart = skip, sEcho = "" };


            var phones = phoneAsyncRepository.GetWithInclude(p => p.Company);

            if (!string.IsNullOrEmpty(dt.sSearch))
            {
                dt.sSearch = dt.sSearch.ToLower();
                phones = phones.Where(a => a.Name.ToLower().Contains(dt.sSearch)
                || a.Price.ToString().ToLower().Contains(dt.sSearch)
                || a.Company.Name.ToLower().Contains(dt.sSearch)
                );

            }
            //Sorting  
            if (!string.IsNullOrEmpty(sortColumn))
            {
                phones = phones.OrderBy(sortColumn + " " + sortColumnDir);
            }

            recordsTotal = phones.Count();
            phones = phones
                    .Skip(dt.iDisplayStart)
                    .Take(dt.iDisplayLength);

            var result = (from p in phones
                          select new { Id = p.Id, Name = p.Name, Price = p.Price, Company = p.Company.Name }
                         ).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result });
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id.HasValue)
            {
                var p = await phoneAsyncRepository.FindByIdAsync(id.Value);
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
                    var vm = new PhoneCreateViewModel { Id = p.Id, Name = p.Name, Price = p.Price, CompanyId = p.CompanyId };
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
            if (phone != null)
            {
                if (ModelState.IsValid)
                {
                    var p = new Phone { Id = phone.Id, CompanyId = phone.CompanyId, Name = phone.Name, Price = phone.Price };

                    await phoneAsyncRepository.UpdateAsyn(p, p.Id);
                    logger.LogInformation($"Was updated product with id {p.Id}");
                    return RedirectToAction("Index");
                }
                else
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
            if (id != null)
            {
                var ph = await phoneAsyncRepository.FindAsync(p => p.Id == id.Value);
                if (ph != null)
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