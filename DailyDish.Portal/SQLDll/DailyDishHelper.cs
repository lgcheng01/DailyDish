using DailyDish.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace DailyDish.Portal.SQLDll
{
    public class DailyDishHelper
    {
        public int CreateUser(string openId, string userName)
        {
            int ret = 0;
          
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into UserInfo(");
                strSql.Append("OpenId,UserName)");
                strSql.Append(" values (");
                strSql.Append("@OpenId,@UserName)");
                SQLiteParameter[] parameters = {
                    SQLiteHelper.MakeSQLiteParameter("@OpenId", DbType.String,openId),
                    SQLiteHelper.MakeSQLiteParameter("@UserName", DbType.String,userName)
                    };

                if (SQLiteHelper.ExecuteSql(strSql.ToString(), parameters) >= 1)
                {
                    ret = 1;
                }
           
            return ret;
        }
    }
}