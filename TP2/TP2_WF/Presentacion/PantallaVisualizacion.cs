using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using TP2_WF.Entidades;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.Common;

namespace TP2_WF.Presentacion
{
    public partial class PantallaVisualizacion : Form
    {
        CsvReader csvReader;
        decimal[] MinMax;
        DataTable CSV;
        int[] frecObs;
        int n;
        int tipoDist;
        double media;
        double desvEstandard;
        double lambda;


        public PantallaVisualizacion(decimal[] minMax, int cantIntervalos, int tamMuestra, int tipoDistribucion)
        {
            // Inicializa los componentes de la pantalla e instancia
            InitializeComponent();

            csvReader = new CsvReader();
            MinMax = minMax;
            CSV = new DataTable();
            frecObs = new int[cantIntervalos];
            n = tamMuestra;
            tipoDist = tipoDistribucion;

        }

        public PantallaVisualizacion(decimal[] minMax, int cantIntervalos, int tamMuestra, int tipoDistribucion, double mean, double stDev)
        {
            // Inicializa los componentes de la pantalla e instancia
            InitializeComponent();

            csvReader = new CsvReader();
            MinMax = minMax;
            CSV = new DataTable();
            frecObs = new int[cantIntervalos];
            n = tamMuestra;
            tipoDist = tipoDistribucion;
            media = mean;
            desvEstandard = stDev;

        }

        public PantallaVisualizacion(decimal[] minMax, int cantIntervalos, int tamMuestra, int tipoDistribucion, double lamb)
        {
            // Inicializa los componentes de la pantalla e instancia
            InitializeComponent();

            csvReader = new CsvReader();
            MinMax = minMax;
            CSV = new DataTable();
            frecObs = new int[cantIntervalos];
            n = tamMuestra;
            tipoDist = tipoDistribucion;
            lambda = lamb;

        }



        private void PantallaVisualizacion_Load(object sender, EventArgs e)
        {

            // Para el eje x:
            decimal[] arrayLimSup = new decimal[frecObs.Length];

            //Obtiene el ancho de intervalo
            decimal anchoIntervalo = (MinMax[1] - MinMax[0]) / frecObs.Length;

            // Se carga el visualizador de datos y las frecuencias observadas
            csvReader.LoadCsvData(CSV, frecObs, MinMax, arrayLimSup);
            gdw_dataSet.DataSource = CSV;

            // Estetico Columnas Tabla
            foreach (DataGridViewColumn columna in gdw_dataSet.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // Establecemos el título del gráfico
            chart1.Titles.Add("Gráfico de Frecuencias Observadas");

            // Se establece la serie para que se vea como histograma
            Series frecObsSerie = new Series();
            frecObsSerie.ChartType = SeriesChartType.Column;
            frecObsSerie.Name = "Frecuencias Observadas";
            chart1.Series.Add(frecObsSerie);
            frecObsSerie.IsVisibleInLegend = false;

            // Se agregan las frecObs al grafico
            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                string intervalo = $"[{Math.Round(arrayLimSup[i] - anchoIntervalo, 3)} - {arrayLimSup[i]}]";
                DataPoint dp = new DataPoint();
                dp.AxisLabel = intervalo;
                dp.YValues = new double[] { frecObs[i] };
                frecObsSerie.Points.Add(dp);

            };

            // Se ejecuta las prueba de bondad para la distribución seleccionada
            List<double> frecEsp;
            double chi;
            double ks;

            if (tipoDist == 0) { (frecEsp, chi, ks) = PruebasBondad.pruebasUniforme(n, frecObs); }

            else if (tipoDist == 1) { (frecEsp, chi, ks) = PruebasBondad.pruebasNormal(n, media, frecObs, desvEstandard, arrayLimSup, anchoIntervalo); }

            else { (frecEsp, chi, ks) = PruebasBondad.pruebasExponencial(n, lambda, frecObs, arrayLimSup, anchoIntervalo); }


            // Configurar etiquetas del eje X
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Title = "Intervalos";

            // Configurar etiquetas del eje Y
            chart1.ChartAreas[0].AxisY.Title = "Frecuencias Observadas";

            // Se genera la tabla de frecObs
            this.cargarTablaFrecObs(arrayLimSup, anchoIntervalo, frecEsp, chi, ks);

            // Se cargan los valores de KS y Chi

            label3.Text = chi.ToString();
            label4.Text = ks.ToString();

        }

        private void cargarTablaFrecObs(decimal[] arrayLimSup, decimal anchoIntervalo, List<double> frecEsp, double chi, double ks)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Intervalo");
            dt.Columns.Add("Frecuencia Observada");
            dt.Columns.Add("Frecuencia Esperada");

            // Agrega los numeros a la tabla y obtiene la frecuencia observada de los intervalos
            decimal limInf;
            calcularIntervalosTabla(arrayLimSup, anchoIntervalo, frecEsp);


            for (int i = 0; i < arrayLimSup.Length; i++)
            {

                limInf = arrayLimSup[i] - anchoIntervalo;
                string intervalo = $"{limInf} <= x < {arrayLimSup[i]}";
                dt.Rows.Add(intervalo, frecObs[i], frecEsp[i]);

            };

            gdw_frecObs.DataSource = dt;
            foreach (DataGridViewColumn columna in gdw_frecObs.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }


        }

        private void calcularIntervalosTabla(decimal[] arrayLimSup, decimal anchoIntervalo, List<double> frecEsp)
        {
            List<decimal> arrayLimInf = new List<decimal>();

            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                arrayLimInf.Add(arrayLimSup[i] - anchoIntervalo);

            }

            List<decimal> newArrayLimInf = new List<decimal>();
            List<decimal> newArrayLimSup = new List<decimal>();
            List<decimal> newObservedFrequency = new List<decimal>();

            // Process the observed frequencies
            for (int i = 0; i < frecObs.Length; i++)
            {
                decimal currentLowerLimit = arrayLimInf[i];
                decimal currentUpperLimit = arrayLimSup[i];
                decimal currentFrequency = frecObs[i];

                // Check if the current frequency is less than 5
                if (currentFrequency < 5)
                {
                    // Find the index of the next interval
                    int nextIndex = i + 1;
                    while (nextIndex < arrayLimInf.Count && frecObs[nextIndex] < 5)
                    {
                        nextIndex++;
                    }

                    // Merge intervals and sum frequencies
                    if (nextIndex < arrayLimInf.Count)
                    {
                        decimal nextLowerLimit = arrayLimInf[nextIndex];
                        decimal nextUpperLimit = arrayLimSup[nextIndex];
                        decimal nextFrequency = frecObs[nextIndex];

                        // Adjust the upper limit of the current interval
                        currentUpperLimit = Math.Min(currentUpperLimit, nextLowerLimit);

                        // Sum the frequencies
                        currentFrequency += nextFrequency;

                        // Add the adjusted interval to the new arrays
                        newObservedFrequency.Add(currentFrequency);
                        newArrayLimInf.Add(currentLowerLimit);
                        newArrayLimSup.Add(currentUpperLimit);
                    }
                    else
                    {
                        // If there's no next interval, just copy the current interval
                        newObservedFrequency.Add(currentFrequency);
                        newArrayLimInf.Add(currentLowerLimit);
                        newArrayLimSup.Add(currentUpperLimit);
                    }
                }
                else
                {
                    // If the frequency is not less than 5, copy the current interval
                    newObservedFrequency.Add(currentFrequency);
                    newArrayLimInf.Add(currentLowerLimit);
                    newArrayLimSup.Add(currentUpperLimit);
                }
            }
            // Convert the new lists to arrays if needed
            decimal[] newArrayLimInfArray = newArrayLimInf.ToArray();
            decimal[] newArrayLimSupArray = newArrayLimSup.ToArray();
            decimal[] newObservedFrequencyArray = newObservedFrequency.ToArray();

            // Print the new arrays
            Console.WriteLine("New arrayLimInf:");
            foreach (var item in newArrayLimInfArray)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\nNew arrayLimSup:");
            foreach (var item in newArrayLimSupArray)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\nNew observedFrequency:");
            foreach (var item in newObservedFrequencyArray)
            {
                Console.Write(item + " ");





                // for (int i = 0; i < newArrayLimSup.Count; i++) { Console.WriteLine(newArrayLimInf[i] + " // " + newArrayLimSup[i] + " // " + newFrecObs[i] + " // " + newFrecEsp[i]); }
            }
        }
    }
}

