using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyDish.Wechat.Entities.Menu
{
    public sealed class MenuSub : MenuModelBase
    {
        public MenuModelBase[] sub_button { get; set; }
    }
}