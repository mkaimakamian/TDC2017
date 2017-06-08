using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class AddressDTO
    {
        public int id;
        public string street;
        public int number;
        public string apartment;
        public string neighborhood;
        public string comment;
        public string countryIso;

        public AddressDTO() { }

        public AddressDTO(int id, string street, int number, string apartment, string neighborhood, string comment, string countryIso2)
        {
            this.id = id;
            this.street = street;
            this.number = number;
            this.apartment = apartment;
            this.neighborhood = neighborhood;
            this.comment = comment;
            this.countryIso = countryIso2;
        }
    }
}
