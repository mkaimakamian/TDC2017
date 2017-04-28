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
        private LanguageBM languageBm;
        //permisos

        private SessionHelper()
        {
        }

        /// <summary>
        /// Inicia la sesión, lo que implica almacenar los datos del usuario y su configuración (idioma y permisos).
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static SessionHelper StartSession(UserBM userMdl, ProfileBM profileMdl, LanguageBM languageBm)
        {
            if (instance == null)
            {
                instance = new SessionHelper();
                instance.userMdl = userMdl;
                instance.profileMdl = profileMdl;
                instance.languageBm = languageBm;
                ConvertIntoList(languageBm);
            }

            //No sé si se necesita porque no opero actualmente con la sesion
            return instance;
        }

        private static void ConvertIntoList(LanguageBM languageBm)
        {
            List<TranslationBM> trans = languageBm.Translations;

            //Si nestá vacío, se crea el diccionario con las traducciones; en otro caso, para aprovechar el binding, re pisan los valores.
            if (instance.translations == null)
            {
                instance.translations = new Dictionary<String, String>();
                foreach (TranslationBM translation in trans)
                {
                   instance.translations.Add(translation.labelCode, translation.translation);
                }
            }
            else
            {
                foreach (TranslationBM translation in trans)
                {
                   instance.translations[translation.labelCode] = translation.translation;
                }
            }
        }

        /// <summary>
        /// Devuelve el Business Model perteneciente al usuario loggeado
        /// </summary>
        /// <returns></returns>
        public static UserBM GetLoggedUser()
        {
            return instance.userMdl;
        }
        /// <summary>
        /// Finaliza la sesión.
        /// </summary>
        public static void EndSession()
        {
            //if (instance != null)
            //{
            //    instance.userMdl = null;
            //    instance.profileMdl = null;                
            //    instance.languageBm = null;
            //}
            instance = null;
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

        /// <summary>
        /// Para un código en particular, devuelve las traducciones.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
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

        public static void SetLanguage(LanguageBM languageBm) {
            instance.languageBm = languageBm;
            ConvertIntoList(languageBm);
        }
    }
}
