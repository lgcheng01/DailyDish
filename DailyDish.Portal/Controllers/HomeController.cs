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

        public ActionResult SubmitTaste(string[] likeTaste, string[] dislikeTaste, string[] taboo, string[] otherTaboo)
        {
            DailyDishHelper ddh = new DailyDishHelper();
            UserInfo user = (UserInfo)Session["wechat"];
            if (otherTaboo.Length > 0)
            {
                ddh.AddTabooData(otherTaboo);
            }
            Guid historyId = Guid.NewGuid();
            ddh.SaveUserTaste(new TasteHistory()
            {
                Id = historyId.ToString(),
                OpenId = user == null ? string.Empty : user.OpenId,
                UserName = user == null ? string.Empty : user.UserName,
                LikeFlavor = likeTaste == null ? "" : string.Join(",", likeTaste),
                DisLikeFlavor = dislikeTaste == null ? "" : string.Join(",", dislikeTaste),
                Dieteticrestraint = taboo == null ? "" : string.Join(",", taboo),
                CreateTime = DateTime.Now
            });
            ddh.GetFactorScore(user.OpenId);
            return Json("提交成功", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRecommendDish(string openId)
        {
            DailyDishHelper ddh = new DailyDishHelper();
            UserInfo user = (UserInfo)Session["wechat"];
            DishesModel model = ddh.GetDishByUser(openId);
            return View("ShowFoodInfo", model);
        }
    }
}