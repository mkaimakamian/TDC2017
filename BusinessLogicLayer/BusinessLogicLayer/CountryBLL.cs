using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObject;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class CountryBLL
    {
        /// <summary>
        /// Devuelve el país para el iso2 requerido
        /// </summary>
        /// <param name="iso2"></param>
        /// <returns></returns>
        public ResultBM GetCountry(string iso2)
        {
            try {
                CountryDAL countryDal = new CountryDAL();
                CountryDTO countryDto = countryDal.GetCountry(iso2);
                CountryBM countryBm = null;

                if (countryDto != null)
                    countryBm = new CountryBM(countryDto);

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", countryBm);
            }
            catch (Exception exception) {
                    return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el país " + iso2 + ".", exception);
            }
        }

        public ResultBM GetCountries()
        {
            try
            {
                CountryDAL countryDal = new CountryDAL();
                List<CountryDTO> countriesDto = countryDal.GetCountries();
                List<CountryBM> countriesBm = ConvertIntoBusinessModel(countriesDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", countriesBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar los países.", exception);
            }
        }

        private List<CountryBM> ConvertIntoBusinessModel(List<CountryDTO> countries)
        {
            List<CountryBM> result = new List<CountryBM>();
            foreach (CountryDTO country in countries)
            {
                result.Add(new CountryBM(country));
            }
            return result;
        }
    }
}
