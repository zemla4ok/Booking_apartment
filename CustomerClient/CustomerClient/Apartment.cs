using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClient
{
    class Apartment
    {
        public string City      { get; set; }
        public string Hotel     { get; set; }
        public int Stars        { get; set; }
        public int Cost         { get; set; }
        public int Places       { get; set; }
        public int FreePlaces   { get; set; }
        public int ApartNumber  { get; set; }

        public Apartment() { }

        public Apartment(string pCity, string pHotel, int pStars, int pCost,
                            int pPlaces, int pFreePlaces, int pApartNum)
        {
            this.City = pCity;
            this.Hotel = pHotel;
            this.Stars = pStars;
            this.Cost = pCost;
            this.Places = pPlaces;
            this.FreePlaces = pFreePlaces;
            this.ApartNumber = pApartNum;
        }
    }
}
