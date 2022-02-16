namespace kioki_1_4;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Programm can");
            Console.WriteLine("Code");
            Console.WriteLine("Decode");
            Console.WriteLine("Exit");
            Console.WriteLine("Enter point to do: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "Code":
                    Console.Clear();
                    Console.WriteLine("Enter string to code: ");
                    var enteredString = Console.ReadLine();
                    Console.WriteLine("Enter key: ");
                    var key = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(CaesarEncryptor.Code(enteredString, key));
                    break;
                case "Decode":
                    Console.Clear();
                    Console.WriteLine("Enter string to decode: ");
                    enteredString = Console.ReadLine();
                    Console.WriteLine("Enter key: ");
                    key = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(CaesarEncryptor.Decode(enteredString, key));
                    break;
                case "Exit":
                    return;
                default:
                    Console.WriteLine("Try again...");
                    break;
            }
        }
    }
}
