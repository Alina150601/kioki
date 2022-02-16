namespace kioki_4;

public class PJW32Encryptor
{
    public static uint PJW32Hash(string input)
    {
        uint hash = 0;
        foreach (var item in input)
        {
            byte byte_of_data = (byte)item;
            hash = (hash << 4) + byte_of_data;
            uint h1 = hash & 0xf0000000;
            if (h1 != 0)
            {
                hash = ((hash ^ (h1 >> 24)) & (0xfffffff));
            }
        }

        return hash;
    }
}
