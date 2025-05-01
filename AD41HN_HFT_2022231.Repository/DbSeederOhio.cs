using AD41HN_HFT_2022231.Models;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository
{
    public static class DbSeederOhio
    {
        public static void ImportJsonToDatabase(FWCDbContext context)
        {
            if (context.OhioGlucose.Any()) return;

            using var reader = new StreamReader("C:\\Users\\Peti ROG\\Desktop\\Tanulós\\Diabetes Webapplication Backend másolata\\AD41HN_HFT_2022231.Repository\\OhioDataSetShort.json");
            var json = reader.ReadToEnd();
            var items = JsonConvert.DeserializeObject<JArray>(json);

            foreach (var item in items)
            {
                var type = item["type"]?.ToString();

                switch (type)
                {
                    case "glucose":
                        var glucose = item.ToObject<OhioGlucose>();
                        context.OhioGlucose.Add(glucose);
                        break;
                    case "meal":
                        var meal = new OhioMeal
                        {
                            Timestamp = item.Value<long>("timestamp"),
                            Type = item.Value<string>("type"),
                            PatID = item.Value<int>("patID"),
                            Key = item.Value<string>("key"),
                            M = item.Value<long>("m"),
                            Collection = item.Value<string>("collection"),
                            Foods = new List<FoodItem>()
                        };

                        var foods = item["foods"] as JArray;
                        if (foods != null)
                        {
                            foreach (var f in foods)
                            {
                                var food = new FoodItem
                                {
                                    Amount = f.Value<double>("amount"),
                                    FoodJsonId = f.Value<int>("id"),
                                    Text = f.Value<string>("text"),
                                    Unit = f.Value<string>("unit"),
                                    Weights = f.Value<int>("weights"),
                                    Details = new MealDetails
                                    {
                                        MealType = f["details"]?["mealType"]?.ToString()
                                    }
                                };

                                meal.Foods.Add(food);
                            }
                        }

                        context.OhioMeal.Add(meal);
                     break;
                        //case "exercise":
                        //    var exercise = item.ToObject<ExerciseSession>();
                        //    context.Exercises.Add(exercise);
                        //    break;
                }
            }

            context.SaveChanges();
        }
    }
}
