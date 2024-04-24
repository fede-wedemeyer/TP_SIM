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

        public static Boolean ChiCuadradoNormal(int n, int[] frecObs, double media, double desvEstandard, decimal[] arrayLimSup, decimal anchoIntervalo) {

            // double marcaClase = (limSup + limInf / 2.0);
            int cantIntervalos = frecObs.Length;

            // double zScore = (marcaClase - media) / desvEstandard;

            decimal[] arrayLimInf = new decimal[arrayLimSup.Length];

            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                decimal limInf = arrayLimSup[i] - anchoIntervalo;
                arrayLimInf[i] = limInf;
            };

            foreach (var frequency in arrayLimSup)
            {
                Console.WriteLine(frequency);
            }

            foreach (var frequency in arrayLimInf)
            {
                Console.WriteLine(frequency);
            }

            /*
                        Normal normalDistribution = new Normal(media, desvEstandard);
                        double probability = normalDistribution.CumulativeDistribution(limSup) - normalDistribution.CumulativeDistribution(limInf);

                        double expectedFrequency = probability * n;


                        double chiCuadrado = 0;
                        foreach (var frequency in frecObs)
                        {
                            chiCuadrado += Math.Pow(frequency - expectedFrequency, 2) / expectedFrequency;
                        }

                        int gradosDeLibertad = cantIntervalos - 1;

                        ChiSquared chiSquaredDist = new ChiSquared(gradosDeLibertad);
                        double pValue = 1.0 - chiSquaredDist.CumulativeDistribution(chiCuadrado);


                        double nivelDeSignificancia = 0.05;

                        Console.WriteLine(pValue);
                        Console.WriteLine(chiCuadrado); */
            return true;
        }

    }
}
