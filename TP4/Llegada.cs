using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Llegada
    {
        string tipo { get; set; }
        double media { get; set; }
        double rnd { get; set; }
        double tiempoEntreLlegadas { get; set; }
        double proximaLlegada { get; set; }

        public Llegada(string tipo, double media)
        {
            this.tipo = tipo;
            this.media = media;

        }

    }


}
