/** COPYRIGHT 2023 
*  Brendan Nelligan
*  SNHU MAT260 Cryptology
**/
Console.WriteLine("Repeated Squaring Calculator");

while(true)
{
    Console.WriteLine("\nPress a key to choose:");
    Console.WriteLine("[E] Compute the modular exponentiation");
    Console.WriteLine("[Q] Quit");
    ConsoleKey key = Console.ReadKey().Key;

    if(key == ConsoleKey.E)
    {
        Console.Write("\nEnter the base: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Enter the exponent: ");
        int e = int.Parse(Console.ReadLine());

        Console.Write("Enter the modulus: ");
        int m = int.Parse(Console.ReadLine());

        Console.WriteLine($"Answer: {ModRepeatedSquares(a, e, m)}");
    }
    if(key == ConsoleKey.Q)
    {
        return;
    }
}


int ModRepeatedSquares(int a, int e, int m)
{
    int maxPow2 = (int)Math.Pow(2, (int)Math.Log2(e));
    if(maxPow2 > 1)
    {
        // Recursively combine with next power of 2 and the remainder
        return (int)Math.Pow(ModRepeatedSquares(a, maxPow2/2, m), 2) * ModRepeatedSquares(a, e - maxPow2, m) % m;
    }
    else 
    {
        return e == 1 ? a : 1;
    }
}