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
    public class AddressBLL
    {
        public ResultBM GetAddress(int addressId)
        {
            try
            {
                CountryBLL countryBll = new CountryBLL();
                AddressDAL addressDal = new AddressDAL();
                AddressDTO addressDto = addressDal.GetAddress(addressId);
                AddressBM addressBm = null;
                ResultBM resultCountry;

                // Si la dirección existe, el país debería existir porque de otro modo no podría haberse dado de alta.
                if (addressDto != null)
                {
                    resultCountry = countryBll.GetCountry(addressDto.countryIso);

                    if (resultCountry.IsValid())
                    {
                        if (resultCountry.GetValue() != null)
                            addressBm = new AddressBM(addressDto, resultCountry.GetValue<CountryBM>());
                        else
                            throw new Exception("El país " + addressDto.countryIso + "para la dirección " + addressId + " no existe.");
                    }
                    else
                        return resultCountry;

                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", addressBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar la dirección " + addressId + ".", exception);
            }
        }
    }
}
