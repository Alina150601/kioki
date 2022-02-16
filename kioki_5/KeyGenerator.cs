namespace kioki_5;

public class KeyGenerator
{
    public static (int, int, int) GenerateKeys(int p, int q)
    {
        var r = p * q;
        var fi = (p - 1) * (q - 1);
        var rnd = new Random();
        var e = rnd.Next(1, fi);
        while(!IsPrimeNumber(e) || !AreCoprimeNumbers(e, fi) || e == 2)
        {
            e = rnd.Next(1, fi);
        }
        var d = EuclidExtended(fi, e).Y;
        if (d < 0)
        {
            d += fi;
        }

        if ((e * d) % fi != 1)
        {
            throw new InvalidOperationException("Exponent d is wrong.");
        }

        return (e, d, r);
    }

    private static bool AreCoprimeNumbers(int a, int b)
    {
        return a == b
            ? a == 1
            : a > b
                ? AreCoprimeNumbers(a - b, b)
                : AreCoprimeNumbers(b - a, a);
    }

    private static bool IsPrimeNumber(int n)
    {
        var result = true;

        if (n > 1)
        {
            for (var i = 2u; i < n; i++)
            {
                if (n % i == 0)
                {
                    result = false;
                    break;
                }
            }
        }
        else
        {
            result = false;
        }

        return result;
    }

    private static (int X, int Y, int D) EuclidExtended(int a, int b)
    {
        if (!(a > b))
        {
            throw new ArgumentException("Must be: a > b");
        }

        var d0 = a;
        var d1 = b;
        var x0 = 1;
        var x1 = 0;
        var y0 = 0;
        var y1 = 1;
        while (d1 > 1)
        {
            var q = d0 / d1;
            var d2 = d0 % d1;
            var x2 = x0 - q * x1;
            var y2 = y0 - q * y1;
            d0 = d1;
            d1 = d2;
            x0 = x1;
            x1 = x2;
            y0 = y1;
            y1 = y2;
        }

        //для нашего алгоритма нужно y1
        return (x1, y1, d1);
    }
}
