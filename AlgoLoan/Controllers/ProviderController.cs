using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        public ProviderController(IProviderRepository providerRepository,
            ISubscriptionRepository subscriptionRepository,
            IMapper mapper)
        {
            _providerRepository = providerRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        // GET: Provider
        public ActionResult Index()
        {
            return View();
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
                    var providerList = (List<ProviderViewModel>)Session["result"];
                    if (id < providerList.Count)
                    {
                        ProviderViewModel provider = providerList[id];
                        var search = (Search)Session["search"];
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
                                PercentagePaid = ((monthlyPayment * i) / totalPayment) * 100,
                                Rate = rate
                            });
                            i++;
                        } while (i <= duration);

                        ViewBag.Count = 1;
                        ViewBag.Search = search;
                        ViewBag.Details = repaymentDetails;
                        ViewBag.totalPayment = totalPayment;
                        return View(provider);
                    }

                    return RedirectToAction("index", "Home");
                }
                else
                {
                    Session["providerId"] = id;
                    return RedirectToAction("Pay", "Subscription");
                }

            }
            return RedirectToAction("index", "Home");
        }
    }
}