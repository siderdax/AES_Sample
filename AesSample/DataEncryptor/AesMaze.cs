using System;
using System.IO;
using System.Security.Cryptography;

namespace AesSample.DataEncryptor
{
    public class AesMaze
    {
        public byte[] Key { get; private set; }
        public byte[] Iv { get; private set; }

        public void CreateKeyIv()
        {
            using(Aes iAes = Aes.Create())
            {
                Key = new byte[iAes.Key.Length];
                Array.Copy(iAes.Key, Key, iAes.Key.Length);
                Iv = new byte[iAes.IV.Length];
                Array.Copy(iAes.IV, Iv, iAes.IV.Length);

                ICryptoTransform ict = iAes.CreateEncryptor(Key, Iv);
            }
        }

        public byte[] Encrypt(string data)
        {
            byte[] encryptedData;

            if (data == null || data.Length <= 0)
            {
                throw new ArgumentNullException("data");
            }

            using(Aes iAes = Aes.Create())
            {
                iAes.Key = Key;
                iAes.IV = Iv;

                ICryptoTransform encryptor = iAes.CreateEncryptor(iAes.Key, iAes.IV);
                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(data);
                        }
                        encryptedData = ms.ToArray();
                    }
                }
            }

            return encryptedData;
        }

        public string Decrypt(byte[] encryptedData)
        {
            if (encryptedData == null || encryptedData.Length <= 0)
            {
                throw new ArgumentNullException("encryptedData");
            }

            string decryptedData = null;

            using(Aes iAes = Aes.Create())
            {
                iAes.Key = Key;
                iAes.IV = Iv;

                ICryptoTransform decryptor = iAes.CreateDecryptor(iAes.Key, iAes.IV);
                using(MemoryStream ms = new MemoryStream(encryptedData))
                {
                    using(CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader srDecrypt = new StreamReader(cs))
                        {
                            decryptedData = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return decryptedData;
        }

        public AesMaze()
        {
            CreateKeyIv();
        }

        public AesMaze(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }
    }
}