using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    internal class VectorDeEstado
    {
        int Id { get; set; }
        int N {  get; set; }
        string evento { get; set; }
        double reloj { get; set; }
        double RNDLLegadaClientePrestamos { get; set; }
        double tiempoEntreLlegadasClientesPrestamos { get; set; }
        double proximaLlegadaClientesPrestamos { get; set; }
        double RNDLLegadaClienteDevolucion { get; set; }
        double tiempoEntreLlegadasClientesDevolucion { get; set; }
        double proximaLlegadaClientesDevolucion { get; set; }
        double RNDLLegadaClienteConsulta { get; set; }
        double tiempoEntreLlegadasClientesConsulta { get; set; }
        double proximaLlegadaClientesConsulta { get; set; }
        double RNDLLegadaClientePC { get; set; }
        double tiempoEntreLlegadasClientesPC { get; set; }
        double proximaLlegadaClientesPC { get; set; }
        double RNDLLegadaClienteInfoGral { get; set; }
        double tiempoEntreLlegadasClientesInfoGral { get; set; }
        double proximaLlegadaClientesInfoGral { get; set; }
        List<Servicio> servicios { get; set; }
        List<Estudiante> estudiantes { get; set; }
    }
}
