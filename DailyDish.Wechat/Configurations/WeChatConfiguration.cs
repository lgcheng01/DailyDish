using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Configurations
{
    public static class WeChatConfiguration
    {
        public static string AppID = ConfigurationManager.AppSettings["AppID"];

        public static string AppSecret = ConfigurationManager.AppSettings["AppSecret"];

        #region OAuth2.0网页授权
        /// <summary>
        /// 该链接会弹出授权页面Url
        /// 注：1. 占位符{0}为回调请求路径，2.回调函数获取code值
        /// </summary>
        public static string OAuthUserInfoUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppID + "&redirect_uri={0}&response_type=code&scope=snsapi_userinfo&state=1#wechat_redirect";

        /// <summary>
        /// 该链接不会弹出授权页面Url
        /// 注：1. 占位符{0}为回调请求路径，2.回调函数获取code值
        /// </summary>
        public static string OAuthBaseInfoUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppID + "&redirect_uri={0}&response_type=code&scope=snsapi_base&state=1#wechat_redirect";

        /// <summary>
        /// 根据AppID,AppSecret及回调函数获取的Code值获取Access Token的Url
        /// 注：1. 占位符{0}为Code,无其他占位符。2. 返回json包含openid
        /// </summary>
        public static string AccessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + AppID + "&secret=" + AppSecret + "&code={0}&grant_type=authorization_code";

        /// <summary>
        /// 根据AppID及AppSecret获取公众账号Token的Url
        /// 注：1. 无占位符。2. 返回json不包含openid
        /// </summary>
        public static string GlobalTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + AppID + "&secret=" + AppSecret;

        /// <summary>
        /// 提供Access Token或者公众账号Token及OpenId获取微信用户信息的Url
        /// 注： 1. 占位符{0}为Access Token 或者 Token，占位符{1}为OpenId。 2. 如果用户为关注，只能得到openid
        /// </summary>
        public static string GetUserInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}";
        #endregion

        #region QrCode二维码
        /// <summary>
        /// 临时二维码请求json
        /// 使用string.format时，报：字符串格式错误，因为其中有{
        /// 解决办法，将原有字符串中的一个{用两个{代替
        /// 注：1. 占位符{0}为SceneID（场景ID）范围：1~1000
        /// </summary>
        public const string TemporaryQrCodeJson = "{{\"expire_seconds\": 1800, \"action_name\": \"QR_SCENE\", \"action_info\": {{\"scene\": {{\"scene_id\": {0}}}}}}}";
        
        /// <summary>
        /// 永久二维码请求json
        /// 注：1. 占位符{0}为SceneID（场景ID）范围：1~1000
        /// </summary>
        public const string PermanentQrCodeJson = "{{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {{\"scene\": {{\"scene_id\": {0}}}}}}}";

        /// <summary>
        /// 创建二维码Ticket的Url
        /// 注：1. 占位符{0}为公众账号Token,无其他占位符
        /// </summary>
        public static string CreateTicketUrl = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";

        /// <summary>
        /// 根据Ticket值获取二维码的Url
        /// 注： 1. 占位符{0}为ticket值,无其他占位符
        /// </summary>
        public static string GetQrCodeUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";
        #endregion

        #region Menu自定义菜单
        /// <summary>
        /// 获取Menu的Url
        /// 注： 1. 占位符{0}为公众账号Token，无其他占位符
        /// </summary>
        public static string GetMenuUrl = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}";

        /// <summary>
        /// 创建Menu的Url
        /// 注： 1. 占位符{0}为公众账号Token，无其他占位符
        /// </summary>
        public static string CreateMenuUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";

        /// <summary>
        /// 删除Menu的Url
        /// 注： 1. 占位符{0}为公众账号Token，无其他占位符
        /// </summary>
        public static string DeleteMenuUrl = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";
        #endregion

        #region 客服接口
        /// <summary>
        /// 发送信息的Url
        /// </summary>
        public static string SendCustomMessageUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
        #endregion
    }
}
