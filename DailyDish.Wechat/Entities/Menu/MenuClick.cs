using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyDish.Wechat.Entities.Menu
{
    public sealed class MenuClick : MenuModelBase
    {
        private string _type = "click";

        public string type
        {
            get { return _type; }
        }

        public string key { get; set; }
    }
}