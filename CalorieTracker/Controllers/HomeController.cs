using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalorieTracker.Models;
using System.Web.Security;
using CalorieTracker.ViewModels;

namespace CalorieTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(AccountServicesViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Update");
            }
            else
            {
                model.logOnModel = new LogOnModel();
                model.registerModel = new RegisterModel();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                    return RedirectToAction("Index", "Update");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            AccountServicesViewModel newModel = new AccountServicesViewModel();
            newModel.logOnModel = model;
            newModel.registerModel = new RegisterModel();
            newModel.registerModel.UserName = null;
            ModelState.Remove("UserName");
            // If we got this far, something failed, redisplay form
            return View(newModel);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
