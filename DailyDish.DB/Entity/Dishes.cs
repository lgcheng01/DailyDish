using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.DB.Entity
{
    public class Dishes
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
        public DateTime CreateTime { get; set; }
    }
}
