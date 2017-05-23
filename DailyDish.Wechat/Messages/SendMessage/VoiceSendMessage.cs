using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.SendMessage
{
    /// <summary>
    /// 客服发送的语音消息
    /// </summary>
    public class VoiceSendMessage : SendMessage
    {
        private string _msgType = "voice";

        public override string msgtype
        {
            get 
            {
                return _msgType;
            }
        }

        public VoiceField voice { get; set; }
    }

    public class VoiceField
    {
        public string media_id { get; set; }
    }
}
