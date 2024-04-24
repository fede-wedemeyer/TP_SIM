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

            Console.WriteLine(expectedFrequency);

            double chiCuadrado = 0;
            foreach (var frequency in frecObs)
            {
                chiCuadrado += Math.Pow(frequency - expectedFrequency, 2) / expectedFrequency;
            }

            int gradosDeLibertad = n - 1;

            ChiSquared chiSquaredDist = new ChiSquared(gradosDeLibertad);
            double pValue = 1.0 - chiSquaredDist.CumulativeDistribution(chiCuadrado);


            double nivelDeSignificancia = 0.05;

            Console.WriteLine(pValue);
            Console.WriteLine(chiCuadrado);
            return pValue < nivelDeSignificancia;
        }
    }
}
