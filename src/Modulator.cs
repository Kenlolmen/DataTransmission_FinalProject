// Aleksander Jaruga 55579 214A
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab12
{
   public class Modulator
    {
        private double Fs;
        private double Tb;
        private double fc; 
        
        public Modulator(double fs, double tb)
        {
            Fs = fs;
            Tb = tb;
            fc = 10 / Tb;
        }
        
        public double[] ModulateASK(List<int> bits)
        {
            int samplesPerBit = (int)(Tb * Fs);
            double[] signal = new double[bits.Count * samplesPerBit];
            
            for (int i = 0; i < bits.Count; i++)
            {
                double amplitude = bits[i] == 1 ? 1.0 : 0.0;
                
                for (int j = 0; j < samplesPerBit; j++)
                {
                    double t = (i * samplesPerBit + j) / Fs;
                    signal[i * samplesPerBit + j] = amplitude * Math.Sin(2 * Math.PI * fc * t);
                }
            }
            
            return signal;
        }
        
        public double[] ModulateFSK(List<int> bits)
        {
            int samplesPerBit = (int)(Tb * Fs);
            double[] signal = new double[bits.Count * samplesPerBit];
            
            double f1 = fc - 2 / Tb; 
            double f2 = fc + 2 / Tb; 
            
            for (int i = 0; i < bits.Count; i++)
            {
                double freq = bits[i] == 1 ? f2 : f1;
                
                for (int j = 0; j < samplesPerBit; j++)
                {
                    double t = (i * samplesPerBit + j) / Fs;
                    signal[i * samplesPerBit + j] = Math.Sin(2 * Math.PI * freq * t);
                }
            }
            
            return signal;
        }
        
        public double[] ModulatePSK(List<int> bits)
        {
            int samplesPerBit = (int)(Tb * Fs);
            double[] signal = new double[bits.Count * samplesPerBit];
            
            for (int i = 0; i < bits.Count; i++)
            {
                double phase = bits[i] == 1 ? Math.PI : 0;
                
                for (int j = 0; j < samplesPerBit; j++)
                {
                    double t = (i * samplesPerBit + j) / Fs;
                    signal[i * samplesPerBit + j] = Math.Sin(2 * Math.PI * fc * t + phase);
                }
            }
            
            return signal;
        }
    }
}