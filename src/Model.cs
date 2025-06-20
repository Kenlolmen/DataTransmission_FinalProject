// Aleksander Jaruga 55579 214A
using System;
using System.Collections.Generic;
using System.Linq;
using ScottPlot;

namespace lab12
{
    public class Model
    {
        private static double Fs = 2000;
        private static int B = 30;
        private static double Tc = 1.0;
        private static double Tb = Tc / B;

        public KanalTransmisyjny kanal { get; } = new KanalTransmisyjny();
        Hamming hamming = new Hamming();
        Modulator modulator = new Modulator(Fs, Tb);
        Demodulator demodulator = new Demodulator(Fs, Tb);

        private static double W = 6.0;
        private static double Fn = W * (1 / Tb);
        private static double Fn1 = (W - 2) * (1 / Tb);
        private static double Fn2 = (W + 2) * (1 / Tb);

        public static int[] bits = new int[]
        {
            1,0,1,0,1,0,1,1,0,0,1,
            1,0,0,1,1,0,0,1,1,0,0,
            0,1,0,0,0,1,1,0,0,1,0,
            0,1,1,0,0,1,1,0,0,1,1,
        };

        static readonly int[,] P = new int[,]
        {
            {1, 0, 0, 1}, {0, 1, 0, 1}, {1, 1, 0, 1}, {0, 0, 1, 1},
            {1, 0, 1, 1}, {0, 1, 1, 1}, {1, 1, 1, 1}, {1, 1, 1, 0},
            {0, 0, 0, 1}, {1, 0, 0, 0}, {0, 1, 0, 0}
        };
        public List<int> modelTransmisyjnyHamming74(int choiceModulator, bool jedendwa = true) // true = I + II
        {
            List<int[]> encodedBlocks = hamming.Koder74(bits);
            List<int> hammingowe = new List<int>();

            foreach (var blok in encodedBlocks)
            {
                hammingowe.AddRange(blok);
            }

            double[] modulated = null;
            switch (choiceModulator)
            {
                case 1:
                    modulated = modulator.ModulateASK(hammingowe);
                    break;
                case 2:
                    modulated = modulator.ModulateFSK(hammingowe);
                    break;
                case 3:
                    modulated = modulator.ModulatePSK(hammingowe);
                    break;
            }

            double[] pierwszy;
            double[] drugi;
            if (jedendwa)
            {
                pierwszy = kanal.Szumienie(modulated);
                drugi = kanal.Tlumienie(pierwszy);
            }
            else
            {
                pierwszy = kanal.Tlumienie(modulated);
                drugi = kanal.Szumienie(pierwszy);
            }

            List<int> demodulated = null;
            switch (choiceModulator)
            {
                case 1:
                    demodulated = demodulator.DemodulateASK(drugi);
                    break;
                case 2:
                    demodulated = demodulator.DemodulateFSK(drugi);
                    break;
                case 3:
                    demodulated = demodulator.DemodulatePSK(drugi);
                    break;
            }

            List<int> decoded = hamming.Dekoder74(demodulated);

            return decoded;
        }
        public List<int> modelTransmisyjnyHamming1511(int choiceModulator, bool jedendwa = true) // true = I + II
        {
            List<int> hammingowe = hamming.Koder1511(bits, P);

            double[] modulated = null;
            switch (choiceModulator)
            {
                case 1:
                    modulated = modulator.ModulateASK(hammingowe);
                    break;
                case 2:
                    modulated = modulator.ModulateFSK(hammingowe);
                    break;
                case 3:
                    modulated = modulator.ModulatePSK(hammingowe);
                    break;
            }

            double[] pierwszy;
            double[] drugi;
            if (jedendwa)
            {
                pierwszy = kanal.Szumienie(modulated);
                drugi = kanal.Tlumienie(pierwszy);
            }
            else
            {
                pierwszy = kanal.Tlumienie(modulated);
                drugi = kanal.Szumienie(pierwszy);
            }

            List<int> demodulated = null;
            switch (choiceModulator)
            {
                case 1:
                    demodulated = demodulator.DemodulateASK(drugi);
                    break;
                case 2:
                    demodulated = demodulator.DemodulateFSK(drugi);
                    break;
                case 3:
                    demodulated = demodulator.DemodulatePSK(drugi);
                    break;
            }
            List<int> decoded = hamming.Dekoder1511(demodulated, P);

            return decoded;
        }
    }
}