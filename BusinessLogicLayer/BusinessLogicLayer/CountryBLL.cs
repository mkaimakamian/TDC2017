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
    }
}
