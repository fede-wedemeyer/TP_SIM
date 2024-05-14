using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_3
{
    public class Datos
    {
        public List<int> dias { get; set; }
        public List<int> semanas { get; set; }
        public List<double> randoms { get; set; }
        public List<int> ausencias { get; set; }

        public Datos(List<int> dias, List<int> semanas, List<double> randoms, List<int> ausencias)
        {
            this.dias = dias;
            this.semanas = semanas;
            this.randoms = randoms;
            this.ausencias = ausencias;
        }
    }
}
