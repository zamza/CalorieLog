using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CalorieTracker.Models;
using System.Configuration;
using CalorieTracker.Helper;
using CalorieTracker.ViewModels;

namespace CalorieTracker.Controllers
{ 
    public class UpdateController : Controller
    {
        private CalorieTrackerDataContext db = new CalorieTrackerDataContext();
        //
        // GET: /Update/

        public ViewResult Index()
        {

            MembershipUser user = Membership.GetUser(User.Identity.Name);
            Guid guid = (Guid)user.ProviderUserKey;

            int? CalTotal = (from o in db.Updates
                             where o.UserId == guid && o.Date == DateTime.Now
                             select (int?)o.Calories).Sum();

            if (CalTotal == null)
            {
                CalTotal = 0;
            }

            ViewBag.DailyTotal = CalTotal;

            var list = new SelectList(new[]
                {
                    new{ID="0", Name="0"},
                    new{ID="1", Name="1"},
                    new{ID="2", Name="2"},
                    new{ID="3", Name="3"},
                    new{ID="4", Name="4"},
                    new{ID="5", Name="5"},
                    new{ID="6", Name="6"},
                    new{ID="7", Name="7"},
                    new{ID="8", Name="8"},
                    new{ID="9", Name="9"},
                    new{ID="10", Name="10"},
                    new{ID="11", Name="11"},
                    new{ID="12", Name="12"},
                },
                "ID", "Name", 1);
            ViewData["list"] = list;

            return View();
        }

        [HttpPost]
        public ActionResult Index(UpdateViewModel updateModel)
        {
            {
                if (Membership.GetUser(User.Identity.Name) != null)
                {
                    Update update = new Update();

                    MembershipUser user = Membership.GetUser(User.Identity.Name);
                    Guid guid = (Guid)user.ProviderUserKey;
                    update.UserId = guid;

                    update.Calories = updateModel.Calories;

                    DateTime adjustedTime = DateTime.Now.AddHours(-updateModel.HoursPrior);

                    update.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    update.Time = DateTime.Parse(adjustedTime.ToString("HH:mm:ss"));

                    if (ModelState.IsValid)
                    {
                        db.Updates.InsertOnSubmit(update);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToRoute("Account", "LogOn");
                }
            }
            
        }

    }
}