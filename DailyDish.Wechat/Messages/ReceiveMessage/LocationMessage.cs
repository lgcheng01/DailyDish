using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.ReceiveMessage
{
    public class LocationMessage : Message
    {
        private string mTemplate = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[location]]></MsgType><Location_X>{3}</Location_X><Location_Y>{4}</Location_Y><Scale>{5}</Scale><Label><![CDATA[{6}]]></Label><MsgId>{7}</MsgId></xml>";

        public string Location_X { get; set; }

        public string Location_Y { get; set; }

        public string Scale { get; set; }

        public string Label { get; set; }

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
            return string.Format(this.Template, this.ToUserName, this.FromUserName, this.CreateTime, this.Location_X, this.Location_Y, this.Scale, this.Label, this.MsgId);
        }
    }
}
