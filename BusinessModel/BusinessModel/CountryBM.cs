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
        private string name;

        public CountryBM() { }

        public CountryBM(CountryDTO countryDto)
        {
            this.iso2 = countryDto.iso2;
            this.iso3 = countryDto.iso3;
            this.name = countryDto.name;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
