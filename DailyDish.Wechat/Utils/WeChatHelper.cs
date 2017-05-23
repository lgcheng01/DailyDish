using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace DailyDish.Wechat.Utils
{
    public static class WeChatHelper
    {
        public static string WeChatNowTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);
            long phptime = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;

            return phptime.ToString();
        }

        public static string ReadRequest(HttpRequestBase request)
        {
            Stream s = request.InputStream;
            byte[] bt = new byte[s.Length];
            s.Read(bt, 0, bt.Length);

            string postStr = Encoding.UTF8.GetString(bt);

            return postStr;
        }

        public static string ReadXmlNode(XmlDocument doc, string xPath)
        {
            XmlNode node = doc.SelectSingleNode(xPath);

            if (node == null)
            {
                return string.Empty;
            }
            else
            {
                return node.InnerText;
            }
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="intput">输入字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA1Encrypt(string intput)
        {
            byte[] StrRes = Encoding.Default.GetBytes(intput);
            HashAlgorithm mySHA = new SHA1CryptoServiceProvider();
            StrRes = mySHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte Byte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", Byte);
            }

            return EnText.ToString();
        }
    }
}
