// S-DES.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

using System.Text;

public class Program
{
    public static void Main()
    {
        //------------------------------Ввод значений-----------------------------------------------
        var i = 0;
        var poradok = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        //var enteredText = File.OpenText("file.txt").ReadToEnd();
        var enteredText = "alinkaaa";
        var bytes = Encoding.ASCII.GetBytes(enteredText).Select(x => (int)x);
        var text = bytes.Take(8).ToList();
        // start cicle

        Console.WriteLine("Введите ключ(10 бит)");
        var userInput = Console.ReadLine()
            .ToCharArray();
        if (userInput.Any(x => x != '0' && x != '1')) return;
        var K = userInput.Select(x => int.Parse(x.ToString())).ToArray();



            //------------------------------Статичные ключи---------------------------------------------

            var IP = new List<int> { 4, 8, 5, 7, 2, 6, 3, 1 };

            //int IP[8] = { 2,6,3,1,4,8,5,7 };
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
            var K1 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
            var K2 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
            var KK = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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
            var KK1 = new List<int> { 0, 0, 0, 0, 0 };
            var KK2 = new List<int> { 0, 0, 0, 0, 0 };
            for (i = 0; i < 5; i++)
            {
                KK1[i] = KK[i];
                KK2[i] = KK[i + 5];
            }

            var size = 5;
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

            for (i = 0; i < 8; i++)
            {
                Console.Write(K1[i]);
            }

            Console.WriteLine();

            Console.WriteLine("------------------------------");
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

            Console.WriteLine("------------------------------");

            //------------------------------Шифрование--------------------------------------------------
            // Console.WriteLine("Текст:" );
            // for (i = 0; i < 8; i++)
            // {
            //     Console.Write(text[i]);
            // }
            // Console.WriteLine();

            //1
            var cypher = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };

            //2
            var EP1 = new List<int> { 0, 0, 0, 0 };
            var EP2 = new List<int> { 0, 0, 0, 0 };
            var k = 0;
            for (i = 0; i < 4; i++)
            {
                EP1[i] = cypher[i];
                EP2[i] = cypher[i + 4];
            }


            //3
            var l = 0;
            var cyp = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
            for (i = 0; i < 8; i++)
            {
                if (cypher[i] == K1[i]) l = 0;
                else l = 1;
                cyp[i] = l;
            }


            //4
            var Brow = new List<int> { 0, 0 };
            var Bcol = new List<int> { 0, 0 };
            var row = 0;
            var col = 0;
            Brow[0] = cyp[0];
            Brow[1] = cyp[3];
            Bcol[0] = cyp[1];
            Bcol[1] = cyp[2];

            if ((Brow[0] == 0) & (Brow[1] == 0)) row = 0;
            else if ((Brow[0] == 0) & (Brow[1] == 1)) row = 1;
            else if ((Brow[0] == 1) & (Brow[1] == 0)) row = 2;
            else row = 3;

            if ((Bcol[0] == 0) & (Bcol[1] == 0)) col = 0;
            else if ((Bcol[0] == 0) & (Bcol[1] == 1)) col = 1;
            else if ((Bcol[0] == 1) & (Bcol[1] == 0)) col = 2;
            else col = 3;

            var BF1 = new List<int> { 0, 0 };
            var BF2 = new List<int> { 0, 0 };
            var F = new List<int> { 0, 0, 0, 0 };
            var F1 = 0;
            var F2 = 0;
            F1 = S0[row][col];
            if (F1 == 0)
            {
                BF1[0] = 0;
                BF1[1] = 0;
            }
            else if (F1 == 1)
            {
                BF1[0] = 0;
                BF1[1] = 1;
            }
            else if (F1 == 2)
            {
                BF1[0] = 1;
                BF1[1] = 0;
            }
            else if (F1 == 3)
            {
                BF1[0] = 1;
                BF1[1] = 1;
            }

            //-
            Brow[0] = cyp[4];
            Brow[1] = cyp[7];
            Bcol[0] = cyp[5];
            Bcol[1] = cyp[6];

            if ((Brow[0] == 0) & (Brow[1] == 0)) row = 0;
            else if ((Brow[0] == 0) & (Brow[1] == 1)) row = 1;
            else if ((Brow[0] == 1) & (Brow[1] == 0)) row = 2;
            else if ((Brow[0] == 1) & (Brow[1] == 1)) row = 3;

            if ((Bcol[0] == 0) & (Bcol[1] == 0)) col = 0;
            else if ((Bcol[0] == 0) & (Bcol[1] == 1)) col = 1;
            else if ((Bcol[0] == 1) & (Bcol[1] == 0)) col = 2;
            else if ((Bcol[0] == 1) & (Bcol[1] == 1)) col = 3;

            F2 = S1[row][col];
            if (F2 == 0)
            {
                BF2[0] = 0;
                BF2[1] = 0;
            }
            else if (F2 == 1)
            {
                BF2[0] = 0;
                BF2[1] = 1;
            }
            else if (F2 == 2)
            {
                BF2[0] = 1;
                BF2[1] = 0;
            }
            else if (F2 == 3)
            {
                BF2[0] = 1;
                BF2[1] = 1;
            }

            F[0] = BF1[0];
            F[1] = BF1[1];
            F[2] = BF2[0];
            F[3] = BF2[1];

            //5
            Console.WriteLine("Результат перестановки Р4:");
            var FF = new List<int> { 0, 0, 0, 0 };
            for (i = 0; i < 4; i++)
            {
                FF[i] = F[P4[i] - 1];
            }


            //6
            var FFF = new List<int> { 0, 0, 0, 0 };
            for (i = 0; i < 4; i++)
            {
                FFF[i] = FF[i] ^ EP1[i];
            }

            //7
            for (i = 0; i < 4; i++)
            {
                cypher[i] = FFF[i];
                cypher[i + 4] = EP2[i];
            }


            //8
            var Swap1 = new List<int> { 0, 0, 0, 0 };
            var Swap2 = new List<int> { 0, 0, 0, 0 };
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

            //10
            for (i = 0; i < 8; i++)
            {
                if (cypher[i] == K2[i]) l = 0;
                else l = 1;
                cyp[i] = l;
            }


            //11
            Brow[0] = cyp[0];
            Brow[1] = cyp[3];
            Bcol[0] = cyp[1];
            Bcol[1] = cyp[2];

            if ((Brow[0] == 0) & (Brow[1] == 0)) row = 0;
            else if ((Brow[0] == 0) & (Brow[1] == 1)) row = 1;
            else if ((Brow[0] == 1) & (Brow[1] == 0)) row = 2;
            else if ((Brow[0] == 1) & (Brow[1] == 1)) row = 3;

            if ((Bcol[0] == 0) & (Bcol[1] == 0)) col = 0;
            else if ((Bcol[0] == 0) & (Bcol[1] == 1)) col = 1;
            else if ((Bcol[0] == 1) & (Bcol[1] == 0)) col = 2;
            else if ((Bcol[0] == 1) & (Bcol[1] == 1)) col = 3;

            F1 = S0[row][col];
            if (F1 == 0)
            {
                BF1[0] = 0;
                BF1[1] = 0;
            }
            else if (F1 == 1)
            {
                BF1[0] = 0;
                BF1[1] = 1;
            }
            else if (F1 == 2)
            {
                BF1[0] = 1;
                BF1[1] = 0;
            }
            else if (F1 == 3)
            {
                BF1[0] = 1;
                BF1[1] = 1;
            }

            //-
            Brow[0] = cyp[4];
            Brow[1] = cyp[7];
            Bcol[0] = cyp[5];
            Bcol[1] = cyp[6];

            if ((Brow[0] == 0) & (Brow[1] == 0)) row = 0;
            else if ((Brow[0] == 0) & (Brow[1] == 1)) row = 1;
            else if ((Brow[0] == 1) & (Brow[1] == 0)) row = 2;
            else if ((Brow[0] == 1) & (Brow[1] == 1)) row = 3;

            if ((Bcol[0] == 0) & (Bcol[1] == 0)) col = 0;
            else if ((Bcol[0] == 0) & (Bcol[1] == 1)) col = 1;
            else if ((Bcol[0] == 1) & (Bcol[1] == 0)) col = 2;
            else if ((Bcol[0] == 1) & (Bcol[1] == 1)) col = 3;

            F2 = S1[row][col];
            if (F2 == 0)
            {
                BF2[0] = 0;
                BF2[1] = 0;
            }
            else if (F2 == 1)
            {
                BF2[0] = 0;
                BF2[1] = 1;
            }
            else if (F2 == 2)
            {
                BF2[0] = 1;
                BF2[1] = 0;
            }
            else if (F2 == 3)
            {
                BF2[0] = 1;
                BF2[1] = 1;
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
                FFF[i] = FF[i] ^ EP1[i];
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
                text[i] = cypher[P1[i] - 1];
            }

            Console.WriteLine();
            Console.WriteLine("----------------------------");

            /*
            Console.WriteLine(S0[1][1] );
            string S;
            S= bitset<2>(S0[1][1]).to_string();
            cout<<bitset<2>(S0[1][1]).to_string()<<endl;
            */
            //------------------------------Розшифровка-------------------------------------------------
            Console.WriteLine("Шифр:");
            for (i = 0; i < 8; i++)
            {
                Console.Write(text[i]);
            }


            //1
            Console.WriteLine("Результат IP перестановки:");
            for (i = 0; i < 8; i++)
            {
                cypher[i] = text[IP[i] - 1];
                Console.Write(cypher[i]);
            }

            Console.WriteLine();

            //2
            Console.WriteLine("Результат E/P перестановки:");
            for (i = 0; i < 4; i++)
            {
                EP1[i] = cypher[i];
                EP2[i] = cypher[i + 4];
            }

            for (i = 0; i < 8; i++)
            {
                cypher[i] = EP2[EP[i] - 1];
                Console.Write(cypher[i]);
            }

            Console.WriteLine();


            //3
            Console.WriteLine("Результат XOR(cypher+K2):");
            for (i = 0; i < 8; i++)
            {
                if (cypher[i] == K2[i]) l = 0;
                else l = 1;
                cyp[i] = l;
                Console.Write(cyp[i]);
            }

            Console.WriteLine();


            //4
            Brow[0] = cyp[0];
            Brow[1] = cyp[3];
            Bcol[0] = cyp[1];
            Bcol[1] = cyp[2];

            if ((Brow[0] == 0) & (Brow[1] == 0)) row = 0;
            else if ((Brow[0] == 0) & (Brow[1] == 1)) row = 1;
            else if ((Brow[0] == 1) & (Brow[1] == 0)) row = 2;
            else if ((Brow[0] == 1) & (Brow[1] == 1)) row = 3;

            if ((Bcol[0] == 0) & (Bcol[1] == 0)) col = 0;
            else if ((Bcol[0] == 0) & (Bcol[1] == 1)) col = 1;
            else if ((Bcol[0] == 1) & (Bcol[1] == 0)) col = 2;
            else if ((Bcol[0] == 1) & (Bcol[1] == 1)) col = 3;

            F1 = S0[row][col];
            if (F1 == 0)
            {
                BF1[0] = 0;
                BF1[1] = 0;
            }
            else if (F1 == 1)
            {
                BF1[0] = 0;
                BF1[1] = 1;
            }
            else if (F1 == 2)
            {
                BF1[0] = 1;
                BF1[1] = 0;
            }
            else if (F1 == 3)
            {
                BF1[0] = 1;
                BF1[1] = 1;
            }

            //-
            Brow[0] = cyp[4];
            Brow[1] = cyp[7];
            Bcol[0] = cyp[5];
            Bcol[1] = cyp[6];

            if ((Brow[0] == 0) & (Brow[1] == 0)) row = 0;
            else if ((Brow[0] == 0) & (Brow[1] == 1)) row = 1;
            else if ((Brow[0] == 1) & (Brow[1] == 0)) row = 2;
            else if ((Brow[0] == 1) & (Brow[1] == 1)) row = 3;

            if ((Bcol[0] == 0) & (Bcol[1] == 0)) col = 0;
            else if ((Bcol[0] == 0) & (Bcol[1] == 1)) col = 1;
            else if ((Bcol[0] == 1) & (Bcol[1] == 0)) col = 2;
            else if ((Bcol[0] == 1) & (Bcol[1] == 1)) col = 3;

            F2 = S1[row][col];
            if (F2 == 0)
            {
                BF2[0] = 0;
                BF2[1] = 0;
            }
            else if (F2 == 1)
            {
                BF2[0] = 0;
                BF2[1] = 1;
            }
            else if (F2 == 2)
            {
                BF2[0] = 1;
                BF2[1] = 0;
            }
            else if (F2 == 3)
            {
                BF2[0] = 1;
                BF2[1] = 1;
            }

            F[0] = BF1[0];
            F[1] = BF1[1];
            F[2] = BF2[0];
            F[3] = BF2[1];
            Console.WriteLine("Результат дешифрования по таблицах S0/S1(F)");
            for (i = 0; i < 4; i++)
            {
                Console.Write(F[i]);
            }

            Console.WriteLine();

            //5
            Console.WriteLine("Результат перестановки Р4:");
            for (i = 0; i < 4; i++)
            {
                FF[i] = F[P4[i] - 1];
                Console.Write(FF[i]);
            }

            Console.WriteLine();

            //6
            Console.WriteLine("Результат XOR(F+IP(1-4)):");
            for (i = 0; i < 4; i++)
            {
                FFF[i] = FF[i] ^ EP1[i];
                Console.Write(FFF[i]);
            }

            Console.WriteLine();

            //7
            Console.WriteLine("Результат сумирования с правой частью(F+IP(4-8))");
            for (i = 0; i < 4; i++)
            {
                cypher[i] = FFF[i];
                cypher[i + 4] = EP2[i];
            }

            for (i = 0; i < 8; i++)
            {
                Console.Write(cypher[i]);
            }

            Console.WriteLine();

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

            Console.WriteLine("Результат свапа(по 4 бита)");
            for (i = 0; i < 8; i++)
            {
                Console.Write(cypher[i]);
            }

            Console.WriteLine();

            //9
            Console.WriteLine("Результат E/P перестановки:");
            for (i = 0; i < 4; i++)
            {
                EP1[i] = cypher[i];
                EP2[i] = cypher[i + 4];
            }

            for (i = 0; i < 8; i++)
            {
                cypher[i] = EP2[EP[i] - 1];
                Console.Write(cypher[i]);
            }

            Console.WriteLine();


            //10
            Console.WriteLine("Результат XOR(cypher+K1):");
            for (i = 0; i < 8; i++)
            {
                if (cypher[i] == K1[i]) l = 0;
                else l = 1;
                cyp[i] = l;
                Console.Write(cyp[i]);
            }

            Console.WriteLine();


            //11
            Brow[0] = cyp[0];
            Brow[1] = cyp[3];
            Bcol[0] = cyp[1];
            Bcol[1] = cyp[2];

            if ((Brow[0] == 0) & (Brow[1] == 0)) row = 0;
            else if ((Brow[0] == 0) & (Brow[1] == 1)) row = 1;
            else if ((Brow[0] == 1) & (Brow[1] == 0)) row = 2;
            else if ((Brow[0] == 1) & (Brow[1] == 1)) row = 3;

            if ((Bcol[0] == 0) & (Bcol[1] == 0)) col = 0;
            else if ((Bcol[0] == 0) & (Bcol[1] == 1)) col = 1;
            else if ((Bcol[0] == 1) & (Bcol[1] == 0)) col = 2;
            else if ((Bcol[0] == 1) & (Bcol[1] == 1)) col = 3;

            F1 = S0[row][col];
            if (F1 == 0)
            {
                BF1[0] = 0;
                BF1[1] = 0;
            }
            else if (F1 == 1)
            {
                BF1[0] = 0;
                BF1[1] = 1;
            }
            else if (F1 == 2)
            {
                BF1[0] = 1;
                BF1[1] = 0;
            }
            else if (F1 == 3)
            {
                BF1[0] = 1;
                BF1[1] = 1;
            }

            //-
            Brow[0] = cyp[4];
            Brow[1] = cyp[7];
            Bcol[0] = cyp[5];
            Bcol[1] = cyp[6];

            if ((Brow[0] == 0) & (Brow[1] == 0)) row = 0;
            else if ((Brow[0] == 0) & (Brow[1] == 1)) row = 1;
            else if ((Brow[0] == 1) & (Brow[1] == 0)) row = 2;
            else if ((Brow[0] == 1) & (Brow[1] == 1)) row = 3;

            if ((Bcol[0] == 0) & (Bcol[1] == 0)) col = 0;
            else if ((Bcol[0] == 0) & (Bcol[1] == 1)) col = 1;
            else if ((Bcol[0] == 1) & (Bcol[1] == 0)) col = 2;
            else if ((Bcol[0] == 1) & (Bcol[1] == 1)) col = 3;

            F2 = S1[row][col];
            if (F2 == 0)
            {
                BF2[0] = 0;
                BF2[1] = 0;
            }
            else if (F2 == 1)
            {
                BF2[0] = 0;
                BF2[1] = 1;
            }
            else if (F2 == 2)
            {
                BF2[0] = 1;
                BF2[1] = 0;
            }
            else if (F2 == 3)
            {
                BF2[0] = 1;
                BF2[1] = 1;
            }

            F[0] = BF1[0];
            F[1] = BF1[1];
            F[2] = BF2[0];
            F[3] = BF2[1];
            Console.WriteLine("Результат дешифрования по таблицах S0/S1(F)");
            for (i = 0; i < 4; i++)
            {
                Console.Write(F[i]);
            }

            Console.WriteLine();


            //12
            Console.WriteLine("Результат перестановки Р4:");
            for (i = 0; i < 4; i++)
            {
                FF[i] = F[P4[i] - 1];
                Console.Write(FF[i]);
            }

            Console.WriteLine();


            //13
            Console.WriteLine("Результат XOR(F+Swap(1-4)):");
            for (i = 0; i < 4; i++)
            {
                FFF[i] = FF[i] ^ EP1[i];
                Console.Write(FFF[i]);
            }

            Console.WriteLine();


            //14
            Console.WriteLine("Результат сумирования с правой частью(F+Swap(4-8))");
            for (i = 0; i < 4; i++)
            {
                cypher[i] = FFF[i];
                cypher[i + 4] = EP2[i];
            }

            for (i = 0; i < 8; i++)
            {
                Console.Write(cypher[i]);
            }

            Console.WriteLine();


            //15
            Console.WriteLine("Результат дешифрования(перестановка IP-1):");
            for (i = 0; i < 8; i++)
            {
                text[i] = cypher[P1[i] - 1];
            }

            var message = string.Concat(text.Select(x => (char)x));
            Console.WriteLine(message);

            Console.WriteLine("----------------------------");
            //------------------------------Закрытие программы------------------------------------------

    }
}
