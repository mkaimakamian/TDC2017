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
    }
}
