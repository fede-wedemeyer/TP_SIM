using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP4
{
    public partial class VentanaSegundaSim : Form
    {
        DataTable dataTable;
        string[] resultados;
        VentanaInicial ventanaInicial;
        public VentanaSegundaSim(DataTable dataTable, string[] resultados, VentanaInicial ventanaInicial)
        {
            InitializeComponent();
            this.dataTable = dataTable;
            this.resultados = resultados;
            this.ventanaInicial = ventanaInicial;
        }


        public void generarDataGridView()
        {
            dataGridView1.DataSource = dataTable;

            // Estilizamos el data grid
            dataGridView1.Columns["Proxima llegada estudiante (Prestamos)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Proxima llegada estudiante (Devoluciones)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Proxima llegada estudiante (Consultas)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Proxima llegada estudiante (Servicio PC)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Proxima llegada estudiante (Información General)"].DefaultCellStyle.ForeColor = Color.Red;

            dataGridView1.Columns["Próximo corte de luz"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Proximo fin de corte de luz"].DefaultCellStyle.ForeColor = Color.Red;

            dataGridView1.Columns["Fin atención 1 (Prestamos)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin atención 2 (Prestamos)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin atención 3 (Prestamos)"].DefaultCellStyle.ForeColor = Color.Red;

            dataGridView1.Columns["Fin atención 1 (Devolución)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin atención 2 (Devolución)"].DefaultCellStyle.ForeColor = Color.Red;


            dataGridView1.Columns["Fin atención 1 (Consultas)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin atención 2 (Consultas)"].DefaultCellStyle.ForeColor = Color.Red;

            dataGridView1.Columns["Fin Atencion PC 1"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin Atencion PC 2"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin Atencion PC 3"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin Atencion PC 4"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin Atencion PC 5"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin Atencion PC 6"].DefaultCellStyle.ForeColor = Color.Red;

            dataGridView1.Columns["Fin atención 1 (Info General)"].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns["Fin atención 2 (Info General)"].DefaultCellStyle.ForeColor = Color.Red;

            dataGridView1.Columns["Fin atención (Membresía)"].DefaultCellStyle.ForeColor = Color.Red;
            
        }

        public void mostrarResultados()
        {

            promPrestamos.Text = resultados[0];
            promDevoluciones.Text = resultados[1];
            promConsultas.Text = resultados[2];
            promPC.Text = resultados[3];
            promInfo.Text = resultados[4];
            promMembresia.Text = resultados[5];

            porcPretamos.Text = resultados[6];
            porcDevoluciones.Text = resultados[7];
            porcConsultas.Text = resultados[8];
            porcPC.Text = resultados[9];
            porcInfo.Text = resultados[10];
            porcMembresía.Text = resultados[11];

            servRapido.Text = resultados[12];

            cantGente.Text = resultados[13];

            totalEstudiantes.Text = resultados[14];

            estAtendidos.Text = resultados[15];

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }
    }
}
