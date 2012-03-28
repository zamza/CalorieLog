using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalorieTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace CalorieTracker.ViewModels
{
    public class HourlyViewModel
    {
        public string[] TimeOfDay = new String[6] { "Morning", "Lunch", "Afternoon", "Dinner", "Evening", "Late" };
        public double[] TimeTotal = new double[6];
        public float CalorieAverage = new float();
    }

    public class WeekViewModel
    {
        private CalorieTrackerDataContext db = new CalorieTrackerDataContext();

        public int CalorieTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public List<up_DayReviews_Last7DaysResult> Last7Days { get; set; }

        public bool Empty
        {
            get
            {
                return (CalorieTotal == 0);
            }
        }
    }
}