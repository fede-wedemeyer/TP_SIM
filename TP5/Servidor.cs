using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Servidor
    {
        public Estado estado { get; set; }
        public Servidor(Estado estado) { this.estado = estado; }

    }

    public enum Estado { Libre, Ocupado, Interrumpido }

}
