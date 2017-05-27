using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.SendMessage
{
    /// <summary>
    /// 客服发送的图文消息
    /// </summary>
    public class NewsSendMessage : SendMessage
    {
        private string _msgType = "news";

        public override string msgtype
        {
            get
            {
                return _msgType;
            }
        }

        public NewsField news { get; set; }
    }

    public class NewsField
    {
        public List<NewsArticleField> articles { get; set; }
    }

    public class NewsArticleField
    {
        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public string picurl { get; set; }
    }
}
