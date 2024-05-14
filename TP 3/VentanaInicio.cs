namespace TP_3
{
    public partial class VentanaInicio : Form
    {
        public VentanaInicio()
        {
            InitializeComponent();
        }

        private void ausentesCero_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void ausenteUno_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void ausenteDos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void ausenteTres_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void ausenteCuatro_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void ausenteCinco_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void cantSemanas_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void cantObreros_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void costoObrero_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void costoVariable_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }


        private void precioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chequea si los caracteres son numeros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }

        }

        private void simularBtn_Click(object sender, EventArgs e)
        {


            double sumaPorcentaje = double.Parse(ausentesCero.Text) + double.Parse(ausenteUno.Text) + double.Parse(ausenteDos.Text) +
                double.Parse(ausenteTres.Text) + double.Parse(ausenteCuatro.Text) + double.Parse(ausenteCinco.Text);

            if (sumaPorcentaje != 100) { MessageBox.Show("La suma de los porcentajes debe ser igual a 100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                this.Visible = false;

                VentanaDatos simulacion = new VentanaDatos(double.Parse(cantSemanas.Text), double.Parse(ausentesCero.Text), double.Parse(ausenteUno.Text), double.Parse(ausenteDos.Text),
                    double.Parse(ausenteTres.Text), double.Parse(ausenteCuatro.Text), double.Parse(ausenteCinco.Text), int.Parse(cantObreros.Text),
                    double.Parse(costoObrero.Text), double.Parse(costoVariable.Text), double.Parse(precioVenta.Text));

                simulacion.simular();
                simulacion.Show();

            }
        }
    }
}
