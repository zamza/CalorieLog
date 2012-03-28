using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieTracker.Models
{
    public class TimesOfDay
    {
        public static TimeSpan MorningStart = new TimeSpan(06, 0, 0);
        public static TimeSpan MorningEnd = new TimeSpan(09, 59, 59);

        public static TimeSpan LunchStart = new TimeSpan(10, 0, 0);
        public static TimeSpan LunchEnd = new TimeSpan(13, 59, 59);

        public static TimeSpan AfternoonStart = new TimeSpan(14, 0, 0);
        public static TimeSpan AfternoonEnd = new TimeSpan(17, 59, 59);

        public static TimeSpan DinnerStart = new TimeSpan(18, 0, 0);
        public static TimeSpan DinnerEnd = new TimeSpan(21, 59, 59);

        public static TimeSpan EveningStart = new TimeSpan(22, 0, 0);
        public static TimeSpan EveningEnd = new TimeSpan(01, 59, 59);

        public static TimeSpan LateNightStart = new TimeSpan(02, 0, 0);
        public static TimeSpan LateNightEnd = new TimeSpan(05, 59, 59);
    }
}