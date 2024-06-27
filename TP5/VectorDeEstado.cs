using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class VectorDeEstado
    {
        public int N { get; set; }
        public string evento { get; set; }
        public double reloj { get; set; }
        public double relojAnterior { get; set; }

        public double RNDLLegadaClientePrestamos { get; set; }
        public double tiempoEntreLlegadasClientesPrestamos { get; set; }
        public double proximaLlegadaClientesPrestamos { get; set; }
        public double RNDLLegadaClienteDevolucion { get; set; }
        public double tiempoEntreLlegadasClientesDevolucion { get; set; }
        public double proximaLlegadaClientesDevolucion { get; set; }
        public double RNDLLegadaClienteConsulta { get; set; }
        public double tiempoEntreLlegadasClientesConsulta { get; set; }
        public double proximaLlegadaClientesConsulta { get; set; }
        public double RNDLLegadaClientePC { get; set; }
        public double tiempoEntreLlegadasClientesPC { get; set; }
        public double proximaLlegadaClientesPC { get; set; }
        public double RNDLLegadaClienteInfoGral { get; set; }
        public double tiempoEntreLlegadasClientesInfoGral { get; set; }
        public double proximaLlegadaClientesInfoGral { get; set; }

        public double RNDProximaInterrupcion { get; set; }
        public double tiempoEntreInterupciones { get; set; }
        public double proximaInterrupcion { get; set; }

        public double c {  get; set; }
        public double tiempoEnfriamiento { get; set; }
        public double finEnfriamiento { get; set; }


        public bool[] servidoresPrestamo { get; set; } = [true, true, true];
        public double[] finAtencionPrestamo { get; set; } = new double[3];
        public double RNDFinAtencionPrestamo { get; set; }
        public double tiempoAtencionPrestamo { get; set; }
        public Queue<Estudiante> colaPrestamos { get; set; } = new Queue<Estudiante> { };



        public bool[] servidoresDevolucion { get; set; } = [true, true];
        public double[] finAtencionDevolucion { get; set; } = new double[2];
        public double RNDFinAtencionDevolucion { get; set; }
        public double tiempoAtencionDevolucion { get; set; }
        public Queue<Estudiante> colaDevolucion { get; set; } = new Queue<Estudiante> { };



        public bool[] servidoresConsulta { get; set; } = [true, true];
        public double[] finAtencionConsulta { get; set; } = new double[2];
        public double RNDFinAtencionConsulta { get; set; }
        public double tiempoAtencionConsulta { get; set; }
        public Queue<Estudiante> colaConsultas { get; set; } = new Queue<Estudiante> { };



        public bool[] servidoresPC { get; set; } = [true, true, true, true, true, true];
        public double[] finAtencionPC{ get; set; } = new double[6];
        public double RNDFinAtencionPC{ get; set; }
        public double tiempoAtencionPC { get; set; }
        public Queue<Estudiante> colaPC { get; set; } = new Queue<Estudiante> { };


        public bool[] servidoresInfoGeneral { get; set; } = [true, true];
        public double[] finAtencionInfoGeneral { get; set; } = new double[2];
        public double RNDFinAtencionInfoGeneral { get; set; }
        public double tiempoAtencionInfoGeneral { get; set; }
        public Queue<Estudiante> colaInfoGeneral { get; set; } = new Queue<Estudiante> { };

        public double RNDGestionarMembresia { get; set; }
        public bool booleanoGestiona { get; set; }
        public Servidor servidoresMembresia { get; set; } = new Servidor(Estado.Libre);
        public double finAtencionMembresia { get; set; }
        public double RNDFinAtencionMembresia { get; set; }
        public double tiempoAtencionMembresia { get; set; }
        public double tiempoRestanteAtencionMembresia { get; set; }
        public Queue<Estudiante> colaGestionarMembresia { get; set; } = new Queue<Estudiante> { };


        public List<Estudiante> estudiantes { get; set; } = new List<Estudiante> { };


        // variables estadisticas

        public double acTiempoEsperaPrestamos { get; set; } = 0;
        public double acTiempoEsperaDevoluciones { get; set; } = 0;
        public double acTiempoEsperaConsultas { get; set; } = 0;
        public double acTiempoEsperaPC { get; set; } = 0;
        public double acTiempoEsperaInfoGral { get; set; } = 0;
        public double acTiempoEsperaGestionarMembresia { get; set; } = 0;
        public int atendidosPrestamos { get; set; } = 0;
        public int atendidosConsultas { get; set; } = 0;
        public int atendidosDevoluciones { get; set; } = 0;
        public int atendidosPC { get; set; } = 0;
        public int atendidosInfoGral { get; set; } = 0;
        public int atendidosGestionarMembresia { get; set; } = 0;

        public double acTiempoServicioPrestamos { get; set; } = 0;
        public double acTiempoServicioDevoluciones { get; set; } = 0;
        public double acTiempoServicioConsultas { get; set; } = 0;
        public double acTiempoServicioPC { get; set; } = 0;
        public double acTiempoServicioInfoGral { get; set; } = 0;
        public double acTiempoServicioGestionarMembresia { get; set; } = 0;

        public int mayorNumeroDeGenteEnCola { get; set; } = 0;

    }
}
