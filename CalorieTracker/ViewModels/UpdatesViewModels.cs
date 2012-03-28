using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CalorieTracker.ViewModels
{
    public class UpdateViewModel
    {
        [Required(ErrorMessage="Please enter a number")]
        public int Calories { get; set; }

        public int HoursPrior { get; set; }
    }
}