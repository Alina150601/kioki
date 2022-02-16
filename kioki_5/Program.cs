using System.Numerics;
//RSA SIGNATURE PROJECT
namespace kioki_5;

public class Program
{
    public static void Main(string[] args)
    {
        const int p = 89;
        const int q = 97;

        var message = File.OpenText("text.txt").ReadToEnd();
        var hash = FNV1AEncode.FNV1AHash(message) % (p * q);
        var (e, d, r) = KeyGenerator.GenerateKeys(p, q);
        var signature = BigInteger.Pow(hash, d) % r;

        var signatureHash = BigInteger.Pow(signature, e) % r;
        Console.WriteLine(signatureHash == hash ? "SUCCESS" : "FAIL");
    }
}
