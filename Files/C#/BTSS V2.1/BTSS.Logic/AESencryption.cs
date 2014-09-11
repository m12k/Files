using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Security.Cryptography;
using System.IO;

namespace BTSS.Logic
{
    //This class should be internal. Meaning: Only this assembly/project can use this class.
    internal class AESencryption
    { 
        //Key
        const string key = "65O04L2J7Z2V6oGp7pSy4YWV6rqg6om95ZmA7q+k4bal4pew7qeY4aO84ZmT4ri3";
        //IV(Initialization Vector)
        const string iv = "6aOy7Iu16J6P5r605oeU4YO/7LWA6r+K";
         
        public static string Encrypt(string data)
        { 
            try
            {
                //1. decode to base64string the key and IV.
                //2. Encrypt the data by converting it to bytes
                byte[] encrypted = EncryptStringToBytes_Aes(data, StringToByte(Base64Decode(key)), StringToByte(Base64Decode(iv)));

                //convert byte to string so will be able to convert to base64string.
                string plainText = ByteToString(encrypted);
                return Base64Encode(plainText);
            }
            catch (Exception)
            {
                throw;
            } 
        }
         
        public static string Decrypt(string data)
        {
            try
            {
                //Decode encoded data and convert to byte
                byte[] results = StringToByte(Base64Decode(data));

                //convert byte to the final string.
                return DecryptStringFromBytes_Aes(results, StringToByte(Base64Decode(key)), StringToByte(Base64Decode(iv)));
                 
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an Aes object 
            // with the specified key and IV. 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            } 

            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an Aes object 
            // with the specified key and IV. 
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                } 
            }

            return plaintext; 
        }
         
        static string ByteToString(byte[] args)
        {
            try 
            {	         
                char[] chars = new char[args.Length / sizeof(char)];
                System.Buffer.BlockCopy(args, 0, chars, 0, args.Length);
                return new string(chars); 
	        }
	        catch (Exception)
	        {
		        throw;
	        }
        }

        static byte[] StringToByte(string str)
        {
            try 
            {
                byte[] bytes = new byte[str.Length * sizeof(char)];
                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }
            catch (Exception)
            {
                throw;
            }
        } 

        static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
