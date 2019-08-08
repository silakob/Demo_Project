using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Iwanttobuy.Models
{
    public class Encryption
    {
        public static byte[] Key { get; set; }
        public static byte[] IV { get; set; }

        public static byte[] Encrypt(string s)
        {
            try
            {
                // Create a new instance of the RijndaelManaged 
                // class.  This generates a new key and initialization  
                // vector (IV). 
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    myRijndael.Key = new byte[32];
                    myRijndael.IV = new byte[16];
                    Key = myRijndael.Key;
                    IV = myRijndael.IV;
                    // Encrypt the string to an array of bytes. 
                    byte[] encrypted = EncryptStringToBytes(s, myRijndael.Key, myRijndael.IV);


                    return encrypted;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string Decrypt(byte[] encrypted)
        {
            try
            {
                // Decrypt the bytes to a string. 
                string roundtrip = DecryptStringFromBytes(encrypted, Key, IV);

                //Display the original data and the decrypted data.
                return roundtrip;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

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

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

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


        //public class StringEncoder
        //{
        //    public static byte[] EncodeToBytes(string str)
        //    {
        //        byte[] bytes = new byte[str.Length * sizeof(char)];
        //        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        //        return bytes;
        //    }
        //    public static string DecodeToString(byte[] bytes)
        //    {
        //        char[] chars = new char[bytes.Length / sizeof(char)];
        //        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        //        return new string(chars);
        //    }
        //}
    }
}