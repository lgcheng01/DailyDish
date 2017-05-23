using DailyDish.Wechat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DailyDish.Wechat.Messages.ReceiveMessage
{
    public class EventMessage : Message
    {
        public string Event { get; set; }

        public string EventKey { get; set; }

        public string Ticket { get; set; }

        public static EventMessage LoadFromXml(string requestXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(requestXml);

            EventMessage eventMessage = new EventMessage
            {
                FromUserName = WeChatHelper.ReadXmlNode(doc, "//FromUserName"),
                ToUserName = WeChatHelper.ReadXmlNode(doc, "//ToUserName"),
                CreateTime = WeChatHelper.ReadXmlNode(doc, "//CreateTime"),
                MsgType = WeChatHelper.ReadXmlNode(doc, "//MsgType"),
                Event = WeChatHelper.ReadXmlNode(doc, "//Event"),
                EventKey = WeChatHelper.ReadXmlNode(doc, "//EventKey"),
                Ticket = WeChatHelper.ReadXmlNode(doc, "//Ticket")
            };

            return eventMessage;
        }
    }
}
