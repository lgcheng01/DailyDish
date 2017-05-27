using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.SendMessage
{
    /// <summary>
    /// 客服发送的音乐消息
    /// </summary>
    public class MusicSendMessage : SendMessage
    {
        private string _msgType = "music";

        public override string msgtype
        {
            get
            {
                return _msgType;
            }
        }

        public MusicField music { get; set; }
    }

    public class MusicField
    {
        public string title { get; set; }

        public string description { get; set; }

        public string musicurl { get; set; }

        public string hqmusicurl { get; set; }

        public string thumb_media_id { get; set; }
    }
}
