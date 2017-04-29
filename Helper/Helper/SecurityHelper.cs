using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Helper
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Devuelve un hashing basado en MD5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String Encrypt(string value) {
            Byte[] bytes;
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(value)) {
                throw new ArgumentNullException("Valor no provisto para realizar hashing.");
            }
        
            bytes = Encoding.Default.GetBytes(value);
            bytes = MD5.Create().ComputeHash(bytes);

            for (int x = 0; x < bytes.Length; x++)
            {
                sb.Append(bytes[x].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Evalúa si el parámetro seed es equivalente al valor hasheado.
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEquivalent(string seed, string hashedValue)
        {
            string hash = SecurityHelper.Encrypt(seed);
            return hash == hashedValue;
        }
    }
}
