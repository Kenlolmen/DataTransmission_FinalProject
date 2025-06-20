// Aleksander Jaruga 55579 214A
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab12
{
    public class Hamming
    {

        private static int[] F(int[] bits)
        {
            int d1 = bits[0];
            int d2 = bits[1];
            int d3 = bits[2];
            int d4 = bits[3];

            int p1 = d1 ^ d2 ^ d4;
            int p2 = d1 ^ d3 ^ d4;
            int p4 = d2 ^ d3 ^ d4;

            return new int[] { p1, p2, d1, p4, d2, d3, d4 };
        }

        public List<int[]> Koder74(int[] input)
        {
            List<int[]> bloki = new List<int[]>();
            var l = input.Length;

            for (int i = 0; i < l; i += 4)
            {
                if (i + 4 > l) break;

                int[] blok4 = new int[4];
                Array.Copy(input, i, blok4, 0, 4);

                int[] blok7 = F(blok4);
                bloki.Add(blok7);
            }

            return bloki;
        }
    

    public List<int> Dekoder74(List<int> kodowane)
    {
        List<int> dekodowane = new List<int>();

        for (int i = 0; i < kodowane.Count; i += 7)
        {
            if (i + 7 > kodowane.Count) break;

            int[] blok = kodowane.Skip(i).Take(7).ToArray();

            int p1 = blok[0];
            int p2 = blok[1];
            int d1 = blok[2];
            int p4 = blok[3];
            int d2 = blok[4];
            int d3 = blok[5];
            int d4 = blok[6];

            int s1 = p1 ^ d1 ^ d2 ^ d4;
            int s2 = p2 ^ d1 ^ d3 ^ d4;
            int s3 = p4 ^ d2 ^ d3 ^ d4;

            int errorPos = s3 * 4 + s2 * 2 + s1 * 1;

            if (errorPos != 0 && errorPos <= 7)
            {
                blok[errorPos - 1] ^= 1;
            }

            dekodowane.Add(blok[2]);
            dekodowane.Add(blok[4]);
            dekodowane.Add(blok[5]);
            dekodowane.Add(blok[6]);
        }

        return dekodowane;
    }





        // Hamming (15,11)
        public List<int> Koder1511(int[] bits, int[,] P)
        {
            List<int> inputBits = bits.ToList();
            List<int> encoded = new List<int>();

            for (int i = 0; i < inputBits.Count; i += 11)
            {
                int[] message = inputBits.Skip(i).Take(11).ToArray();

                int[] parityBits = new int[4];

                for (int j = 0; j < 4; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < 11; k++)
                    {
                        sum += message[k] * P[k, j];
                    }
                    parityBits[j] = sum % 2;
                }

                encoded.AddRange(parityBits);
                encoded.AddRange(message);
            }


            return encoded;
        }

        public List<int> Dekoder1511(List<int> receivedBits, int[,] P)
        {
            List<int> decoded = new List<int>();

            int rows = P.GetLength(0);
            int cols = P.GetLength(1);
            int[,] PT = new int[cols, rows];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    PT[j, i] = P[i, j];

            int[,] H = new int[4, 15];
            for (int i = 0; i < 4; i++) H[i, i] = 1;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 11; j++)
                    H[i, j + 4] = PT[i, j];

            for (int i = 0; i < receivedBits.Count; i += 15)
            {
                int[] codeword = receivedBits.Skip(i).Take(15).ToArray();
                int[] syndrome = new int[4];

                for (int j = 0; j < 4; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < 15; k++)
                    {
                        sum += codeword[k] * H[j, k];
                    }
                    syndrome[j] = sum % 2;
                }

                int errorPosition = 0;
                for (int b = 0; b < 4; b++)
                {
                    errorPosition += syndrome[b] << (3 - b);
                }

                if (errorPosition != 0 && errorPosition <= 15)
                {
                    codeword[errorPosition - 1] ^= 1; 
                }

                decoded.AddRange(codeword.Skip(4)); 
            }

            return decoded;
        }
    }
}