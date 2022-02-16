using System.Collections;
using System.Text;

namespace kioki_2;

public class Program
{
    public static void Main()
    {
        //------------------------------Ввод значений-----------------------------------------------
        var userInput = File.OpenText("file.txt").ReadToEnd();
        var finalCode = new List<char>();
        var finalDecode = new List<char>();
        foreach (var letter in userInput)
        {
            var i = 0;
            var poradok = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var bytesArray = Encoding.Default.GetBytes(letter.ToString());
            var text = new BitArray(bytesArray);

            var K = new List<byte> { 1, 0, 0, 0, 1, 1, 0, 1, 0, 0 };

            //------------------------------Статичные ключи---------------------------------------------
            var IP = new List<int> { 2, 6, 3, 1, 4, 8, 5, 7 };
            var P10 = new List<int> { 3, 5, 2, 7, 4, 10, 1, 9, 8, 6 };
            var P8 = new List<int> { 6, 3, 7, 4, 8, 5, 10, 9 };
            var P4 = new List<int> { 2, 4, 3, 1 };
            var EP = new List<int> { 4, 1, 2, 3, 2, 3, 4, 1 };
            var S0 = new int[][]
            {
                new[] { 1, 0, 3, 2 },
                new[] { 3, 2, 1, 0 },
                new[] { 0, 2, 1, 3 },
                new[] { 3, 1, 3, 2 }
            };
            var S1 = new int[][]
            {
                new[] { 0, 1, 2, 3 },
                new[] { 2, 0, 1, 3 },
                new[] { 3, 0, 1, 0 },
                new[] { 2, 1, 0, 3 }
            };
            //------------------------------Вычисление ключей-------------------------------------------
            var P1 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
            var K1 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0 };
            var K2 = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0 };
            var KK = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //------P1
            for (i = 0; i < 8; i++)
            {
                P1[IP[i] - 1] = poradok[i];
            }

            //------K1
            //1
            for (i = 0; i < 10; i++)
            {
                KK[i] = K[P10[i] - 1];
            }

            //2
            var KK1 = new List<byte> { 0, 0, 0, 0, 0 };
            var KK2 = new List<byte> { 0, 0, 0, 0, 0 };

            for (i = 0; i < 5; i++)
            {
                KK1[i] = KK[i];
                KK2[i] = KK[i + 5];
            }

            int size = 5;
            var temp = KK1[0];
            for (i = 0, size--; i < size; i++) KK1[i] = KK1[i + 1];
            KK1[size] = temp;
            size = 5;
            temp = KK2[0];
            for (i = 0, size--; i < size; i++) KK2[i] = KK2[i + 1];
            KK2[size] = temp;
            for (i = 0; i < 5; i++)
            {
                KK[i] = KK1[i];
                KK[i + 5] = KK2[i];
            }

            //3
            for (i = 0; i < 8; i++)
            {
                K1[i] = KK[P8[i] - 1];
            }

            //------K2
            //1
            size = 5;
            temp = KK1[0];
            for (i = 0, size--; i < size; i++) KK1[i] = KK1[i + 1];
            KK1[size] = temp;
            size = 5;
            temp = KK2[0];
            for (i = 0, size--; i < size; i++) KK2[i] = KK2[i + 1];
            KK2[size] = temp;

            size = 5;
            temp = KK1[0];
            for (i = 0, size--; i < size; i++) KK1[i] = KK1[i + 1];
            KK1[size] = temp;
            size = 5;
            temp = KK2[0];
            for (i = 0, size--; i < size; i++) KK2[i] = KK2[i + 1];
            KK2[size] = temp;
            for (i = 0; i < 5; i++)
            {
                KK[i] = KK1[i];
                KK[i + 5] = KK2[i];
            }

            //2
            for (i = 0; i < 8; i++)
            {
                K2[i] = KK[P8[i] - 1];
            }

            //------------------------------Шифрование--------------------------------------------------

            //1 IP
            var cypher = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (i = 0; i < 8; i++)
            {
                cypher[i] = text[IP[i] - 1]
                    ? (byte)1
                    : (byte)0;
            }

            //2 split on two parts
            var EP1 = new List<byte> { 0, 0, 0, 0 };
            var EP2 = new List<byte> { 0, 0, 0, 0 };
            //int k = 0;
            for (i = 0; i < 4; i++)
            {
                EP1[i] = cypher[i];
                EP2[i] = cypher[i + 4];
            }

            for (i = 0; i < 8; i++)
            {
                cypher[i] = EP2[EP[i] - 1];
            }

            //3 XOR1 (encryption round)
            var l = true;
            var cyp = new List<bool>(Enumerable.Repeat(true, 8));
            for (i = 0; i < 8; i++)
            {
                if (cypher[i] == K1[i]) l = false;
                else l = true;
                cyp[i] = l;
            }

            //4 SBOX1
            var Brow = new List<bool> { true, true };
            var Bcol = new List<bool> { true, true };
            int row = 0;
            int col = 0;
            Brow[0] = cyp[0];
            Brow[1] = cyp[3];
            Bcol[0] = cyp[1];
            Bcol[1] = cyp[2];

            if (!Brow[0] & !Brow[1]) row = 0;
            else if (!Brow[0] & Brow[1]) row = 1;
            else if (Brow[0] & !Brow[1]) row = 2;
            else if (Brow[0] & Brow[1]) row = 3;

            if ((Bcol[0] == false) & (Bcol[1] == false)) col = 0;
            else if ((Bcol[0] == false) & (Bcol[1])) col = 1;
            else if ((Bcol[0]) & (Bcol[1] == false)) col = 2;
            else if ((Bcol[0]) & (Bcol[1])) col = 3;

            var BF1 = new List<bool> { true, true };
            var BF2 = new List<bool> { true, true };
            var F = new List<bool> { true, true, true, true };
            int F1, F2;
            F1 = S0[row][col];
            if (F1 == 0)
            {
                BF1[0] = false;
                BF1[1] = false;
            }
            else if (F1 == 1)
            {
                BF1[0] = false;
                BF1[1] = true;
            }
            else if (F1 == 2)
            {
                BF1[0] = true;
                BF1[1] = false;
            }
            else if (F1 == 3)
            {
                BF1[0] = true;
                BF1[1] = true;
            }

            //- SBOX2
            Brow[0] = cyp[4];
            Brow[1] = cyp[7];
            Bcol[0] = cyp[5];
            Bcol[1] = cyp[6];

            if (!Brow[0] & !Brow[1]) row = 0;
            else if (!Brow[0] & Brow[1]) row = 1;
            else if (Brow[0] & !Brow[1]) row = 2;
            else if (Brow[0] & Brow[1]) row = 3;

            if (!Bcol[0] & !Bcol[1]) col = 0;
            else if (!Bcol[0] & Bcol[1]) col = 1;
            else if (Bcol[0] & !Bcol[1]) col = 2;
            else if (Bcol[0] & Bcol[1]) col = 3;

            F2 = S1[row][col];
            if (F2 == 0)
            {
                BF2[0] = false;
                BF2[1] = false;
            }
            else if (F2 == 1)
            {
                BF2[0] = false;
                BF2[1] = true;
            }
            else if (F2 == 2)
            {
                BF2[0] = true;
                BF2[1] = false;
            }
            else if (F2 == 3)
            {
                BF2[0] = true;
                BF2[1] = true;
            }

            F[0] = BF1[0];
            F[1] = BF1[1];
            F[2] = BF2[0];
            F[3] = BF2[1];

            //5 P4
            var FF = new List<bool>(Enumerable.Repeat(true, 4));
            for (i = 0; i < 4; i++)
            {
                FF[i] = F[P4[i] - 1];
            }

            //6 XOR
            var FFF = new List<byte> { 0, 0, 0, 0 };
            for (i = 0; i < 4; i++)
            {
                var FFByte = FF[i]
                    ? (byte)1
                    : (byte)0;
                FFF[i] = (byte)(FFByte ^ EP1[i]);
            }

            //7
            for (i = 0; i < 4; i++)
            {
                cypher[i] = FFF[i];
                cypher[i + 4] = EP2[i];
            }

            //8
            var Swap1 = new List<byte> { 0, 0, 0, 0 };
            var Swap2 = new List<byte> { 0, 0, 0, 0 };
            for (i = 0; i < 4; i++)
            {
                Swap1[i] = cypher[i];
                Swap2[i] = cypher[i + 4];
            }

            for (i = 0; i < 4; i++)
            {
                cypher[i] = Swap2[i];
                cypher[i + 4] = Swap1[i];
            }

            //9
            for (i = 0; i < 4; i++)
            {
                EP1[i] = cypher[i];
                EP2[i] = cypher[i + 4];
            }

            for (i = 0; i < 8; i++)
            {
                cypher[i] = EP2[EP[i] - 1];
            }

            //10 XOR2
            for (i = 0; i < 8; i++)
            {
                if (cypher[i] == K2[i]) l = false;
                else l = true;
                cyp[i] = l;
            }

            //11
            Brow[0] = cyp[0];
            Brow[1] = cyp[3];
            Bcol[0] = cyp[1];
            Bcol[1] = cyp[2];

            if (!Brow[0] & !Brow[1]) row = 0;
            else if (!Brow[0] & Brow[1]) row = 1;
            else if (Brow[0] & !Brow[1]) row = 2;
            else if (Brow[0] & Brow[1]) row = 3;

            if (!Bcol[0] & !Bcol[1]) col = 0;
            else if (!Bcol[0] & Bcol[1]) col = 1;
            else if (Bcol[0] & !Bcol[1]) col = 2;
            else if (Bcol[0] & Bcol[1]) col = 3;

            F1 = S0[row][col];
            if (F1 == 0)
            {
                BF1[0] = false;
                BF1[1] = false;
            }
            else if (F1 == 1)
            {
                BF1[0] = false;
                BF1[1] = true;
            }
            else if (F1 == 2)
            {
                BF1[0] = true;
                BF1[1] = false;
            }
            else if (F1 == 3)
            {
                BF1[0] = true;
                BF1[1] = true;
            }

            //-
            Brow[0] = cyp[4];
            Brow[1] = cyp[7];
            Bcol[0] = cyp[5];
            Bcol[1] = cyp[6];

            if (!Brow[0] & !Brow[1]) row = 0;
            else if (!Brow[0] & Brow[1]) row = 1;
            else if (Brow[0] & !Brow[1]) row = 2;
            else if (Brow[0] & Brow[1]) row = 3;

            if (!Bcol[0] & !Bcol[1]) col = 0;
            else if (!Bcol[0] & Bcol[1]) col = 1;
            else if (Bcol[0] & !Bcol[1]) col = 2;
            else if (Bcol[0] & Bcol[1]) col = 3;

            F2 = S1[row][col];
            if (F2 == 0)
            {
                BF2[0] = false;
                BF2[1] = false;
            }
            else if (F2 == 1)
            {
                BF2[0] = false;
                BF2[1] = true;
            }
            else if (F2 == 2)
            {
                BF2[0] = true;
                BF2[1] = false;
            }
            else if (F2 == 3)
            {
                BF2[0] = true;
                BF2[1] = true;
            }

            F[0] = BF1[0];
            F[1] = BF1[1];
            F[2] = BF2[0];
            F[3] = BF2[1];

            //12 P4
            for (i = 0; i < 4; i++)
            {
                FF[i] = F[P4[i] - 1];
            }

            //13
            for (i = 0; i < 4; i++)
            {
                var FFByte = FF[i]
                    ? (byte)1
                    : (byte)0;
                FFF[i] = (byte)(FFByte ^ EP1[i]);
            }

            //14
            for (i = 0; i < 4; i++)
            {
                cypher[i] = FFF[i];
                cypher[i + 4] = EP2[i];
            }

            //15
            for (i = 0; i < 8; i++)
            {
                text[i] = cypher[P1[i] - 1] == 1;
            }

            var nashByte1 = ConvertToByte(text);
            var nashChar1 = (char)nashByte1;
            finalCode.Add(nashChar1);
            //------------------------------Раcшифровка-------------------------------------------------

            //1
            for (i = 0; i < 8; i++)
            {
                cypher[i] = text[IP[i] - 1]
                    ? (byte)1
                    : (byte)0;
            }

            //2
            for (i = 0; i < 4; i++)
            {
                EP1[i] = cypher[i];
                EP2[i] = cypher[i + 4];
            }

            for (i = 0; i < 8; i++)
            {
                cypher[i] = EP2[EP[i] - 1];
            }

            //3 XOR1
            for (i = 0; i < 8; i++)
            {
                if (cypher[i] == K2[i]) l = false;
                else l = true;
                cyp[i] = l;
            }

            //4
            Brow[0] = cyp[0];
            Brow[1] = cyp[3];
            Bcol[0] = cyp[1];
            Bcol[1] = cyp[2];

            if (!Brow[0] & !Brow[1]) row = 0;
            else if (!Brow[0] & Brow[1]) row = 1;
            else if (Brow[0] & !Brow[1]) row = 2;
            else if (Brow[0] & Brow[1]) row = 3;

            if (!Bcol[0] & !Bcol[1]) col = 0;
            else if (!Bcol[0] & Bcol[1]) col = 1;
            else if (Bcol[0] & !Bcol[1]) col = 2;
            else if (Bcol[0] & Bcol[1]) col = 3;

            F1 = S0[row][col];
            if (F1 == 0)
            {
                BF1[0] = false;
                BF1[1] = false;
            }
            else if (F1 == 1)
            {
                BF1[0] = false;
                BF1[1] = true;
            }
            else if (F1 == 2)
            {
                BF1[0] = true;
                BF1[1] = false;
            }
            else if (F1 == 3)
            {
                BF1[0] = true;
                BF1[1] = true;
            }

            //-
            Brow[0] = cyp[4];
            Brow[1] = cyp[7];
            Bcol[0] = cyp[5];
            Bcol[1] = cyp[6];

            if (!Brow[0] & !Brow[1]) row = 0;
            else if (!Brow[0] & Brow[1]) row = 1;
            else if (Brow[0] & !Brow[1]) row = 2;
            else if (Brow[0] & Brow[1]) row = 3;

            if (!Bcol[0] & !Bcol[1]) col = 0;
            else if (!Bcol[0] & Bcol[1]) col = 1;
            else if (Bcol[0] & !Bcol[1]) col = 2;
            else if (Bcol[0] & Bcol[1]) col = 3;

            F2 = S1[row][col];
            if (F2 == 0)
            {
                BF2[0] = false;
                BF2[1] = false;
            }
            else if (F2 == 1)
            {
                BF2[0] = false;
                BF2[1] = true;
            }
            else if (F2 == 2)
            {
                BF2[0] = true;
                BF2[1] = false;
            }
            else if (F2 == 3)
            {
                BF2[0] = true;
                BF2[1] = true;
            }

            F[0] = BF1[0];
            F[1] = BF1[1];
            F[2] = BF2[0];
            F[3] = BF2[1];

            //5
            for (i = 0; i < 4; i++)
            {
                FF[i] = F[P4[i] - 1];
            }

            //6
            for (i = 0; i < 4; i++)
            {
                var FFByte = FF[i]
                    ? (byte)1
                    : (byte)0;
                FFF[i] = (byte)(FFByte ^ EP1[i]);
            }

            //7
            for (i = 0; i < 4; i++)
            {
                cypher[i] = FFF[i];
                cypher[i + 4] = EP2[i];
            }

            //8
            for (i = 0; i < 4; i++)
            {
                Swap1[i] = cypher[i];
                Swap2[i] = cypher[i + 4];
            }

            for (i = 0; i < 4; i++)
            {
                cypher[i] = Swap2[i];
                cypher[i + 4] = Swap1[i];
            }

            //9
            for (i = 0; i < 4; i++)
            {
                EP1[i] = cypher[i];
                EP2[i] = cypher[i + 4];
            }

            for (i = 0; i < 8; i++)
            {
                cypher[i] = EP2[EP[i] - 1];
            }

            //10 XOR2
            for (i = 0; i < 8; i++)
            {
                if (cypher[i] == K1[i]) l = false;
                else l = true;
                cyp[i] = l;
            }

            //11
            Brow[0] = cyp[0];
            Brow[1] = cyp[3];
            Bcol[0] = cyp[1];
            Bcol[1] = cyp[2];

            if (!Brow[0] & !Brow[1]) row = 0;
            else if (!Brow[0] & Brow[1]) row = 1;
            else if (Brow[0] & !Brow[1]) row = 2;
            else if (Brow[0] & Brow[1]) row = 3;

            if (!Bcol[0] & !Bcol[1]) col = 0;
            else if (!Bcol[0] & Bcol[1]) col = 1;
            else if (Bcol[0] & !Bcol[1]) col = 2;
            else if (Bcol[0] & Bcol[1]) col = 3;

            F1 = S0[row][col];
            if (F1 == 0)
            {
                BF1[0] = false;
                BF1[1] = false;
            }
            else if (F1 == 1)
            {
                BF1[0] = false;
                BF1[1] = true;
            }
            else if (F1 == 2)
            {
                BF1[0] = true;
                BF1[1] = false;
            }
            else if (F1 == 3)
            {
                BF1[0] = true;
                BF1[1] = true;
            }

            //-
            Brow[0] = cyp[4];
            Brow[1] = cyp[7];
            Bcol[0] = cyp[5];
            Bcol[1] = cyp[6];

            if (!Brow[0] & !Brow[1]) row = 0;
            else if (!Brow[0] & Brow[1]) row = 1;
            else if (Brow[0] & !Brow[1]) row = 2;
            else if (Brow[0] & Brow[1]) row = 3;

            if (!Bcol[0] & !Bcol[1]) col = 0;
            else if (!Bcol[0] & Bcol[1]) col = 1;
            else if (Bcol[0] & !Bcol[1]) col = 2;
            else if (Bcol[0] & Bcol[1]) col = 3;

            F2 = S1[row][col];
            if (F2 == 0)
            {
                BF2[0] = false;
                BF2[1] = false;
            }
            else if (F2 == 1)
            {
                BF2[0] = false;
                BF2[1] = true;
            }
            else if (F2 == 2)
            {
                BF2[0] = true;
                BF2[1] = false;
            }
            else if (F2 == 3)
            {
                BF2[0] = true;
                BF2[1] = true;
            }

            F[0] = BF1[0];
            F[1] = BF1[1];
            F[2] = BF2[0];
            F[3] = BF2[1];

            //12
            for (i = 0; i < 4; i++)
            {
                FF[i] = F[P4[i] - 1];
            }

            //13
            for (i = 0; i < 4; i++)
            {
                var FFByte = FF[i]
                    ? (byte)1
                    : (byte)0;
                FFF[i] = (byte)(FFByte ^ EP1[i]);
            }

            //14
            for (i = 0; i < 4; i++)
            {
                cypher[i] = FFF[i];
                cypher[i + 4] = EP2[i];
            }

            //15
            for (i = 0; i < 8; i++)
            {
                text[i] = cypher[P1[i] - 1] == 1;
            }

            var nashByte = ConvertToByte(text);
            var nashChar = (char)nashByte;
            //Console.WriteLine("Декодирование");
            //Console.WriteLine(nashChar);
            finalDecode.Add(nashChar);
            //------------------------------Закрытие программы------------------------------------------
        }

        Console.WriteLine("Кодирование:");
        Console.WriteLine(string.Concat(finalCode));
        Console.WriteLine("Декодирование:");
        Console.WriteLine(string.Concat(finalDecode));
        return;
    }

    static byte ConvertToByte(BitArray bits)
    {
        if (bits.Count != 8)
        {
            throw new ArgumentException("bits");
        }

        byte[] bytes = new byte[1];
        bits.CopyTo(bytes, 0);
        return bytes[0];
    }
}
