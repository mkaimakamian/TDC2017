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
        /// <summary>
        /// Devuelve la dirección según el id informado por parámetro
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
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

                    if (!resultCountry.IsValid()) return resultCountry;
                    if (resultCountry.GetValue() != null) addressBm = new AddressBM(addressDto, resultCountry.GetValue<CountryBM>());
                    else throw new Exception("El país " + addressDto.countryIso + "para la dirección " + addressId + " no existe.");
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", addressBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar la dirección " + addressId + ".", exception);
            }
        }

        public ResultBM SaveAddress(AddressBM addressBm)
        {
            try
            {
                AddressDAL addressDal = new AddressDAL();
                AddressDTO addressDto = null;
                ResultBM validResult = IsValid(addressBm);

                if (!validResult.IsValid()) return validResult;
                addressDto = new AddressDTO(addressBm.id, addressBm.street, addressBm.number, addressBm.apartment, addressBm.neighborhood, addressBm.comment, addressBm.country.iso2);
                addressDal.SaveAddress(addressDto);
                addressBm.id = addressDto.id;

                return new ResultBM(ResultBM.Type.OK, "Dirección guardada.", addressBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al guardar la dirección.", exception);
            }
        }
        
        public ResultBM UpdateAddress(AddressBM addressBm)
        {
            try
            {
                AddressDAL addressDal = new AddressDAL();
                AddressDTO addressDto = null;
                ResultBM validResult = IsValid(addressBm);

                if (!validResult.IsValid()) return validResult;
                addressDto = new AddressDTO(addressBm.id, addressBm.street, addressBm.number, addressBm.apartment, addressBm.neighborhood, addressBm.comment, addressBm.country.iso2);
                addressDal.UpdateAddress(addressDto);

                return new ResultBM(ResultBM.Type.OK, "Dirección actualizada.", addressBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al guardar la dirección.", exception);
            }
        }


        private ResultBM IsValid(AddressBM addressBm)
        {
            if (addressBm.street == null || addressBm.street.Length == 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe completarse la dirección");

            if (addressBm.number < 0)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe completarse el número de calle");

            if (addressBm.country == null)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe completarse el país");

            return new ResultBM(ResultBM.Type.OK);
        }

    }
}
