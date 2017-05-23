using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.ReceiveMessage
{
    public class ImageMessage: Message
    {
        private string mTemplate = "<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[image]]></MsgType><PicUrl><![CDATA[{3}]]></PicUrl><MediaId><![CDATA[{4}]]></MediaId><MsgId>{5}</MsgId></xml>";

        public string PicUrl { get; set; }

        public string MediaId { get; set; }

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
            return string.Format(this.Template, this.ToUserName, this.FromUserName, this.CreateTime, this.PicUrl, this.MediaId, this.MsgId);
        }
    }
}
