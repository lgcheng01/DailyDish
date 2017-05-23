using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.SendMessage
{
    /// <summary>
    /// 客服发送的视频消息
    /// </summary>
    public class VideoSendMessage : SendMessage
    {
        private string _msgType = "video";

        public override string msgtype
        {
            get
            {
                return _msgType;
            }
        }

        public VideoField video { get; set; }
    }

    public class VideoField
    {
        public string media_id { get; set; }

        public string thumb_media_id { get; set; }

        public string title { get; set; }

        public string description { get; set; }
    }
}
