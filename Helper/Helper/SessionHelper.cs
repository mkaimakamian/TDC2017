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
        private UserMDL userMdl;
        private ProfileMDL profileMdl;
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
        public static SessionHelper StartSession(UserMDL userMdl, ProfileMDL profileMdl, List<TranslationMDL> translations) {
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

        private static Dictionary<String, String> ConvertIntoList(List<TranslationMDL> translations)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            foreach (TranslationMDL translation in translations)
            {
                result.Add(translation.labelCode, translation.translation);
            }

            return result;
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

    }
}
