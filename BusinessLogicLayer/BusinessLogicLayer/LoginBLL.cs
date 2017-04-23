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
            UserMDL userMdl;
            List<TranslationMDL> translations;
            UserBLL userBll = new UserBLL();
            TranslationBLL translationBll = new TranslationBLL();
            ProfileBLL profileBll = new ProfileBLL();
            ProfileMDL profileMdl;

            try
            {
                ValidateInput(user, password);
                userMdl = userBll.GetUser(user, password);
                //TODO - 1. Chequeo de consistencia                    
                translations = translationBll.GetTranslations(userMdl.languageId);                
                profileMdl = profileBll.GetProfile(userMdl.permissionId);
                SessionHelper.StartSession(userMdl, profileMdl, translations);
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
