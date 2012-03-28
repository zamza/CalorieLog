using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalorieTracker.Models;

namespace CalorieTracker.ViewModels
{
    public class AccountServicesViewModel
    {
        public RegisterModel registerModel { get; set; }
        public LogOnModel logOnModel { get; set; }
    }
}