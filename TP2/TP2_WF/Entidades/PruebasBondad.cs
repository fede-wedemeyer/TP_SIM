using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace TP2_WF.Entidades
{
    class PruebasBondad
    {
        public static Boolean ChiCuadradoUniforme(int n, int[] frecObs)
        {


            int cantIntervalos = frecObs.Length;
            double expectedFrequency = n / cantIntervalos;


            double chiCuadrado = 0;
            foreach (var frequency in frecObs)
            {
                chiCuadrado += Math.Pow(frequency - expectedFrequency, 2) / expectedFrequency;
            }

            int gradosDeLibertad = cantIntervalos - 1;

            ChiSquared chiSquaredDist = new ChiSquared(gradosDeLibertad);
            double pValue = 1.0 - chiSquaredDist.CumulativeDistribution(chiCuadrado);


            double nivelDeSignificancia = 0.05;
            return pValue < nivelDeSignificancia;
        }

        public static Boolean ChiCuadradoNormal(int n, int[] frecObs, double media, double desvEstandard, decimal[] arrayLimSup, decimal anchoIntervalo)
        {

            // double marcaClase = (limSup + limInf / 2.0);
            int cantIntervalos = frecObs.Length;

            // double zScore = (marcaClase - media) / desvEstandard;

            double[] arrayLimInf = new double[arrayLimSup.Length];

            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                double limInf = (Decimal.ToDouble(arrayLimSup[i])) - Decimal.ToDouble(anchoIntervalo);
                arrayLimInf[i] = limInf;
            };

            double[] arrayExpectedFreq = new double[frecObs.Length];
            double chiCuadrado = 0;

            for (int i = 0; i < frecObs.Length; i++)
            {
                Normal normalDistribution = new Normal(media, desvEstandard);
                double probability = normalDistribution.CumulativeDistribution(Decimal.ToDouble(arrayLimSup[i])) - normalDistribution.CumulativeDistribution(arrayLimInf[i]);

                double expectedFrequency = probability * n;
                arrayExpectedFreq[i] = expectedFrequency;

                chiCuadrado += Math.Pow(frecObs[i] - expectedFrequency, 2) / expectedFrequency;

            }

            int gradosDeLibertad = cantIntervalos - 1;

            ChiSquared chiSquaredDist = new ChiSquared(gradosDeLibertad);
            double pValue = 1.0 - chiSquaredDist.CumulativeDistribution(chiCuadrado);


            double nivelDeSignificancia = 0.05;
            return true;
        }

        public static Boolean ChiCuadradoExponencial(int n, int[] frecObs, double media, double lambda, decimal[] arrayLimSup, decimal anchoIntervalo)
        {
            if (lambda == 0) { lambda = media / n;  }
            Console.WriteLine("Lambda: " + lambda);


            int cantIntervalos = frecObs.Length;
            double[] arrayLimInf = new double[arrayLimSup.Length];

            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                double limInf = (Decimal.ToDouble(arrayLimSup[i])) - Decimal.ToDouble(anchoIntervalo);
                arrayLimInf[i] = limInf;
            };

            double expectedFrequencySum = 0;
            double chiCuadrado = 0;

            for (int i = 0; i < cantIntervalos; i++)
            {
                double marcaClase = (arrayLimInf[i] + Decimal.ToDouble(arrayLimSup[i])) / 2;

                Exponential exponentialDistribution = new Exponential(lambda);
                double probability = exponentialDistribution.CumulativeDistribution(Decimal.ToDouble(arrayLimSup[i])) - exponentialDistribution.CumulativeDistribution(arrayLimInf[i]);

                double expectedFrequency = probability * n;
                chiCuadrado += Math.Pow(frecObs[i] - expectedFrequency, 2) / expectedFrequency;

                Console.WriteLine("Frecuencia: " + frecObs[i]);
                Console.WriteLine("Frecuencia esperada: " + expectedFrequency);
                Console.WriteLine("Chi Cuadrado: " + chiCuadrado);

            }
            return true;
        }
    }
}
