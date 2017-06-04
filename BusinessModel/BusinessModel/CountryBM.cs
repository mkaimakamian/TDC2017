using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class CountryBM
    {
        public string iso2;
        public string iso3;
        public string name;

        public CountryBM(CountryDTO countryDto)
        {
            this.iso2 = countryDto.iso2;
            this.iso3 = countryDto.iso3;
            this.name = countryDto.name;
        }
    }
}
