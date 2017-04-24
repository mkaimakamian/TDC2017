using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel;

namespace Helper
{
    public class SessionHelper
    {
        private static SessionHelper instance;
        private UserBM userMdl;
        private ProfileBM profileMdl;
        private Dictionary<String, String> translations;
        //permisos

        private SessionHelper()
        {
        }

        /// <summary>
        /// Inicia la sesión, lo que implica almacenar los datos del usuario y su configuración (idioma y permisos).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static SessionHelper StartSession(UserBM userMdl, ProfileBM profileMdl, List<TranslationBM> translations) {
            if (instance == null)
            {
                instance = new SessionHelper();
                instance.userMdl = userMdl;
                instance.profileMdl = profileMdl;
                instance.translations = ConvertIntoList(translations);
                // TODO - falta recibir por parámetro permisos.
            }

            //No sé si se necesita porque no opero actualmente con la sesion
            return instance;
        }

        private static Dictionary<String, String> ConvertIntoList(List<TranslationBM> translations)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            foreach (TranslationBM translation in translations)
            {
                result.Add(translation.labelCode, translation.translation);
            }

            return result;
        }

        public static UserBM GetLoggedUser()
        {
            return instance.userMdl;
        }
        /// <summary>
        /// Finaliza la sesión.
        /// </summary>
        public static void EndSession()
        {
            if (instance != null)
            {
                instance.userMdl = null;
                instance.profileMdl = null;
                instance.translations = null;
            }
        }

        /// <summary>
        /// Devuelve tru si el usuario en sesión tiene permisos sobre el objeto cuyo código es pasado por parámetro.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool HasPermission(string code)
        {
            return instance.profileMdl.HasPermission(code);
        }

        public static string GetTranslation(string code)
        {
            string value;
            instance.translations.TryGetValue(code, out value);

            // El valor default debería estar definido en la tabla de traducciones.
            if (value == null || value.Length == 0)
            {
                return "UNDEFINED";
            } else {
                return value;
            }
        }
    }
}
