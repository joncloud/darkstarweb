using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public static class MySqlFunctions
    {
        public static string Password(string key)
        {
            byte[] rawKey = Encoding.UTF8.GetBytes(key);
            using (SHA1Managed enc = new SHA1Managed())
            {
                byte[] encoded = enc.ComputeHash(enc.ComputeHash(rawKey));
                StringBuilder builder = new StringBuilder(encoded.Length);

                foreach (byte part in encoded)
                {
                    builder.Append(part.ToString("X2"));
                }

                return "*" + builder.ToString();
            }
        }
    }
}
