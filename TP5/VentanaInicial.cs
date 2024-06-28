using System.Data;
using System.Runtime.CompilerServices;

namespace TP4
{
    public partial class VentanaInicial : Form
    {
        public VentanaInicial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                int N = int.Parse(cantIteraciones.Text);
                int mostrarDesde = int.Parse(desdeLinea.Text);

                double mediaPrestamoLlegada = calcularMedia(int.Parse(prestamoEstudianteRate.Text), int.Parse(prestamoEstudianteTime.Text));
                double mediaDevolucionLlegada = calcularMedia(int.Parse(devolucionEstudianteRate.Text), int.Parse(devolucionEstudianteTime.Text));
                double mediaConsultaLlegada = calcularMedia(int.Parse(consultaEstudianteRate.Text), int.Parse(consultaEstudianteTime.Text));
                double mediaPCLlegada = calcularMedia(int.Parse(pcEstudianteRate.Text), int.Parse(pcEstudianteTime.Text));
                double mediaInfolegada = calcularMedia(int.Parse(infoEstudianteRate.Text), int.Parse(infoEstudianteTime.Text));

                double mediaPrestamo = calcularMedia(int.Parse(prestamoRate.Text), int.Parse(prestamoTime.Text));
                double mediaDevolucion = calcularMedia(int.Parse(devolucionRate.Text), int.Parse(devolucionTime.Text));
                double mediaConsulta = calcularMedia(int.Parse(consultaRate.Text), int.Parse(consultaTime.Text));
                double mediaPC = calcularMedia(int.Parse(pcRate.Text), int.Parse(pcTime.Text));
                double mediaInfo = calcularMedia(int.Parse(infoRate.Text), int.Parse(infoTime.Text));
                double mediaMembresia = calcularMedia(int.Parse(membRate.Text), int.Parse(membTime.Text));
                double probabilidadMembresia = double.Parse(probMemb.Text) / 100;

                Simulacion simulacion = new Simulacion();
                (DataTable dt, DataTable dtRk) = simulacion.simular(N, mediaPrestamoLlegada, mediaDevolucionLlegada, mediaConsultaLlegada, mediaPCLlegada, mediaInfolegada, mostrarDesde, mediaPrestamo, mediaDevolucion, mediaConsulta, mediaPC, mediaInfo, mediaMembresia, probabilidadMembresia, 3, false);
                string[] resultados = simulacion.calcularVariablesEstadisticas();

                VentanaDatos vd = new VentanaDatos(dt, resultados, this, dtRk);
                vd.generarDataGridView();
                vd.mostrarResultados();
                vd.ShowDialog();
            }

            catch (FormatException ex) { MessageBox.Show("Por favor ingrese los parámetros correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            

        }

        private double calcularMedia(int rate, int time)
        {
            double media;
            double lambda = (double)rate / (double)time;
            media = 1 / lambda;
            return media;

        }

        private void cantIteraciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void desdeLinea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void probMemb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void membRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void prestamoEstudianteRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void prestamoEstudianteTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void devolucionEstudianteRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void devolucionEstudianteTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void consultaEstudianteRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void consultaEstudianteTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void pcEstudianteRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void pcEstudianteTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void infoEstudianteRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void infoEstudianteTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void prestamoRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void prestamoTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void devolucionRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void devolucionTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void consultaRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void consultaTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void pcRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void pcTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void infoRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void infoTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void membTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        public void simularConUnoMenos() 
        {

            int N = int.Parse(cantIteraciones.Text);
            int mostrarDesde = int.Parse(desdeLinea.Text);

            double mediaPrestamoLlegada = calcularMedia(int.Parse(prestamoEstudianteRate.Text), int.Parse(prestamoEstudianteTime.Text));
            double mediaDevolucionLlegada = calcularMedia(int.Parse(devolucionEstudianteRate.Text), int.Parse(devolucionEstudianteTime.Text));
            double mediaConsultaLlegada = calcularMedia(int.Parse(consultaEstudianteRate.Text), int.Parse(consultaEstudianteTime.Text));
            double mediaPCLlegada = calcularMedia(int.Parse(pcEstudianteRate.Text), int.Parse(pcEstudianteTime.Text));
            double mediaInfolegada = calcularMedia(int.Parse(infoEstudianteRate.Text), int.Parse(infoEstudianteTime.Text));

            double mediaPrestamo = calcularMedia(int.Parse(prestamoRate.Text), int.Parse(prestamoTime.Text));
            double mediaDevolucion = calcularMedia(int.Parse(devolucionRate.Text), int.Parse(devolucionTime.Text));
            double mediaConsulta = calcularMedia(int.Parse(consultaRate.Text), int.Parse(consultaTime.Text));
            double mediaPC = calcularMedia(int.Parse(pcRate.Text), int.Parse(pcTime.Text));
            double mediaInfo = calcularMedia(int.Parse(infoRate.Text), int.Parse(infoTime.Text));
            double mediaMembresia = calcularMedia(int.Parse(membRate.Text), int.Parse(membTime.Text));
            double probabilidadMembresia = double.Parse(probMemb.Text) / 100;


            Simulacion sim = new Simulacion();
            (DataTable dt, DataTable dtRk) = sim.simular(N, mediaPrestamoLlegada, mediaDevolucionLlegada, mediaConsultaLlegada, mediaPCLlegada, mediaInfolegada, mostrarDesde, mediaPrestamo, mediaDevolucion, mediaConsulta, mediaPC, mediaInfo, mediaMembresia, probabilidadMembresia, 3, true);
            string[] resultados = sim.calcularVariablesEstadisticas();

            VentanaDatos vd = new VentanaDatos(dt, resultados, this, dtRk);
            vd.generarDataGridView();
            vd.mostrarResultados();
            vd.ShowDialog();

        }

    }
}
