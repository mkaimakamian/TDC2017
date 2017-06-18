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

        public ResultBM GetVolunteer(int volunteerId)
        {
            try {
                AddressBLL addressBll = new AddressBLL();
                ResultBM addressResult = null;
                AddressBM addressBm = null;
                BranchBLL branchBll = new BranchBLL();
                ResultBM branchResult = null;
                BranchBM branchBm = null;
                UserBLL userBll = new UserBLL();
                ResultBM userResult = null;
                UserBM userBm = null;
                VolunteerDAL volunteerDal = new VolunteerDAL();                
                VolunteerBM volunteerBm = null;
                VolunteerDTO volunteerDto = volunteerDal.GetVolunteer(volunteerId);                

                if (volunteerDto != null)
                {
                    //Debería existir
                    addressResult = addressBll.GetAddress(volunteerDto.addressId);
                    if (!addressResult.IsValid()) return addressResult;
                    if (addressResult.GetValue() == null) throw new Exception("La dirección " + volunteerDto.addressId + " para el voluntario " + volunteerId + " no existe.");
                    addressBm = addressResult.GetValue<AddressBM>();

                    branchResult = branchBll.GetBranch(volunteerDto.branchId);
                    if (!branchResult.IsValid()) return branchResult;
                    if (branchResult.GetValue() == null) throw new Exception("La sede " + volunteerDto.branchId + " para el voluntario " + volunteerId + " no existe.");
                    branchBm = branchResult.GetValue<BranchBM>();

                    //El usuario podría no existir porque el voluntario no requiere necesariamente que se lo asocie 
                    //con un susuario de sistema
                    userResult = userBll.GetUser(volunteerDto.userId);
                    if (!userResult.IsValid()) return userResult;

                    if (userResult.GetValue() != null) userBm = userResult.GetValue<UserBM>();

                    volunteerBm = new VolunteerBM(volunteerDto, addressBm, branchBm, userBm);
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", volunteerBm);
                
            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el voluntario " + volunteerId + ".", exception);
            }
        }


        public ResultBM GetVolunteers()
        {
            try
            {
                VolunteerDAL volunteerDal = new VolunteerDAL();
                List<VolunteerDTO> volunteersDto = volunteerDal.GetVolunteers();
                List<VolunteerBM> volunteersBm = ConvertIntoBusinessModel(volunteersDto);
                return new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", volunteersBm);
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar los donadores.", exception);
            }
        }

        public ResultBM SaveVolunteer(VolunteerBM volunteerBm)
        {
            try
            {
                PersonBLL personBll = new PersonBLL();
                PersonBM personBm = null;
                ResultBM personResult;                
                VolunteerDAL volunteerDal = new VolunteerDAL();
                VolunteerDTO volunteerDto = null;

                ResultBM validResult = IsValid(volunteerBm);

                if (validResult.IsValid())
                {
                    personResult = personBll.SavePerson(volunteerBm);

                    if (personResult.IsValid())
                    {
                        personBm = personResult.GetValue() as PersonBM;
                        volunteerDto = new VolunteerDTO(personBm.id, volunteerBm.branch.id, volunteerBm.user == null ? 0 : volunteerBm.user.Id);
                        volunteerDal.SaveVolunteer(volunteerDto);
                        volunteerBm.volunteerId = volunteerDto.volunteerId;

                        return new ResultBM(ResultBM.Type.OK, "Voluntario guardado.", volunteerBm);
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
            if (volunteerBm.branch == null)
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Debe especificar sede.");

            return new ResultBM(ResultBM.Type.OK);
        }

        private List<VolunteerBM> ConvertIntoBusinessModel(List<VolunteerDTO> volunteers)
        {
            List<VolunteerBM> result = new List<VolunteerBM>();
            foreach (VolunteerDTO volunteer in volunteers)
            {
                result.Add(new VolunteerBM(volunteer));
            }
            return result;
        }

    }
}
