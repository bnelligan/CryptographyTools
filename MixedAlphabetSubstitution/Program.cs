using System.Linq;

namespace MixedAlphabetSubstitution
{
    /** COPYRIGHT 2023 
     *  Brendan Nelligan
     *  SNHU MAT260 Cryptology
     **/
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Mixed Alphabet Substitution Cipher Tool ~~~\n");

            while (true)
            {
                Console.WriteLine(
                "\n" +
                "Press a key to choose: \n" +
                "[A] view cipher alphabet\n" +
                "[E] encrypt plaintext\n" +
                "[D] decrypt ciphertext\n" +
                "[Q] quit\n");
                ConsoleKey selectionKey = Console.ReadKey().Key;
                Console.WriteLine();

                // Cipher alphabet view mode
                if(selectionKey == ConsoleKey.A)
                {
                    Console.WriteLine("Enter key: ");
                    string key = Console.ReadLine();
                    string cipherAlphabet = CreateMixedAlphabet(key).AsText();
                    Console.WriteLine("Cipher alphabet:");
                    Console.WriteLine(cipherAlphabet);
                }

                // Encryption mode
                if (selectionKey == ConsoleKey.E)
                {
                    Console.WriteLine("Enter plaintext: ");
                    string plaintext = Console.ReadLine();

                    Console.WriteLine("Enter key: ");
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
                if (selectionKey == ConsoleKey.Q)
                {
                    return;
                }
            }
        }

        static string Encrypt(string plaintext, string key)
        {
            int[] plaintextAsInt = plaintext.AsIntArray();

            int[] keyAlphabet = CreateMixedAlphabet(key);

            int[] ciphertextAsInt = plaintextAsInt
                .Select((n, i) => keyAlphabet[n])
                .ToArray();

            return ciphertextAsInt.AsText();
        }

        static string Decrypt(string ciphertext, string key)
        {
            int[] ciphertextAsInt = ciphertext.AsIntArray();

            int[] keyAlphabet = CreateMixedAlphabet(key);

            int[] plaintextAsInt = ciphertextAsInt
                .Select((n, i) => Array.IndexOf(keyAlphabet, n))
                .ToArray();

            return plaintextAsInt.AsText();
        }

        static int[] CreateMixedAlphabet(string key)
        {
            List<int> mixedAlphabet = new List<int>();
            key.AsIntArray().ToList()
                .ForEach(k => { if (!mixedAlphabet.Contains(k)) mixedAlphabet.Add(k); });
            Enumerable.Range(0, 25).ToList()
                .ForEach(x => { if (!mixedAlphabet.Contains(x)) mixedAlphabet.Add(x); });
            return mixedAlphabet.ToArray();
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
    }
}