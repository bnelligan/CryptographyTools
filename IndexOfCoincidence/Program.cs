namespace IndexOfCoincidence
{
    /** COPYRIGHT 2023 
     *  Brendan Nelligan
     *  SNHU MAT260 Cryptology
     **/
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Index of Coincidence Calculator ~~~");

            Console.WriteLine("Enter the ciphertext: ");
            string ciphertext = Console.ReadLine();

            int[] letterFrequencyLookup = new int[26];
            int[] ciphertextAsInt = ciphertext.AsIntArray();
            int totalCount = ciphertextAsInt.Length;
            foreach(int letterInt in ciphertextAsInt)
            {
                letterFrequencyLookup[letterInt]++;
            }

            float indexOfCoincidence = letterFrequencyLookup
                .Sum(x => (x * (x - 1) / (float)totalCount / (totalCount - 1)));
            Console.WriteLine($"Index of coincidence: {indexOfCoincidence:F4}");

            int n = totalCount;
            float I = indexOfCoincidence;
            float keywordLength = 0.0265f * n / ((0.065f - I) + n * (I - 0.0385f));
            Console.WriteLine($"Estimated keyword length: {keywordLength}");
            Console.WriteLine($"Rounded keyword length: {Math.Round(keywordLength)}");
        }
    }

    public static class CipherExtensions
    {
        public static int[] AsIntArray(this string text)
        {
            return text.Replace(" ", "").Select(c => c.LetterToNumber()).ToArray();
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