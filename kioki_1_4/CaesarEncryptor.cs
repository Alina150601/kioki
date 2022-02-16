using System.Text;

namespace kioki_1_4;

public class CaesarEncryptor
{
    private static string CodeDecode(string enteredString, int key)
    {
        var strBuilder = new StringBuilder();
        const string lang = "abcdefghijklmnopqrstuvwxyz";
        enteredString = enteredString.ToLower();
        foreach (var charic in enteredString)
        {
            var foundCharic = charic;
            var positionInLang = lang.IndexOf(foundCharic);
            if (positionInLang < 0)
            {
                //if not found, add without changes
                strBuilder.Append(foundCharic);
            }
            else
            {
                var newPosition = (positionInLang + key +26) % 26;
                var arrayOfCharsAlphabet = lang.ToCharArray();
                foundCharic = arrayOfCharsAlphabet[newPosition];
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
        return CodeDecode(enteredString, -key);
    }
}
