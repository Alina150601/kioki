namespace kioki_4;
//HASH CODE PROJECT
public class Program
{
    public static void Main(string[] args)
    {
        var userInput = File.OpenText("file.txt").ReadToEnd();
        Console.WriteLine(PJW32Encryptor.PJW32Hash(userInput));
    }
}
