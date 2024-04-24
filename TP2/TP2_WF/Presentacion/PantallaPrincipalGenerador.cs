using System;
using System.Windows.Forms;
using TP2_WF.Entidades;
using TP2_WF.Presentacion;

namespace TP2_WF
{
    public partial class PantallaPrincipalGenerador : Form
    {
        ValidadorParametros validadorParametros;
        GeneradorNros generadorNros;
        private decimal[] minMax;

        public PantallaPrincipalGenerador()
        {
            // Inicializa los componentes del form e instancia el validador y el generador de numeros
            InitializeComponent();
            validadorParametros = new ValidadorParametros();
            generadorNros = new GeneradorNros();
            minMax = new decimal[2];
        }

        private void cbo_selectDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Dependiendo de la distribución seleccionada los parametros a cargar cambian,
            // por lo que se refleja en los labels, y si corresponde radio buttons

            if (cbo_selectDist.SelectedIndex == 0)
            {
                // Que se habilita e inhabilita al seleccionar Uniforme
                lbl_param1.Text = "Límite Inferior del intervalo";
                lbl_param2.Text = "Límite Superior del intervalo";
                txt_param1.Enabled = true;
                txt_param2.Enabled = true;
                gbx_radioButtons.Hide();
                txt_tamM.Text = "";
                txt_param1.Text = "";
                txt_param2.Text = "";

            }
            else if (cbo_selectDist.SelectedIndex == 1)
            {
                // Que se habilita e inhabilita al seleccionar Normal
                lbl_param1.Text = "Media de la muestra";
                lbl_param2.Text = "Desviación Estandar de la muestra";
                txt_param1.Enabled = true;
                txt_param2.Enabled = true;
                gbx_radioButtons.Hide();
                txt_tamM.Text = "";
                txt_param1.Text = "";
                txt_param2.Text = "";

            }
            else
            {
                // Que se habilita e inhabilita al seleccionar Exponencial Negativa
                lbl_param1.Text = "Lambda de la muestra";
                lbl_param2.Text = "Media de la muestra";
                gbx_radioButtons.Show();
                txt_param1.Enabled = true;
                txt_param2.Enabled = false;
                rbt_lambda.Checked = true;
                txt_tamM.Text = "";
                txt_param1.Text = "";
                txt_param2.Text = "";

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Establece los valores por defecto de los comboBox al cargar el forms
            cbo_selectDist.SelectedIndex = 0;
            cbo_selectInterval.SelectedIndex = 0;
        }

        private bool estaVacio(string texto)
        {
            return texto == "";
        }


        private void button1_Click(object sender, EventArgs e)
        {

            // Se obtiene la cantidad de intervalos mediante el combo box
            int cantIntervalos = int.Parse(cbo_selectInterval.Text);

            // Se valida que el textBox muestra no este vacio
            if (estaVacio(txt_tamM.Text))
            {
                MessageBox.Show("Tamaño de muestra no ingresado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Convertimos el string obtenido del form a int
            int tamMuestra = int.Parse(txt_tamM.Text);

            // Se valida que el tamaño de muestra sea superior a cero
            bool muestraValida = validadorParametros.validarSuperiorACero(tamMuestra);
            if (!muestraValida)
            {
                MessageBox.Show("El tamaño de muestra no es valido.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Dependiendo la distribución el camino a tomar es distinto
            if (cbo_selectDist.SelectedIndex == 0)
            {
                // Se valida que no esten vacios los parametros
                if (estaVacio(txt_param1.Text) || estaVacio(txt_param2.Text))
                {
                    MessageBox.Show("No se ingresaron los dos parametros.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convertimos los strings obtenidos del form a double
                double paramLimInf = Double.Parse(txt_param1.Text);
                double paramLimSup = Double.Parse(txt_param2.Text);
                // Verificamos que los parametros sean validos (limite inferior < limite superior)                
                bool parametrosValidos = validadorParametros.validarParametrosUniforme(paramLimInf, paramLimSup);

                if (!parametrosValidos)
                {
                    MessageBox.Show("Los parametros ingresados no son validos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Se ejecuta el generador
                minMax = generadorNros.Uniforme(tamMuestra, paramLimInf, paramLimSup);

                // Generamos la pantalla con la visualización de los datos
                PantallaVisualizacion pantallaVisualizacion = new PantallaVisualizacion(minMax, cantIntervalos, tamMuestra, cbo_selectDist.SelectedIndex);
                pantallaVisualizacion.ShowDialog();

            }

            else if (cbo_selectDist.SelectedIndex == 1)
            {
                // Se valida que no esten vacios los parametros
                if (estaVacio(txt_param1.Text) || estaVacio(txt_param2.Text))
                {
                    MessageBox.Show("No se ingresaron los dos parametros.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convertimos los strings obtenidos del form a double
                double paramLimInf = Double.Parse(txt_param1.Text);
                double paramLimSup = Double.Parse(txt_param2.Text);


                // Convertimos los strings obtenidos del form a double
                double paramMedia = Double.Parse(txt_param1.Text);
                double paramSD = Double.Parse(txt_param2.Text);

                // Verificamos que la deviación estandar sea mayor a 0
                bool parametrosValidos = validadorParametros.validarSuperiorACero(paramSD);

                if (!parametrosValidos)
                {
                    MessageBox.Show("Los parametros ingresados no son validos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Se ejecuta el generador
                minMax = generadorNros.Normal_BoxMuller(tamMuestra, paramMedia, paramSD);
                PantallaVisualizacion pantallaVisualizacion = new PantallaVisualizacion(minMax, cantIntervalos, tamMuestra, cbo_selectDist.SelectedIndex, paramMedia, paramSD);
                pantallaVisualizacion.ShowDialog();
            }

            else
            {
                if (rbt_lambda.Checked)
                {
                    // Se valida que no este vacio el parametro
                    if (estaVacio(txt_param1.Text))
                    {
                        MessageBox.Show("No se ingresaro el parametro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Convertimos el string obtenido del form a double
                    double paramLambda = Double.Parse(txt_param1.Text);
                    bool parametrosValidos = validadorParametros.validarSuperiorACero(paramLambda);

                    // Verificamos que la lambda sea mayor a 0
                    if (!parametrosValidos)
                    {
                        MessageBox.Show("Los parametros ingresados no son validos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Se ejecuta el generador
                    minMax = generadorNros.Exponencial_NegativaLambda(tamMuestra, paramLambda);

                    // Se instancia la pantalla y se le envian los datos por parametro
                    PantallaVisualizacion pantallaVisualizacion = new PantallaVisualizacion(minMax, cantIntervalos, tamMuestra, cbo_selectDist.SelectedIndex, paramLambda);
                    pantallaVisualizacion.ShowDialog();
                }

                else
                {
                    // Se valida que no este vacio el parametro
                    if (estaVacio(txt_param2.Text))
                    {
                        MessageBox.Show("No se ingresaro el parametro.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Convertimos el string obtenido del form a double
                    double paramMedia = Double.Parse(txt_param2.Text);

                    // Verificamos que la media sea mayor a 0
                    bool parametrosValidos = validadorParametros.validarSuperiorACero(paramMedia);

                    if (!parametrosValidos)
                    {
                        MessageBox.Show("Los parametros ingresados no son validos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Se ejecuta el generador
                    minMax = generadorNros.Exponencial_NegativaMedia(tamMuestra, paramMedia);

                    // Se instancia la pantalla y se le envian los datos por parametro
                    PantallaVisualizacion pantallaVisualizacion = new PantallaVisualizacion(minMax, cantIntervalos, tamMuestra, cbo_selectDist.SelectedIndex, paramMedia);
                    pantallaVisualizacion.ShowDialog();
                }
            }
        }

        private void rbt_lambda_CheckedChanged(object sender, EventArgs e)
        {
            // Inhabilita el Textbox de Media al estar seleccionado
            txt_param1.Enabled = true;
            txt_param2.Enabled = false;
            txt_param2.Text = "";

        }

        private void rbt_media_CheckedChanged(object sender, EventArgs e)
        {
            // Inhabilita el Textbox de Lambda al estar seleccionado
            txt_param1.Enabled = false;
            txt_param2.Enabled = true;
            txt_param1.Text = "";
        }

        private void txt_param2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo digitos, caracteres de control y un solo punto decimal o signo menos
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != ',' || txt_param2.Text.Contains(",")) &&
                (e.KeyChar != '-' || txt_param2.Text.Length != 0))
            {
                e.Handled = true;
            }
        }

        private void txt_param1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo digitos, caracteres de control y un solo punto decimal o signo menos
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != ',' || txt_param2.Text.Contains(",")) &&
                (e.KeyChar != '-' || txt_param2.Text.Length != 0))
            {
                e.Handled = true;
            }
        }

        private void txt_tamM_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo digitos y caracteres de control 
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

		private void cbo_selectInterval_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
