using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    internal class Simulacion
    {
        public VectorDeEstado vectorUno { get; set; }
        public VectorDeEstado vectorDos { get; set; }
        int cantidadSimulaciones { get; set; }


        public Simulacion()
        {
        vectorUno = new VectorDeEstado();
        vectorDos = new VectorDeEstado();

        }

        public void inicializar(int cantServidoresPrestamo, int cantServidoresDevolucion, 
            int cantServidoresConsulta, int cantServidoresPC, int cantServidoresInfoGral, 
            double mediaPrestamo, double mediaDevolucion, double mediaConsulta, 
            double mediaPC, double mediaInfoGral) 
        {
            Servicio ServicioPrestamo = new Servicio("servicio_prestamo", cantServidoresPrestamo, mediaPrestamo);
            Servicio ServicioDevolucion = new Servicio("servicio_devolucion", cantServidoresDevolucion, mediaDevolucion);
            Servicio ServicioConsulta = new Servicio("servicio_consulta", cantServidoresConsulta, mediaConsulta);
            Servicio ServicioPC = new Servicio("servicio_pc", cantServidoresPC, mediaPC);
            Servicio ServicioInfoGral = new Servicio("servicio_info_gral", cantServidoresInfoGral, mediaInfoGral);



        }

        public void simular()
        {


        }
    }

    
}
