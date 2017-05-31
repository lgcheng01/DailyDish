using DailyDish.Wechat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DailyDish.Wechat.Messages.ReceiveMessage
{
    public class TextMessage : Message
    {
        private string mTemplate = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content><MsgId>{4}</MsgId></xml>";

        public string Content { get; set; }

        public string MsgId { get; set; }

        public override string Template
        {
            get
            {
                return mTemplate;
            }
        }

        public override string GenerateContent()
        {
            return string.Format(this.Template, this.ToUserName, this.FromUserName, this.CreateTime, this.Content, this.MsgId);
        }

        public static TextMessage LoadFromXml(string requestXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(requestXml);

            TextMessage textMessage = new TextMessage
            {
                FromUserName = WeChatHelper.ReadXmlNode(doc, "//FromUserName"),
                ToUserName = WeChatHelper.ReadXmlNode(doc, "//ToUserName"),
                CreateTime = WeChatHelper.ReadXmlNode(doc, "//CreateTime"),
                MsgType = WeChatHelper.ReadXmlNode(doc, "//MsgType"),
                Content = WeChatHelper.ReadXmlNode(doc, "//Content"),
                MsgId = WeChatHelper.ReadXmlNode(doc, "//MsgId")
            };

            return textMessage;
        }
    }
}
