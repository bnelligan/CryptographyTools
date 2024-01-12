using System.Text;

namespace HillCipher
{
    internal class Program
    {
        /** COPYRIGHT 2023 
         *  Brendan Nelligan
         *  SNHU MAT260 Cryptology
         **/
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Hill Cipher Tool ~~~\n");

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
                    string plaintext = Console.ReadLine().Replace(" ", "");

                    M2x2_Mod26 key = ReadCipherKey();

                    string ciphertext = Encrypt(plaintext, key);
                    Console.WriteLine($"Ciphertext: \n{ciphertext}");

                    string plaintextCheck = Decrypt(ciphertext, key);
                    Console.WriteLine($"Decrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Decryption mode
                if (selectionKey == ConsoleKey.D)
                {
                    Console.WriteLine("Enter ciphertext: ");
                    string ciphertext = Console.ReadLine().Replace(" ", "");

                    M2x2_Mod26 key = ReadCipherKey();

                    string plaintext = Decrypt(ciphertext, key);
                    Console.WriteLine($"Plaintext: \n{plaintext}");

                    string plaintextCheck = Encrypt(plaintext, key);
                    Console.WriteLine($"Encrypted plaintext for checking: \n{plaintextCheck}");
                }

                // Quit
                if (selectionKey == ConsoleKey.Q)
                    return;
            }
            
        }

        static string Encrypt(string plaintext, M2x2_Mod26 key)
        {
            if (plaintext.Length % 2 != 0) 
                plaintext += "X";
            string cryptoText = "";
            for(int i = 0; i < plaintext.Length; i += 2)
            {
                M2x2_Mod26 x = new M2x2_Mod26(
                    plaintext[i].LetterToNumber(), 0, 
                    plaintext[i + 1].LetterToNumber(), 0);
                M2x2_Mod26 y = key * x;
                cryptoText += y.M11.NumberToLetter();
                cryptoText += y.M21.NumberToLetter();
            }
            return cryptoText;
        }

        static string Decrypt(string cryptotext, M2x2_Mod26 key)
        {
            return Encrypt(cryptotext, key.Inverse());
        }

        static M2x2_Mod26 ReadCipherKey()
        {
            Console.WriteLine("Enter encryption key:");
            Console.Write("M11: ");
            int M11 = int.Parse(Console.ReadLine());
            Console.Write("M12: ");
            int M12 = int.Parse(Console.ReadLine());
            Console.Write("M21: ");
            int M21 = int.Parse(Console.ReadLine());
            Console.Write("M22: ");
            int M22 = int.Parse(Console.ReadLine());
            return new M2x2_Mod26(M11, M12, M21, M22);
        }

        
    }

    public struct M2x2_Mod26
    {
        int m11, m12;
        int m21, m22;
        public int M11 => m11;
        public int M12 => m12;
        public int M21 => m21;
        public int M22 => m22;

        public M2x2_Mod26(int m11, int m12, int m21, int m22)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
        }

        public static M2x2_Mod26 operator *(M2x2_Mod26 a, M2x2_Mod26 b)
        {
            return new M2x2_Mod26
            {
                m11 = (a.m11 * b.m11 + a.m12 * b.m21),
                m12 = (a.m11 * b.m12 + a.m12 * b.m22),
                m21 = (a.m21 * b.m11 + a.m22 * b.m21),
                m22 = (a.m21 * b.m12 + a.m22 * b.m22)
            } % 26;
        }

        public static M2x2_Mod26 operator *(int k, M2x2_Mod26 a)
        {
            return new M2x2_Mod26
            {
                m11 = k * a.m11,
                m12 = k * a.m12,
                m21 = k * a.m21,
                m22 = k * a.m22,
            } % 26;
        }

        public static M2x2_Mod26 operator +(M2x2_Mod26 a, M2x2_Mod26 b)
        {
            return new M2x2_Mod26
            {
                m11 = (a.m11 + b.m11),
                m12 = (a.m12 + b.m12),
                m21 = (a.m21 + b.m21),
                m22 = (a.m22 + b.m22)   
            } % 26;
        }

        public static M2x2_Mod26 operator %(M2x2_Mod26 A, int m)
        {
            return new M2x2_Mod26(mod(A.m11), mod(A.m12), mod(A.m21), mod(A.m22));
        }

        public int Det()
        {
            return mod(m11 * m22 - m12 * m21); // Add very large factor of 26^2 to fix negative determinants
        }

        public bool IsInvertible()
        {
            // Check if the determinant is relatively prime to 26
            return Det() % 2 != 0 && Det() % 13 != 0;
        }

        public M2x2_Mod26 Inverse()
        {
            // [a b; c d]^(-1) = det(A)^(-1) [d -b; -c a]
            if(IsInvertible())
                return Mod26Inverse[Det()] * new M2x2_Mod26(m22, -m12, -m21, m11) % 26;
            else
                throw new Exception("Matrix is not invertible!");
        }

        public string Print()
        {
            return $"[{m11} {m12}; {m21} {m22}]";
        }
        
        // fix Int modulus for negatives
        static int mod (int x) { return x - 26 * (int)Math.Floor((double)x / 26); }
            
        private Dictionary<int, int> Mod26Inverse = new Dictionary<int, int>()
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
        public static T[] MakeRepeatedCopy<T>(this T[] repeatedValue, int length)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < length; i++)
            {
                result.Add(repeatedValue[i % (repeatedValue.Length)]);
            }
            return result.ToArray();
        }
    }
}