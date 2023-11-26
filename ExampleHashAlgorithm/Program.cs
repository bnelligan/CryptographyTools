using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleHashAlgorithm
{
    /** COPYRIGHT 2023 
    *  Brendan Nelligan
    *  SNHU MAT260 Cryptology
    **/
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Hash Example Tool ~~~");

            while (true)
            {
                Console.WriteLine(
                    "\n" +
                    "Press a key to choose: \n" +
                    "[H] hash plaintext\n" +
                    "[Q] quit\n");
                ConsoleKey selectionKey = Console.ReadKey().Key;
                Console.WriteLine();

                // Hash Encryption mode
                if (selectionKey == ConsoleKey.H)
                {
                    Console.WriteLine("Enter plaintext: ");
                    string plaintext = Console.ReadLine();
                    string hashText = Hash(plaintext);
                    Console.WriteLine($"Hash: {hashText}");
                }

                // Quit
                if(selectionKey == ConsoleKey.Q)
                {
                    return;
                }
            }
        }

        static string Hash(string plaintext)
        {
            // Filter out spaces and special characters
            plaintext = plaintext.AsIntArray().AsText();
            // Pad filtered plaintext to a multiple of 5
            int padding = 5 - (plaintext.Length % 5);
            padding = (padding == 5) ? 0 : padding;
            plaintext = plaintext.PadRight(plaintext.Length + padding, 'X');

            // Aggregate the sum of each 5th letter group into a character
            char y1 = plaintext.AsIntArray()
                        .Where((x, i) => (i + 5) % 5 == 0)
                        .Sum().Mod26().NumberToLetter();
            char y2 = plaintext.AsIntArray()
                        .Where((x, i) => (i + 4) % 5 == 0)
                        .Sum().Mod26().NumberToLetter();
            char y3 = plaintext.AsIntArray()
                        .Where((x, i) => (i + 3) % 5 == 0)
                        .Sum().Mod26().NumberToLetter();
            char y4 = plaintext.AsIntArray()
                        .Where((x, i) => (i + 2) % 5 == 0)
                        .Sum().Mod26().NumberToLetter();
            char y5 = plaintext.AsIntArray()
                        .Where((x, i) => (i + 1) % 5 == 0)
                        .Sum().Mod26().NumberToLetter();
            return $"{y1}{y2}{y3}{y4}{y5}";
        }
    }

    public static class CipherExtensions
    {
        public static int[] AsIntArray(this string text)
        {
            return text
                .ToUpper()
                .Where(c => (c >= 'A' && c <= 'Z'))
                .Select(c => c.LetterToNumber())
                .ToArray();
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
        public static int Mod26(this int x)
        {
            return x - 26 * (int)Math.Floor((double)x / 26); 
        }
    }
}