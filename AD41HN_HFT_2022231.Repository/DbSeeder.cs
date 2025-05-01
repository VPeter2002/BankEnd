using AD41HN_HFT_2022231.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD41HN_HFT_2022231.Repository
{
    public static class DbSeeder
    {
        public static void SeedCareSensData(FWCDbContext context)
        {
            if (!context.CareSensAirDatas.Any())
            {
                var dataList = new List<CareSensAirData>();

                using (TextFieldParser parser = new TextFieldParser("C:\\Users\\Peti ROG\\Desktop\\Tanulós\\Diabetes Webapplication Backend másolata\\CareSensAirDatas.csv")) 
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] row = parser.ReadFields();

                        var data = new CareSensAirData
                        {
                            Device = row[0],
                            SerialNumber = row[1],
                            Numberof_Value = int.Parse(row[2]),
                            Date_Time = DateTime.Parse(row[3]),
                            Value = double.Parse(row[4], CultureInfo.InvariantCulture),
                            Unit = row[5],
                            Trend_Rate = string.IsNullOrWhiteSpace(row[6]) ? 0.0 : double.Parse(row[6], CultureInfo.InvariantCulture)
                        };

                        dataList.Add(data);
                    }
                }

                context.CareSensAirDatas.AddRange(dataList);
                context.SaveChanges(); // itt automatikusan generálódik az Id
            }
        }
    }
}
