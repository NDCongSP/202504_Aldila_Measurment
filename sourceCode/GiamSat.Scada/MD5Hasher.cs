using System;
using System.Security.Cryptography;
using System.Text;

namespace GiamSat.Scada
{
    public class MD5Hasher
    {
        // Generate MD5 hash from input string
        public static string EncryptToMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Convert the input string to bytes and compute the hash
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert byte array to hex string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2")); // lower-case hex

                return sb.ToString();
            }
        }

        // Compare a plain string to an MD5 hash
        public static bool CompareWithMD5(string input, string md5Hash)
        {
            string hashedInput = EncryptToMD5(input);
            return string.Equals(hashedInput, md5Hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
