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
    public partial class VentanaRungeKutta : Form
    {
        DataTable dtRk;
        public VentanaRungeKutta(DataTable dtRk)
        {
            InitializeComponent();
            this.dtRk = dtRk;
        }

        public void generarDataGridView()
        {
            dataGridView1.DataSource = dtRk;
        }
    }
}
