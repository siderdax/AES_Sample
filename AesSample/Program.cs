using System;
using AesSample.DataEncryptor;

namespace AesSample
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] customKey = new byte[]
            {
                16, 20, 98, 240, 43, 80, 238, 15, 189, 239, 63, 236, 170, 47, 106, 3, 44, 159, 50,
                136, 226, 241, 109, 91, 94, 56, 94, 45, 45, 80, 37, 45
            };
            byte[] customIv = new byte[]
            {
                192, 193, 155, 175, 186, 68, 0, 37, 83, 243, 176, 170, 58, 126, 122, 20
            };
            string target = "This Text must be recovered.";
            byte[] metamorphosis;

            // AesMaze am = new AesMaze(); <- Create random Key, IV
            AesMaze am = new AesMaze(customKey, customIv);

            try
            {
                System.Console.WriteLine("Key:");
                foreach (byte b in am.Key)
                {
                    System.Console.Write(b + " ");
                }
                System.Console.WriteLine("");
                System.Console.WriteLine("IV:");
                foreach (byte b in am.Iv)
                {
                    System.Console.Write(b + " ");
                }
                System.Console.WriteLine("");
                System.Console.WriteLine("");

                System.Console.WriteLine("Enctypting text(" + target + ")");
                System.Console.WriteLine("result:");
                metamorphosis = am.Encrypt(target);
                foreach (byte b in metamorphosis)
                {
                    System.Console.Write(b + " ");
                }
                System.Console.WriteLine("");
                System.Console.WriteLine("Decrypting");
                System.Console.WriteLine("result:");
                System.Console.WriteLine(am.Decrypt(metamorphosis));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.ReadLine();
        }
    }
}