using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleBlockCipher
{
  /** COPYRIGHT 2023 
    *  Brendan Nelligan
    *  SNHU MAT260 Cryptology
    **/
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Block Cipher Tool ~~~\n");

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
                    Console.WriteLine($"Ciphertext: \n{ciphertext.Select(a => ((int)a).ToBitfieldString()).Aggregate((a, b) => a + b)}");

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

            int keyBitfield = key.FromBitfieldString();

            int[] ciphertextAsInt = plaintextAsInt.Select(x => 
            {
                return EncryptBlock(x, keyBitfield) 
                    | (EncryptBlock(x >> 4, keyBitfield) << 4);
            }).ToArray();

            return ciphertextAsInt.AsText();
        }
            
        static string Decrypt(string ciphertext, string key)
        {
            int[] ciphertextAsInt = ciphertext.AsIntArray();

            int keyBitfield = key.FromBitfieldString();

            int[] plaintextAsInt = ciphertextAsInt.Select(x => 
            {
                return DecryptBlock(x, keyBitfield) 
                    | (DecryptBlock(x >> 4, keyBitfield) << 4);
            }).ToArray();
            return plaintextAsInt.AsText();
        }

        static int EncryptBlock(int x, int k)
        {
            // Compute t1 t2
            int x3x4x3 = x.Bit(1) << 2 | x.Bit(0) << 1 | x.Bit(1);
            int k1k2k3 = k & 7;
            int t1t2 = S[x3x4x3 ^ k1k2k3];
            
            // Compute u1 u2
            int x1x2 = x.Bit(3) << 1 | x.Bit(2);
            int u1u2 = x1x2 ^ t1t2;
    
            // Assemble the encrypted block
            int x3x4 = x.Bit(1) << 1 | x.Bit(0);
            int y = (x3x4 << 2) | u1u2;
            return y;
        }

        static int DecryptBlock(int y, int k)
        {
            // Compute t1 t2
            int y1y2y1 = y.Bit(3) << 2 | y.Bit(2) << 1 | y.Bit(3);
            int k1k2k3 = k & 7;
            int t1t2 = S[y1y2y1 ^ k1k2k3];

            // Compute u1 u2
            int y3y4 = y.Bit(1) << 1 | y.Bit(0);
            int u1u2 = y3y4 ^ t1t2;

            // Assemble the decrypted block
            int y1y2 = y.Bit(3) << 1 | y.Bit(2);
            int x = (u1u2 << 2) | y1y2;
            return x;
        }
        
        static Dictionary<int, int> S = new Dictionary<int, int>()
        {
            [0] = 3,    // 000 -> 11
            [1] = 1,    // 001 -> 01
            [2] = 0,    // 010 -> 00
            [3] = 2,    // 011 -> 10
            [4] = 1,    // 100 -> 01
            [5] = 0,    // 101 -> 00
            [6] = 3,    // 110 -> 11
            [7] = 2     // 111 -> 10
        };
    }

    public static class CipherExtensions
    {
        public static int[] AsIntArray(this string text)
        {
            return text.Select(c => c.LetterToNumber()).ToArray();
        }
        public static int FromBitfieldString(this string text)
        {
            return text
            .AsEnumerable()
            .Reverse()
            .Select((c, i) => {return c == '1' ? 1 << i : 0;})
            .Sum();
        }
        public static string ToBitfieldString(this int b)
        {
            return $"{b.Bit(7)}{b.Bit(6)}{b.Bit(5)}{b.Bit(4)} {b.Bit(3)}{b.Bit(2)}{b.Bit(1)}{b.Bit(0)} ";
        }
        public static int Bit(this int x, int i)
        {
            return (x & (1 << i)) >> i;
        }
        public static string AsText(this int[] intArray)
        {
            return new string(intArray.Select(n => n.NumberToLetter()).ToArray());
        }
        public static int LetterToNumber(this char letter)
        {
            return char.ToUpper(letter);
        }
        public static char NumberToLetter(this int letter)
        {
            return (char)(letter);
        }
    }

}
