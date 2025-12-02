using AD41HN_HFT_2022231.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json; // <-- FIGYELEM! System.Text.Json-t használunk, nem Newtonsoft-ot!

namespace AD41HN_HFT_2022231.Repository
{
    public static class DbSeederOhio
    {
        public static void ImportJsonToDatabase(FWCDbContext context)
        {
            // Ha már van adat, ne csináljunk semmit
            if (context.OhioGlucose.Any()) return;

            // Fájl elérési útja (Javítsd, ha kell!)
            var path = @"C:\Users\Peti ROG\Desktop\Tanulós\Diabetes Webapplication Backend másolata\AD41HN_HFT_2022231.Repository\OhioDataSet.json";

            if (!File.Exists(path)) return;

            var jsonString = File.ReadAllText(path);

            // A JSON Beállítások (Ez a lényeg!)
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Kis-nagybetű nem számít!
            };

            // Mivel a JSON-ben egy tömbben vannak vegyesen az objektumok,
            // először egy általános JsonElement listába olvassuk be.
            var items = JsonSerializer.Deserialize<List<JsonElement>>(jsonString, options);

            foreach (var item in items)
            {
                // Megnézzük a "type" tulajdonságot
                if (item.TryGetProperty("type", out var typeProp))
                {
                    var type = typeProp.GetString();

                    switch (type)
                    {
                        case "glucose":
                            var glucose = JsonSerializer.Deserialize<OhioGlucose>(item.GetRawText(), options);
                            context.OhioGlucose.Add(glucose);
                            break;

                        case "meal":
                            var meal = JsonSerializer.Deserialize<OhioMeal>(item.GetRawText(), options);

                            // 👇 EZT A RÉSZT RAKD VISSZA! 👇
                            // Nullázzuk az ID-kat, hogy az EF újként kezelje őket
                            if (meal.Foods != null)
                            {
                                foreach (var food in meal.Foods)
                                {
                                    food.Id = 0;
                                }
                            }
                            // 👆 EDDIG TART A JAVÍTÁS 👆

                            context.OhioMeal.Add(meal);
                            break;

                        case "insulin":
                            var insulin = JsonSerializer.Deserialize<OhioInsulin>(item.GetRawText(), options);
                            context.OhioInsulin.Add(insulin);
                            break;

                        case "exercise":
                            var exercise = JsonSerializer.Deserialize<OhioExercise>(item.GetRawText(), options);
                            context.OhioExercise.Add(exercise);
                            break;
                    }
                }
            }

            context.SaveChanges();
        }
    }
}