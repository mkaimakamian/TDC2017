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
                PersonBLL personBll = new PersonBLL();
                ResultBM personResult = null;
                BranchBLL branchBll = new BranchBLL();
                ResultBM branchResult = null;
                UserBLL userBll = new UserBLL();
                ResultBM userResult = null;
                UserBM userBm = null;
                VolunteerDAL volunteerDal = new VolunteerDAL();                
                VolunteerBM volunteerBm = null;
                VolunteerDTO volunteerDto = volunteerDal.GetVolunteer(volunteerId);
                
                if (volunteerDto != null)
                {

                    personResult = personBll.GetPerson(volunteerDto.id);

                    if (personResult.IsValid())
                    {
                        if (personResult.GetValue() != null)
                        {
                            branchResult = branchBll.GetBranch(volunteerDto.branchId);

                            if (branchResult.IsValid())
                            {
                                if (branchResult.GetValue() != null)
                                {
                                    //El usuario podría no existir porque el voluntario no requiere necesariamente que se lo asocie 
                                    //con un susuario de sistema
                                    userResult = userBll.GetUser(volunteerDto.userId);
                                    
                                    if (userResult.IsValid())
                                    {
                                        if (userResult.GetValue() != null) 
                                            userBm = userResult.GetValue<UserBM>();
                                    }
                                    else
                                        return userResult;

                                    volunteerBm = new VolunteerBM(volunteerDto, personResult.GetValue<PersonBM>(), branchResult.GetValue<BranchBM>(), userBm);
                                }
                                else
                                    throw new Exception("El brach " + volunteerDto.branchId + " para el voluntario " + volunteerId + " no existe.");
                            }
                            else
                                return branchResult;
                        }
                        else
                            throw new Exception("La persona " + volunteerDto.id + " para el voluntario " + volunteerId + " no existe.");
                    }
                    else
                        return personResult;
                }

                return new ResultBM(ResultBM.Type.OK, "Operación exitosa.", volunteerBm);
                
            }
            catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al recuperar el voluntario " + volunteerId + ".", exception);
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

    }
}
