using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CalorieTracker.Models;

namespace CalorieTracker.DAL
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<CalorieContext>
    {

    }
}