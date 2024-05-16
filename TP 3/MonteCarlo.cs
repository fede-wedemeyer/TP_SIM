using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_3
{
    internal class MonteCarlo
    {

        public static Datos simularAusentismo(double cantSemanas, double ausentesCero, double ausentesUno, double ausentesDos, double ausentesTres, double ausentesCuatro, double ausentesCinco, double cantEmpleados, double costoEmpleados, double costoVariable) 
        {
            Random random = new Random();

            List<int> dias = new List<int>();
            List<int> semanas = new List<int>();
            List<double> randoms = new List<double>();
            List<int> ausencias = new List<int>();

            int semanaActual = 1;
            int diaActual = 1;
            
            for (int i = 0; i < cantSemanas; i++) 
            { 
                for (int j = 0; j < 5; j++) 
                {
                    double rnd = random.NextDouble();
                    randoms.Add(Math.Round(rnd, 2));
                    semanas.Add(semanaActual);
                    dias.Add(diaActual++);
                    ausencias.Add(calcularAusente(rnd, ausentesCero, ausentesUno, ausentesDos, ausentesTres, ausentesCuatro, ausentesCinco));
                }
                semanaActual++;
            }
            return new Datos(dias, semanas, randoms, ausencias);
        }


        private static int calcularAusente(double rnd, double ausentesCero, double ausentesUno, double ausentesDos, double ausentesTres, double ausentesCuatro, double ausentesCinco)
        {
            if (rnd < ausentesCero / 100)
            {
                return 0;
            }
            else if (rnd < (ausentesCero + ausentesUno) / 100)
            {
                return 1;
            }
            else if (rnd < (ausentesCero + ausentesUno + ausentesDos) / 100)
            {
                return 2;
            }
            else if (rnd < (ausentesCero + ausentesUno + ausentesDos + ausentesTres) / 100)
            {
                return 3;
            }
            else if (rnd < (ausentesCero + ausentesUno + ausentesDos + ausentesTres + ausentesCuatro) / 100)
            {
                return 4;
            }
            else
            {
                return 5;
            }

        }
    }
}
