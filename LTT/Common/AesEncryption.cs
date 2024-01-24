//using System.Security.Cryptography;
//using System.Text;

//namespace LTT.Common
//{
//    using Microsoft.AspNetCore.DataProtection.KeyManagement;
//    using System;
//    using System.IO;
//    using System.Security.Cryptography;
//    using System.Text;

//    public class AesEncryption
//    {
//        private const string Key = "YourSecretKey123"; // 16, 24, or 32 bytes (128, 192, or 256 bits) depending on AES key size

//        public static string Encrypt(string plainText)
//        {
//            using (Aes aesAlg = Aes.Create())
//            {
//                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
//                aesAlg.GenerateIV();  // Generate a random IV for each encryption

//                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

//                using (MemoryStream msEncrypt = new MemoryStream())
//                {
//                    // Prepend the IV to the ciphertext
//                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

//                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
//                    {
//                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
//                        {
//                            swEncrypt.Write(plainText);
//                        }
//                    }

//                    return Convert.ToBase64String(msEncrypt.ToArray());
//                }
//            }
//        }

    

//    public static string Decrypt(string cipherText)
//    {
//        using (Aes aesAlg = Aes.Create())
//        {
//                aesAlg.Key = Encoding.UTF8.GetBytes(Key);

//            // Extract the IV from the beginning of the ciphertext
//            byte[] iv = new byte[aesAlg.BlockSize / 8];
//            Array.Copy(Convert.FromBase64String(cipherText), 0, iv, 0, iv.Length);
//            aesAlg.IV = iv;

//            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

//            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
//            {
//                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
//                {
//                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
//                    {
//                        return srDecrypt.ReadToEnd();
//                    }
//                }
//            }
//        }
//    }

//}

//}
