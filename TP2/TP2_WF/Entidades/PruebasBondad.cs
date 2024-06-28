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
        public static (List<double>, double, double, List<double>, List<double>, List<double>, List<double>, List<double>) pruebasUniforme(int n, int[] frecObs)
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
            (double chiCalculado, List<double> listaChis, List<double> listaChisAC) = calcularChiCuadrado(frecObs.ToList(), frecEsp);
            (double ksCalculado, List<double> listFORFER, List<double> listFER, List<double> listFOR) = calcularKS(frecObs.ToList(), frecEsp, n);

            // retornar o mostrar resultados
            Console.WriteLine("CHI: " + chiCalculado);
            Console.WriteLine("KS: " + ksCalculado);

            return (frecEsp, chiCalculado, ksCalculado, listaChis, listaChisAC, listFORFER, listFER, listFOR);

        }

        // Pruebas de bondad para distribución normal
        public static (List<double>, double, double, List<double>, List<double>, List<double>, List<double>, List<double>, List<double>) pruebasNormal(int n, double media, List<int> frecObs, double desviacionEstandar, List<decimal> arrayLimSup, decimal anchoIntervalo)
        {
            List<double> frecEsp = new List<double>();
            List<double> listLimInf = new List<double>();
            Normal normalDist = new Normal(media, desviacionEstandar);

            // Obtenemos la lista de frecuencias esperadas
            for (int i = 0; i < arrayLimSup.Count; i++)
            {
                // Obtenemos los límites inferiores del intervalo restandole al superior el ancho del intervalo
                double limSup = Decimal.ToDouble(arrayLimSup[i]);
                double limInf = limSup - Decimal.ToDouble(anchoIntervalo);
                listLimInf.Add(limInf);
                // Cálculo para la probabilidad del intervalo
                double probabilidadIntervalo = normalDist.CumulativeDistribution(limSup) - normalDist.CumulativeDistribution(limInf);

                frecEsp.Add(probabilidadIntervalo * n);
            };

            unirIntervalos(arrayLimSup, listLimInf, anchoIntervalo, frecEsp, frecObs);

            // obtenemos chi cuadrado y ks
            (double chiCalculado, List<double> listaChis, List<double> listaChisAC) = calcularChiCuadrado(frecObs, frecEsp);
            (double ksCalculado, List<double> listFORFER, List<double> listFER, List<double> listFOR) = calcularKS(frecObs.ToList(), frecEsp, n);
            // retornar o mostrar resultados
            Console.WriteLine("CHI: " + chiCalculado);
            Console.WriteLine("KS: " + ksCalculado);

            return (frecEsp, chiCalculado, ksCalculado, listaChis, listaChisAC, listLimInf, listFORFER, listFER, listFOR);
        }


        // nuevo

        public static (List<double>, double, double, List<double>, List<double>, List<double>, List<double>, List<double>, List<double>) pruebasExponencial(int n, double lambda, List<int> frecObs, List<decimal> arrayLimSup, decimal anchoIntervalo)
        {
            // obtenemos las frecuencias esperadas para cada intervalo
            List<double> frecEsp = new List<double>();
            List<double> listLimInf = new List<double>();

            for (int i = 0; i < arrayLimSup.Count; i++)
            {
                // Obtenemos los límites inferiores del intervalo restandole al superior el ancho del intervalo
                double limSup = Decimal.ToDouble(arrayLimSup[i]);
                double limInf = limSup - Decimal.ToDouble(anchoIntervalo);
                listLimInf.Add(limInf);

                // Se llama a la función que calcula los valores de las frecuencias esperadas y se agregan a la lista de frecEsp
                double frec = frecEspIntervaloExp(lambda, limInf, limSup, n);
                frecEsp.Add(frec);


                Console.WriteLine("Frecuencia observada: " + frecObs[i]);
                Console.WriteLine("Frecuencia esperada: " + frecEsp[i]); 
            };

            unirIntervalos(arrayLimSup, listLimInf, anchoIntervalo, frecEsp, frecObs);

            // obtenemos chi cuadrado y ks
            (double chiCalculado, List<double> listaChis, List<double> listaChisAC) = calcularChiCuadrado(frecObs, frecEsp);
            (double ksCalculado, List<double> listFORFER, List<double> listFER, List<double> listFOR) = calcularKS(frecObs.ToList(), frecEsp, n);

            // retornar o mostrar resultados
            Console.WriteLine("CHI: " + chiCalculado);
            Console.WriteLine("KS: " + ksCalculado);

            return (frecEsp, chiCalculado, ksCalculado, listaChis, listaChisAC, listLimInf, listFORFER, listFER, listFOR);
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
        private static (double, List<double>, List<double>) calcularChiCuadrado(List<int> frecObs, List<double> frecEsp)
        {
            List<double> listaChisAC = new List<double>();
            List<double> listaChis = new List<double>();
            double chi = 0.0;
            for (int i = 0; i < frecObs.Count; i++)
            {
                double chiActual = Math.Pow(frecEsp[i] - frecObs[i], 2) / frecEsp[i];
                chi += chiActual;
                listaChisAC.Add(chi);
                listaChis.Add(chiActual);
            }

            return (chi, listaChis, listaChisAC);
        }

        // Calcula el KS haciendo |F.E.R - F.O.R|
        private static (double, List<double>, List<double>, List<double>) calcularKS(List<int> frecObs, List<double> frecEsp, int n)
        {
            List<double> listFER = new List<double>();
            List<double> listFOR = new List<double>();

            // Obtenemos la diferencia absoluta entre las frecuencias relativas
            List<double> difAbsolutaFrecuencias = new List<double>();
            for (int i = 0; i < frecEsp.Count; i++)
            {
                double frecRelativaEsp = frecEsp[i] / n;
                double frecRelativaObs = (double)frecObs[i] / n;
                difAbsolutaFrecuencias.Add(Math.Abs(frecRelativaEsp - frecRelativaObs));
                listFER.Add(frecRelativaEsp);
                listFOR.Add(frecRelativaObs);
            }

            // Retorna el máximo valor de la lista
            return (difAbsolutaFrecuencias.Max(),difAbsolutaFrecuencias, listFER, listFOR);

        }

        private static void unirIntervalos(List<decimal> arrayLimSup, List<double> arrayLimInf, decimal anchoIntervalo, List<double> frecEsp, List<int> frecObs)
        {
            for (int x = 0; x < arrayLimSup.Count; x++)
            {
                try 
                {
                    while (frecEsp[x] < 5)
                    {
                        Console.WriteLine("SE UNIO UN INTERVALO");

                        if (x == (arrayLimSup.Count - 1))
                        {

                            arrayLimInf.RemoveAt(x);
                            arrayLimSup.RemoveAt(x - 1);
                            frecEsp[x - 1] += frecEsp[x];
                            frecEsp.RemoveAt(x);
                            frecObs[x - 1] += frecObs[x];
                            frecObs.RemoveAt(x);



                        }
                        else
                        {
                            arrayLimInf.RemoveAt(x + 1);
                            arrayLimSup.RemoveAt(x);
                            frecEsp[x] += frecEsp[x + 1];
                            frecEsp.RemoveAt(x + 1);
                            frecObs[x] += frecObs[x + 1];
                            frecObs.RemoveAt(x + 1);

                        }

                    }
                } catch (Exception) { break; }
            }

        }
    }
}
