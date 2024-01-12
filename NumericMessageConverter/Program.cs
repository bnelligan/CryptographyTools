/** COPYRIGHT 2023 
*  Brendan Nelligan
*  SNHU MAT260 Cryptology
**/
Console.WriteLine("Numeric Message Converter");
while(true)
{
    Console.WriteLine("\nPress a key to choose:");
    Console.WriteLine("[E] Encode text as number");
    Console.WriteLine("[D] Decode number as text");
    Console.WriteLine("[Q] Quit");
    
    ConsoleKey menuKey = Console.ReadKey().Key;
    if(menuKey == ConsoleKey.E)
    {
        Console.WriteLine("\nEnter the text to encode as number:");
        string plaintext = Console.ReadLine();

        int encodedNumber = plaintext.EncodeNumeric();
        Console.WriteLine($"Encoded number: {encodedNumber}");

        string textCheck = encodedNumber.DecodeNumeric();
        Console.WriteLine($"Decoded text for checking: {textCheck}");
    }
    if(menuKey == ConsoleKey.D)
    {
        Console.WriteLine("\nEnter the number to decode as text:");
        if(int.TryParse(Console.ReadLine(), out int number))
        {
            string plaintext = number.DecodeNumeric();
            Console.WriteLine($"Decoded text: {plaintext}");
            int numberCheck = plaintext.EncodeNumeric();
            Console.WriteLine($"Encoded number for checking: {numberCheck}");
        }
    }
    if(menuKey == ConsoleKey.Q) 
    { 
        return; 
    }
}

public static class CipherExtensions
{
    public static int EncodeNumeric(this string text)
    {
        return text.AsIntArray().Reverse()
        .Select((c, i) => c * (int)Math.Pow(26, i))
        .Sum();
    }

    public static string DecodeNumeric(this int numeric)
    {
        string text = "";
        while(numeric > 0)
        {
            char nextLetter = (numeric % 26).NumberToLetter();
            text += nextLetter;
            numeric /= 26;
        }
        return string.Concat(text.Reverse());
    }
    public static int[] AsIntArray(this string text)
    {
        return text.ToUpper().Where((c) => c >= 'A' && c <= 'Z').Select(c => c.LetterToNumber()).ToArray();
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