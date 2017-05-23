using DailyDish.Wechat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.Wechat.Managers
{
    public class TokenManager
    {
        public static bool TestTokenValidation(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = WeChatHelper.SHA1Encrypt(tmpStr);
            tmpStr = tmpStr.ToLower();

            return tmpStr == signature;
        }
    }
}
