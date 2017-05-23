using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.SendMessage
{
    /// <summary>
    /// 客服发送的图片消息
    /// </summary>
    public class ImageSendMessage : SendMessage
    {
        private string _msgType = "image";

        public override string msgtype
        {
            get
            {
                return _msgType;
            }
        }

        public ImageField image { get; set; }
    }

    public class ImageField
    {
        public string media_id { get; set;  }
    }
}
