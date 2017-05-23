using DailyDish.Wechat.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyDish.Wechat.Messages.ReceiveMessage
{
    public class Message: ITemplate
    {
        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public string MsgType { get; set; }

        public string CreateTime { get; set; }

        public virtual string Template
        {
            get;
            set;
        }

        public virtual string GenerateContent()
        {
            throw new NotImplementedException();
        }
    }
}