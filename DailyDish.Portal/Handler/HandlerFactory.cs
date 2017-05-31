using DailyDish.Wechat.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DailyDish.Portal.Handler
{
    public class HandlerFactory
    {
        public static IHandler CreateHandler(string requestXml)
        {
            IHandler handler = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(requestXml);
            string msgType = doc.SelectSingleNode("//MsgType").InnerText;
            switch (msgType)
            {
                case "event":
                    handler = new EventHandler(requestXml);
                    break;
                case "text":
                    handler = new TextHandler(requestXml);
                    break;
                default:
                    throw new NotSupportedException("目前不支持该类型：" + msgType + "。");
            }

            return handler;
        }
    }
}
