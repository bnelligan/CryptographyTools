/** COPYRIGHT 2023 
*  Brendan Nelligan
*  SNHU MAT260 Cryptology
**/
Console.WriteLine("Mod table generator");
while(true)
{
    Console.WriteLine("\nPress a key to choose:");
    Console.WriteLine("[B] Print the modular exponentiation table for a single base.");
    Console.WriteLine("[G] Find the generators for a modulus.");
    Console.WriteLine("[Q] Quit");

    ConsoleKey menuKey = Console.ReadKey().Key;

    if(menuKey == ConsoleKey.B)
    {
        Console.Write("\nEnter the base: ");
        int b = int.Parse(Console.ReadLine());
        Console.Write("Enter the modulus: ");
        int m = int.Parse(Console.ReadLine());

        for(int i = 1; i < m; i++)
        {
            int y = ModRepeatedSquares(b, i, m);
            Console.WriteLine($"{b}^{i} MOD {m} = {y}");
        }
    }
    if(menuKey == ConsoleKey.G)
    {
        Console.Write("\nEnter the modulus: ");
        int m = int.Parse(Console.ReadLine());
        List<int> generators = new();
        for(int i = 2; i < (m-1); i++)
        {
            HashSet<int> resultSet = new();
            for(int j = 1; j < m; j++)
            {
                resultSet.Add(ModRepeatedSquares(i, j, m));
            }
            if(resultSet.Count() == (m-1))
            {
                generators.Add(i);
            }
        }
        Console.Write($"There are {generators.Count()} generators for {m}: \n[{string.Join(',', generators)}]\n");
    }
    if(menuKey == ConsoleKey.Q)
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