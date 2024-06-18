using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Estudiante
    {
        public int id;
        public string estado;
        public double tiempoLlegada;
        public double tiempoFinAtencion;
        public TipoServicio servicio;
        public int servidor;
        public double horaInicioAtencion;

        public Estudiante(
            string estado,
            double tiempoLlegada,
            TipoServicio servicio)
        {
            this.estado = estado;
            this.tiempoLlegada = tiempoLlegada;

            this.servicio = servicio;
            servidor = -1;

        }

    }
}
