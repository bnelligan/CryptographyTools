namespace EuclideanAlgorithm
{
  /** COPYRIGHT 2023 
    *  Brendan Nelligan
    *  SNHU MAT260 Cryptology
    **/
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("~~~ Euclidean GCD Tool ~~~\n");

            while (true)
            {
                Console.WriteLine(
                "\n" +
                "Press a key to choose: \n" +
                "[D] compute GCD with s and t\n" +
                "[Q] quit\n");
                ConsoleKey selectionKey = Console.ReadKey().Key;
                Console.WriteLine();

                // Encryption mode
                if (selectionKey == ConsoleKey.D)
                {
                    Console.Write("Enter a: ");
                    int a = int.Parse(Console.ReadLine());

                    Console.Write("Enter b: ");
                    int b = int.Parse(Console.ReadLine());

                    int s, t;
                    int gcd = GCD(a, b, out s, out t);
                    Console.WriteLine($"GCD = {gcd}\ns = {s}\nt = {t}");
                }

                // Quit
                if(selectionKey == ConsoleKey.Q)
                {
                    return;
                }
            }
        }
        private static int GCD(int a, int b, out int s, out int t)
        {
            if(a == 0)
            {
                s = 0;
                t = 1;
                return b;
            }
            else
            {
                int inner_s, inner_t;
                int gcd = GCD(b%a, a, out inner_s, out inner_t);
                s = inner_t - (b/a) * inner_s;
                t = inner_s;
                return gcd;
            }
        }
    }
}
