using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TP_3
{
    public partial class VentanaDatos : Form
    {
        double cantSemanas;
        double ausentesCero;
        double ausentesUno;
        double ausentesDos;
        double ausentesTres;
        double ausentesCuatro;
        double ausentesCinco;
        int cantEmpleadosTotales;
        double costoEmpleados;
        double costoVariable;
        double costoVentas;

        public VentanaDatos(double cantSemanas, double ausentesCero, double ausentesUno, double ausentesDos, double ausentesTres, double ausentesCuatro, double ausentesCinco, int cantEmpleados, double costoEmpleados, double costoVariable, double costoVentas)
        {
            InitializeComponent();
            this.cantSemanas = cantSemanas;
            this.ausentesCero = ausentesCero;
            this.ausentesUno = ausentesUno;
            this.ausentesDos = ausentesDos;
            this.ausentesTres = ausentesTres;
            this.ausentesCuatro = ausentesCuatro;
            this.ausentesCinco = ausentesCinco;
            this.cantEmpleadosTotales = cantEmpleados;
            this.costoEmpleados = costoEmpleados;
            this.costoVariable = costoVariable;
            this.costoVentas = costoVentas;
        }
        public VentanaDatos()
        {
            InitializeComponent();
        }

        public void simular()
        {
            Datos datos = MonteCarlo.simularAusentismo(cantSemanas, ausentesCero, ausentesUno, ausentesDos, ausentesTres, ausentesCuatro, ausentesCinco, cantEmpleadosTotales, costoEmpleados, costoVariable);
            PopulateDataGridView(datos);
            
        }


        public void PopulateDataGridView(Datos datos)
        {
            // Create a DataTable
            DataTable table = new DataTable();

            table.Columns.Add("Dias", typeof(int));
            table.Columns.Add("Semana", typeof(int));
            table.Columns.Add("Random", typeof(double));
            table.Columns.Add("Ausentes", typeof(int));
            table.Columns.Add("Cantidad de empleados", typeof(int));
            table.Columns.Add("¿Fabrica abierta?", typeof(string));
            table.Columns.Add("Costos variables", typeof(double));
            table.Columns.Add("Costo de empleados", typeof(double));
            table.Columns.Add("Costo total", typeof(double));
            table.Columns.Add("Utilidad", typeof(double));
            table.Columns.Add("Utilidad acumulada", typeof(double));

            double utilidadAcumulada = 0;

            for (int i = 0; i < cantSemanas * 5; i++)
            {
                double costoDeEmpleadosDelDia = (cantEmpleadosTotales - datos.ausencias[i]) * costoEmpleados;
                double utilidad = costoVentas - costoVariable - costoDeEmpleadosDelDia;
                int cantidadEmpleados = cantEmpleadosTotales - datos.ausencias[i];

                // Add a new row to the DataTable
                DataRow row = table.NewRow();
                // Populate the row with data from the arrays
                row["Dias"] = datos.dias[i];
                row["Semana"] = datos.semanas[i];
                row["Random"] = datos.randoms[i];
                row["Ausentes"] = datos.ausencias[i];
                row["Cantidad de empleados"] = cantidadEmpleados;
                if (cantidadEmpleados < 20)
                {
                    row["¿Fabrica abierta?"] = "No";
                    row["Costos variables"] = 0;
                    row["Costo de empleados"] = cantEmpleadosTotales * costoEmpleados;
                    row["Costo total"] = cantEmpleadosTotales * costoEmpleados;
                    row["Utilidad"] = (cantEmpleadosTotales * costoEmpleados) * -1;
                    utilidadAcumulada += cantEmpleadosTotales * costoEmpleados * -1;

                }

                else
                {
                    row["¿Fabrica abierta?"] = "Si";
                    row["Costos variables"] = costoVariable;
                    row["Costo de empleados"] = costoDeEmpleadosDelDia;
                    row["Costo total"] = costoVariable + costoDeEmpleadosDelDia;
                    row["Utilidad"] = utilidad;
                    utilidadAcumulada += utilidad;

                }
                row["Utilidad acumulada"] = utilidadAcumulada;



                // Add the populated row to the DataTable
                table.Rows.Add(row);

            }

            dataGridView1.DataSource = table;
            SetearLabels(utilidadAcumulada);

        }

        public void crearDistribucionDeFrecuencias() 
        {
            DataTable table = new DataTable();

            table.Columns.Add("Cantidad de obreros ausentes", typeof(string));
            table.Columns.Add($"Frecuencia de ausencia en días en 22 semanas", typeof(int));
            table.Columns.Add("Frecuencia en %", typeof(double));
            table.Columns.Add("Frecuencia acumulada en %", typeof(double));

            // 22 semanas * 5 dias laborables
            double cantidadDias = 22 * 5;


            DataRow row1 = table.NewRow();
            row1["Cantidad de obreros ausentes"] = "0";
            row1[$"Frecuencia de ausencia en días en 22 semanas"] = (int)Math.Round(cantidadDias * (ausentesCero/100));
            row1["Frecuencia en %"] = ausentesCero;
            row1["Frecuencia acumulada en %"] = ausentesCero;
            table.Rows.Add(row1);

            DataRow row2 = table.NewRow();
            row2["Cantidad de obreros ausentes"] = "1";
            row2[$"Frecuencia de ausencia en días en 22 semanas"] = (int)Math.Round(cantidadDias * (ausentesUno/100));
            row2["Frecuencia en %"] = ausentesUno;
            row2["Frecuencia acumulada en %"] = ausentesCero + ausentesUno;
            table.Rows.Add(row2);

            DataRow row3 = table.NewRow();
            row3["Cantidad de obreros ausentes"] = "2";
            row3[$"Frecuencia de ausencia en días en 22 semanas"] = (int)Math.Round(cantidadDias * (ausentesDos/100));
            row3["Frecuencia en %"] = ausentesDos;
            row3["Frecuencia acumulada en %"] = ausentesCero + ausentesUno + ausentesDos;
            table.Rows.Add(row3);

            DataRow row4 = table.NewRow();
            row4["Cantidad de obreros ausentes"] = "3";
            row4[$"Frecuencia de ausencia en días en 22 semanas"] = (int)Math.Round(cantidadDias * (ausentesTres/100));
            row4["Frecuencia en %"] = ausentesTres;
            row4["Frecuencia acumulada en %"] = ausentesCero + ausentesUno + ausentesDos + ausentesTres;
            table.Rows.Add(row4);

            DataRow row5 = table.NewRow();
            row5["Cantidad de obreros ausentes"] = "4";
            row5[$"Frecuencia de ausencia en días en 22 semanas"] = (int)Math.Round(cantidadDias * (ausentesCuatro/100));
            row5["Frecuencia en %"] = ausentesCuatro;
            row5["Frecuencia acumulada en %"] = ausentesCero + ausentesUno + ausentesDos + ausentesTres + ausentesCuatro;
            table.Rows.Add(row5);

            DataRow row6 = table.NewRow();
            row6["Cantidad de obreros ausentes"] = "5";
            row6[$"Frecuencia de ausencia en días en 22 semanas"] = (int)Math.Round(cantidadDias * (ausentesCinco/100));
            row6["Frecuencia en %"] = ausentesCinco;
            row6["Frecuencia acumulada en %"] = ausentesCero + ausentesUno + ausentesDos + ausentesTres + ausentesCuatro + ausentesCinco;
            table.Rows.Add(row6);


            dataGridView2.DataSource = table;
        }

        private void SetearLabels(double utilidadFinal) 
        { 
            cantSemLbl.Text = cantSemanas.ToString();
            cantEmplLbl.Text = cantEmpleadosTotales.ToString();
            costoEmplLbl.Text = "$ " +costoEmpleados.ToString();
            costosVariablesLbl.Text = "$" + costoVariable.ToString();
            totalVentasLbl.Text = "$" + costoVentas.ToString();
            utilidadAcumuladaLbl.Text = "$ " + utilidadFinal.ToString();

        }

        private void VentanaDatos_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).Dispose();
        }

    }
}
