using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_WGUI.Classes
{
    public class Statistics
    {
        Dictionary<int, CityInfo> CityCatalogue;

        //Lee Added so that I can create an object without arguments
        public Statistics()
        {

        }

        //Constructor
        public Statistics(string filename, string filetype)
        {
            CityCatalogue = new Dictionary<int, CityInfo>();
            DataModeler modeler = new DataModeler();
            CityCatalogue = modeler.ParseFile(filename, filetype);
        }

        //City Methods

        //Returns a list of cities from the city dictionary that have a name matching parameter
        public List<CityInfo> DisplayCityInformation(string name)
        {
            List<CityInfo> list = new List<CityInfo>();
            foreach(CityInfo city in CityCatalogue.Values)
            {
                string temp = city.cityName;
                if(temp != null)
                {
                    if (city.cityName.Equals(name))
                    {
                        list.Add(city);
                    }
                }
                
            }
            return list;
        }

        //Gets first city in province, then compares population of current city to next in province.
        //Returns city with highest population.
        public CityInfo DisplayLargestPopulationCity(string province)
        {
            CityInfo toReturn = null;
            foreach(CityInfo city in CityCatalogue.Values)
            {
                string temp = city.GetProvince();
                if (temp != null)
                {
                    if (city.GetProvince().Equals(province))
                    {
                        if (toReturn == null)
                        {
                            toReturn = city;
                        }
                        else if (city.GetPopulation() > toReturn.GetPopulation())
                        {
                            toReturn = city;
                        }
                    }
                }
                    
            }
            return toReturn;
        }

        //Gets first city in province, then compares population of current city to next in province.
        //Returns city with lowest population.
        public CityInfo DisplaySmallestPopulationCity(string province)
        {
            CityInfo toReturn = null;
            foreach (CityInfo city in CityCatalogue.Values)
            {
                string temp = city.GetProvince();
                if (temp != null)
                {
                    if (city.GetProvince().Equals(province))
                    {
                        if (toReturn == null)
                        {
                            toReturn = city;
                        }
                        else if (city.GetPopulation() < toReturn.GetPopulation())
                        {
                            toReturn = city;
                        }
                    }
                }
                    
            }
            return toReturn;
        }

        //Finds a province in the dictionary and totals all cities in that province's population.
        //Returns 0 if city name unrecognized.
        public int DisplayProvincePopulation(string name)
        {
            int totalPop = 0;
            foreach (CityInfo city in CityCatalogue.Values)
            {
                string temp = city.GetProvince();
                if (temp != null)
                {
                    if (city.GetProvince().Equals(name))
                        totalPop += city.GetPopulation();
                }
                    
            }
            return totalPop;
        }

        //Returns a list of all cities (as CityInfo) in a province based on supplied name
        //Returns an empty list if name unrecognized
        public List<CityInfo> DisplayProvinceCities(string name)
        {
            
            List<CityInfo> cities = new List<CityInfo>();
            foreach (CityInfo city in CityCatalogue.Values)
            {
                string temp = city.GetProvince();
                if(temp != null)
                {
                    if (city.GetProvince().Equals(name))
                        cities.Add(city);
                }
                
            }
            return cities;
        }

        public CityPopComparison CompareCitiesPopulation(CityInfo x, CityInfo y)
        {
            return x.GetPopulation() > y.GetPopulation() 
                ? new CityPopComparison(x, y, x.GetPopulation(), y.GetPopulation())
                : new CityPopComparison(y, x, y.GetPopulation(), x.GetPopulation());
        }

        public void ShowCityOnMap(string name, string province)
        {
            string url = "https://www.latlong.net";
            CityInfo city = null;
            foreach(CityInfo cities in CityCatalogue.Values)
            {
                string temp = cities.GetProvince();
                if (temp != null)
                {
                    if (cities.GetProvince().Equals(province) && cities.cityName.Equals(name))
                    {
                        city = cities;
                    }
                }
                    
            }
            if(city != null)
            {
                url += "/c/?lat=" + city.GetLocation()[0] + "&long=" + city.GetLocation()[1];
            }
            System.Diagnostics.Process.Start("explorer.exe",$"\"{url}\""); 
        }

        //Sourced from https://stackoverflow.com/a/60809937
        //Returns value in Km, instead of in meters like reference link above.
        public double CalculateDistanceBetweenCities(CityInfo city1, CityInfo city2)
        {
            var d1 = city1.GetLocation()[0] * (Math.PI / 180.0);
            var num1 = city1.GetLocation()[1] * (Math.PI / 180.0);
            var d2 = city2.GetLocation()[0] * (Math.PI / 180.0);
            var num2 = city1.GetLocation()[1] * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376.5000 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        //Returns a sorted dictionary of provinces, with the population as the key in ascending order
        public List<Ranking> RankProvincesByPopulation()
        {
            List<Ranking> ranking = new List<Ranking>();
            int on = 0;
            int qc = 0;
            int ns = 0;
            int nb = 0;
            int mb = 0;
            int bc = 0;
            int pe = 0;
            int sk = 0;
            int ab = 0;
            int nl = 0;
            int nt = 0;
            int yt = 0;
            int nu = 0;

            foreach(CityInfo city in CityCatalogue.Values)
            {
                switch (city.province)
                {
                    case"Ontario":
                        on += city.population;
                        break;
                    case "Quebec":
                        qc += city.population;
                        break;
                    case "Nova Scotia":
                        ns += city.population;
                        break;
                    case "New Brunswick":
                        nb += city.population;
                        break;
                    case "Manitoba":
                        mb += city.population;
                        break;
                    case "British Columbia":
                        bc += city.population;
                        break;
                    case "Prince Edward Island":
                        pe += city.population;
                        break;
                    case "Saskatchewan":
                        sk += city.population;
                        break;
                    case "Alberta":
                        ab += city.population;
                        break;
                    case "Newfoundland and Labrador":
                        nl += city.population;
                        break;
                    case "Northwest Territories":
                        nt += city.population;
                        break;
                    case "Yukon":
                        yt += city.population;
                        break;
                    case "Nunavut":
                        nu += city.population;
                        break;
                    default:
                        break;
                }
            }
            ranking.Add(new Ranking(on, "Ontario"));
            ranking.Add(new Ranking(qc, "Quebec"));
            ranking.Add(new Ranking(ns, "Nova Scotia"));
            ranking.Add(new Ranking(nb, "New Brunswick"));
            ranking.Add(new Ranking(mb, "Manitoba"));
            ranking.Add(new Ranking(bc, "British Columbia"));
            ranking.Add(new Ranking(pe, "Prince Edward Island"));
            ranking.Add(new Ranking(sk, "Saskatchewan"));
            ranking.Add(new Ranking(ab, "Alberta"));
            ranking.Add(new Ranking(nl, "Newfoundland and Labrador"));
            ranking.Add(new Ranking(nt, "Northwest Territories"));
            ranking.Add(new Ranking(yt, "Yukon"));
            ranking.Add(new Ranking(nu, "Nunavut"));

            ranking.Sort(new Ranksorter());

            return ranking;
        }

        //Returns a sorted dictionary of provinces, with the number of cities as the key in ascending order
        public List<Ranking> RankProvincesByCities()
        {
            List<Ranking> ranking = new List<Ranking>();
            int on = 0;
            int qc = 0;
            int ns = 0;
            int nb = 0;
            int mb = 0;
            int bc = 0;
            int pe = 0;
            int sk = 0;
            int ab = 0;
            int nl = 0;
            int nt = 0;
            int yt = 0;
            int nu = 0;

            foreach (CityInfo city in CityCatalogue.Values)
            {
                switch (city.province)
                {
                    case "Ontario":
                        on ++;
                        break;
                    case "Quebec":
                        qc ++;
                        break;
                    case "Nova Scotia":
                        ns ++;
                        break;
                    case "New Brunswick":
                        nb ++;
                        break;
                    case "Manitoba":
                        mb ++;
                        break;
                    case "British Columbia":
                        bc ++;
                        break;
                    case "Prince Edward Island":
                        pe ++;
                        break;
                    case "Saskatchewan":
                        sk ++;
                        break;
                    case "Alberta":
                        ab ++;
                        break;
                    case "Newfoundland and Labrador":
                        nl ++;
                        break;
                    case "Northwest Territories":
                        nt ++;
                        break;
                    case "Yukon":
                        yt ++;
                        break;
                    case "Nunavut":
                        nu ++;
                        break;
                    default:
                        break;
                }
            }
            ranking.Add(new Ranking(on, "Ontario"));
            ranking.Add(new Ranking(qc, "Quebec"));
            ranking.Add(new Ranking(ns, "Nova Scotia"));
            ranking.Add(new Ranking(nb, "New Brunswick"));
            ranking.Add(new Ranking(mb, "Manitoba"));
            ranking.Add(new Ranking(bc, "British Columbia"));
            ranking.Add(new Ranking(pe, "Prince Edward Island"));
            ranking.Add(new Ranking(sk, "Saskatchewan"));
            ranking.Add(new Ranking(ab, "Alberta"));
            ranking.Add(new Ranking(nl, "Newfoundland and Labrador"));
            ranking.Add(new Ranking(nt, "Northwest Territories"));
            ranking.Add(new Ranking(yt, "Yukon"));
            ranking.Add(new Ranking(nu, "Nunavut"));

            ranking.Sort(new Ranksorter());

            return ranking;
        }

        //Returns a string detailing a province's capital as well as the capital's coordinates
        public string GetCapital(string province)
        {
            foreach(CityInfo city in CityCatalogue.Values)
            {
                string temp = city.GetProvince();
                if (temp != null)
                {
                    if (city.GetProvince().Equals(province) && city.capital.Equals("admin"))
                    {
                        return city.cityName + ":\n\t" + " Latitude: " + city.latitude + "\n\tLongitude: " + city.longitude;
                    }
                }
                    
            }
            return "Province not found";
        }
    }
}
