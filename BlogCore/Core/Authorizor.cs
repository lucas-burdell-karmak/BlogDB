using System;
using System.Security.Cryptography;
using System.Text;

namespace BlogDB.Core
{
    public class Authorizor : IAuthorizor
    {

        private static byte[] CombineByteArrays(byte[] array1, byte[] array2)
        {
            byte[] output = new byte[array1.Length + array2.Length];
            var currentIndex = 0;
            for (var i = 0; i < array1.Length; i++)
            {
                output[currentIndex] = array1[i];
                currentIndex++;
            }
            for (var i = 0; i < array2.Length; i++)
            {
                output[currentIndex] = array2[i];
                currentIndex++;
            }
            return output;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private static byte[] HexStringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public bool TryValidateAuthor(string name, string passwordHash, out Author author)
        {
            throw new NotImplementedException();
        }

        public bool TryRegisterAuthor(string name, string passwordHash, out Author author)
        {
            Guid salt = Guid.NewGuid();
            HMACSHA512 hash = new HMACSHA512();

            hash.Key = salt.ToByteArray();
            byte[] computedHash = hash.ComputeHash(HexStringToByteArray(passwordHash));

            string hexOfComputedHash = ByteArrayToString(computedHash);

            throw new Exception();
        }
    }
}
