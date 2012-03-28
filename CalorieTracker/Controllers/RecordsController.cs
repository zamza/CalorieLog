using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CalorieTracker.Models;
using CalorieTracker.Controllers;
using CalorieTracker.Helper;
using CalorieTracker.ViewModels;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace CalorieTracker.Controllers
{
    public class RecordsController : Controller
    {
        private CalorieTrackerDataContext db = new CalorieTrackerDataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WeekTracker()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            Guid guid = (Guid)user.ProviderUserKey;

            UpdateHandler up2days = new UpdateHandler();
            up2days.UpdatesToReviews(guid);

            DateTime Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
           
            var last7Days = db.up_DayReviews_Last7Days(guid).ToList();


            WeekViewModel viewModel = new WeekViewModel();
            viewModel.CalorieTotal = 0;
            foreach (var day in last7Days)
            {
                viewModel.CalorieTotal += day.CaloriesTotal;
            }

            viewModel.Last7Days = last7Days;
            return View(viewModel);
        }

        public ActionResult HourTracker()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            Guid guid = (Guid)user.ProviderUserKey;

            var hourSums = (from d in db.DayReviews
                            where d.UserId == guid
                            select new
                            {
                                TotalCalories = db.DayReviews.Sum(p => p.CaloriesTotal),
                                NumDays = db.DayReviews.Count(),
                                MorningTotal = db.DayReviews.Sum(p => p.Morning),
                                LunchTotal = db.DayReviews.Sum(p => p.Lunch),
                                AfternoonTotal = db.DayReviews.Sum(p => p.Afternoon),
                                DinnerTotal = db.DayReviews.Sum(p => p.Dinner),
                                EveningTotal = db.DayReviews.Sum(p => p.Evening),
                                LateTotal = db.DayReviews.Sum(p => p.Late)
                            }).FirstOrDefault();

            //FIND A WAY TO USE SPROC ^^

            HourlyViewModel viewModel = new HourlyViewModel();

            try
            {
                viewModel.TimeTotal[0] = Math.Round(hourSums.MorningTotal * 100f / hourSums.TotalCalories);
                viewModel.TimeTotal[1] = Math.Round(hourSums.LunchTotal * 100f / hourSums.TotalCalories);
                viewModel.TimeTotal[2] = Math.Round(hourSums.AfternoonTotal * 100f / hourSums.TotalCalories);
                viewModel.TimeTotal[3] = Math.Round(hourSums.DinnerTotal * 100f / hourSums.TotalCalories);
                viewModel.TimeTotal[4] = Math.Round(hourSums.EveningTotal * 100f / hourSums.TotalCalories);
                viewModel.TimeTotal[5] = Math.Round(hourSums.LateTotal * 100f / hourSums.TotalCalories);

                viewModel.CalorieAverage = hourSums.TotalCalories / hourSums.NumDays;
            }
            catch (NullReferenceException e)
            {
                viewModel.TimeTotal[0] = 0;
                viewModel.TimeTotal[1] = 0;
                viewModel.TimeTotal[2] = 0;
                viewModel.TimeTotal[3] = 0;
                viewModel.TimeTotal[4] = 0;
                viewModel.TimeTotal[5] = 0;

                viewModel.CalorieAverage = 0;
            }

            return View(viewModel);
        }

        public ActionResult CalendarTracker()
        {
            return View();
        }

        /*public JsonResult RetrieveDayHistory()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            Guid guid = (Guid)user.ProviderUserKey;

            List<DayReview> dayReview = (from d in db.DayReviews
                                         where d.UserId == guid
                                         select d).ToList();
            System.Diagnostics.Debug.WriteLine(dayReview);

            return Json(dayReview);
        }*/

    }
}
