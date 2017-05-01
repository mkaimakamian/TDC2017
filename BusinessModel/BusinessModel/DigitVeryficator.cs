using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public interface DigitVeryficator
    {
        /// <summary>
        /// Devuelve la cadena utilizada como semilla para hashear.
        /// </summary>
        /// <returns></returns>
        string GetSeed();

        /// <summary>
        /// Devuelve el hash previamente calculado.
        /// </summary>
        /// <returns></returns>
        string GetDigit();

        /// <summary>
        /// Devuelve el nombre de la entidad; empleada para el chequeo vertical.
        /// </summary>
        /// <returns></returns>
        //string GetEntity();
    }
}
