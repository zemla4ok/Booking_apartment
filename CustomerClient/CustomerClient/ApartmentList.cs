using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClient
{
    class ApartmentList
    {
        List<Apartment> lst = new List<Apartment>();

        public ApartmentList() { }

        public ApartmentList(List<Apartment> pList)
        {
            this.lst = pList;
        }

        public void AddApartment(Apartment pApartment)
        {
            this.lst.Add(pApartment);
        }

        public List<Apartment> GetApartments()
        {
            return this.lst;
        }
    }
}
