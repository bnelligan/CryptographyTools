namespace AffineCipher
{
    /** COPYRIGHT 2023 
     *  Brendan Nelligan
     *  SNHU MAT260 Cryptology
     **/
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Affine Cipher Tool ~~~\n");

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

                    Console.WriteLine("Enter a: ");
                    int key_a = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter b: ");
                    int key_b = int.Parse(Console.ReadLine());

                    string ciphertext = Encrypt(plaintext, key_a, key_b);
                    Console.WriteLine($"Ciphertext: \n{ciphertext}");

                    string plaintextCheck = Decrypt(ciphertext, key_a, key_b);
                    Console.WriteLine($"Decrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Decryption mode
                if (selectionKey == ConsoleKey.D)
                {
                    Console.WriteLine("Enter ciphertext: ");
                    string ciphertext = Console.ReadLine();

                    Console.WriteLine("Enter a: ");
                    int key_a = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter b: ");
                    int key_b = int.Parse(Console.ReadLine());

                    string plaintext = Decrypt(ciphertext, key_a, key_b);
                    Console.WriteLine($"Plaintext: \n{plaintext}");

                    string plaintextCheck = Encrypt(plaintext, key_a, key_b);
                    Console.WriteLine($"Encrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Quit
                if (selectionKey == ConsoleKey.Q)
                {
                    return;
                }
            }
        }

        static string Encrypt(string plaintext, int key_a, int key_b)
        {
            int[] plaintextAsInt = plaintext.AsIntArray();

            int[] ciphertextAsInt = plaintextAsInt
                .Select((n, i) => { return Mod26(key_a * n + key_b); })
                .ToArray();

            return ciphertextAsInt.AsText();
        }

        static string Decrypt(string ciphertext, int key_a, int key_b)
        {
            int[] ciphertextAsInt = ciphertext.AsIntArray();

            int[] plaintextAsInt = ciphertextAsInt
                .Select((n, i) => { return Mod26(InvM26[key_a] * (n - key_b)); })
                .ToArray();

            return plaintextAsInt.AsText();
        }
        
        static int Mod26(int x) { return x - 26 * (int)Math.Floor((double)x / 26); }
        private static Dictionary<int, int> InvM26 = new Dictionary<int, int>()
        {
            [1] = 1,
            [3] = 9,
            [5] = 21,
            [7] = 15,
            [9] = 3,
            [11] = 19,
            [15] = 7,
            [17] = 23,
            [19] = 11,
            [21] = 5,
            [23] = 17,
            [25] = 25
        };
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