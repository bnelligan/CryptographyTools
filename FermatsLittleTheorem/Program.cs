/** COPYRIGHT 2023 
*  Brendan Nelligan
*  SNHU MAT260 Cryptology
**/

Console.WriteLine("Fermat's Little Theorem Calculator\n");

Console.Write("Enter the base: ");
int a = int.Parse(Console.ReadLine());

Console.Write("Enter the exponent: ");
int e = int.Parse(Console.ReadLine());

Console.Write("Enter the modulus: ");
int m = int.Parse(Console.ReadLine());

if(e == m)
{
    // Second clause of Fermat's Little Theorem
    Console.WriteLine($"{a}^{e} MOD {m} = {a}");
}
else if(a % m != 0)
{
    // First clause of Fermat's Little Theorem
    Console.WriteLine($"{a}^{e} MOD {m} = {Math.Pow(a, e % (m-1)) % m}");
}
else
{
    throw new InvalidOperationException("Error: the base and modulus are not relatively prime!");
}