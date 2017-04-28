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
    public class UserBLL
    {
        /// <summary>
        /// Devuelve una instancia del tipo UserMDL con los datos del usuario.
        /// Si el usuario no existe se produce una excepción.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public UserBM GetUser(string user, string password)
        {
            UserDAL userDal = new UserDAL();
            UserDTO userDto = userDal.LogIn(user, SecurityHelper.Encrypt(password));

            if (userDto == null)
            {
                throw new Exception("Las credenciales ingresadas son inválidas.");
                
            }
            return new UserBM(userDto);
        }

        public bool UpdateUser(UserBM userBm)
        {
            UserDAL userDal = new UserDAL();
            UserDTO userDto = new UserDTO(userBm.id, userBm.name, userBm.active, userBm.languageId, userBm.permissionId);
            bool result = userDal.UpdateUser(userDto);
            return result;

        }

        public bool ChangeCurrentLanguage(int languageId)
        {
            // Se recupera el usuario de la sesión para cambiarle el id, y luego utilizar el objeto para actualizar el dato en la base
            UserBM userBm = SessionHelper.GetLoggedUser();
            int originalLanguage = userBm.languageId;
            userBm.languageId = languageId;

            //Actualización
            bool result = UpdateUser(userBm);

            if (result)
            {
                //Quizá debería manejarme con el modelo de lenguage
                LanguageBLL languageBll = new LanguageBLL();
                LanguageBM languageBm = languageBll.GetLanguage(languageId);

                //TODO - El idioma y las traducciones deberían ser competencia del business model del usuario
                SessionHelper.SetLanguage(languageBm);
            }
            else
            {
                userBm.languageId = originalLanguage;
                throw new Exception("El idioma no pudo actualizarse.");
            }

            return result;
        }
    }
}
