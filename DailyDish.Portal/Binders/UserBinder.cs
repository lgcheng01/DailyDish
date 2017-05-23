using DailyDish.DB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyDish.Portal.Binders
{
    public class UserBinder : IModelBinder
    {
        private string sessionKey = "wechat_user";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {


            UserInfo user = null;
            if (controllerContext.RequestContext.HttpContext.Session[sessionKey] == null)
            {
                user = new UserInfo();
                controllerContext.RequestContext.HttpContext.Session[sessionKey] = user;
            }
            else
            {
                user = (UserInfo)controllerContext.RequestContext.HttpContext.Session[sessionKey];
            }
            return user;

        }
    }
}