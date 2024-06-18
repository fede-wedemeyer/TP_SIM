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
    public partial class VentanaNuevaSimulacion : Form
    {
        DataTable dataTable;
        string[] resultados;
        string valorAnterior;

        public VentanaNuevaSimulacion(DataTable dataTable, string[] resultados, string valorAnterior)
        {
            InitializeComponent();
            this.dataTable = dataTable;
            this.resultados = resultados;
            this.valorAnterior = valorAnterior;
        }


        public void generarDataGridView()
        {
            dataGridView1.DataSource = dataTable;

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

            tiempoAnterior.Text = valorAnterior;
        }
    }
}
