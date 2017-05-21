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
            ProfileBM profileBm;

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

                        //3. Recuperación de permisos
                        profileBm = profileBll.GetProfile(userBm.PermissionId).GetValue() as ProfileBM;

                        //4. Recuperación de idioma
                        languageBm = languageBll.GetLanguage(userBm.LanguageId);

                        //5. Armado de sesión
                        SessionHelper.StartSession(userBm, profileBm, languageBm);

                        //6 Chequeo de consistencia horizontal
                        result = dvBll.IsHorizontallyConsistent(userBm);

                        if (!result.IsValid())
                        {
                            //Sólo un admin puede continuar no es suficiente con que pueda restaurar
                            result = new ResultBM(ResultBM.Type.CORRUPTED_DATABASE, result.description, null, profileBm.HasPermission("GE999"));
                        } else 
                        {
                            //4.2 Chequeo de vertical
                            result = dvBll.IsVerticallyConsistent();

                            if (!result.IsValid())
                            {
                                //Sólo un admin puede continuar no es suficiente con que pueda restaurar
                                result = new ResultBM(ResultBM.Type.CORRUPTED_DATABASE, result.description, null, profileBm.HasPermission("GE999"));
                            } else 
                            {
                                result = new ResultBM(ResultBM.Type.OK, "Inicio de sesión exitoso para el usuario " + user);
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                result = new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception, false);
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
