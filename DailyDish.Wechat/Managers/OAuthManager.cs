using DailyDish.Wechat.Configurations;
using DailyDish.Wechat.Entities.Token;
using DailyDish.Wechat.Entities.User;
using DailyDish.Wechat.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Managers
{
    /// <summary>
    /// 网页授权管理者
    /// </summary>
    public class OAuthManager
    {
        /// <summary>
        /// 获取弹出授权页面的Url
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public static string GetPopupOAuthRedirectUrl(string redirectUrl)
        {
            return string.Format(WeChatConfiguration.OAuthUserInfoUrl, redirectUrl);
        }

        /// <summary>
        /// 获取不弹出授权页面的Url
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public static string GetNotPopupOAuthRedirectUrl(string redirectUrl)
        {
            return string.Format(WeChatConfiguration.OAuthBaseInfoUrl, redirectUrl);
        }

        /// <summary>
        /// 获取Access Token获得,包括openId
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static OAuthToken GetAccessToken(string code)
        {
            string Str = WeChatHttpUtility.GetJson(string.Format(WeChatConfiguration.AccessTokenUrl, code));
            OAuthToken Oauth_Token_Model = JsonConvert.DeserializeObject<OAuthToken>(Str);
            return Oauth_Token_Model;
        }

        /// <summary>
        /// 获取全局Token
        /// </summary>
        /// <returns></returns>
        public static OAuthGlobalToken GetGlobalAccessToken()
        {
            string Str = WeChatHttpUtility.GetJson(WeChatConfiguration.GlobalTokenUrl);
            OAuthGlobalToken Oauth_GlobalToken_Model = JsonConvert.DeserializeObject<OAuthGlobalToken>(Str);
            return Oauth_GlobalToken_Model;
        }

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static OAuthUser GetUserInfo(string accessToken, string openId)
        {
            string Str = WeChatHttpUtility.GetJson(string.Format(WeChatConfiguration.GetUserInfoUrl, accessToken, openId));
            OAuthUser OAuthUser_Model = JsonConvert.DeserializeObject<OAuthUser>(Str);
            return OAuthUser_Model;
        }
    }
}
