using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    /// <summary>
    /// Esta clase tiene por objetivo proveer a los usuarios de la lógica de negocio, un objeto que le permita conocer 
    /// el resultado de la operación y operar, de ser requerido, con los objetos que devuelva.
    /// </summary>
    public class ResultBM
    {
        public enum Type 
        {
            OK,
            INVALID_CREDENTIAL,
            CHECKSUM_ERROR,
            INCOMPLETE_FIELDS,
            EXCEPTION,
            CORRUPTED_DATABASE,
            FAIL,
            EMPTY_PROFILE
        };

        private Type type;
        public string description; //Descripción a mostrar
        public bool keepGoing;     //Indica si el resultado implica la detención del flujo normal del programa
        private object value;       //Valor del resultado

        public ResultBM(Type type, string description = "", object value = null, bool keepGoing = true)
        {
            this.type = type;
            this.description = description;
            this.value = value;
            this.keepGoing = keepGoing;
        }

        /// <summary>
        /// Evalúa si el resultado es válido (OK) o no (cualquier otro).
        /// </summary>
        /// <returns></returns>
        public Boolean IsValid()
        {
            return IsCurrentError(Type.OK);
        }
        
        /// <summary>
        /// Evalúa si el tipo de resultado coincide con el pasado por parámetro.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Boolean IsCurrentError(Type type) 
        {
            return (this.type == type);
        }

        /// <summary>
        /// Devuelve el objeto, casteando el typo a través de Generic.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return (T)Convert.ChangeType(this.value, typeof(T));
        }

        /// <summary>
        /// Devueve el objeto sin obligar el casting.
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            return this.value;
        }
    }
}
