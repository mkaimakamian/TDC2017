using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Helper
{
    public class SessionHelper
    {
        private static SessionHelper instance;
        private string user;
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
        public static SessionHelper StartSession(string user, Dictionary<String, String> translations) {
            if (instance == null)
            {
                instance = new SessionHelper();
                instance.user = user;
                instance.translations = translations;
                // TODO - falta recibir por parámetro permisos.
            }

            //No sé si se necesita porque no opero actualmente con la sesion
            return instance;
        }

        /// <summary>
        /// Finaliza la sesión.
        /// </summary>
        public static void EndSession()
        {
            if (instance != null)
            {
                instance.user = null;
                instance.translations = null;
            }
        }

    }
}
