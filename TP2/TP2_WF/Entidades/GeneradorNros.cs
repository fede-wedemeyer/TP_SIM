using System;

namespace TP2_WF.Entidades
{
    class GeneradorNros
    {
        // Ruta relativa al archivo CSV con los datos
        private string _archivoCSV = @"../../Recursos/numeros.csv";

        public void CambiarMinMax(decimal nro_gen, decimal[] arreglo_minMax)
        {
            // nro < min : cambiar min
            if (nro_gen < arreglo_minMax[0])
            {
                arreglo_minMax[0] = nro_gen;
            };
            // nro > max : cambiar max
            if (nro_gen > arreglo_minMax[1])
            {
                arreglo_minMax[1] = nro_gen;
            };
        }

        // Generador de nros aleatorios correspondientes a una Uniforme[A,B)
        public decimal[] Uniforme(int tam_muestra, double a = 0, double b = 1)
        // Parametros de entrada: tamaño de la muestra, a = nro minimo y b = nro máximo
        // En caso de no introducirse a y/o b, se asume Uniforme[0,1)
        {
            // Se establecen las variables iniciales

            // Se abre archivo CSV
            CsvWriter csv = new CsvWriter(_archivoCSV);

            // nro_gen: será el nro generado en cada iteración del ciclo for.
            double nro_gen;
            decimal nro_gen_4dp;

            // rnd: nro aleatorio perteneciente a una Uniforme[0,1)
            Random rnd = new Random();

            //minMax: arreglo que contiene el minimo y el máximo
            decimal[] minMax = new decimal[2];

            for (int i = 0; i < tam_muestra; i++)
            {
                // Aplicamos la fórmula para realizar la generación de nros que siguen una distribución Uniforme[A,B)
                nro_gen = a + rnd.NextDouble() * (b - a);
                nro_gen_4dp = Math.Truncate((decimal)nro_gen * 10000) / 10000;

                // Primera iteracion
                if (i == 0)
                {
                    minMax[0] = nro_gen_4dp;
                    minMax[1] = nro_gen_4dp;
                }
                else
                {
                    // Otras iteraciones
                    CambiarMinMax(nro_gen_4dp, minMax);
                };

                // Carga los datos al csv
                csv.WriteToCsvFile(nro_gen_4dp);
            }
            csv.closeStream();

            // Le sumamos un digito de presición al máximo para que sea contado en las frecuencias observadas
            minMax[1] += (decimal)0.0001;
            return minMax;
        }

        // Generador de nros aleatorios correspondientes a una Normal según Método de Box-Muller
        public decimal[] Normal_BoxMuller(int tam_muestra, double media = 0, double dev_est = 1)
        // Parametros de entrada: tamaño de la muestra, media y deviación estándar
        // En caso de no introducirse media y/o deviación estándar, se asume Normal(0,1)
        {
            // Se establecen las variables iniciales

            // Se abre archivo CSV
            CsvWriter csv = new CsvWriter(_archivoCSV);

            // rnd1 y rnd2: serán los dos random pertenecientes a una Uniforme[0,1) utilizados para la generación de nros.
            // Se inserto una semilla aleatoria porque sino generaba errores
            Random rnd1 = new Random(Guid.NewGuid().GetHashCode());
            Random rnd2 = new Random(Guid.NewGuid().GetHashCode());

            // Variables auxiliares para calculos intermedios:
            double b;
            double ang;

            // nro_gen: será el nros aleatorio generado por los rnd anteriores
            double nro_gen;
            decimal nro_gen_4dp;

            //minMax: arreglo que contiene el minimo y el máximo
            decimal[] minMax = new decimal[2];

            for (int i = 0; i < tam_muestra; i++)
            {
                b = Math.Sqrt(-2 * Math.Log(rnd1.NextDouble()));
                ang = 2 * Math.PI * rnd2.NextDouble();

                nro_gen = media + dev_est * b * Math.Cos(ang);

                nro_gen_4dp = Math.Truncate((decimal)nro_gen * 10000) / 10000;

                // Primera iteracion
                if (i == 0)
                {
                    minMax[0] = nro_gen_4dp;
                    minMax[1] = nro_gen_4dp;
                }
                else
                {
                    // Otras iteraciones
                    CambiarMinMax(nro_gen_4dp, minMax);
                };

                // Carga los datos al csv
                csv.WriteToCsvFile(nro_gen_4dp);
            }
            csv.closeStream();

            // Le sumamos un digito de presición al máximo para que sea contado en las frecuencias observadas
            minMax[1] += (decimal)0.0001;
            return minMax;
        }

        // Generador de nros aleatorios correspondientes a una Normal según Método de Convolusion
        public decimal[] Normal_Convolusion(int tam_muestra, double media = 0, double dev_est = 1)
        // Parametros de entrada: tamaño de la muestra, media y deviación estándar
        // En caso de no introducirse media y/o deviación estándar, se asume Normal(0,1)
        {
            // Se establecen las variables iniciales

            // Se abre archivo CSV
            CsvWriter csv = new CsvWriter(_archivoCSV);

            // rnd: nro aleatorio perteneciente a una Uniforme[0,1)
            Random rnd = new Random();

            // nro_gen: será el nro generado en cada iteración del ciclo for.
            double nro_gen;
            decimal nro_gen_4dp;

            // Variables para la Convolusión
            // acumulador: guardará la sumatoria de los 12 nros aleatorios generados
            double acumulador = 0;
            // Guardara el nro rnd generado
            double x;

            //minMax: arreglo que contiene el minimo y el máximo
            decimal[] minMax = new decimal[2];

            for (int i = 0; i < tam_muestra; i++)
            {
                // Convolusión
                for (int j = 0; j < 12; j++)
                {
                    x = rnd.NextDouble();
                    acumulador += x;
                };

                // Se termina el método
                nro_gen = (acumulador - 6) * dev_est + media;
                nro_gen_4dp = Math.Truncate((decimal)nro_gen * 10000) / 10000;

                // Primera iteracion
                if (i == 0)
                {
                    minMax[0] = nro_gen_4dp;
                    minMax[1] = nro_gen_4dp;
                }
                else
                {
                    // Otras iteraciones
                    CambiarMinMax(nro_gen_4dp, minMax);
                };

                // Carga los datos al csv
                csv.WriteToCsvFile(nro_gen_4dp);

                // Se resetea el acumulador para la próxima generación
                acumulador = 0;
            }
            csv.closeStream();

            // Le sumamos un digito de presición al máximo para que sea contado en las frecuencias observadas
            minMax[1] += (decimal)0.0001;
            return minMax;
        }

        // Generador de nros aleatorios correspondientes a una Exponencial Negativa mediante lambda
        public decimal[] Exponencial_NegativaLambda(int tam_muestra, double lambda)
        // Parametros de entrada: tamaño de la muestra, lambda
        {
            // Se establecen las variables iniciales

            // Se abre archivo CSV
            CsvWriter csv = new CsvWriter(_archivoCSV);

            // rnd: nro aleatorio perteneciente a una Uniforme[0,1)
            Random rnd = new Random();

            // nro_gen: será el nro generado en cada iteración del ciclo for.
            double nro_gen;
            decimal nro_gen_4dp;

            //minMax: arreglo que contiene el minimo y el máximo
            decimal[] minMax = new decimal[2];

            for (int i = 0; i < tam_muestra; i++)
            {
                nro_gen = (-1 / lambda) * Math.Log(1 - rnd.NextDouble());
                nro_gen_4dp = Math.Truncate((decimal)nro_gen * 10000) / 10000;

                // Primera iteracion
                if (i == 0)
                {
                    // En la exponencial negativa, el minimo es siempre 0
                    minMax[0] = 0;
                    minMax[1] = nro_gen_4dp;
                }
                else
                {
                    // Otras iteraciones
                    CambiarMinMax(nro_gen_4dp, minMax);
                };

                // Carga los datos al csv
                csv.WriteToCsvFile(nro_gen_4dp);
            }

            csv.closeStream();

            // Le sumamos un digito de presición al máximo para que sea contado en las frecuencias observadas
            minMax[1] += (decimal)0.0001;
            return minMax;
        }

        // Generador de nros aleatorios correspondientes a una Exponencial Negativa mediante media
        public decimal[] Exponencial_NegativaMedia(int tam_muestra, double media)
        // Parametros de entrada: tamaño de la muestra, media
        {
            // Se calcula lambda y se utiliza la función anterior
            double lambda = 1 / media;
            return Exponencial_NegativaLambda(tam_muestra, lambda);
        }
    }
}


