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
        public ActionResult Index(UserInfo user,string openId)
        {

            DailyDishHelper ddh = new DailyDishHelper();
            ddh.QueryUser(openId);

            TasteModel taste = new TasteModel()
            {
                Flavor = ddh.QueryFlavor(),
            };
            return View(taste);
        }

        public ActionResult SubmitTaste(UserInfo user,string[] likeTaste, string[] dislikeTaste, string[] taboo)
        {
            Guid historyId = Guid.NewGuid();
            DailyDishHelper ddh = new DailyDishHelper();
            ddh.SaveUserTaste(new TasteHistory()
            {
                Id = historyId.ToString(),
                OpenId = user.OpenId,
                UserName = user.UserName,
                LikeFlavor = likeTaste == null ? "": string.Join(",", likeTaste),
                DisLikeFlavor = dislikeTaste == null ? "" : string.Join(",", dislikeTaste),
                Dieteticrestraint = taboo == null ? "" : string.Join(",", taboo)
            });

            return Json("提交成功", JsonRequestBehavior.AllowGet);
        }
    }
}