using System.Data.Entity;

namespace Wines.Models
{
    public class Wine
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public string WineByRegion { get; set; }
        public string Grape1 { get; set; }
        public string WineByStopper { get; set; }
        public string WineByName { get; set; }
        public int Vintage { get; set; }
        public string Producer { get; set; }
        public decimal ABV { get; set; }
        public string GoWellWith { get; set; }
        public string TestingNote { get; set; }

        public string WineImage { get; set; }
        public string FoodImage { get; set; }
        public string RegionImage { get; set; }
        public string CountryImage { get; set; }


    }

   
}