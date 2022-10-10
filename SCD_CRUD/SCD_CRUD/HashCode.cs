using System.Text;
using System.Security.Cryptography;
using System;
using System.Linq;

namespace SCD_CRUD
{
    class HashCode
    {
        #region Encrypt and Decrypt
        public string Encrypt(string text)
        {
            byte[] src = Encoding.UTF8.GetBytes(text);
            byte[] key = Encoding.ASCII.GetBytes("contactcentre");
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;

            using (ICryptoTransform encrypt = aes.CreateEncryptor(key, null))
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                encrypt.Dispose();
                return Convert.ToBase64String(dest);
            }
        }

        public string Decrypt(string text)
        {
            byte[] src = Convert.FromBase64String(text);
            RijndaelManaged aes = new RijndaelManaged();
            byte[] key = Encoding.ASCII.GetBytes("contactcentrT");
            aes.KeySize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.ECB;
            using (ICryptoTransform decrypt = aes.CreateDecryptor(key, null))
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                decrypt.Dispose();
                return Encoding.UTF8.GetString(dest);
            }
        }
        #endregion

        #region Salt plus Hashnig
        public string GetMd5Hash(string input)
        {
            string ReversePassword = new string(input.Reverse().ToArray());
            byte[] ba = Encoding.Default.GetBytes(ReversePassword);
            var hexString = BitConverter.ToString(ba);
            hexString = hexString.Replace("-", "");
            hexString = hexString + ReversePassword;
            using (MD5 md5Hash = MD5.Create())
            {

                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(hexString));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        // Verify a hash against a string.
        public bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);

        }
        #endregion

    }

}
