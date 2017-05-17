using DailyDish.DB;
using DailyDish.DB.Entity;
using DailyDish.Portal.Models;
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
            DailyDishHelper ddh = new DailyDishHelper();

            TasteModel taste = new TasteModel()
            {
                Flavor = ddh.QueryFlavor(),
            };
            return View(taste);
        }

        public ActionResult SubmitTaste(string[] likeTaste, string[] dislikeTaste, string[] taboo)
        {
            DailyDishHelper ddh = new DailyDishHelper();
            ddh.SaveUserTaste(new TasteHistory()
            {
                Id = Guid.NewGuid(),
                OpenId = Guid.NewGuid().ToString(),
                UserName = "",
                LikeFlavor = likeTaste == null ? "": string.Join(",", likeTaste),
                DisLikeFlavor = dislikeTaste == null ? "" : string.Join(",", dislikeTaste),
                Dieteticrestraint = taboo == null ? "" : string.Join(",", taboo)
            });

            return Json("提交成功", JsonRequestBehavior.AllowGet);
        }
    }
}