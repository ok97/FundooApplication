using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class EncryptionDecryption
    {
        public static string Encryption(string password)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(password);
            string encryptedPassword = Convert.ToBase64String(bytesToEncode);
            return encryptedPassword;
        }
    }
}