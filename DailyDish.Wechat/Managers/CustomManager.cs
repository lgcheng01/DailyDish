using DailyDish.Wechat.Configurations;
using DailyDish.Wechat.Messages.SendMessage;
using DailyDish.Wechat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Managers
{
    /// <summary>
    /// 客服管理者
    /// </summary>
    public class CustomManager
    {
        /// <summary>
        /// 发送客服信息
        /// </summary>
        /// <param name="globalToken"></param>
        /// <param name="message">SendMessage Json</param>
        public void SendCustomMessage(string globalToken, SendMessage message)
        {
            WeChatHttpUtility.SendHttpRequest(string.Format(WeChatConfiguration.SendCustomMessageUrl, globalToken), message.GetJsonString());
        }
    }
}
