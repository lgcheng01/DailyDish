using DailyDish.DB;
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
            if (QueryUser(openId) == null)
            {
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
            return ret;
        }

        public int AddTabooData(string[] taboo)
        {
            int ret = 0;
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Flavor(");
            strSql.Append("FlavorName,Type)");
            strSql.Append("values (");
            strSql.Append("@FlavorName,@Type)");
            for (int i = 0; i < taboo.Length; i++)
            {
                SQLiteParameter[] parameters = {
                sh.MakeSQLiteParameter("@FlavorName",DbType.String,taboo[i]),
                sh.MakeSQLiteParameter("@Type",DbType.String,"taboo"),
                };
                if (sh.ExecuteSql(strSql.ToString(), parameters) >= 1)
                {
                    ret = 1;
                }
            }
            return ret;
        }

        public int SaveUserTaste(TasteHistory history)
        {
            int ret = 0;
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TasteHistory(");
            strSql.Append("Id,OpenId,UserName,LikeFlavor,DisLikeFlavor,Dieteticrestraint,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@Id,@OpenId,@UserName,@LikeFlavor,@DisLikeFlavor,@Dieteticrestraint,@CreateTime)");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@Id", DbType.String,history.Id),
                    sh.MakeSQLiteParameter("@OpenId", DbType.String,history.OpenId),
                    sh.MakeSQLiteParameter("@UserName", DbType.String,history.UserName),
                    sh.MakeSQLiteParameter("@LikeFlavor", DbType.String,history.LikeFlavor),
                    sh.MakeSQLiteParameter("@DisLikeFlavor", DbType.String,history.DisLikeFlavor),
                    sh.MakeSQLiteParameter("@Dieteticrestraint", DbType.String,history.Dieteticrestraint),
                    sh.MakeSQLiteParameter("@CreateTime", DbType.DateTime,history.CreateTime)
                    };

            if (sh.ExecuteSql(strSql.ToString(), parameters) >= 1)
            {
                ret = 1;
            }

            return ret;
        }

        public List<Flavor> QueryFlavor()
        {
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM Flavor");

          return  sh.Query(strSql.ToString()).Tables[0].AsEnumerable().Select(r => new Flavor { Id = Int32.Parse(r[0].ToString()), FlavorName = r[1].ToString(), Type = r[2].ToString() }).ToList<Flavor>();
        }

        public UserInfo QueryUser(string openId)
        {
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM UserInfo");
            strSql.Append(" where OpenId=@OpenId ");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@OpenId", DbType.String,openId)};
            DataSet data = sh.Query(strSql.ToString(), parameters);
            if (data.Tables[0].Rows.Count != 0)
            {
                UserInfo user = sh.Query(strSql.ToString(), parameters).Tables[0].AsEnumerable().Select(r => new UserInfo { OpenId = r[0].ToString(), UserName = r[1].ToString() }).ToList<UserInfo>()[0];

                return user;
            }
            else
            {
                return null;
            }
        }
        public int ImportDishes(Dishes dishes)
        {
            int ret = 0;
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Dishes(");
            strSql.Append("Id,DishName,FirstTaste,SecondTaste,Explain,MainIngredients,Accessory,PracticeUrl,Status,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@Id,@DishName,@FirstTaste,@SecondTaste,@Explain,@MainIngredients,@Accessory,@PracticeUrl,@Status,@CreateTime)");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@Id", DbType.String,dishes.Id),
                    sh.MakeSQLiteParameter("@DishName", DbType.String,dishes.DishName),
                    sh.MakeSQLiteParameter("@FirstTaste", DbType.String,dishes.FirstTaste),
                    sh.MakeSQLiteParameter("@SecondTaste", DbType.String,dishes.SecondTaste),
                    sh.MakeSQLiteParameter("@Explain", DbType.String,dishes.Explain),
                    sh.MakeSQLiteParameter("@MainIngredients", DbType.String,dishes.MainIngredients),
                    sh.MakeSQLiteParameter("@Accessory", DbType.String,dishes.Accessory),
                    sh.MakeSQLiteParameter("@PracticeUrl", DbType.String,dishes.PracticeUrl),
                    sh.MakeSQLiteParameter("@Status", DbType.String,dishes.Status),
                    sh.MakeSQLiteParameter("@CreateTime", DbType.DateTime,dishes.CreateTime)
                    };

            if (sh.ExecuteSql(strSql.ToString(), parameters) >= 1)
            {
                ret = 1;
            }

            return ret;
        }

    }
}