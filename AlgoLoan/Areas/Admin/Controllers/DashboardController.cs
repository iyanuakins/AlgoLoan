using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlgoLoan.Models;
using AutoMapper;
using DAL.EF;
using DAL.Repo;

namespace AlgoLoan.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IProviderRepository _providerRepository;
        private readonly ISearchRepository _searchRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public DashboardController(IProviderRepository providerRepository, 
            ISearchRepository searchRepository, 
            ISubscriptionRepository subscriptionRepository, 
            IMapper mapper)
        {
            _providerRepository = providerRepository;
            _searchRepository = searchRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var result = _providerRepository.GetAllProviders();
            var allProviders = _mapper.Map<List<ProviderViewModel>>(result);
            var searches = _searchRepository.GetAll();
            int totalSearch = searches.Count;
            decimal averageAmount = (searches.Sum(s => s.amount) / totalSearch);
            decimal averageDuration = (searches.Sum(s => s.duration) / totalSearch);
            return View(allProviders);
        }

        public ActionResult AddProvider()
        {
            ViewBag.Message = "";
            return View();
        }

        public ActionResult AddProvider(ProviderViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.totalClicks = 0;
                model.uniqueClicks = 0;
                model.dateAdded = DateTime.Now;

                var provider = _mapper.Map<Provider>(model);
                _providerRepository.Add(provider);
                if (_providerRepository.Save())
                {
                    ViewBag.Message = "Provider Successfully added";
                    return RedirectToAction("AddProvider");
                }
                ViewBag.Message = "Something went wrong unable to add provider";
                return View();
            }
            return View();
        }

        public ActionResult ViewSearch()
        {
            var searches = _searchRepository.GetAll();
            var allSearches = _mapper.Map<List<SearchViewModel>>(searches);
            return View(allSearches);
        }
        
        public ActionResult ViewSubscriptions()
        {
            var subs = _subscriptionRepository.GetAll();
            var allSubscriptions = _mapper.Map<List<SubscriptionViewModel>>(subs);
            return View(allSubscriptions);
        }
    }
}