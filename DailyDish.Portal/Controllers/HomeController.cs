using DailyDish.DB;
using DailyDish.DB.Entity;
using DailyDish.Portal.Models;
using DailyDish.Portal.SQLDll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DailyDish.Portal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string openId)
        {
            UserInfo user = new UserInfo();

            DailyDishHelper ddh = new DailyDishHelper();
            ddh.GetFactorScore("abcd");

            user = ddh.QueryUser(openId);

            Session["wechat"] = user;

            TasteModel taste = new TasteModel()
            {
                Flavor = ddh.QueryFlavor(),
            };
            return View(taste);
        }

        public ActionResult ShowThanksPage()
        {
            return View("ShowThanksPage");
        }

        public ActionResult ShowFoodInfo()
        {
            return View("ShowFoodInfo");
        }

        public ActionResult SubmitTaste(string[] likeTaste, string[] dislikeTaste, string[] taboo, string otherTaboo)
        {
            DailyDishHelper ddh = new DailyDishHelper();
            UserInfo user = (UserInfo)Session["wechat"];
            if (!string.IsNullOrEmpty(otherTaboo))
            {
                string regex = @"[,|，|\s]+";
                string others = otherTaboo.Replace(Regex.Match(otherTaboo, regex).Value, ",");
                string[] newOthers = others.Split(',');
                if (taboo == null)
                {
                    taboo = newOthers;
                }
                else
                {
                    taboo = taboo.Concat(newOthers).ToArray();
                }
                ddh.AddTabooData(newOthers);
            }
            Guid historyId = Guid.NewGuid();
            ddh.SaveUserTaste(new TasteHistory()
            {
                Id = historyId.ToString(),
                OpenId = string.IsNullOrEmpty(user.OpenId) ? string.Empty : user.OpenId,
                UserName = string.IsNullOrEmpty(user.UserName) ? string.Empty : user.UserName,
                LikeFlavor = likeTaste == null ? "" : string.Join(",", likeTaste),
                DisLikeFlavor = dislikeTaste == null ? "" : string.Join(",", dislikeTaste),
                Dieteticrestraint = taboo == null ? "" : string.Join(",", taboo),
                CreateTime = DateTime.Now
            });
            Task.Factory.StartNew(() => { ddh.GetFactorScore(user.OpenId); });
            return Json("提交成功", JsonRequestBehavior.AllowGet);
        }


    }
}