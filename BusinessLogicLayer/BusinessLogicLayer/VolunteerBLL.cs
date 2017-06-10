using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class VolunteerBLL
    {
        public ResultBM SaveVolunteer(VolunteerBM volunteerBm)
        {
            try
            {
                //AddressDAL addressDal = new AddressDAL();
                //AddressDTO addressDto = null;
                PersonBLL personBll = new PersonBLL();
                PersonBM personBm = null;
                ResultBM personResult;
                ResultBM validResult = IsValid(volunteerBm);
                VolunteerDAL volunteerDal = new VolunteerDAL();
                VolunteerDTO volunteerDto = null;

                if (validResult.IsValid())
                {
                    personResult = personBll.SavePerson(volunteerBm);

                    if (personResult.IsValid())
                    {

                        personBm = personResult.GetValue() as PersonBM;
                        volunteerDto = new VolunteerDTO(personBm.id, personBm.address.id, donorBm.organization.id, donorBm.canBeContacted);
                        volunteerDal.SaveVolunteer(volunteerDto);
                        volunteerBm.volunteerId = volunteerDto.volunteerId;
                        
                        //Guardar la sede
                        //Si tiene usuario asignado, guardarlo
                        //return new ResultBM(ResultBM.Type.OK, "Dirección guardada.", addressBm);
                    }
                    else {
                        return personResult;
                    }                    
                }
                else
                {
                    return validResult;
                }
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al guardar la dirección.", exception);
            }
        }

        private ResultBM IsValid(VolunteerBM volunteerBm)
        {
            return new ResultBM(ResultBM.Type.OK);
        }

    }
}
