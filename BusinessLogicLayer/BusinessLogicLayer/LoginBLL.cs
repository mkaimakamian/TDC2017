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
        public ResultBM LogIn(string user, string password)
        {
            UserBLL userBll = new UserBLL();
            UserBM userBm;
            
            LanguageBLL languageBll = new LanguageBLL();
            LanguageBM languageBm;

            ProfileBLL profileBll = new ProfileBLL();
            ProfileBM profileMdl;

            DigitVerificatorBLL dvBll = new DigitVerificatorBLL();

            ResultBM result;

            try
            {
                //1. Validación input
                result = IsValid(user, password);

                if (result.IsValid())
                {
                    //2. Validación usuario
                    result = userBll.GetUser(user, password);

                    if (result.IsValid())
                    {
                        userBm = result.GetValue<UserBM>();

                        // userBm.IsLocked()                

                        //3.1 Chequeo de consistencia horizontal
                        result = dvBll.IsHorizontallyConsistent(userBm);

                        if (result.IsValid())
                        {
                            //3.2 Chequeo de vertical
                            result = dvBll.IsVerticallyConsistent();
                            
                            if (result.IsValid())
                            {
                                //4. Recuperación de idioma
                                languageBm = languageBll.GetLanguage(userBm.LanguageId);

                                //5. Recuperación de permisos
                                profileMdl = profileBll.GetProfile(userBm.PermissionId).GetValue() as ProfileBM;

                                //6. Armado de sesión
                                SessionHelper.StartSession(userBm, profileMdl, languageBm);
                                result = new ResultBM(ResultBM.Type.OK, "Inicio de sesión exitoso para el usuario " + user);
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                result = new ResultBM(ResultBM.Type.EXCEPTION, exception.Message);
            }

            return result;
        }

        /// <summary>
        /// Valida que los datos ingresados cumplan con los requisitos mínimos de validación.
        /// Si no se cumplen los requisitos, se produce una excepción.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        private ResultBM IsValid(String user, String password)
        {
            if (user.Length == 0 || password.Length == 0)
            {
                return new ResultBM(ResultBM.Type.INCOMPLETE_FIELDS, "Deben completarse todos los campos.");
            } else {
                return new ResultBM(ResultBM.Type.OK);
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
