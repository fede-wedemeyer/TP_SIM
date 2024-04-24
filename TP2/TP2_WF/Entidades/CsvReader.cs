using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;

namespace TP2_WF.Entidades
{
    internal class CsvReader
    {
        // Ruta relativa al archivo CSV con los datos
        private string _archivoCSV = @"../../Recursos/numeros.csv";

        public void LoadCsvData(DataTable dt, int[] frecObs, decimal[] minMax, decimal[] arrayLimSup)
        {
            // Carga con los numeros contenidos en el CSV una tabla, un array (frecObs) con las frecuencias
            // observadas de cada intervalo y otro array (arrayLimSup) con los limites superiores de cada intervalo
            try
            {
               // Crea la cabecera de la tabla
                dt.Columns.Add("Numeros Generados");

                //Leer lineas del archivo:
                string[] lines = File.ReadAllLines(_archivoCSV);

                //Obtiene el ancho de intervalo
                decimal anchoIntervalo = (minMax[1] - minMax[0]) / frecObs.Length;

                //Obtiene arrayLimSup con los limites superiores de los intervalos
                arrayLimSup = establecerLimSupInt(arrayLimSup, minMax[0], anchoIntervalo);

                // Agrega los numeros a la tabla y obtiene la frecuencia observada de los intervalos
                foreach (string line in lines)
                {
                    dt.Rows.Add(line);
                    this.asignarIntervalo(decimal.Parse(line), frecObs, arrayLimSup);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fallo al cargar datos del CSV: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public decimal[] establecerLimSupInt(decimal[] arrayLimSup, decimal minimo, decimal anchoIntervalo)
        {
            // Carga el arrayLimSup con los limites superiores de cada intervalo (Desde el primero al ultimo)
            for (int i =0; i<arrayLimSup.Length; i++)
            {
                arrayLimSup[i] = minimo + (i + 1) * anchoIntervalo;
            }

            return arrayLimSup;
        }

        private void asignarIntervalo(decimal val, int[] frecObs, decimal[] arrayLimSup)
        {
            // Recibe un valor por parametro (val) que va a comparar con los limites superiores en el vector arrayLimSup
            // y suma 1 al numero de intervalo que corresponda en el array frecObs


            for (int i = 0; i<arrayLimSup.Length; i++)
            {
                // Valida que el valor sea menor que el limite superior del intervalo, si es mayor o
                // igual se procede a verificar lo mismo con el intervalo siguiente
                if(val < arrayLimSup[i])
                {
                    // Se suma 1 a la frecuencia observada del intervalo que corresponda
                    frecObs[i]++;
                    return;
                }
            }
        }

    }
}