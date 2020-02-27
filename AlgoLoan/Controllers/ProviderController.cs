using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlgoLoan.Models;
using AutoMapper;
using DAL.EF;
using DAL.Repo;
using Microsoft.AspNet.Identity;

namespace AlgoLoan.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderRepository _providerRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IMapper _mapper;
        public ProviderController(IProviderRepository providerRepository,
            ISubscriptionRepository subscriptionRepository,
            IVisitRepository visitRepository,
            IMapper mapper)
        {
            _providerRepository = providerRepository;
            _subscriptionRepository = subscriptionRepository;
            _visitRepository = visitRepository;
            _mapper = mapper;
        }

        [Authorize]
        public ActionResult Details(int id = -1)
        {
            if (id >= 0)
            {
                var userId = User.Identity.GetUserId();
                bool[] checks = _subscriptionRepository.CheckUserSubscription(userId);
                if (checks.All(c => c))
                {
                    var providerList = (List<ProviderViewModel>) Session["result"];
                    if (id < providerList.Count)
                    {
                        ProviderViewModel provider = providerList[id];
                        var search = (SearchViewModel) Session["search"];
                        int duration = search.duration;
                        decimal amount = search.amount;
                        decimal rate = (provider.maxRate + provider.maxRate / 2) + 100;
                        decimal totalPayment = 0;
                        decimal monthlyPayment = 0;
                        do
                        {
                            decimal amountLeft = amount * (rate / 100);
                            monthlyPayment = amountLeft / duration;
                            totalPayment += monthlyPayment;
                            amount = amountLeft - monthlyPayment;
                            duration--;
                        } while (duration > 0);
                        var repaymentDetails = new List<RepaymentViewModel>();
                        duration = search.duration;
                        monthlyPayment = totalPayment / duration;
                        int i = 1;
                        do
                        {
                            repaymentDetails.Add(new RepaymentViewModel
                            {
                                AmountLeft = totalPayment - (monthlyPayment * i),
                                MonthlyPayment = monthlyPayment,
                                PercentagePaid = decimal.Round((monthlyPayment * i) / totalPayment, 2),
                                Rate = decimal.Round(rate - 100, 2)
                            });
                            i++;
                        } while (i <= duration);

                        ViewBag.Count = 1;
                        ViewBag.Search = search;
                        ViewBag.RepaymentDetails = repaymentDetails;
                        ViewBag.TotalAmount = totalPayment;
                        return View(provider);
                    }

                    return RedirectToAction("index", "Home");
                }

                Session["providerId"] = id;
                return RedirectToAction("Pay", "Subscription");
            }
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Details(string providerId)
        {
            var userId = User.Identity.GetUserId();
            int id = Convert.ToInt32(providerId);
            Provider selectedProvider = _providerRepository.GetById(id);

            if (_visitRepository.IsUniqueVisit(userId, id))
            {
                _visitRepository.Add(new Visit
                {
                    date = DateTime.Now,
                    providerId = id,
                    providerName = selectedProvider.name,
                    userId = userId
                });
                if (_visitRepository.Save())
                {
                    selectedProvider.totalClicks += 1;
                    selectedProvider.uniqueClicks += 1;
                    selectedProvider.lastVisited = DateTime.Now;
                    _providerRepository.Save();
                    return Content($"<script> (() => window.open('{selectedProvider.link}'))() </script>");
                }

                return View();
            }
            selectedProvider.totalClicks += 1;
            selectedProvider.lastVisited = DateTime.Now;
            _providerRepository.Save();
            return Content($"<script> (() => window.open('{selectedProvider.link}'))() </script>");
        }
    }
}