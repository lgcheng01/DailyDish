using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.DB.Entity
{
    public class DishScore
    {
        public string Id { get; set; }
        public string OpenId { get; set; }
        public string DishesId { get; set; }
        public string DishName { get; set; }
        public double Score { get; set; }
        public double FactorScore { get; set; }
        public int Time { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
