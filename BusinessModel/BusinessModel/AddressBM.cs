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

        public AddressBM(AddressDTO addressDto, CountryBM countrBm)
        {
            this.id = addressDto.id;
            this.street = addressDto.street;
            this.number = addressDto.number;
            this.apartment = addressDto.apartment;
            this.neighborhood = addressDto.neighborhood;
            this.comment = addressDto.comment;
            this.country = countrBm;
        }
    }
}
