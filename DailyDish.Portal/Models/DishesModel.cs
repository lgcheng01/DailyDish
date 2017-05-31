using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyDish.Portal.Models
{
    public class DishesModel
    {
        public string Id { get; set; }
        public string DishName { get; set; }
        public string FirstTaste { get; set; }
        public string SecondTaste { get; set; }
        public string Explain { get; set; }
        public string MainIngredients { get; set; }
        public string Accessory { get; set; }
        public string PracticeUrl { get; set; }
        public int Status { get; set; }
        public double Score { get; set; }
        public DateTime CreateTime { get; set; }
    }
}