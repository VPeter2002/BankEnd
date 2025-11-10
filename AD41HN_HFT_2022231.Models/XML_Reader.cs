using AD41HN_HFT_2022231.Models;
using System;
using System.IO;
using System.Xml.Serialization;

public class XmlPatientLoader
{
    public static Patient LoadPatientFromXml(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("A megadott XML fájl nem található.", filePath);

        XmlSerializer serializer = new XmlSerializer(typeof(Patient));
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            return (Patient)serializer.Deserialize(fs);
        }
    }
}
