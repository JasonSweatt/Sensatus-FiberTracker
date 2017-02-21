using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Class to be used for providing security to sensitive data by Encryption/ Decryption
    /// </summary>
    public class DataSecurity
    {
        /// <summary>
        /// The bytes
        /// </summary>
        private static byte[] _bytes = Encoding.ASCII.GetBytes("ZeroCool");

        /// <summary>
        /// Encrypt normal string.
        /// </summary>
        /// <param name="originalString">String to be encrypted.</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return string.Empty;

            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(_bytes, _bytes), CryptoStreamMode.Write);
            var writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        /// <summary>
        /// Decrypt encrypted string to the normal string
        /// </summary>
        /// <param name="cryptedString">Decrypted string</param>
        /// <returns>Normal (Decrypted) string</returns>
        /// <exception cref="ArgumentNullException">The string which needs to be decrypted can not be null.</exception>
        public string Decrypt(string cryptedString)
        {
            if (string.IsNullOrEmpty(cryptedString))
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");
            var cryptoProvider = new DESCryptoServiceProvider();
            var memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            var cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(_bytes, _bytes), CryptoStreamMode.Read);
            var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}