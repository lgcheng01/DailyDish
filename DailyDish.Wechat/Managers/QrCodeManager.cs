using DailyDish.Wechat.Configurations;
using DailyDish.Wechat.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace DailyDish.Wechat.Managers
{
    /// <summary>
    /// 二维码管理者
    /// </summary>
    public class QrCodeManager
    {
        /// <summary>
        /// 根据场景ID获取ticket
        /// </summary>
        /// <param name="sceneID">场景ID</param>
        /// <param name="isTemp">是否是临时二维码</param>
        /// <returns></returns>
        private static string GetTicket(int sceneID, string globalToken, bool isTemp)
        {
            if (sceneID > 0 && sceneID <= 1000)
            {
                return null;
            }

            string result = null;
            string data = string.Empty;
            if (isTemp)
            {
                data = string.Format(WeChatConfiguration.TemporaryQrCodeJson, sceneID);
            }
            else
            {
                data = string.Format(WeChatConfiguration.PermanentQrCodeJson, sceneID);
            }

            string ticketJson = WeChatHttpUtility.SendHttpRequest(string.Format(WeChatConfiguration.CreateTicketUrl, globalToken), data);

            XDocument doc = JsonConvert.DeserializeXNode(ticketJson, "root");
            XElement root = doc.Root;
            if (root != null)
            {
                XElement ticket = root.Element("ticket");
                if (ticket != null)
                {
                    result = ticket.Value;
                }
            }

            return result;
        }
        /// <summary>
        /// 创建临时二维码
        /// </summary>
        /// <param name="sceneID">场景id，int类型</param>
        /// <returns></returns>
        public static string GenerateTemp(int sceneID, string globalToken)
        {
            string ticket = GetTicket(sceneID, globalToken, true);
            if (ticket == null)
            {
                return null;
            }

            return WeChatHttpUtility.GetData(string.Format(WeChatConfiguration.GetQrCodeUrl, HttpUtility.UrlEncode(ticket)));
        }
        /// <summary>
        /// 创建永久二维码
        /// </summary>
        /// <param name="sceneID">场景id，int类型</param>
        /// <returns></returns>
        public static string GeneratePermanent(int sceneID, string globalToken)
        {
            string ticket = GetTicket(sceneID, globalToken, false);
            if (ticket == null)
            {
                return null;
            }

            return WeChatHttpUtility.GetData(string.Format(WeChatConfiguration.GetQrCodeUrl, HttpUtility.UrlEncode(ticket)));
        }

        /// <summary>
        /// 保存场景永久二维码成图片
        /// </summary>
        /// <param name="sceneID"></param>
        /// <param name="globalToken"></param>
        /// <param name="imageName"></param>
        /// <param name="path"></param>
        public static void SavePermanentQrCodeImage(int sceneID, string globalToken, string imageName, string path)
        {
            if (!Directory.Exists(path))
            {
                throw new Exception("不存在此路径： " + path);
            }
            string ticket = GetTicket(sceneID, globalToken, false);
            string qrUrl = string.Format(WeChatConfiguration.GetQrCodeUrl, HttpUtility.UrlEncode(ticket));

            WebClient myWebClient = new WebClient();

            try
            {
                myWebClient.DownloadFile(qrUrl, Path.Combine(path, imageName));
            }
            catch (Exception)
            {
                throw new Exception("获取二维码图片失败！");
            }
        }
    }
}
