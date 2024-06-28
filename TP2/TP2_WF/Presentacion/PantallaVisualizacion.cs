using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using TP2_WF.Entidades;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.Common;
using System.Linq;

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

            List<decimal> listLimSup = arrayLimSup.ToList(); // LO convertimos a lista PORQUE ES MEJOR
            List<int> frecObsList = frecObs.ToList();

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
                string intervalo = $"[{Math.Round(arrayLimSup[i] - anchoIntervalo, 2)} - {Math.Round(arrayLimSup[i], 2)}]";
                DataPoint dp = new DataPoint();
                dp.AxisLabel = intervalo;
                dp.YValues = new double[] { frecObs[i] };
                frecObsSerie.Points.Add(dp);

            };

            // Se ejecuta las prueba de bondad para la distribución seleccionada
            List<double> frecEsp;
            List<double> listaChis = new List<double>();
            List<double> listaChisAC = new List<double>();
            List<double> listLimInf = new List<double>();
            List<double> listFORFER = new List<double>();
            List<double> listFER = new List<double>();
            List<double> listFOR = new List<double>();
            double chi;
            double ks;

            if (tipoDist == 0) 
            { 
                (frecEsp, chi, ks, listaChis, listaChisAC, listFORFER, listFER, listFOR) = PruebasBondad.pruebasUniforme(n, frecObs);

                // Configurar etiquetas del eje X
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.Title = "Intervalos";

                // Configurar etiquetas del eje Y
                chart1.ChartAreas[0].AxisY.Title = "Frecuencias Observadas";

                // Se genera la tabla de frecObs
                this.cargarTablaFrecObsUniforme(listLimSup, anchoIntervalo, frecEsp, chi, ks, listaChis, listaChisAC, listFORFER, listFER, listFOR);

                // Se cargan los valores de KS y Chi

                label3.Text = Math.Round(chi, 3).ToString();
                label4.Text = Math.Round(ks, 3).ToString();
            }

            else if (tipoDist == 1) {

                (frecEsp, chi, ks, listaChis, listaChisAC, listLimInf, listFORFER, listFER, listFOR) = PruebasBondad.pruebasNormal(n, media, frecObsList, desvEstandard, listLimSup, anchoIntervalo);

                // Configurar etiquetas del eje X
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.Title = "Intervalos";

                // Configurar etiquetas del eje Y
                chart1.ChartAreas[0].AxisY.Title = "Frecuencias Observadas";

                // Se genera la tabla de frecObs
                cargarTablaFrecObs(listLimSup, listLimInf, frecEsp, frecObsList, chi, ks, listaChis, listaChisAC, listFORFER, listFER, listFOR);

                // Se cargan los valores de KS y Chi
                label3.Text = Math.Round(chi, 3).ToString();
                label4.Text = Math.Round(ks, 3).ToString();
            }

            else 
            { 
                
                (frecEsp, chi, ks, listaChis, listaChisAC, listLimInf, listFORFER, listFER, listFOR) = PruebasBondad.pruebasExponencial(n, lambda, frecObsList, listLimSup, anchoIntervalo);

                // Configurar etiquetas del eje X
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.ChartAreas[0].AxisX.Title = "Intervalos";

                // Configurar etiquetas del eje Y
                chart1.ChartAreas[0].AxisY.Title = "Frecuencias Observadas";

                // Se genera la tabla de frecObs
                cargarTablaFrecObs(listLimSup, listLimInf, frecEsp, frecObsList, chi, ks, listaChis, listaChisAC, listFORFER, listFER, listFOR);

                // Se cargan los valores de KS y Chi

                label3.Text = Math.Round(chi,3).ToString();
                label4.Text = Math.Round(ks,3).ToString();
            } 

        }

        private void cargarTablaFrecObsUniforme(List<decimal> arrayLimSup, decimal anchoIntervalo, List<double> frecEsp, double chi, double ks, List<double> listaChis, List<double> listaChisAC, List<double> listFORFER, List<double> listFER, List<double> listFOR)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Intervalo");
            dt.Columns.Add("Frecuencia Observada");
            dt.Columns.Add("Frecuencia Esperada");
            dt.Columns.Add("Chi actual");
            dt.Columns.Add("Chi calculado");
            dt.Columns.Add("Frec. Esperada relativa");
            dt.Columns.Add("Frec. Observada relativa");
            dt.Columns.Add("|FOR - FER|");

            // Agrega los numeros a la tabla y obtiene la frecuencia observada de los intervalos
            //var newLimInf = calcularIntervalosTabla(arrayLimSup, anchoIntervalo, frecEsp);

            for (int i = 0; i < arrayLimSup.Count; i++)
            {
                string intervalo = $"{Math.Round(arrayLimSup[i] - anchoIntervalo, 2)} <= x < {Math.Round(arrayLimSup[i], 2)}";
                dt.Rows.Add(intervalo, frecObs[i], Math.Round(frecEsp[i], 3), Math.Round(listaChis[i], 3), Math.Round(listaChisAC[i], 3), Math.Round(listFER[i], 3), Math.Round(listFOR[i], 3), Math.Round(listFORFER[i], 3));

            };

            gdw_frecObs.DataSource = dt;
            foreach (DataGridViewColumn columna in gdw_frecObs.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        private void cargarTablaFrecObs(List<decimal> arrayLimSup, List<double> listLimInf, List<double> frecEsp, List<int> newFrecObs, double chi, double ks, List<double> listaChis, List<double> listaChisAC, List<double> listFORFER, List<double> listFER, List<double> listFOR) 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Intervalo");
            dt.Columns.Add("Frecuencia Observada");
            dt.Columns.Add("Frecuencia Esperada");
            dt.Columns.Add("Chi actual");
            dt.Columns.Add("Chi calculado");
            dt.Columns.Add("Frec. Esperada relativa");
            dt.Columns.Add("Frec. Observada relativa");
            dt.Columns.Add("|FOR - FER|");

            // Agrega los numeros a la tabla y obtiene la frecuencia observada de los intervalos
            //var newLimInf = calcularIntervalosTabla(arrayLimSup, anchoIntervalo, frecEsp);

            for (int i = 0; i < arrayLimSup.Count; i++)
            {
                string intervalo = $"{Math.Round(listLimInf[i], 2)} <= x < {Math.Round(arrayLimSup[i], 2)}";
                dt.Rows.Add(intervalo, newFrecObs[i], Math.Round(frecEsp[i], 3), Math.Round(listaChis[i], 3), Math.Round(listaChisAC[i], 3), Math.Round(listFER[i], 3), Math.Round(listFOR[i], 3), Math.Round(listFORFER[i], 3));

            };

            gdw_frecObs.DataSource = dt;
            foreach (DataGridViewColumn columna in gdw_frecObs.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}

