using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClient
{
    class Apartment
    {
        public string City      { get; private set; }
        public string Hotel     { get; private set; }
        public int Stars        { get; private set; }
        public int Cost         { get; private set; }
        public int Places       { get; private set; }
        public int FreePlaces   { get; private set; }
        public int ApartNumber  { get; private set; }
        public string CloseDate { get; private set; }

        public Apartment() { }

        public Apartment(string pCity, string pHotel, int pStars, int pCost,
                            int pPlaces, int pFreePlaces, int pApartNum, string pCloseDate)
        {
            this.City = pCity;
            this.Hotel = pHotel;
            this.Stars = pStars;
            this.Cost = pCost;
            this.Places = pPlaces;
            this.FreePlaces = pFreePlaces;
            this.ApartNumber = pApartNum;
            this.CloseDate = pCloseDate;
        }
    }
}
