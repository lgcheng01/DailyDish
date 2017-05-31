using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.ReceiveMessage
{
    public class VoiceMessage : Message
    {
        private string mTemplate = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[voice]]></MsgType><MediaId><![CDATA[{3}]]></MediaId><Format><![CDATA[{4}]]></Format><MsgId>{5}</MsgId></xml>";

        public string MediaId { get; set; }

        public string Format { get; set; }

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
            return string.Format(this.Template, this.ToUserName, this.FromUserName, this.CreateTime, this.MediaId, this.Format, this.MsgId);
        }
    }
}
