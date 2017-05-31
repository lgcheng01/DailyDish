using DailyDish.DB;
using DailyDish.DB.Entity;
using DailyDish.Portal.Models;
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
            strSql.Append("values");
            for (int i = 0; i < taboo.Length; i++)
            {
                strSql.Append(string.Format("('{0}','taboo'),", taboo[i]));
            }
            if (sh.ExecuteSql(strSql.ToString().Trim(',')) >= 1)
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

            return sh.Query(strSql.ToString()).Tables[0].AsEnumerable().Select(r => new Flavor { Id = Int32.Parse(r[0].ToString()), FlavorName = r[1].ToString(), Type = r[2].ToString() }).ToList<Flavor>();
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

        public List<Dishes> QueryDishes()
        {
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM Dishes");

            List<Dishes> list = sh.Query(strSql.ToString(), null).Tables[0].AsEnumerable().Select(r => new Dishes
            {
                Id = r[0].ToString(),
                DishName = r[1].ToString(),
                FirstTaste = r[2].ToString(),
                SecondTaste = r[3].ToString(),
                Explain = r[4].ToString(),
                MainIngredients = r[5].ToString(),
                Accessory = r[6].ToString(),
                PracticeUrl = r[7].ToString(),
                Status = Int32.Parse(r[8].ToString()),
                CreateTime = DateTime.Parse(r[9].ToString())
            }).ToList<Dishes>();
            return list;
        }

        public Dishes QueryDishesById(string dishesId)
        {
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM Dishes");
            strSql.Append(" where Id=@Id ");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@Id", DbType.String,dishesId)};
            Dishes list = sh.Query(strSql.ToString(), parameters).Tables[0].AsEnumerable().Select(r => new Dishes
            {
                Id = r[0].ToString(),
                DishName = r[1].ToString(),
                FirstTaste = r[2].ToString(),
                SecondTaste = r[3].ToString(),
                Explain = r[4].ToString(),
                MainIngredients = r[5].ToString(),
                Accessory = r[6].ToString(),
                PracticeUrl = r[7].ToString(),
                Status = Int32.Parse(r[8].ToString()),
                CreateTime = DateTime.Parse(r[9].ToString())
            }).ToList<Dishes>()[0];
            return list;
        }
        public TasteHistory QueryTasteHistoryByUser(string openId)
        {
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM TasteHistory");
            strSql.Append(" where OpenId=@OpenId ");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@OpenId", DbType.String,openId)};
            TasteHistory taste = sh.Query(strSql.ToString(), parameters).Tables[0].AsEnumerable().Select(r => new TasteHistory
            {
                Id = r[0].ToString(),
                OpenId = r[1].ToString(),
                UserName = r[2].ToString(),
                LikeFlavor = r[3].ToString(),
                DisLikeFlavor = r[4].ToString(),
                Dieteticrestraint = r[5].ToString(),
                //CreateTime =DateTime.Parse(r[6].ToString())
            }).ToList<TasteHistory>().OrderByDescending(x => x.CreateTime).FirstOrDefault();
            return taste;
        }

        public int InsertScore(List<DishScore> score)
        {
            int ret = 0;

            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DishScore(");
            strSql.Append("Id,OpenId,DishesId,DishName,Score,FactorScore,Time,CreateTime,UpdateTime)");
            strSql.Append(" values ");
            foreach (var item in score)
            {
                strSql.Append(string.Format("('{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}','{8}'),", item.Id, item.OpenId, item.DishesId, item.DishName, item.Score, item.FactorScore, item.Time.ToString(), item.CreateTime.ToString("s"), item.UpdateTime.ToString("s")));
            }
            if (sh.ExecuteSql(strSql.ToString().Trim(',')) >= 1)
            {
                ret = 1;
            }

            return ret;
        }
        public int DeleteScore(string openId)
        {
            int ret = 0;

            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from DishScore where OpenId=@OpenId");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@OpenId",DbType.String,openId)};

            if (sh.ExecuteSql(strSql.ToString(), parameters) >= 1)
            {
                ret = 1;
            }

            return ret;
        }

        public DishScore QuerySingleScoreByUser(string openId, string disedId)
        {
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM DishScore");
            strSql.Append(" where OpenId=@OpenId and DishesId=@DishesId ");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@OpenId", DbType.String,openId),
                    sh.MakeSQLiteParameter("@DishesId", DbType.String,disedId.ToString())};
            DishScore DishScore = sh.Query(strSql.ToString(), parameters).Tables[0].AsEnumerable().Select(r => new DishScore
            {
                Id = r[0].ToString(),
                OpenId = r[1].ToString(),
                DishesId = r[2].ToString(),
                DishName = r[3].ToString(),
                Score = double.Parse(r[4].ToString()),
                FactorScore = double.Parse(r[5].ToString()),
                Time = Int32.Parse(r[6].ToString()),
                CreateTime = DateTime.Parse(r[7].ToString()),
                UpdateTime = DateTime.Parse(r[8].ToString())
            }).ToList<DishScore>()[0];
            return DishScore;
        }

        public List<DishScore> QueryScoreByUser(string openId)
        {
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM DishScore");
            strSql.Append(" where OpenId=@OpenId ");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@OpenId", DbType.String,openId),
            };
            DataSet data = sh.Query(strSql.ToString(), parameters);
            List<DishScore> DishScore = sh.Query(strSql.ToString(), parameters).Tables[0].AsEnumerable().Select(r => new DishScore
            {
                Id = r[0].ToString(),
                OpenId = r[1].ToString(),
                DishesId = r[2].ToString(),
                DishName = r[3].ToString(),
                Score = double.Parse(r[4].ToString()),
                FactorScore = double.Parse(r[5].ToString()),
                Time = Int32.Parse(r[6].ToString()),
                CreateTime = DateTime.Parse(r[7].ToString()),
                UpdateTime = DateTime.Parse(r[8].ToString())
            }).ToList<DishScore>();
            return DishScore;
        }

        public bool UpdateScore(string openId, string disedId)
        {
            DishScore dish = QuerySingleScoreByUser(openId, disedId);
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishScore set ");
            strSql.Append("Score=@Score,");
            strSql.Append("Time=@Time");
            strSql.Append(" where OpenId=@OpenId and DishesId=@DishesId");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@Score",DbType.Double,dish.FactorScore*Math.Log10(dish.Time==1?10:dish.Time-1)),
                    sh.MakeSQLiteParameter("@Time",DbType.Int32,dish.Time==1?10:dish.Time-1),
                    sh.MakeSQLiteParameter("@OpenId",DbType.String,openId),
                    sh.MakeSQLiteParameter("@DishesId",DbType.String,disedId)
            };

            if (sh.ExecuteSql(strSql.ToString(), parameters) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int InsertRecommendedHistory(RecommendedHistory history)
        {
            int ret = 0;
            SQLiteHelper sh = new SQLiteHelper();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RecommendedHistory(");
            strSql.Append("Id,OpenId,DishesId,DishName,Score,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@Id,@OpenId,@DishesId,@DishName,@Score,@CreateTime)");
            SQLiteParameter[] parameters = {
                    sh.MakeSQLiteParameter("@Id", DbType.String,history.Id),
                    sh.MakeSQLiteParameter("@OpenId", DbType.String,history.OpenId),
                    sh.MakeSQLiteParameter("@DishesId", DbType.String,history.DishesId),
                     sh.MakeSQLiteParameter("@DishName", DbType.String,history.DishName),
                    sh.MakeSQLiteParameter("@Score", DbType.Double,history.Score),
                    sh.MakeSQLiteParameter("@CreateTime", DbType.DateTime,history.CreateTime)
                    };

            if (sh.ExecuteSql(strSql.ToString(), parameters) >= 1)
            {
                ret = 1;
            }

            return ret;
        }

        public bool GetFactorScore(string openId)
        {
            List<DishScore> scorelist = QueryScoreByUser(openId);
            if (scorelist.Count != 0)
            {
                DeleteScore(openId);
            }
            TasteHistory taste = QueryTasteHistoryByUser(openId);
            List<Dishes> dishes = QueryDishes();
            List<DishScore> scoreList = new List<DishScore>();

            foreach (var item in dishes)
            {
                double x = 0;
                double y = 0;
                double z = 0;
                if (taste.LikeFlavor.Contains(item.FirstTaste) || taste.LikeFlavor.Contains(item.SecondTaste))
                {
                    x = 0.5F;
                }
                if (taste.DisLikeFlavor.Contains(item.FirstTaste) || taste.DisLikeFlavor.Contains(item.SecondTaste))
                {
                    x = -0.5F;
                }
                if (taste.Dieteticrestraint.Split(',').Where(r => item.MainIngredients.Contains(r)).Count() > 0)
                {
                    y = -1F;
                }
                if (taste.Dieteticrestraint.Split(',').Where(r => item.Accessory.Contains(r)).Count() > 0)
                {
                    z = -0.25F;
                }
                DishScore score = new DishScore()
                {
                    Id = Guid.NewGuid().ToString(),
                    DishesId = item.Id,
                    OpenId = taste.OpenId,
                    DishName = item.DishName,
                    FactorScore = x + y + z,
                    Time = 10,
                    Score = (x + y + z) * Math.Log10(10),
                    CreateTime = DateTime.UtcNow.AddHours(8),
                    UpdateTime = DateTime.UtcNow.AddHours(8)
                };
                scoreList.Add(score);
            }
            InsertScore(scoreList);
            return true;
        }
        


    public bool UpdateDishScore(string openId, string disedId)
    {
        return UpdateScore(openId, disedId);
    }

    public void SaveRecommendHistory(string openId, string dishesId, double score, string dishName)
    {
        RecommendedHistory history = new RecommendedHistory()
        {
            Id = Guid.NewGuid().ToString(),
            OpenId = openId,
            DishesId = dishesId,
            DishName = dishName,
            Score = score,
            CreateTime = DateTime.Now,
        };
        InsertRecommendedHistory(history);
    }

    public DishesModel GetDishByUser(string openId)
    {
        List<DishScore> score = QueryScoreByUser(openId);
        if (score.Count == 0)
        {
            GetFactorScore(openId);
            score = QueryScoreByUser(openId);
        }
        DishScore dishscore = score.OrderByDescending(r => r.Score).ThenBy(r => r.DishesId).First();
        Dishes dish = QueryDishesById(dishscore.DishesId);
        DishesModel model = new DishesModel()
        {
            Id = dishscore.DishesId,
            SecondTaste = dish.SecondTaste,
            Status = dish.Status,
            Accessory = dish.Accessory,
            CreateTime = dish.CreateTime,
            DishName = dish.DishName,
            Explain = dish.Explain,
            FirstTaste = dish.FirstTaste,
            MainIngredients = dish.MainIngredients,
            PracticeUrl = dish.PracticeUrl,
            Score = dishscore.Score
        };

        return model;
    }
}

}