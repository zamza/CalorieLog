using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using CalorieTracker.Models;

namespace CalorieTracker.Helper
{

    public class UpdateHandler : Controller
    {
        private CalorieTrackerDataContext db = new CalorieTrackerDataContext();

        public void UpdatesToReviews(Guid guid)
        {
            DateTime todaysDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            var PastDaysUpdates = db.up_Updates_RetrieveOld(guid, todaysDate).ToList();

            if (PastDaysUpdates != null)
            {
                var WhichDays = db.up_Updates_RetrieveListOfDays(guid, todaysDate).ToList();

                foreach (var day in WhichDays)
                {
                    DayReview dayReview = new DayReview();
                    foreach (var update in PastDaysUpdates)
                    {
                        dayReview.Date = day.Date;
                        dayReview.UserId = guid;

                        if (update.Date == day.Date)
                        {
                            dayReview.CaloriesTotal += update.Calories;

                            if ((update.Time > TimesOfDay.MorningStart) && (update.Time < TimesOfDay.MorningEnd))
                            {
                                dayReview.Morning += update.Calories;
                            }
                            else if ((update.Time > TimesOfDay.LunchStart) && (update.Time < TimesOfDay.LunchEnd))
                            {
                                dayReview.Lunch += update.Calories;
                            }
                            else if ((update.Time > TimesOfDay.AfternoonStart) && (update.Time < TimesOfDay.AfternoonEnd))
                            {
                                dayReview.Afternoon += update.Calories;
                            }
                            else if ((update.Time > TimesOfDay.DinnerStart) && (update.Time < TimesOfDay.DinnerEnd))
                            {
                                dayReview.Dinner += update.Calories;
                            }
                            else if ((update.Time > TimesOfDay.EveningStart) && (update.Time < TimesOfDay.EveningEnd))
                            {
                                dayReview.Evening += update.Calories;
                            }
                            else if ((update.Time > TimesOfDay.LateNightStart) && (update.Time < TimesOfDay.LateNightEnd))
                            {
                                dayReview.Late += update.Calories;
                            }
                        }
                    }
                    db.DayReviews.InsertOnSubmit(dayReview);
                }
                db.up_Updates_DeleteOld(guid, todaysDate);
                db.SubmitChanges();
            }

        }
    }
}
