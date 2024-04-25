using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
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

        // nuevo

        public static void pruebasExponencial(int n, double lambda, int[] frecObs, decimal[] arrayLimSup, decimal anchoIntervalo)
        {
            // obtenemos las frecuiencias esperadas para cada intervalo
            List<double> frecEsp = new List<double>();

            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                double limSup = Decimal.ToDouble(arrayLimSup[i]);
                double limInf = limSup - Decimal.ToDouble(anchoIntervalo);
                double frec = frecEspIntervalo(lambda, limInf, limSup, n);
                frecEsp.Add(frec);
            };

            // obtenemos chi cuadrado y ks
            double chiCalculado = calcularChiCuadrado(frecObs.ToList(), frecEsp);
            double ksCalculado = calcularKS(frecObs.ToList(), frecEsp, n);

            // retornar o mostrar resultados
            Console.WriteLine("CHI: " + chiCalculado);
            Console.WriteLine("KS: " + ksCalculado);
        }

        private static double frecEspIntervalo(double lambda, double a, double b, int n)
        {

            // Esta función calcula P(a≤x<b)= e^−λa − e^−λb * n


            // primero calculamos e ^ (-lambda * a)
            double expLambdaA = Math.Exp(-lambda * a);

            // luego e^(-lambda * b)
            double expLambdaB = Math.Exp(-lambda * b);

            // Calcula la probabilidad de que la variable esté en el intervalo [a, b] multiplicado por el tamaño de muestra
            return (expLambdaA - expLambdaB) * n;
        }

        private static double calcularChiCuadrado(List<int> frecObs, List<double> frecEsp )
        {
            double chi = 0.0;
            for (int i = 0; i <  frecObs.Count; i++)
            {
                chi += Math.Pow(frecObs[i] - frecEsp[i], 2) / frecEsp[i];
            }

            return chi;
        }

        private static double calcularKS(List<int> frecObs, List<double> frecEsp, int n)
        {
            List<double> difAbsolutaFrecuencias = new List<double>();
            for (int i= 0; i < frecEsp.Count; i++)
            {
                double frecRelativaEsp = frecEsp[i] / n;
                double frecRelativaObs = frecObs[i] / n;
                difAbsolutaFrecuencias.Add(Math.Abs(frecRelativaEsp - frecRelativaObs));
            }

            return difAbsolutaFrecuencias.Max();

        }
    }
}
