using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AlgoLoan.Models;
using AutoMapper;
using DAL.EF;
using DAL.Repo;

namespace AlgoLoan.Controllers
{
    public class HomeController : Controller
    {
        private IProviderRepository _providerRepository;
        private ISearchRepository _searchRepository;
        private IMapper _mapper;

        public HomeController(IProviderRepository providerRepository, 
            ISearchRepository searchRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _searchRepository = searchRepository;
            _mapper = mapper;
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
                var searchModel = new SearchViewModel {amount = data.Amount, duration = data.Duration, type = data.Type};
                Search searchObj = _mapper.Map<Search>(searchModel);
                _searchRepository.Add(searchObj);
                if (_searchRepository.Save())
                {
                    var result = _providerRepository.GetProviders(data.Amount, data.Duration, data.Type);
                    if (result == null)
                    {
                        Session["result"] = null;
                        return RedirectToAction("Providers");
                    }
                    var providerList = _mapper.Map<List<ProviderViewModel>>(result);
                    Session["result"] = providerList;
                    Session["search"] = searchModel;
                    return RedirectToAction("Providers");
                }

                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult Providers()
        {

            var providerList = (List<ProviderViewModel>)Session["result"];
            var search = (SearchViewModel)Session["search"];
            var model = Tuple.Create(search, providerList);
            return View(model);

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