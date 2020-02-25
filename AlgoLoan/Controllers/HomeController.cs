using System.Collections.Generic;
using System.Web.Mvc;
using AlgoLoan.Models.ViewModels;
using AutoMapper;
using DAL.Repo;

namespace AlgoLoan.Controllers
{
    public class HomeController : Controller
    {
        private IProviderRepository _providerRepository;
        private IMapper _mapper;

        public HomeController(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormViewModel data)
        {
            if (ModelState.IsValid)
            {
                var result = _providerRepository.GetProviders(data.Amount, data.Duration, data.Type);
                if (result == null)
                {
                    Session["result"] = null;
                    return RedirectToAction("Providers");
                }
                var providerList = _mapper.Map<List<ProviderViewModel>>(result);
                Session["result"] = providerList;
                return RedirectToAction("Providers");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Providers()
        {

            var providerList = (List<ProviderViewModel>)Session["result"];
            return View(providerList);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}