using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using DailyDish.Portal.SQLDll;
using DailyDish.DB.Entity;

namespace DailyDish.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string filePath = @"I:\srouce\DishDatabase——dishes.txt";
            string[] lines = File.ReadAllLines(filePath);
            List<string[]> rows = lines.AsParallel().Skip(1).Select(l => l.Split('\t')).ToList();

            DailyDishHelper ddh = new DailyDishHelper();
            foreach (var item in rows)
            {
                Dishes dishes = new Dishes()
                {
                    Id = Guid.NewGuid().ToString(),
                    DishName = item[0],
                    FirstTaste =item[1],
                    SecondTaste =item[2],
                    Explain = item[3],
                    MainIngredients =item[4],
                    Accessory =item[5],
                    PracticeUrl =item[6],
                    Status =1,
                    CreateTime =DateTime.Now
                };
                ddh.ImportDishes(dishes);
            }
        }
    }
}
