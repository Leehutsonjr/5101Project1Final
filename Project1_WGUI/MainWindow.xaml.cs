using Project1_WGUI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project1_WGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool _start { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        Statistics _statistics;
        
        CityInfo _cityInfo = new CityInfo();

        public string _instructions = "For Province Info: Please select a province and click \"City Info\" \n" +
                                     "For City Info: Please select a province and a city and click \"City Info\"\n" +
                                       "For Rank Info: Please make a selection and click \"Rank\"\n" +
                                        "To Compare populations: Please enter two cities and click \"Compare\"\n" +
                                        "To calculate distance: Please enter two cities and press \"Calculate\"\n" +
                                           "To show city on a map: Please enter a province and a city and click \"Show City on Map\"\n" +
                                        "To reset tool to default: Please click \"Reset\"\n" ;

        //Clear all fields
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            parseProvince.Text = "Province";
            parseCity.Text = "City";
            compareCity1.Text = "City #1";
            compareCity2.Text = "City #2";
            distCity1.Text = "City #1";
            distCity2.Text = "City #2";
            display.Text = "Please Select a parsing option and click \"Parse\"";
            _start = false;
        }

        //Parse CSV
        private void Parse_Click(object sender, RoutedEventArgs e)
        {
            //textbox text
            var province = parseProvince.Text;
            var city = parseCity.Text;

            switch(selection.Text)
            {
                case "CSV":
                    _statistics = new Statistics("..\\Data\\Canadacities.csv", "CSV");
                    _start = true;
                    display.Text = "The CSV file has been loaded.\n\n";
                    display.Text += _instructions;
                    break;
                case "JSON":
                    _statistics = new Statistics("..\\Data\\Canadacities-JSON.json", "JSON"); 
                    _start = true;
                    display.Text = "The JSON file has been loaded.\n\n";
                    display.Text += _instructions;
                    break;
                case "XML":
                    _statistics = new Statistics("..\\Data\\Canadacities-XML.xml", "XML");
                    _start = true;
                    display.Text = "The XML file has been loaded.\n\n";
                    display.Text += _instructions;
                    break;
                default:
                    display.Text = "Please try again.\n\n";
                    display.Text += "Please Select a parsing option and click \"Parse\"";
                    break;
            }
        }

        private void ProvInfo_Click(object sender, RoutedEventArgs e)
        {
            //Store province name in variable
            var province = parseProvince.Text;

            //initialize string for display
            string provinceInfo = "";

            if(_start)
            {
                var cities = _statistics.DisplayProvinceCities(province);

                if (cities.Count != 0)
                {
                    provinceInfo = $"Province Information:\n " +
                    $"Name: {province}\n " +
                    $"Population: {_statistics.DisplayProvincePopulation(province)}\n " +
                    $"Capitol: {_statistics.GetCapital(province)}\n" +
                    $"Cities: \n " +
                    $"----------------------------------------------\n";

                    foreach (var p in cities)
                    {
                        provinceInfo += $"{p.cityName} \n";
                    }
                }
                else
                {
                    provinceInfo = "Incorrect Province name please try again.\n\n" + _instructions;
                }
                display.Text = provinceInfo;
            }
            else
            {
                display.Text = "Error. \nPlease Select a parsing option and click \"Parse\"";
            }
        }

        private void CityInfo_Click(object sender, RoutedEventArgs e)
        {
            //textbox text
            var province = parseProvince.Text;
            var city = parseCity.Text;

            string cityInformation = "";

            if (_start)
            {
                var cityInfo = _statistics.DisplayCityInformation(city);

                if (cityInfo.Count != 0)
                {
                    foreach (var c in cityInfo)
                    {
                        //Location
                        var longitude = c.GetLocation()[0];

                        cityInformation += $"{c.cityName}:\n" +
                            $"----------------\n " +
                            $"Population: {c.GetPopulation()}\n " +
                            $"Location:\n" +
                            $"Longitude: {c.GetLocation()[0]}\n" +
                            $"Latitude: {c.GetLocation()[1]}\n " +
                            $"Largest City: " +
                            $"{_statistics.DisplayLargestPopulationCity(province).cityName}\n " +
                            $"Smallest City: {_statistics.DisplaySmallestPopulationCity(province).cityName}\n";
                    }
                }
                else
                {
                    cityInformation = "Incorrect Province and City name please try again.\n\n" + _instructions;
                }

                display.Text = cityInformation;
            }
            else
            {
                display.Text = "Error. \nPlease Select a parsing option and click \"Parse\"";
            }
        }

        private void Rank_Click(object sender, RoutedEventArgs e)
        {
            List<Ranking> ranking; 
            if (_start)
            {
                display.Text = "";

                switch (rankSel.Text)
                {
                    case "Cities":
                        ranking = _statistics.RankProvincesByCities();

                        display.Text = "Ranking by Cities:\n\n";

                        foreach(var r in ranking)
                        {
                            display.Text += $"{r.name} - Population: {r.pop}\n";
                        }
                        break;
                    case "Population":
                        ranking = _statistics.RankProvincesByPopulation();

                        display.Text = "Ranking by Population:\n\n";

                        foreach (var r in ranking)
                        {
                            display.Text += $"{r.name} - Population: {r.pop}\n";
                        }
                        break;
                    default:
                        display.Text = "Please try again.\n\n";
                        display.Text += _instructions;
                        break;
                }
            }
            else
            {
                display.Text = "Error. \nPlease Select a parsing option and click \"Parse\"";
            }

        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            //textbox text
            var city1 = compareCity1.Text;
            var city2 = compareCity2.Text;

            CityPopComparison cityPopComparison;
            

            if (_start)
            {
                //Convert to city info
                var cityInfo1 = _statistics.DisplayCityInformation(city1);
                var cityInfo2 = _statistics.DisplayCityInformation(city2);

                CityInfo item1 = cityInfo1[0];
                CityInfo item2 = cityInfo2[0];

                cityPopComparison = _statistics.CompareCitiesPopulation(item1, item2);
                display.Text = $"The city with the largest population is: {cityPopComparison.city.cityName}\n " +
                    $"City: {cityPopComparison.city.cityName} - Population: {cityPopComparison.largerPop}\n " +
                    $"City:{cityPopComparison.city2.cityName} - Population: {cityPopComparison.smallerpop}";
            }
            else
            {
                display.Text = "Error. \nPlease Select a parsing option and click \"Parse\"";
            }


            //TODO
            //display.Text = _statistics.CompareCitiesPopulation(city1, city2);
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            //textbox text
            var city1 = distCity1.Text;
            var city2 = distCity2.Text;

            double results = 0;

            if (_start)
            {
                var cityInfo1 = _statistics.DisplayCityInformation(city1);
                var cityInfo2 = _statistics.DisplayCityInformation(city2);

                CityInfo item1 = cityInfo1[0];
                CityInfo item2 = cityInfo2[0];

                

                results = _statistics.CalculateDistanceBetweenCities(item1, item2);
                display.Text = $"The distance between {item1.cityName} and {item2.cityName} is {(int)results} KM.";
            }
            else
            {
                display.Text = "Error. \nPlease Select a parsing option and click \"Parse\"";
            }
        }

        private void ShowCity_Click(object sender, RoutedEventArgs e)
        {
            if (_start)
            {
                var province = parseProvince.Text;
                var city = parseCity.Text;

                _statistics.ShowCityOnMap(city, province);
                display.Text = _instructions;
            }
            else
            {
                display.Text = "Error. \nPlease Select a parsing option and click \"Parse\"";
            }
        }
    }
}
