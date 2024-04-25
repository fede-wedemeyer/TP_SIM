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
        // Prueba de bondad uniforme
        public static (List<double>, double, double) pruebasUniforme(int n, int[] frecObs)
        {
            // obtenemos las frecuencias esperadas para cada intervalo
            List<double> frecEsp = new List<double>();
            int cantIntervalos = frecObs.Length;
            double esperada = n / cantIntervalos;

            for (int i = 0; i < cantIntervalos; i++)
            {
                frecEsp.Add(esperada);

                //Console.WriteLine(frecEsp[i]);
                //Console.WriteLine(frecObs[i]);

            }


            // obtenemos chi cuadrado y ks
            double chiCalculado = calcularChiCuadrado(frecObs.ToList(), frecEsp);
            double ksCalculado = calcularKS(frecObs.ToList(), frecEsp, n);

            // retornar o mostrar resultados
            Console.WriteLine("CHI: " + chiCalculado);
            Console.WriteLine("KS: " + ksCalculado);

            return (frecEsp, chiCalculado, ksCalculado);

        }

        // Pruebas de bondad para distribución normal
        public static (List<double>, double, double) pruebasNormal(int n, double media, int[] frecObs, double desviacionEstandar, decimal[] arrayLimSup, decimal anchoIntervalo)
        {
            List<double> frecEsp = new List<double>();
            Normal normalDist = new Normal(media, desviacionEstandar);

            // Obtenemos la lista de frecuencias esperadas
            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                // Obtenemos los límites inferiores del intervalo restandole al superior el ancho del intervalo
                double limSup = Decimal.ToDouble(arrayLimSup[i]);
                double limInf = limSup - Decimal.ToDouble(anchoIntervalo);
                // Cálculo para la probabilidad del intervalo
                double probabilidadIntervalo = normalDist.CumulativeDistribution(limSup) - normalDist.CumulativeDistribution(limInf);
                frecEsp.Add(probabilidadIntervalo * n);

                // Console.WriteLine("Frecuencia observada: " + frecObs[i]);
                // Console.WriteLine("Frecuencia esperada: " + frecEsp[i]); 

            };

            // obtenemos chi cuadrado y ks
            double chiCalculado = calcularChiCuadrado(frecObs.ToList(), frecEsp);
            double ksCalculado = calcularKS(frecObs.ToList(), frecEsp, n);

            // retornar o mostrar resultados
            Console.WriteLine("CHI: " + chiCalculado);
            Console.WriteLine("KS: " + ksCalculado);

            return (frecEsp, chiCalculado, ksCalculado);
        }


        // nuevo

        public static (List<double>, double, double) pruebasExponencial(int n, double lambda, int[] frecObs, decimal[] arrayLimSup, decimal anchoIntervalo)
        {
            // obtenemos las frecuencias esperadas para cada intervalo
            List<double> frecEsp = new List<double>();

            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                // Obtenemos los límites inferiores del intervalo restandole al superior el ancho del intervalo
                double limSup = Decimal.ToDouble(arrayLimSup[i]);
                double limInf = limSup - Decimal.ToDouble(anchoIntervalo);

                // Se llama a la función que calcula los valores de las frecuencias esperadas y se agregan a la lista de frecEsp
                double frec = frecEspIntervaloExp(lambda, limInf, limSup, n);
                frecEsp.Add(frec);


                Console.WriteLine("Frecuencia observada: " + frecObs[i]);
                Console.WriteLine("Frecuencia esperada: " + frecEsp[i]); 
            };

            // obtenemos chi cuadrado y ks
            double chiCalculado = calcularChiCuadrado(frecObs.ToList(), frecEsp);
            double ksCalculado = calcularKS(frecObs.ToList(), frecEsp, n);

            // retornar o mostrar resultados
            Console.WriteLine("CHI: " + chiCalculado);
            Console.WriteLine("KS: " + ksCalculado);

            return (frecEsp, chiCalculado, ksCalculado);
        }

        private static double frecEspIntervaloExp(double lambda, double a, double b, int n)
        {

            // Esta función calcula P(a≤x<b)= e^−λa − e^−λb * n


            // primero calculamos e ^ (-lambda * a)
            double expLambdaA = Math.Exp(-lambda * a);

            // luego e^(-lambda * b)
            double expLambdaB = Math.Exp(-lambda * b);

            // Calcula la probabilidad de que la variable esté en el intervalo [a, b] multiplicado por el tamaño de muestra
            return (expLambdaA - expLambdaB) * n;
        }

        // Toma las frecuencias esperadas y observadas y calcula el valor de Chi con la fórmula (O-E)^2 / E
        private static double calcularChiCuadrado(List<int> frecObs, List<double> frecEsp)
        {
            double chi = 0.0;
            for (int i = 0; i < frecObs.Count; i++)
            {
                chi += Math.Pow(frecObs[i] - frecEsp[i], 2) / frecEsp[i];
            }

            return chi;
        }

        // Calcula el KS haciendo |F.E.R - F.O.R|
        private static double calcularKS(List<int> frecObs, List<double> frecEsp, int n)
        {

            // Obtenemos la diferencia absoluta entre las frecuencias relativas
            List<double> difAbsolutaFrecuencias = new List<double>();
            for (int i = 0; i < frecEsp.Count; i++)
            {
                double frecRelativaEsp = frecEsp[i] / n;
                double frecRelativaObs = frecObs[i] / n;
                difAbsolutaFrecuencias.Add(Math.Abs(frecRelativaEsp - frecRelativaObs));
            }

            // Retorna el máximo valor de la lista
            return difAbsolutaFrecuencias.Max();

        }
    }
}
