using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Class to be used for providing security to sensitive data by Encryption/ Decryption
    /// </summary>
    public class DataSecurity
    {
        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("ZeroCool");

        /// <summary>
        /// Encrypt normal string.
        /// </summary>
        /// <param name="originalString">String to be encrypted.</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))            
                return string.Empty;
            
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
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
        public string Decrypt(string cryptedString)
        {
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                   ("The string which needs to be decrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                    (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}
