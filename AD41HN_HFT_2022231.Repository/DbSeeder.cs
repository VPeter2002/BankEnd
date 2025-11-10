using AD41HN_HFT_2022231.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        public static void SeedXmlPatientData(FWCDbContext context, string xmlPath)
        {
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine("Nem található XML: " + xmlPath);
                return;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Patient));
                using (FileStream fs = new FileStream(xmlPath, FileMode.Open))
                {
                    Patient patient = (Patient)serializer.Deserialize(fs);

                    // Kontextusba mentés (tetszőleges relációkat itt lehet építeni)
                    context.Patients.Add(patient);
                    context.SaveChanges();

                    Console.WriteLine($"Beteg {patient.Id} sikeresen betöltve.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("XML beolvasási hiba: " + ex.Message);
            }
        }
    }
}
