using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project1_WGUI.Classes
{
    public class DataModeler
    {
        Dictionary<int, CityInfo> CityCatalogue = null;
        public delegate void ParserHandler(string filename);

        public Dictionary<int, CityInfo> ParseFile(string filename, string filetype)
        {


            if (filetype == "CSV")
            {
                ParserHandler handlerCSV = ParseCSV;
                handlerCSV(filename);

            }
            else if (filetype == "XML")
            {
                ParserHandler handlerXML = ParseXML;
                handlerXML(filename);
            }
            else if (filetype == "JSON")
            {
                ParserHandler handlerJSON = ParseJSON;
                handlerJSON(filename);
            }
            else
                Console.WriteLine("Invalid File name");
            return CityCatalogue;
        }

        public void ParseXML(string filename)
        {
            CityCatalogue = new Dictionary<int, CityInfo>();
            XmlDocument xml = new XmlDocument();
            xml.Load(filename);

            foreach (XmlNode node in xml.DocumentElement)
            {
                string city_ascii = node["city_ascii"].InnerText;
                float lat = float.Parse(node["lat"].InnerText);
                float lng = float.Parse(node["lng"].InnerText);
                int population = int.Parse(node["population"].InnerText);
                string capital = node["capital"].InnerText;
                int id = int.Parse(node["id"].InnerText);
                string province = node["admin_name"].InnerText;

                CityInfo city = new CityInfo(city_ascii, lat, lng, population, capital, province);

                CityCatalogue.Add(id, city);
            }
        }

        public void ParseJSON(string filename) //This needs to be fixed by making a class etc...
        {
            using (StreamReader r = new StreamReader(filename)) //@"Data\"
            {
                CityCatalogue = new Dictionary<int, CityInfo>();
                string json = r.ReadToEnd();
                List<JsonCity> items = JsonConvert.DeserializeObject<List<JsonCity>>(json);
                for (int i = 0; i < items.Count; i++)
                {
                    CityInfo city = new CityInfo(items[i].city_ascii, items[i].lat, items[i].lng, items[i].population, items[i].capital, items[i].admin_name);
                    CityCatalogue.Add(items[i].id, city);
                }
                //CityCatalogue = new Dictionary<int, CityInfo>();
                //string json = r.ReadToEnd();
                //List<CityInfo> items = JsonConvert.DeserializeObject<List<CityInfo>>(json);
                //for (int i = 0; i < items.Count; i++)
                //{
                //    CityInfo city = new CityInfo(items[i].cityAscii, items[i].latitude, items[i].longitude, items[i].population, items[i].capital, items[i].province);
                //    CityCatalogue.Add(items[i].cityID, city);
                //}
            }
        }

        public void ParseCSV(string filename)
        {
            string currentDirectory = Path.GetDirectoryName(filename);
            string fullPathOnly = Path.GetFullPath(currentDirectory + "\\Canadacities.csv");
            //Reading the contents of the csv file as individual lines
            string[] csvLines = System.IO.File.ReadAllLines(fullPathOnly);
            CityCatalogue = new Dictionary<int, CityInfo>();

            //Creating lists with the csv data
            List<String> cityName = new List<String>();
            List<float> cityLat = new List<float>();
            List<float> citLng = new List<float>();
            List<String> provinceName = new List<String>();
            List<int> population = new List<int>();
            List<int> id = new List<int>();
            List<String> capital = new List<String>();

            //Splitting each row into column data
            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] rowData = csvLines[i].Split(',');
                cityName.Add(rowData[1]);

                float g = float.Parse(rowData[2]);
                cityLat.Add(g);

                float l = float.Parse(rowData[3]);
                citLng.Add(l);

                provinceName.Add(rowData[5]);

                int p = int.Parse(rowData[7]);
                population.Add(p);

                int rawId = int.Parse(rowData[8]);
                id.Add(rawId);

                capital.Add(rowData[6]);

            }
            for (int i = 1; i < cityName.Count; i++)
            {
                CityInfo city = new CityInfo(cityName[i], cityLat[i], citLng[i], population[i], capital[i], provinceName[i]);

                CityCatalogue.Add(id[i], city);
            }
        }
    }

    public class JsonCity
    {
        public int id;
        public string city;
        public string city_ascii;
        public int population;
        public string admin_name;
        public double lat;
        public double lng;
        public string capital;
    }
}
