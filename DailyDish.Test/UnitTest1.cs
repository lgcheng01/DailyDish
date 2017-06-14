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
            string filePath = @"D:\v-chuya\DishInfo.txt";
            string[] lines = File.ReadAllLines(filePath);
            List<string[]> rows = lines.AsParallel().Skip(1).Select(l => l.Split('\t')).ToList();

            DailyDishHelper ddh = new DailyDishHelper();
            foreach (var item in rows)
            {
                Dishes dishes = new Dishes()
                {
                    Id = Guid.NewGuid().ToString(),
                    DishImage = item[0],
                    DishName = item[1],
                    FirstTaste =item[2],
                    SecondTaste =item[3],
                    Explain = item[4],
                    MainIngredients =item[5],
                    Accessory =item[6],
                    PracticeUrl =item[7],
                    Status =1,
                    CreateTime =DateTime.Now
                };
                ddh.ImportDishes(dishes);
            }
        }
    }
}
