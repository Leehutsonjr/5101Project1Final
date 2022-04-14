using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_WGUI.Classes
{
    public class CityInfo
    {
        public int cityID { get; set; }
        public string cityName { get; set; }
        public string cityAscii { get; set; }

        public int population;
        public string province;
        public double latitude;
        public double longitude;
        public string capital;


        public CityInfo()
        {

        }
        public CityInfo(string cityName, double lat, double lng, int pop, string capital, string province)
        {
            this.cityName = cityName;
            latitude = lat;
            longitude = lng;
            population = pop;
            this.capital = capital;

            if(province != null)
            {
                if (province.Equals("Québec"))
                {
                    this.province = "Quebec";
                }
                else
                {
                    this.province = province;
                }
            }
        }

        public string GetProvince()
        {
            return province;
        }
        public int GetPopulation()
        {
            return population;
        }
        public double[] GetLocation()
        {
            double[] latLon = new double[2];
            latLon[0] = latitude;
            latLon[1] = longitude;
            return latLon;
        }

    }
}
