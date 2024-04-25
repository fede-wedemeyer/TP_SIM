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
                string intervalo = $"[{arrayLimSup[i] - anchoIntervalo},{arrayLimSup[i]})";
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

            for (int i = 0; i< arrayLimSup.Length;i++)
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
    }
}

