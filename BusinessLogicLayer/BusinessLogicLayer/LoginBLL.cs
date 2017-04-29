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
            UserBM userMdl;
            LanguageBM languageBm;
            UserBLL userBll = new UserBLL();
            LanguageBLL languageBll = new LanguageBLL();
            ProfileBLL profileBll = new ProfileBLL();
            ProfileBM profileMdl;
            bool consistentOk;

            try
            {
                //1. Validación input
                ValidateInput(user, password);
                
                //2. Validación usuario
                userMdl = userBll.GetUser(user, password);
                
                //3.1 Chequeo de consistencia horizontal
                consistentOk = SecurityHelper.IsEquivalent(userMdl.GetSeed(), userMdl.GetDigit());

                if (!consistentOk)
                {
                    // Exception dentro de un catch??
                    throw new Exception("El usuario ha sido alterado.");
                }

                //3.2 Chequeo de horizontal

                //TODO - armar un bll que me provea de los servicios de checkeo

                //4. Recuperación de idioma
                languageBm = languageBll.GetLanguage(userMdl.languageId);
                
                //5. Recuperación de permisos
                profileMdl = profileBll.GetProfile(userMdl.permissionId);

                //6. Armado de sesión
                SessionHelper.StartSession(userMdl, profileMdl, languageBm);
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
