using DAL.EF;
using DAL.Repo;
using Microsoft.AspNet.Identity;
using Paystack.Net.SDK.Transactions;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AlgoLoan.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly IProviderRepository _providerRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionController(IProviderRepository providerRepository,
            ISubscriptionRepository subscriptionRepository)
        {
            _providerRepository = providerRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public ActionResult Pay()
        {
            if (Session["providerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Pay(string type)
        {
            if (!String.IsNullOrWhiteSpace(type))
            {
                string secretKey = ConfigurationManager.AppSettings["PayStackSec"];
                var paystackTransactionAPI = new PaystackTransaction(secretKey);
                string email = User.Identity.GetUserName();
                
                int amount = type == "kilo" ? 100000 :
                    type == "mega" ? 360000 : 960000;
                
                var response = await paystackTransactionAPI.InitializeTransaction(email, amount,
                    callbackUrl: "https://localhost:44389/Subscription/VerifyPayment");
                
                if (response.status)
                {
                    Session["type"] = type;
                    Response.AddHeader("Access-Control-Allow-Origin", "*");
                    Response.AppendHeader("Access-Control-Allow-Origin", "*");
                    return Redirect(response.data.authorization_url);
                }

                return RedirectToAction("Failed");
            }
            return View();
        }

        public async Task<ActionResult> VerifyPayment(string reference)
        {
            if (!String.IsNullOrWhiteSpace(reference))
            {
                string secretKey = ConfigurationManager.AppSettings["PayStackSec"];
                var paystackTransactionAPI = new PaystackTransaction(secretKey);
                var response = await paystackTransactionAPI.VerifyTransaction(reference);
                if (response.status && response.data.status == "success")
                {
                    string userId = User.Identity.GetUserId();
                    User loggedInUser = _providerRepository.GetUserById(userId);
                    var type = (string)Session["type"];

                    int days = type == "kilo" ? 30 :
                        type == "mega" ? 120 : 365;

                    bool[] checks = _subscriptionRepository.CheckUserSubscription(userId);

                    if (checks[0])
                    {
                        var sub = _subscriptionRepository.GetByUserId(userId);
                        sub.startDate = DateTime.Now;
                        sub.endDate = DateTime.Now.AddDays(days);
                        sub.lastSubDate = DateTime.Now;
                        sub.type = "subType";
                    }
                    else
                    {
                        _subscriptionRepository.Add(new Subscription
                        {
                            startDate = DateTime.Now,
                            endDate = DateTime.Now.AddDays(days),
                            lastSubDate = DateTime.Now,
                            type = "subType",
                            User = loggedInUser,
                            userId = userId
                        });
                    }
                    _subscriptionRepository.Save();

                    int providerId = (int)Session["providerId"];
                    return RedirectToAction("Details", "Provider", new { id = providerId });
                }
                return RedirectToAction("Failed");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Failed()
        {
            return View();
        }
    }
}