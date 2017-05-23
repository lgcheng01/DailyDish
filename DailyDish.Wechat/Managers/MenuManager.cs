using DailyDish.Wechat.Configurations;
using DailyDish.Wechat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Managers
{
    /// <summary>
    /// 菜单管理者
    /// </summary>
    public class MenuManager
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="globalToken"></param>
        /// <returns></returns>
        public static string GetMenu(string globalToken)
        {
            string url = string.Format(WeChatConfiguration.GetMenuUrl, globalToken);

            return WeChatHttpUtility.GetData(url);
        }
        
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="globalToken"></param>
        public static void CreateMenu(string menu, string globalToken)
        {
            string url = string.Format(WeChatConfiguration.CreateMenuUrl, globalToken);
            WeChatHttpUtility.SendHttpRequest(url, menu);
        }
        
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="globalToken"></param>
        public static void DeleteMenu(string globalToken)
        {
            string url = string.Format(WeChatConfiguration.DeleteMenuUrl, globalToken);
            WeChatHttpUtility.GetData(url);
        }
        
        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <returns></returns>
        public static string LoadMenu()
        {
            return string.Empty;
        }
    }
}
