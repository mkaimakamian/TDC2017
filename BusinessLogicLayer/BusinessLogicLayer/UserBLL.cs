using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;
using BusinessModel;
using Helper;

namespace BusinessLogicLayer
{
    public class UserBLL : BLEntity
    {
        /// <summary>
        /// Devuelve una instancia del tipo UserMDL con los datos del usuario.
        /// Si el usuario no existe se produce una excepción.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public ResultBM GetUser(string user, string password)
        {
            UserDAL userDal = new UserDAL();
            UserDTO userDto = userDal.LogIn(user, SecurityHelper.Encrypt(password));            

            if (userDto == null)
            {
                //throw new Exception("Las credenciales ingresadas son inválidas.");
                return new ResultBM(ResultBM.Type.INVALID_CREDENTIAL, "Las credenciales ingresadas son inválidas.");
            }

            return new ResultBM(ResultBM.Type.OK, "Usuario encontrado: " + userDto.name, new UserBM(userDto)); 
        }

        /// <summary>
        /// Devuelve el listado de todos los usuarios.
        /// </summary>
        /// <returns></returns>
        public ResultBM GetUsers()
        {
            UserDAL userDal = new UserDAL();
            List<UserDTO> userDtos = userDal.GetUsers();
            List<UserBM> userBms = new List<UserBM>();

            foreach (UserDTO user in userDtos)
            {
                userBms.Add(new UserBM(user));
            }

            return new ResultBM(ResultBM.Type.OK, "Recuperación de los usuarios exitosa.", userBms);
        }

        public bool UpdateUser(UserBM userBm)
        {
            //TODO - Devolver resultBM
            UserDAL userDal = new UserDAL();
            DigitVerificatorBLL dvBll = new DigitVerificatorBLL();
            string digit = dvBll.CreateDigit(userBm);
            UserDTO userDto = new UserDTO(userBm.Id, userBm.Name, userBm.Active, userBm.LanguageId, userBm.PermissionId, userBm.Password, digit);
            bool result = userDal.UpdateUser(userDto);

            //Corregir: se asume que es solo para el usuario
            dvBll.UpdateVerticallDigit();
            return result;
        }

        public ResultBM CreateUser(UserBM userBm)
        {
            UserDAL userDal = new UserDAL();
            DigitVerificatorBLL dvBll = new DigitVerificatorBLL();
            userBm.Hdv = dvBll.CreateDigit(userBm);
            userBm.Password = SecurityHelper.Encrypt(userBm.Password);
            UserDTO userDto = new UserDTO(userBm.Name, userBm.Active, userBm.LanguageId, userBm.PermissionId, userBm.Password, userBm.Hdv);
            bool updated = userDal.SaveUser(userDto);

            if (updated)
            {
                ResultBM result = dvBll.UpdateVerticallDigit();
                if (result.IsValid())
                {
                    return new ResultBM(ResultBM.Type.OK, "Usuario creado: " + userDto.name, new UserBM(userDto));
                }
                else
                {
                    return new ResultBM(ResultBM.Type.EXCEPTION, "El usuario se creó pero el dígito verificador vertical, no.");
                }
                
            }
            else
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "El usuario no pudo crearse.");
            }
        }

        /// <summary>
        /// Actualiza el idioma del usuario en sesión, según el id del idioma pasado por parámetro.
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public bool ChangeCurrentLanguage(int languageId)
        {
            // Se recupera el usuario de la sesión para cambiarle el id, y luego utilizar el objeto para actualizar el dato en la base
            UserDAL userDal = new UserDAL();
            int userId = SessionHelper.GetLoggedUser().Id;            
            UserDTO userDto = userDal.GetUser(userId);

            UserBM userBm = new UserBM(userDto);

            int originalLanguage = userBm.LanguageId;
            userBm.LanguageId = languageId;

            //Actualización
            bool result = UpdateUser(userBm);

            if (result)
            {
                //Quizá debería manejarme con el modelo de lenguage
                LanguageBLL languageBll = new LanguageBLL();
                LanguageBM languageBm = languageBll.GetLanguage(languageId);
                SessionHelper.SetLanguage(languageBm);
            }
            else
            {
                userBm.LanguageId = originalLanguage;
                throw new Exception("El idioma no pudo actualizarse.");
            }

            return result;
        }

        public ResultBM DeleteUser(int userId)
        {
            try
            {
                DigitVerificatorBLL dvBll = new DigitVerificatorBLL();
                UserDAL userDal = new UserDAL();
                bool isDeleted = userDal.DeleteUser(userId);

                if (isDeleted)
                {
                    ResultBM result = dvBll.UpdateVerticallDigit();
                    if (result.IsValid())
                    {
                        return new ResultBM(ResultBM.Type.OK, "Usuario con id " + userId + " ha sido eliminado satisfactoriamente.");
                    }
                    else
                    {
                        return new ResultBM(ResultBM.Type.FAIL, "Error al generar el registro de integridad para la entidad users.");
                    }
                }
                else
                {
                    return new ResultBM(ResultBM.Type.FAIL, "Error al borrar al usuario con id " + userId);
                }                
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido una excepción al intentar borrar al usuario con id " + userId + ". " + exception.Message);
            }
        }

        public ResultBM GetCollection()
        {
            return GetUsers();
        }


        public ResultBM Delete(object entity)
        {
            return DeleteUser(((UserBM)entity).Id);
        }
    }
}
