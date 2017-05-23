using DailyDish.Wechat.Handlers;
using DailyDish.Wechat.Interfaces;
using DailyDish.Wechat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyDish.Portal.Controllers
{
    public class WechatController : Controller
    {
        // GET: Wechat
        private static readonly string Token = "DailyDish";

        [HttpGet]
        public ActionResult Index(string signature, string timestamp, string nonce, string echoStr)
        {
            string[] array = { Token, timestamp, nonce };
            Array.Sort(array);

            string tempStr = string.Join("", array);
            tempStr = WeChatHelper.SHA1Encrypt(tempStr).ToLower();
            if (tempStr == signature)
            {
                return Content(echoStr);
            }
            else
            {
                return Content(string.Format("Failed - signature:{0}, tempStr: {1}", signature, tempStr));
            }
        }

        [HttpPost]
        public ActionResult Index()
        {
            string requestXml = WeChatHelper.ReadRequest(this.Request);

            IHandler handler = HandlerFactory.CreateHandler(requestXml);

            return Content(handler.HandleRequest());
        }
    }
}