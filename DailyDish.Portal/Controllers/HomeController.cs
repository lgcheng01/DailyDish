using DailyDish.DB;
using DailyDish.Portal.SQLDll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyDish.Portal.Controllers
{
    public class HomeController : Controller 
    {
        public ActionResult Index()
        {
           
            return View();
        }

        
    }
}