using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Evento : IComparable<Evento>
    {
        public TipoEvento tipo { get; set; }
        public TipoServicio servicio { get; set; }
        public double tiempo { get; set; }

        public double media;

        public Evento(TipoEvento tipo, TipoServicio servicio, double tiempo, double media)
        {
            this.tipo = tipo;
            this.servicio = servicio;
            this.tiempo = tiempo;
            this.media = media;

        }

        public int CompareTo(Evento? other)
        {
            return this.tiempo.CompareTo(other.tiempo);
        }
    }

    public enum TipoEvento
    {
        Llegada,
        FinAtencion
    }

    public enum TipoServicio
    {
        Prestamos,
        Devolucion,
        Consulta,
        PC,
        InfoGral,
        GestionarMembresia
    }
}
