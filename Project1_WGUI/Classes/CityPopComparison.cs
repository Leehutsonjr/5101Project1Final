namespace Project1_WGUI.Classes
{
    //Class to allow return of a city object AND two population values for requirement 3.c.iv.
    public class CityPopComparison
    {
        public CityInfo city;
        public CityInfo city2;
        public int largerPop;
        public int smallerpop;
        public CityPopComparison(CityInfo city, CityInfo city2, int large, int small)
        {
            this.city = city;
            this.city2 = city2;
            largerPop = large;
            smallerpop = small;
        }
    }


}
