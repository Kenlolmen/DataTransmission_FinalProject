// Aleksander Jaruga 55579 214A
using System;
using lab12;
using System.Collections.Generic;
using System.Linq;
using ScottPlot;

public class BER
{
    private Model model = new Model();

    public static int[] bits = new int[]
    {
        1,0,1,0,1,0,1,1,0,0,1,
        1,0,0,1,1,0,0,1,1,0,0,
        0,1,0,0,0,1,1,0,0,1,0,
        0,1,1,0,0,1,1,0,0,1,1,
    };

    public static double ObliczBER(int[] oryginal, List<int> odbior)
    {
        int errors = 0;
        int len = Math.Min(oryginal.Length, odbior.Count);

        if (len == 0)
            return 1.0;

        for (int i = 0; i < len; i++)
        {
            if (oryginal[i] != odbior[i])
                errors++;
        }
        return (double)errors / len;
    }

    public static void RysujWykresWszystkie(List<double> alphas,
        List<double> ber1511_ask, List<double> ber74_ask,
        List<double> ber1511_fsk, List<double> ber74_fsk,
        List<double> ber1511_psk, List<double> ber74_psk)
    {
        var plt = new ScottPlot.Plot();

        // ASK - kolory czerwone
        var askScatter1511 = plt.Add.Scatter(alphas.ToArray(), ber1511_ask.ToArray());
        askScatter1511.LegendText = "ASK Hamming(15,11)";
        askScatter1511.LineWidth = 3;
        askScatter1511.MarkerSize = 8;
        askScatter1511.Color = ScottPlot.Colors.Red;

        var askScatter74 = plt.Add.Scatter(alphas.ToArray(), ber74_ask.ToArray());
        askScatter74.LegendText = "ASK Hamming(7,4)";
        askScatter74.LineWidth = 3;
        askScatter74.MarkerSize = 8;
        askScatter74.Color = ScottPlot.Colors.Red;

        // FSK - kolory zielone
        var fskScatter1511 = plt.Add.Scatter(alphas.ToArray(), ber1511_fsk.ToArray());
        fskScatter1511.LegendText = "FSK Hamming(15,11)";
        fskScatter1511.LineWidth = 3;
        fskScatter1511.MarkerSize = 8;
        fskScatter1511.Color = ScottPlot.Colors.Green;

        var fskScatter74 = plt.Add.Scatter(alphas.ToArray(), ber74_fsk.ToArray());
        fskScatter74.LegendText = "FSK Hamming(7,4)";
        fskScatter74.LineWidth = 3;
        fskScatter74.MarkerSize = 8;
        fskScatter74.Color = ScottPlot.Colors.Green;


        // PSK - kolory niebieskie
        var pskScatter1511 = plt.Add.Scatter(alphas.ToArray(), ber1511_psk.ToArray());
        pskScatter1511.LegendText = "PSK Hamming(15,11)";
        pskScatter1511.LineWidth = 3;
        pskScatter1511.MarkerSize = 8;
        pskScatter1511.Color = ScottPlot.Colors.Blue;

        var pskScatter74 = plt.Add.Scatter(alphas.ToArray(), ber74_psk.ToArray());
        pskScatter74.LegendText = "PSK Hamming(7,4)";
        pskScatter74.LineWidth = 3;
        pskScatter74.MarkerSize = 8;
        pskScatter74.Color = ScottPlot.Colors.Blue;


        plt.Title("BER vs Alfa - Porównanie modulacji ASK, FSK, PSK");
        plt.XLabel("Alfa");
        plt.YLabel("BER");
        plt.ShowLegend();
        plt.Axes.SetLimitsY(0, 1);

        string images = "images"; 
        string fileName = "ber_alfa_wszystkie_modulacje.png";
        string filePathBeta = Path.Combine(images, fileName);

        plt.SavePng(filePathBeta, 1400, 900);
    }
    public static void RysujWykresWszystkieBeta(List<double> betas,
        List<double> ber1511_ask, List<double> ber74_ask,
        List<double> ber1511_fsk, List<double> ber74_fsk,
        List<double> ber1511_psk, List<double> ber74_psk)
    {
        var plt = new ScottPlot.Plot();

        // ASK - kolory czerwone
        var askScatter1511 = plt.Add.Scatter(betas.ToArray(), ber1511_ask.ToArray());
        askScatter1511.LegendText = "ASK Hamming(15,11)";
        askScatter1511.LineWidth = 3;
        askScatter1511.MarkerSize = 8;
        askScatter1511.Color = ScottPlot.Colors.Red;

        var askScatter74 = plt.Add.Scatter(betas.ToArray(), ber74_ask.ToArray());
        askScatter74.LegendText = "ASK Hamming(7,4)";
        askScatter74.LineWidth = 3;
        askScatter74.MarkerSize = 8;
        askScatter74.Color = ScottPlot.Colors.Red;


        // FSK - kolory zielone
        var fskScatter1511 = plt.Add.Scatter(betas.ToArray(), ber1511_fsk.ToArray());
        fskScatter1511.LegendText = "FSK Hamming(15,11)";
        fskScatter1511.LineWidth = 3;
        fskScatter1511.MarkerSize = 8;
        fskScatter1511.Color = ScottPlot.Colors.Green;

        var fskScatter74 = plt.Add.Scatter(betas.ToArray(), ber74_fsk.ToArray());
        fskScatter74.LegendText = "FSK Hamming(7,4)";
        fskScatter74.LineWidth = 3;
        fskScatter74.MarkerSize = 8;
        fskScatter74.Color = ScottPlot.Colors.Green;


        // PSK - kolory niebieskie
        var pskScatter1511 = plt.Add.Scatter(betas.ToArray(), ber1511_psk.ToArray());
        pskScatter1511.LegendText = "PSK Hamming(15,11)";
        pskScatter1511.LineWidth = 3;
        pskScatter1511.MarkerSize = 8;
        pskScatter1511.Color = ScottPlot.Colors.Blue;

        var pskScatter74 = plt.Add.Scatter(betas.ToArray(), ber74_psk.ToArray());
        pskScatter74.LegendText = "PSK Hamming(7,4)";
        pskScatter74.LineWidth = 3;
        pskScatter74.MarkerSize = 8;
        pskScatter74.Color = ScottPlot.Colors.Blue;


        plt.Title("BER vs Beta - Porównanie modulacji ASK, FSK, PSK (kanał mnożący)");
        plt.XLabel("Beta (β)");
        plt.YLabel("BER");
        plt.ShowLegend();
        plt.Axes.SetLimitsY(0, 1);

        string images = "images"; // Ensure this directory exists or change to your desired path
        string fileName = "ber_beta_wszystkie_modulacje.png";
        string filePathBeta = Path.Combine(images, fileName);

        plt.SavePng(filePathBeta, 1400, 900);
        
    }

    public void ZbadajBER() //llm pomagal 
    {
        List<double> alphasBase = new List<double> { 0.0, 0.1, 0.2, 0.3, 0.5, 0.7, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0, 6.0, 7.0, 8.0 };

        List<double> ber1511_ask = new();
        List<double> ber74_ask = new();
        List<double> ber1511_fsk = new();
        List<double> ber74_fsk = new();
        List<double> ber1511_psk = new();
        List<double> ber74_psk = new();
        List<double> alphas = new();

        for (int modulator = 1; modulator <= 3; modulator++)
        {
            List<double> ber1511 = new();
            List<double> ber74 = new();

            foreach (double alpha in alphasBase)
            {
                model.kanal.alfa = alpha;
                Console.WriteLine($"Modulator: {modulator}, Alfa: {alpha}");

                double sum1511 = 0;
                double sum74 = 0;
                int trials = 10;

                for (int trial = 0; trial < trials; trial++)
                {
                    List<int> output1511 = model.modelTransmisyjnyHamming1511(modulator, false); // false = kanał addytywny
                    List<int> output74 = model.modelTransmisyjnyHamming74(modulator, false);

                    sum1511 += ObliczBER(bits, output1511);
                    sum74 += ObliczBER(bits, output74);
                }

                double avgBer1511 = sum1511 / trials;
                double avgBer74 = sum74 / trials;

                ber1511.Add(avgBer1511);
                ber74.Add(avgBer74);

                Console.WriteLine($"  BER (15,11): {avgBer1511:F4}");
                Console.WriteLine($"  BER (7,4): {avgBer74:F4}");

            }

            if (modulator == 1)
            {
                ber1511_ask = new List<double>(ber1511);
                ber74_ask = new List<double>(ber74);
            }
            else if (modulator == 2)
            {
                ber1511_fsk = new List<double>(ber1511);
                ber74_fsk = new List<double>(ber74);
            }
            else if (modulator == 3)
            {
                ber1511_psk = new List<double>(ber1511);
                ber74_psk = new List<double>(ber74);
            }

            if (modulator == 1) alphas = new List<double>(alphasBase);
        }

        RysujWykresWszystkie(alphas, ber1511_ask, ber74_ask, ber1511_fsk, ber74_fsk, ber1511_psk, ber74_psk);
    }
    public void ZbadajBERKanalMnozacy()
    {
        List<double> betasBase = new List<double> { 0.0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0, 4.0, 5.0, 6.0, 8.0, 10.0 };

        List<double> ber1511_ask = new();
        List<double> ber74_ask = new();
        List<double> ber1511_fsk = new();
        List<double> ber74_fsk = new();
        List<double> ber1511_psk = new();
        List<double> ber74_psk = new();
        List<double> betas = new();

        for (int modulator = 1; modulator <= 3; modulator++)
        {
            List<double> ber1511 = new();
            List<double> ber74 = new();

            foreach (double beta in betasBase)
            {
                model.kanal.beta = beta;
                Console.WriteLine($"Modulator: {modulator}, Beta: {beta}");

                double sum1511 = 0;
                double sum74 = 0;
                int trials = 10;

                for (int trial = 0; trial < trials; trial++)
                {
                    List<int> output1511 = model.modelTransmisyjnyHamming1511(modulator, true); // true = kanał mnożący
                    List<int> output74 = model.modelTransmisyjnyHamming74(modulator, true);

                    sum1511 += ObliczBER(bits, output1511);
                    sum74 += ObliczBER(bits, output74);
                }

                double avgBer1511 = sum1511 / trials;
                double avgBer74 = sum74 / trials;

                ber1511.Add(avgBer1511);
                ber74.Add(avgBer74);

                Console.WriteLine($"  BER (15,11): {avgBer1511:F4}");
                Console.WriteLine($"  BER (7,4): {avgBer74:F4}");

            }

            if (modulator == 1)
            {
                ber1511_ask = new List<double>(ber1511);
                ber74_ask = new List<double>(ber74);
            }
            else if (modulator == 2)
            {
                ber1511_fsk = new List<double>(ber1511);
                ber74_fsk = new List<double>(ber74);
            }
            else if (modulator == 3)
            {
                ber1511_psk = new List<double>(ber1511);
                ber74_psk = new List<double>(ber74);
            }

            if (modulator == 1) betas = new List<double>(betasBase);
        }

        RysujWykresWszystkieBeta(betas, ber1511_ask, ber74_ask, ber1511_fsk, ber74_fsk, ber1511_psk, ber74_psk);
    }
    













// zad 13


public static void RysujKombinowaneKontury(List<double> alphas, List<double> betas,
                                           double[,] berData1, double[,] berData2,
                                           double[,] berData3, double[,] berData4,
                                           string modulationName, string[] hammingCodes,
                                           string fileName)
{
    string baseFileName = fileName.Replace(".png", "");

    var berDataList = new List<double[,]> { berData1, berData2, berData3, berData4 };
    var fileSuffixes = new string[]
    {
        "_hamming74_I_II.png",
        "_hamming74_II_I.png",
        "_hamming1511_I_II.png",
        "_hamming1511_II_I.png"
    };

    string[] alphaLabels = alphas.Select(a => a.ToString("F1")).ToArray();
    string[] betaLabels = betas.Select(b => b.ToString("F1")).ToArray();

        for (int i = 0; i < berDataList.Count; i++)
        {
            var plt = new ScottPlot.Plot();

            // TRANSPOZYCJA DANYCH - to rozwiązuje problem!
            var transposedData = TransposeMatrix(berDataList[i]);

            var hm = plt.Add.Heatmap(transposedData);
            hm.Colormap = new ScottPlot.Colormaps.Viridis();
            plt.Title($"{modulationName} - {hammingCodes[i]}");
            plt.XLabel("Alfa");
            plt.YLabel("Beta");

            var cb = plt.Add.ColorBar(hm);
            cb.Label = "BER (Bit Error Rate)";

            plt.Axes.SetLimitsX(0, alphas.Count - 1);
            plt.Axes.SetLimitsY(0, betas.Count - 1);

            plt.Axes.Bottom.SetTicks(Enumerable.Range(0, alphas.Count).Select(j => (double)j).ToArray(), alphaLabels);
            plt.Axes.Left.SetTicks(Enumerable.Range(0, betas.Count).Select(j => (double)j).ToArray(), betaLabels);


            string imagesFolder = "images"; 
            string filePath = Path.Combine(imagesFolder, $"{baseFileName}{fileSuffixes[i]}");
            plt.SavePng(filePath, 800, 600);

            
    }
}

private static double[,] TransposeMatrix(double[,] matrix)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    double[,] transposed = new double[cols, rows];
    
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            transposed[j, i] = matrix[i, j];
        }
    }
    
    return transposed;
}

public void ZbadajBER3D()
{
    List<double> alphas = new List<double> { 0.0, 0.5, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };
    List<double> betas = new List<double> { 0.0, 0.5, 1.0, 1.5, 2.0, 3.0, 4.0, 5.0 };

    string[] modulationNames = { "ASK", "FSK", "PSK" };
    string[] hammingCodes = { "Hamming(7,4) I + II", "Hamming(7,4) II + I", "Hamming(15,11) I + II", "Hamming(15,11) II + I" };

    for (int modulator = 1; modulator <= 3; modulator++)
    {
        double[,] berData1 = new double[alphas.Count, betas.Count]; // Hamming(7,4) I + II
        double[,] berData2 = new double[alphas.Count, betas.Count]; // Hamming(7,4) II + I
        double[,] berData3 = new double[alphas.Count, betas.Count]; // Hamming(15,11) I + II
        double[,] berData4 = new double[alphas.Count, betas.Count]; // Hamming(15,11) II + I

        for (int i = 0; i < alphas.Count; i++)
        {
            for (int j = 0; j < betas.Count; j++)
            {
                model.kanal.alfa = alphas[i];
                model.kanal.beta = betas[j];
                    for (int hammingType = 0; hammingType < 4; hammingType++)
                    {
                        double sumBer = 0;
                        int trials = 10;

                        for (int trial = 0; trial < trials; trial++)
                        {
                            List<int> output;
                            if (hammingType == 0)
                            {
                                output = model.modelTransmisyjnyHamming74(modulator, true);
                            }
                            else if (hammingType == 1)
                            {
                                output = model.modelTransmisyjnyHamming74(modulator, false);
                            }
                            else if (hammingType == 2)
                            {
                                output = model.modelTransmisyjnyHamming1511(modulator, true);
                            }
                            else
                            {
                                output = model.modelTransmisyjnyHamming1511(modulator, false);
                            }

                            sumBer += ObliczBER(bits, output);
                        }

                        double avgBer = sumBer / trials;
                        switch (hammingType)
                        {
                            case 0: berData1[i, j] = avgBer; break;
                            case 1: berData2[i, j] = avgBer; break;
                            case 2: berData3[i, j] = avgBer; break;
                            case 3: berData4[i, j] = avgBer; break;
                        }
                }
            }
        }

        
        string fileName = $"{modulationNames[modulator - 1].ToLower()}.png";
        
        RysujKombinowaneKontury(alphas, betas, berData1, berData2, berData3, berData4,
                               modulationNames[modulator - 1], hammingCodes, fileName);
    }

}
}