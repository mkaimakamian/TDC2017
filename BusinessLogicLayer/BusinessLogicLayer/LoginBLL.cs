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
    public class LoginBLL
    {
        public void LogIn(string user, string password)
        {
            UserBLL userBll = new UserBLL();
            UserBM userBm;
            
            LanguageBLL languageBll = new LanguageBLL();
            LanguageBM languageBm;

            ProfileBLL profileBll = new ProfileBLL();
            ProfileBM profileMdl;

            DigitVerificatorBLL dvBll = new DigitVerificatorBLL();           

            try
            {
                //1. Validación input
                ValidateInput(user, password);
                
                //2. Validación usuario
                userBm = userBll.GetUser(user, password);

                // userBm.IsLocked()                

                //3.1 Chequeo de consistencia horizontal
                if (!dvBll.IsHorizontallyConsistent(userBm))
                {
                    // Exception dentro de un catch??
                    throw new Exception("El usuario ha sido alterado.");
                }

                //3.2 Chequeo de vertical
                if (!dvBll.IsVerticallyConsistent())
                {
                    // Exception dentro de un catch??
                    throw new Exception("Al menos una de las tablas ha sido manipulada.");
                }

                //4. Recuperación de idioma
                languageBm = languageBll.GetLanguage(userBm.languageId);
                
                //5. Recuperación de permisos
                profileMdl = profileBll.GetProfile(userBm.permissionId);

                //6. Armado de sesión
                SessionHelper.StartSession(userBm, profileMdl, languageBm);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Valida que los datos ingresados cumplan con los requisitos mínimos de validación.
        /// Si no se cumplen los requisitos, se produce una excepción.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public void ValidateInput(String user, String password)
        {
            if (user.Length == 0 || password.Length == 0)
            {
                throw new Exception("Todos los campos deben ser completados.");
            }
        }       

        /// <summary>
        /// Finaliza la sesión.
        /// </summary>
        public void Logout()
        {
            SessionHelper.EndSession();
        }

    }
}
