using System.Security.Cryptography;
using System.Text;
using System;

namespace wastelands.src.utils
{
    public static class IdGenerator
    {
        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string DeHex(string source)
        {
            char[] subs = new char[source.Length / 2];

            for(int i = 0; i < source.Length; i += 2)
            {
                subs[(int)(i / 2)] = (char)Convert.ToInt32(source.Substring(i, 2), 16);
            }

            return new string(subs);
        }

        public static string FromString(string source)
        {
            string hash;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                hash = GetHash(sha256Hash, source);
            }

            return DeHex(hash.Substring(0, 6));
        }
    }
}
