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

        public ResultBM UpdateUser(UserBM userBm, bool updatePassword=false)
        {
            UserDAL userDal = new UserDAL();
            DigitVerificatorBLL dvBll = new DigitVerificatorBLL();
            ResultBM digitUpdated;
            ResultBM validation;
            UserDTO userDto;

            try
            {
                validation = IsValid(userBm);
                if (validation.IsValid())
                {
                    if (updatePassword)
                    {
                        userBm.Password = SecurityHelper.Encrypt(userBm.Password);
                    }
                    string digit = dvBll.CreateDigit(userBm);
                    userDto = new UserDTO(userBm.Id, userBm.Name, userBm.Active, userBm.LanguageId, userBm.PermissionId, userBm.Password, digit);
                    userDal.UpdateUser(userDto);

                    //Corregir: se asume que es solo para el usuario
                    //Ver qué ocurre ante fallo
                    digitUpdated = dvBll.UpdateVerticallDigit();

                    if (digitUpdated.IsValid())
                    {
                        return new ResultBM(ResultBM.Type.OK, "Usuario con id " + userBm.Id + " actualizado correctamente.");
                    }
                    else
                    {
                        return digitUpdated;
                    }
                }
                else
                {
                    return validation;
                }
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al actualizar los datos del usuario con id " + userBm.Id + ".", exception);
            }
        }

        /// <summary>
        /// Crea un usuario.
        /// </summary>
        /// <param name="userBm"></param>
        /// <returns></returns>
        public ResultBM CreateUser(UserBM userBm)
        {
            UserDAL userDal = new UserDAL();
            DigitVerificatorBLL dvBll = new DigitVerificatorBLL();
            UserDTO userDto;
            ResultBM digitUpdated;
            ResultBM validation;

            try
            {
                validation = IsValid(userBm);
                if (validation.IsValid())
                {
                    userBm.Password = SecurityHelper.Encrypt(userBm.Password);
                    userBm.Hdv = dvBll.CreateDigit(userBm);                    
                    userDto = new UserDTO(userBm.Name, userBm.Active, userBm.LanguageId, userBm.PermissionId, userBm.Password, userBm.Hdv);
                    userDal.SaveUser(userDto);

                    digitUpdated = dvBll.UpdateVerticallDigit();

                    if (digitUpdated.IsValid())
                    {
                        return new ResultBM(ResultBM.Type.OK, "Usuario creado: " + userDto.name, new UserBM(userDto));
                    }
                    else
                    {
                        return digitUpdated;
                    }

                }
                else
                {
                    return validation;
                }
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al crear el usuario con " + userBm.Name + ".", exception);
            }
        }

        /// <summary>
        /// Actualiza el idioma del usuario en sesión, según el id del idioma pasado por parámetro.
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public ResultBM ChangeCurrentLanguage(int languageId)
        {
            // Se recupera el usuario de la sesión para cambiarle el id del idioma
            UserDAL userDal = new UserDAL();
            UserDTO userDto = userDal.GetUser(SessionHelper.GetLoggedUser().Id);
            UserBM userBm = new UserBM(userDto);
            ResultBM updateResult;

            try
            {
                int originalLanguage = userBm.LanguageId;
                userBm.LanguageId = languageId;

                updateResult = UpdateUser(userBm);

                if (updateResult.IsValid())
                {
                    //Quizá debería manejarme con el modelo de lenguage
                    LanguageBLL languageBll = new LanguageBLL();
                    LanguageBM languageBm = languageBll.GetLanguage(languageId);
                    SessionHelper.SetLanguage(languageBm);
                    SessionHelper.GetLoggedUser().LanguageId = languageId;
                }
                else
                {
                    userBm.LanguageId = originalLanguage;
                }

                return updateResult;
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido un error al actualizar el idioma.", exception);
            }
        }

        public ResultBM DeleteUser(int userId)
        {
            try
            {
                DigitVerificatorBLL dvBll = new DigitVerificatorBLL();
                UserDAL userDal = new UserDAL();
                userDal.DeleteUser(userId);

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
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, "Se ha producido una excepción al intentar borrar al usuario con id " + userId + ". " + exception.Message);
            }
        }

        /// <summary>
        /// Controla que se cumplan las condiciones para poder operar.
        /// </summary>
        /// <param name="userBM"></param>
        private ResultBM IsValid(UserBM userBM) {
            if (userBM.Name.Length == 0 || userBM.Password.Length == 0) {
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Todos los campos deben ser completados.");
            }
            return new ResultBM(ResultBM.Type.OK);
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
