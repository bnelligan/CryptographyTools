using System;

namespace VigenereCipher
{
   /** COPYRIGHT 2023 
    *  Brendan Nelligan
    *  SNHU MAT260 Cryptology
    **/
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Vigenere Cipher Tool ~~~\n");

            while (true)
            {
                Console.WriteLine(
                "\n" +
                "Press a key to choose: \n" +
                "[E] encrypt plaintext\n" +
                "[D] decrypt ciphertext\n" +
                "[Q] quit\n");
                ConsoleKey selectionKey = Console.ReadKey().Key;
                Console.WriteLine();

                // Encryption mode
                if (selectionKey == ConsoleKey.E)
                {
                    Console.WriteLine("Enter plaintext: ");
                    string plaintext = Console.ReadLine();

                    Console.WriteLine("Enter keyword: ");
                    string key = Console.ReadLine();

                    string ciphertext = Encrypt(plaintext, key);
                    Console.WriteLine($"Ciphertext: \n{ciphertext}");

                    string plaintextCheck = Decrypt(ciphertext, key);
                    Console.WriteLine($"Decrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Decryption mode
                if (selectionKey == ConsoleKey.D)
                {
                    Console.WriteLine("Enter ciphertext: ");
                    string ciphertext = Console.ReadLine();

                    Console.WriteLine("Enter keyword: ");
                    string key = Console.ReadLine();

                    string plaintext = Decrypt(ciphertext, key);
                    Console.WriteLine($"Plaintext: \n{plaintext}");

                    string plaintextCheck = Encrypt(plaintext, key);
                    Console.WriteLine($"Encrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Quit
                if(selectionKey == ConsoleKey.Q)
                {
                    return;
                }
            }
        }

        static string Encrypt(string plaintext, string key)
        {
            int[] plaintextAsInt = plaintext.AsIntArray();

            int[] keyAsInt = key.AsIntArray()
                .MakeRepeatedCopy(plaintextAsInt.Length);

            int[] ciphertextAsInt = plaintextAsInt
                .Select((n, i) => { return (n + keyAsInt[i]) % 26; })
                .ToArray();

            return ciphertextAsInt.AsText();
        }
            
        static string Decrypt(string ciphertext, string key)
        {
            int[] ciphertextAsInt = ciphertext.AsIntArray();

            int[] keyAsInt = key.AsIntArray()
                .MakeRepeatedCopy(ciphertextAsInt.Length);

            int[] plaintextAsInt = ciphertextAsInt
                .Select((n, i) => { return (n - keyAsInt[i] + 26) % 26; })
                .ToArray();

            return plaintextAsInt.AsText();
        }
    }

    public static class CipherExtensions
    {
        public static int[] AsIntArray(this string text)
        {
            return text.Replace(" ", "").Select(c => c.LetterToNumber()).ToArray();
        }
        public static string AsText(this int[] intArray)
        {
            return new string(intArray.Select(n => n.NumberToLetter()).ToArray());
        }
        public static int LetterToNumber(this char letter)
        {
            return char.ToUpper(letter) - 'A';
        }
        public static char NumberToLetter(this int letter)
        {
            return (char)(letter + 'A');
        }
        public static T[] MakeRepeatedCopy<T>(this T[] repeatedValue, int length)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < length; i++)
            {
                result.Add(repeatedValue[i % repeatedValue.Length]);
            }
            return result.ToArray();
        }
    }

}
