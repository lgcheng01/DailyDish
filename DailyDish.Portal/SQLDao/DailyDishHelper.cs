﻿using DailyDish.DB;
using DailyDish.DB.Entity;
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
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("OpenId,UserName)");
            strSql.Append(" values (");
            strSql.Append("@OpenId,@UserName)");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@OpenId", DbType.String,openId),
                    sh.MakeSQLiteParameter("@UserName", DbType.String,userName)
                    };

            if (sh.ExecuteSql(strSql.ToString(), parameters) >= 1)
            {
                ret = 1;
            }

            return ret;
        }

        public int SaveUserTaste(TasteHistory history)
        {
            int ret = 0;
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TasteHistory(");
            strSql.Append("Id,OpenId,UserName,LikeFlavor,DisLikeFlavor)");
            strSql.Append(" values (");
            strSql.Append("@Id,@OpenId,@UserName,@LikeFlavor,@DisLikeFlavor)");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@Id", DbType.Guid,history.Id),
                    sh.MakeSQLiteParameter("@OpenId", DbType.String,history.OpenId),
                    sh.MakeSQLiteParameter("@UserName", DbType.String,history.UserName),
                    sh.MakeSQLiteParameter("@LikeFlavor", DbType.String,history.LikeFlavor),
                    sh.MakeSQLiteParameter("@DisLikeFlavor", DbType.String,history.DisLikeFlavor)
                    };

            if (sh.ExecuteSql(strSql.ToString(), parameters) >= 1)
            {
                ret = 1;
            }

            return ret;
        }

    }
}