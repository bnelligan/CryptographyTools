namespace ColumnarTransposition
{
    /** COPYRIGHT 2023 
     *  Brendan Nelligan
     *  SNHU MAT260 Cryptology
     **/
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Columnar Transposition Cipher Tool ~~~\n");

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

                    Console.WriteLine("Enter # of columns: ");
                    int columnCount = int.Parse(Console.ReadLine());

                    string ciphertext = Encrypt(plaintext, columnCount);
                    Console.WriteLine($"Ciphertext: \n{ciphertext}");

                    string plaintextCheck = Decrypt(ciphertext, columnCount);
                    Console.WriteLine($"Decrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Decryption mode
                if (selectionKey == ConsoleKey.D)
                {
                    Console.WriteLine("Enter ciphertext: ");
                    string ciphertext = Console.ReadLine();

                    Console.WriteLine("Enter # of columns: ");
                    int columnCount = int.Parse(Console.ReadLine());

                    string plaintext = Decrypt(ciphertext, columnCount);
                    Console.WriteLine($"Plaintext: \n{plaintext}");

                    string plaintextCheck = Encrypt(plaintext, columnCount);
                    Console.WriteLine($"Encrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Quit
                if (selectionKey == ConsoleKey.Q)
                {
                    return;
                }
            }
        }

        static string Encrypt(string plaintext, int columnCount)
        {
            plaintext = plaintext.Replace(" ", "");
            int rowCount = (int)Math.Ceiling((float)plaintext.Length / columnCount);
            string[] columns = new string[columnCount];
            for (int i = 0; i < plaintext.Length; i++)
            {
                columns[i % columns.Length] += plaintext[i];
            }
            return columns.Aggregate((a, b) => a + (b.Length < rowCount ? b + "T" : b));
        }

        static string Decrypt(string ciphertext, int columnCount)
        {
            return Encrypt(ciphertext, ciphertext.Length / columnCount);
        }
    }
}