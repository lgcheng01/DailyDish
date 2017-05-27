using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.SendMessage
{
    /// <summary>
    /// 客服发送的文本消息
    /// </summary>
    public class TextSendMessage : SendMessage
    {
        private string _msgType = "text";

        public override string msgtype
        {
            get
            {
                return _msgType;
            }
        }

        public TextField text { get; set; }
    }

    public class TextField
    {
        public string content { get; set; }
    }
}
