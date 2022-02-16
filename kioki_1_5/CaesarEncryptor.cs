using System.Text;

namespace kioki_1_5;

public class CaesarEncryptor
{
    public const int amountLetters = 33;
    private static string CodeDecode(string enteredString, int key)
    {
        var strBuilder = new StringBuilder();
        const string lang = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        enteredString = enteredString.ToLower();
        foreach (var charic in enteredString)
        {
            var foundCharic = charic;
            var positionInLang = lang.IndexOf(foundCharic) + 1;
            if (positionInLang == 0)
            {
                //if not found, add without changes
                strBuilder.Append(foundCharic);
            }
            else
            {
                var newPosition = (positionInLang * key) % amountLetters;
                var arrayOfCharsAlphabet = lang.ToCharArray();
                foundCharic = arrayOfCharsAlphabet[newPosition - 1];
                strBuilder.Append(foundCharic);
            }
        }

        return strBuilder.ToString();
    }

    public static string Code(string enteredString, int key)
    {
        return CodeDecode(enteredString, key);
    }

    public static string Decode(string enteredString, int key)
    {
        var rnd = new Random();
        var newKey = 0.1;
        while (newKey % 1 != 0)
        {
            newKey = (amountLetters * rnd.Next(1, 1000) + 1) / (double)key;
        }

        return CodeDecode(Code(enteredString,key), (int)newKey);
    }

    public static bool IsCoprime(int a, int b)
    {
        while (true)
        {
            if (a == b) return a == 1;
            if (a > b)
            {
                a -= b;
                continue;
            }

            var a1 = a;
            a = b - a;
            b = a1;
        }
    }
}
