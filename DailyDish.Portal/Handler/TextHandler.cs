using DailyDish.Wechat.Messages.ReceiveMessage;
using DailyDish.Wechat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DailyDish.Portal.SQLDll;
using DailyDish.Wechat.Interfaces;

namespace DailyDish.Portal.Handler
{
    public class TextHandler : IHandler
    {
        private string RequestXml { get; set; }

        /// <summary>
        /// 以用户请求的xml初始化该处理
        /// </summary>
        /// <param name="requestXml"></param>
        public TextHandler(string requestXml)
        {
            this.RequestXml = requestXml;
        }

        /// <summary>
        /// 处理请求并返回信息给用户
        /// </summary>
        /// <returns></returns>
        public string HandleRequest()
        {
            string response = string.Empty;

            TextMessage tm = TextMessage.LoadFromXml(this.RequestXml);

            if (tm != null)
            {
                switch (tm.Content.ToLower())
                {
                    case "?":
                    case "？":
                        response = QuestionMarkHandler(tm);
                        break;
                    default:
                        response = DefaultHandler(tm);
                        break;
                }
            }

            return response;
        }

        /// <summary>
        /// 用户输入？或者?输出当前时间
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public string QuestionMarkHandler(TextMessage tm)
        {
            string toOpenid = tm.ToUserName;
            tm.ToUserName = tm.FromUserName;
            tm.FromUserName = toOpenid;
            tm.Content = "现在时间：" + DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            tm.CreateTime = WeChatHelper.WeChatNowTime();

            return tm.GenerateContent();
        }

        /// <summary>
        /// 输出用户输入的信息
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public string DefaultHandler(TextMessage tm)
        {
            DailyDishHelper ddh = new DailyDishHelper();
            ddh.CreateUser(tm.FromUserName, "waiting..");
            if (tm.Content.Contains("1"))
            {
                tm.Content = "请点此链接设置个人口味:" + ConfigurationManager.AppSettings["WechatUrl"] + tm.FromUserName;
            }
            else if (tm.Content.Contains("2"))
            {

                tm.Content = "请点此链接查看你的专属推荐";
            }


            string toOpenid = tm.ToUserName;
            tm.ToUserName = tm.FromUserName;
            tm.FromUserName = toOpenid;
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            return tm.GenerateContent();
        }
    }

}