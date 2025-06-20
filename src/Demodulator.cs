// Aleksander Jaruga 55579 214A
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab12
{
public class Demodulator
    {
        private double Fs;
        private double Tb;
        private double fc;
        
        public Demodulator(double fs, double tb)
        {
            Fs = fs;
            Tb = tb;
            fc = 10 / Tb;
        }
        
        public List<int> DemodulateASK(double[] signal)
        {
            int samplesPerBit = (int)(Tb * Fs);
            int numBits = signal.Length / samplesPerBit;
            List<int> bits = new List<int>();
            
            double[] envelope = new double[signal.Length];
            for (int i = 0; i < signal.Length; i++)
            {
                envelope[i] = Math.Abs(signal[i]);
            }
            
            int filterLength = samplesPerBit / 10;
            double[] filtered = new double[envelope.Length];
            for (int i = 0; i < envelope.Length; i++)
            {
                double sum = 0;
                int count = 0;
                for (int j = -filterLength/2; j <= filterLength/2; j++)
                {
                    int idx = i + j;
                    if (idx >= 0 && idx < envelope.Length)
                    {
                        sum += envelope[idx];
                        count++;
                    }
                }
                filtered[i] = sum / count;
            }
            
            double[] bitAmplitudes = new double[numBits];
            for (int i = 0; i < numBits; i++)
            {
                double sum = 0;
                int count = 0;
                
                int start = i * samplesPerBit + samplesPerBit / 4;
                int end = i * samplesPerBit + 3 * samplesPerBit / 4;
                
                for (int j = start; j < end && j < filtered.Length; j++)
                {
                    sum += filtered[j];
                    count++;
                }
                
                bitAmplitudes[i] = count > 0 ? sum / count : 0;
            }
            
            double threshold = bitAmplitudes.Average();
            
            for (int i = 0; i < numBits; i++)
            {
                bits.Add(bitAmplitudes[i] > threshold ? 1 : 0);
            }
            
            return bits;
        }
        
        public List<int> DemodulateFSK(double[] signal)
        {
            int samplesPerBit = (int)(Tb * Fs);
            int numBits = signal.Length / samplesPerBit;
            List<int> bits = new List<int>();
            
            double f1 = fc - 2 / Tb;
            double f2 = fc + 2 / Tb;
            
            for (int i = 0; i < numBits; i++)
            {
                double corr1 = 0;
                double corr2 = 0;
                
                for (int j = 0; j < samplesPerBit; j++)
                {
                    int idx = i * samplesPerBit + j;
                    if (idx < signal.Length)
                    {
                        double t = (i * samplesPerBit + j) / Fs;
                        corr1 += signal[idx] * Math.Sin(2 * Math.PI * f1 * t);
                        corr2 += signal[idx] * Math.Sin(2 * Math.PI * f2 * t);
                    }
                }

                bits.Add(corr2 > corr1 ? 1 : 0);
            }
            
            return bits;
        }
        
        public List<int> DemodulatePSK(double[] signal)
        {
            int samplesPerBit = (int)(Tb * Fs);
            int numBits = signal.Length / samplesPerBit;
            List<int> bits = new List<int>();
            
            for (int i = 0; i < numBits; i++)
            {
                double correlation = 0;
                
                for (int j = 0; j < samplesPerBit; j++)
                {
                    int idx = i * samplesPerBit + j;
                    if (idx < signal.Length)
                    {
                        double t = (i * samplesPerBit + j) / Fs;
                        double reference = Math.Sin(2 * Math.PI * fc * t);
                        correlation += signal[idx] * reference;
                    }
                }
                
                correlation /= samplesPerBit;
        
                bits.Add(correlation < 0 ? 1 : 0);
            }
            
            return bits;
        }
    }
}