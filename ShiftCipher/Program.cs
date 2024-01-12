namespace ShiftCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Shift Cipher Tool ~~~\n");

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

                    Console.WriteLine("Enter shift amount: ");
                    int shift = int.Parse(Console.ReadLine());

                    string ciphertext = Encrypt(plaintext, shift);
                    Console.WriteLine($"Ciphertext: \n{ciphertext}");

                    string plaintextCheck = Decrypt(ciphertext, shift);
                    Console.WriteLine($"Decrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Decryption mode
                if (selectionKey == ConsoleKey.D)
                {
                    Console.WriteLine("Enter ciphertext: ");
                    string ciphertext = Console.ReadLine();

                    Console.WriteLine("Enter shift amount: ");
                    int shift = int.Parse(Console.ReadLine());

                    string plaintext = Decrypt(ciphertext, shift);
                    Console.WriteLine($"Plaintext: \n{plaintext}");

                    string plaintextCheck = Encrypt(plaintext, shift);
                    Console.WriteLine($"Encrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Quit
                if (selectionKey == ConsoleKey.Q)
                {
                    return;
                }
            }
        }

        static string Encrypt(string plaintext, int shift)
        {
            int[] plaintextAsInt = plaintext.AsIntArray();
            

            int[] ciphertextAsInt = plaintextAsInt
                .Select((n, i) => { return (n + shift) % 26; })
                .ToArray();

            return ciphertextAsInt.AsText();
        }

        static string Decrypt(string ciphertext, int shift)
        {
            int[] ciphertextAsInt = ciphertext.AsIntArray();

            int[] plaintextAsInt = ciphertextAsInt
                .Select((n, i) => { return (n - shift + 26) % 26; })
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
    }
}