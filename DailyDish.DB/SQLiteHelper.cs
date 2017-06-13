using DailyDish.DB.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DailyDish.DB
{
    public class SQLiteHelper
    {
        private static string DbFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["SQLString"]);
        private static string connectionString = "Data Source=" + DbFile;

        public SQLiteHelper()
        {
            CreateDataBaseIfNotExists();
        }

        public bool Exists(string strSql)
        {
            object obj = ExecuteSql(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Exists(string strSql, params SQLiteParameter[] cmdParms)
        {
            object obj = ExecuteSql(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public int ExecuteSql(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        public int ExecuteSql(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        connection.Open();
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }


        public static SQLiteDataReader ExecuteReader(string SQLString, params SQLiteParameter[] cmdParms)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SQLiteDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SQLite.SQLiteException e)
            {
                throw new Exception(e.Message);
            }

        }

        public object GetSingle(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        public DataSet Query(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SQLite.SQLiteException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        public static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }


        public SQLiteParameter MakeSQLiteParameter(string name, DbType type, object value)
        {
            SQLiteParameter parm = new SQLiteParameter(name, type);
            parm.Value = value;
            return parm;
        }

        public void CreateDataBaseIfNotExists()
        {
            while (!File.Exists(DbFile))
            {
                Thread.Sleep(500);
            }
            CreateDatabase();
        }
        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile, true);
        }
        private void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                ExecuteSql("CREATE TABLE IF NOT EXISTS UserInfo('OpenId' varchar(100) not null,'UserName' varchar(100) not null)");
                ExecuteSql("CREATE TABLE IF NOT EXISTS TasteHistory('Id' varchar(100) not null,'OpenId' varchar(100) not null,'UserName' varchar(100) not null,'LikeFlavor' varchar(100),'DisLikeFlavor' varchar(100),'Dieteticrestraint' varchar(100),'CreateTime' datetime)");
                ExecuteSql("CREATE TABLE IF NOT EXISTS Flavor('Id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,'FlavorName' varchar(100),'Type' varchar(100))");
                ExecuteSql("CREATE TABLE IF NOT EXISTS  Dishes( 'Id' varchar(100) NOT NULL,'DishImage' varchar(100), 'DishName' varchar(100), 'FirstTaste' varchar(100), 'SecondTaste' varchar(100), 'Explain' varchar(100), 'MainIngredients' varchar(100), 'Accessory' varchar(100), 'PracticeUrl' varchar(100), 'Status' INTEGER, 'CreateTime' datetime)");
                ExecuteSql("CREATE TABLE IF NOT EXISTS  DishScore( 'Id' varchar(100) NOT NULL, 'OpenId' varchar(100), 'DishesId' varchar(100), 'DishName' varchar(100), 'Score' double, 'FactorScore' double, 'Time' INTEGER, 'CreateTime' datetime, 'UpdateTime' datetime)");
                ExecuteSql("CREATE TABLE IF NOT EXISTS  RecommendedHistory( 'Id' varchar(100) NOT NULL, 'OpenId' varchar(100), 'DishesId' varchar(100),DishName varchar(100),'Score' double, 'CreateTime' datetime)");
            }

        }
    }
}
