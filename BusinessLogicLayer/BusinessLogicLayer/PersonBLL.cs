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
    public class PersonBLL
    {

        public ResultBM GetPerson(int personId)
        {
            try
            {
                AddressBLL addressBll = new AddressBLL();
                ResultBM resultAddress = null;
                PersonDAL personDal = new PersonDAL();
                PersonBM personBm = null;                
                PersonDTO personDto = personDal.GetPerson(personId);

                // Si la persona existe, debería existir la dirección.
                if (personDto != null)
                {
                    resultAddress = addressBll.GetAddress(personDto.addressId);

                    if (resultAddress.IsValid())
                    {
                        if (resultAddress.GetValue() != null)
                            personBm = new PersonBM(personDto, resultAddress.GetValue<AddressBM>());
                        else
                            throw new Exception("La dirección " + personDto.addressId + "para la persona " + personId + " no existe.");
                    }
                    else
                        return resultAddress;
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", personBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar la persona " + personId + ".", exception);
            }
        }



        //public ResultBM CreatePerson(PersonBM personBm)
        //{
        //    PersonDAL personDal = new PersonDAL();
        //    PersonDTO personDto;
        //    ResultBM validationResult;

        //    try
        //    {
        //        validationResult = IsValid(personBm);

        //        if (validationResult.IsValid())
        //        {
        //            //guardar direccion

        //            personDto = new PersonDTO(personBm.name, personBm.lastName, personBm.birthdate, personBm.email, personBm.phone, personBm.gender, personBm.dni, personBm.addressId);
        //            personDal.SavePerson(personDto);

        //            return new ResultBM(ResultBM.Type.OK, "Se ha creado la persona con el nombre " + personBm.name + " " + personBm.lastName + ".");
        //        }
        //        else
        //        {
        //            return validationResult;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear al donador.", exception);
        //    }
        //}

        //private ResultBM IsValid(PersonBM personBm)
        //{
        //    //TODO perform validation
        //    return new ResultBM(ResultBM.Type.OK);
        //}
    }
}
