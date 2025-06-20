// Aleksander Jaruga 55579 214A
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab12
{
    public class KanalTransmisyjny
    {
        public double alfa = 1.5; 
        public double beta = 2.0;
        private Random random = new Random();
        private double Tc = 1.0;
        private double Fs = 2000;
    

        public double[] Szumienie(double[] xt)
        {
            int n = xt.Length;
            double[] yt = new double[n];

            for (int i = 0; i < n; i++)
            {
                double gt = 2 * random.NextDouble() - 1; 
                yt[i] = xt[i] + alfa * gt;
            }

            return yt;

        }

        public double[] Tlumienie(double[] xt) //llm
        {
            int n = xt.Length;
            double[] yt = new double[n];
            
            double t0 = Tc * 0.95; 
            
            for (int i = 0; i < n; i++)
            {
                double t = i / Fs;
                double gt = Math.Exp(-beta * t) * Math.Max(0, 1 - t / t0);
                yt[i] = xt[i] * gt;
            }

            return yt;
        }
    }
}   
