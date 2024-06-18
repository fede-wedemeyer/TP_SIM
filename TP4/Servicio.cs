using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Servicio
    {
        private TipoServicio tipo { get; set; } 
        private List<Servidor> servidores { get; set; }
        private double media { get; set; }
        private List<Estudiante> cola  {get; set; }

        public Servicio(TipoServicio tipo, int nroServidores, double media)
        {
            this.servidores = new List<Servidor>();
            this.cola = new List<Estudiante>();
            this.tipo = tipo;
            this.media = media;

            for (int i = 0; i < nroServidores; i++)
            {
                this.servidores[i] = new Servidor();
                this.servidores[i].Id = i + 1;
                this.servidores[i].Estado = true;
            }

        }

    }
}
