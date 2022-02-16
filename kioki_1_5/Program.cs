namespace kioki_1_5;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Enter string to code/decode: ");
            var enteredString = Console.ReadLine();
            if (enteredString.ToLower().Trim() == "exit")
            {
                return;
            }
            bool success;
            int key;
            do
            {
                Console.WriteLine("Enter key: ");
                var input = Console.ReadLine();
                success = int.TryParse(input, out key);
            } while (!success || !IsKeyValid(key));

            Console.WriteLine(CaesarEncryptor.Code(enteredString, key));
            Console.WriteLine(CaesarEncryptor.Decode(enteredString, key));
        }
    }

    private static bool IsKeyValid(int key)
    {
        if (CaesarEncryptor.IsCoprime(key, CaesarEncryptor.amountLetters))
        {
            return true;
        }

        Console.WriteLine("Try again... key is not valid");
        return false;
    }
}
