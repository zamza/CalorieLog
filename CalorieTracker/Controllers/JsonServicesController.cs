using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using CalorieTracker.Models;

namespace CalorieTracker.Controllers
{
    public class JsonServicesController : Controller
    {
        private CalorieTrackerDataContext db = new CalorieTrackerDataContext();

        public JsonResult RetrieveDayHistory()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            Guid guid = new Guid("9bc7e654-a605-4ed9-8b65-06a108780c95");

            List<DayReview> dayReview = (from d in db.DayReviews
                                         where d.UserId == guid
                                         select d).ToList();
            JsonResult result = Json("HI");

            return result;
        }

        public JsonResult SayHi()
        {
            JsonResult result = Json("HI");

            return result;
        }

    }
}
