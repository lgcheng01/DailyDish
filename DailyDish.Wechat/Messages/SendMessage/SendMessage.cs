using DailyDish.Wechat.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Messages.SendMessage
{
    /// <summary>
    /// 客服消息基类
    /// 注：未添加客服字段，后续需要进行客服管理的话再添加
    /// </summary>
    public abstract class SendMessage
    {
        public string touser { get; set; }

        public abstract string msgtype { get; }

        public string GetJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
