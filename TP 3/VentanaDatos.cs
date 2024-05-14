using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_3
{
    public partial class VentanaDatos : Form
    {
        double ausentesCero;
        double ausentesUno;
        double ausentesDos;
        double ausentesTres;
        double ausentesCuatro;
        double ausentesCinco;
        double cantEmpleados;
        double costoEmpleados;
        double costoVariable;

        public VentanaDatos(double ausentesCero, double ausentesUno, double ausentesDos, double ausentesTres, double ausentesCuatro, double ausentesCinco, double cantEmpleados, double costoEmpleados, double costoVariable)
        {
            InitializeComponent();
            this.ausentesCero = ausentesCero;
            this.ausentesUno = ausentesUno;
            this.ausentesDos = ausentesDos;
            this.ausentesTres = ausentesTres;
            this.ausentesCuatro = ausentesCuatro;
            this.ausentesCinco = ausentesCinco;
            this.cantEmpleados = cantEmpleados;
            this.costoEmpleados = costoEmpleados;
            this.costoVariable = costoVariable;
        }

        private void VentanaDatos_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).Dispose();
        }
    }
}
