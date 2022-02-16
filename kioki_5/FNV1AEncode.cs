namespace kioki_5;

public class FNV1AEncode
{
    public static uint FNV1AHash(string input)
    {
        const uint FNV_prime = 0x1000193;
        const uint FNV_offset_basic = 0x811C9DC5;
        uint hash = FNV_offset_basic;
        foreach (var item in input)
        {
            int byte_of_data = item;
            if (byte_of_data >= 0)
            {
                hash ^= (uint)byte_of_data;
                hash *= FNV_prime;
            }
        }

        return hash;
    }
}
