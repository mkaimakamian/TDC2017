using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class AddressBM
    {
        public int id;
        public string street;
        public int number;
        public string apartment;
        public string neighborhood;
        public string comment;
        public CountryBM country;

        public AddressBM(AddressDTO addressDto, CountryBM countryBm)
        {
            this.id = addressDto.id;
            this.street = addressDto.street;
            this.number = addressDto.number;
            this.apartment = addressDto.apartment;
            this.neighborhood = addressDto.neighborhood;
            this.comment = addressDto.comment;
            this.country = countryBm;
        }

        public AddressBM(string street, int number, string apartment, string neighborhood, string comment, CountryBM countryBm)
        {
            this.street = street;
            this.number = number;
            this.apartment = apartment;
            this.neighborhood = neighborhood;
            this.comment = comment;
            this.country = countryBm;
        }
    }
}
