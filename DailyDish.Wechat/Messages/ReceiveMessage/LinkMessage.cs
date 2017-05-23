using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.ReceiveMessage
{
    public class LinkMessage : Message
    {
        private string mTemplate = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[link]]></MsgType><Title><![CDATA[{3}]]></Title><Description><![CDATA[{4}]]></Description><Url><![CDATA[{5}]]></Url><MsgId>{6}</MsgId></xml>";

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

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
            return string.Format(this.Template, this.ToUserName, this.FromUserName, this.CreateTime, this.Title, this.Description, this.Url, this.MsgId);
        }
    }
}
