using DailyDish.Wechat.Interfaces;
using DailyDish.Wechat.Messages.ReceiveMessage;
using DailyDish.Wechat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Portal.Handler
{
    public class EventHandler : IHandler
    {
        /// <summary>
        /// 请求的xml
        /// </summary>
        private string RequestXml { get; set; }

        /// <summary>
        /// 以用户请求的xml初始化该处理
        /// </summary>
        /// <param name="requestXml"></param>
        public EventHandler(string requestXml)
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

            EventMessage em = EventMessage.LoadFromXml(this.RequestXml);

            if (em != null)
            {
                switch (em.Event.ToLower())
                {
                    case "subscribe":
                        if (string.IsNullOrEmpty(em.EventKey))
                        {
                            response = SubscribeEventHandler(em);
                        }
                        else
                        {
                            response = ScanSubscribeEventHandler(em);
                        }
                        break;
                    case "SCAN":
                        response = ScanEventHandler(em);
                        break;
                    case "click":
                        response = ClickEventHandler(em);
                        break;
                    default:
                        break;
                }
            }

            return response;
        }

        /// <summary>
        /// 未关注用户非场景关注事件
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string SubscribeEventHandler(EventMessage em)
        {
       
            TextMessage tm = new TextMessage();
            tm.ToUserName = em.FromUserName;
            tm.FromUserName = em.ToUserName;
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            tm.Content = "";
            return tm.GenerateContent();
        }

        /// <summary>
        /// 未关注用户场景扫描关注事件
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string ScanSubscribeEventHandler(EventMessage em)
        {
            
            TextMessage tm = new TextMessage();
            tm.ToUserName = em.FromUserName;
            tm.FromUserName = em.ToUserName;
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            tm.Content = "场景扫描关注事件触发！";

            // em.EventKey包含场景值，前缀为qrscene_,后面为二维码场景值1~1000

            return tm.GenerateContent();
        }

        /// <summary>
        /// 已关注用户场景扫描事件
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string ScanEventHandler(EventMessage em)
        {
            TextMessage tm = new TextMessage();
            tm.ToUserName = em.FromUserName;
            tm.FromUserName = em.ToUserName;
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            tm.Content = "场景扫描事件触发！";

            // em.EventKey包含场景值，无前缀,直接为二维码场景值1~1000

            return tm.GenerateContent();
        }

        /// <summary>
        /// 处理点击事件
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string ClickEventHandler(EventMessage em)
        {
            string result = string.Empty;
            if (em != null && em.EventKey != null)
            {
                switch (em.EventKey.ToUpper())
                {
                    case "BTN_GOOD":
                        result = btnGoodClick(em);
                        break;

                    case "BTN_HELP":
                        result = btnHelpClick(em);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
        /// <summary>
        /// 赞一下
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string btnGoodClick(EventMessage em)
        {
            //回复欢迎消息
            TextMessage tm = new TextMessage();
            tm.ToUserName = em.FromUserName;
            tm.FromUserName = em.ToUserName;
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            tm.Content = @"谢谢您的支持！";
            return tm.GenerateContent();
        }
        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        private string btnHelpClick(EventMessage em)
        {
            //回复欢迎消息
            TextMessage tm = new TextMessage();
            tm.ToUserName = em.FromUserName;
            tm.FromUserName = em.ToUserName;
            tm.CreateTime = WeChatHelper.WeChatNowTime();
            tm.Content = @"查询天气，输入tq 城市名称\拼音\首字母";
            return tm.GenerateContent();
        }
    }
}
