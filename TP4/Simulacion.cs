using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Simulacion
    {
        public VectorDeEstado vectorDeEstado { get; set; }
        int cantidadSimulaciones { get; set; }


        public Simulacion()
        {
            vectorDeEstado = new VectorDeEstado();
        }

        public DataTable simular(int cantIteraciones, double mediaPrestamo, double mediaDevolucion, double mediaConsulta,
            double mediaPC, double mediaInfoGral, int mostrarDesde, double mediaFinPrestamo, double mediaFinDevolucion, 
            double mediaFinConsulta, double mediaFinPC, double mediaFinInfoGral, double mediaFinGestionarMembresia, double probMemb)
        {
            vectorDeEstado.N = 0;
            vectorDeEstado.evento = "inicializacion";
            vectorDeEstado.reloj = 0.00;

            Random rnd = new Random();
            vectorDeEstado.RNDLLegadaClientePrestamos = rnd.NextDouble();
            vectorDeEstado.tiempoEntreLlegadasClientesPrestamos = calcularTiempo(vectorDeEstado.RNDLLegadaClientePrestamos, mediaPrestamo);
            vectorDeEstado.proximaLlegadaClientesPrestamos = vectorDeEstado.tiempoEntreLlegadasClientesPrestamos;
            vectorDeEstado.RNDLLegadaClienteDevolucion = rnd.NextDouble();
            vectorDeEstado.tiempoEntreLlegadasClientesDevolucion = calcularTiempo(vectorDeEstado.RNDLLegadaClienteDevolucion, mediaDevolucion);
            vectorDeEstado.proximaLlegadaClientesDevolucion = vectorDeEstado.tiempoEntreLlegadasClientesDevolucion;
            vectorDeEstado.RNDLLegadaClienteConsulta = rnd.NextDouble();
            vectorDeEstado.tiempoEntreLlegadasClientesConsulta = rnd.NextDouble();
            vectorDeEstado.proximaLlegadaClientesConsulta = calcularTiempo(vectorDeEstado.RNDLLegadaClienteConsulta, mediaConsulta);
            vectorDeEstado.RNDLLegadaClientePC = rnd.NextDouble();
            vectorDeEstado.tiempoEntreLlegadasClientesPC = calcularTiempo(vectorDeEstado.RNDLLegadaClientePC, mediaPC);
            vectorDeEstado.proximaLlegadaClientesPC = vectorDeEstado.tiempoEntreLlegadasClientesPC;
            vectorDeEstado.RNDLLegadaClienteInfoGral = rnd.NextDouble();
            vectorDeEstado.tiempoEntreLlegadasClientesInfoGral = calcularTiempo(vectorDeEstado.RNDLLegadaClienteInfoGral, mediaInfoGral);
            vectorDeEstado.proximaLlegadaClientesInfoGral = vectorDeEstado.tiempoEntreLlegadasClientesInfoGral;

            SortedSet<Evento> colaEventos = new SortedSet<Evento> { };

            Evento llegadaPrestamo = new Evento(TipoEvento.Llegada, TipoServicio.Prestamos, vectorDeEstado.proximaLlegadaClientesPrestamos, mediaPrestamo);
            Evento llegadaDevolucion = new Evento(TipoEvento.Llegada, TipoServicio.Devolucion, vectorDeEstado.proximaLlegadaClientesDevolucion, mediaDevolucion);
            Evento llegadaConsulta = new Evento(TipoEvento.Llegada, TipoServicio.Consulta, vectorDeEstado.proximaLlegadaClientesConsulta, mediaConsulta);
            Evento llegadaPC = new Evento(TipoEvento.Llegada, TipoServicio.PC, vectorDeEstado.proximaLlegadaClientesPC, mediaPC);
            Evento llegadaInfoGral = new Evento(TipoEvento.Llegada, TipoServicio.InfoGral, vectorDeEstado.proximaLlegadaClientesInfoGral, mediaInfoGral);


            colaEventos.Add(llegadaPrestamo);
            colaEventos.Add(llegadaDevolucion);
            colaEventos.Add(llegadaConsulta);
            colaEventos.Add(llegadaPC);
            colaEventos.Add(llegadaInfoGral);

            DataTable dt = crearDataTable();
            

            int i = 0;
            while (i < cantIteraciones)
            {
                if (i >= mostrarDesde && i <= mostrarDesde + 300)
                {

                    cargarDataGrid(i, dt);

                }

                Evento eventoActual = colaEventos.Min;
                colaEventos.Remove(eventoActual);

                vectorDeEstado.relojAnterior = vectorDeEstado.reloj;
                vectorDeEstado.reloj = eventoActual.tiempo;
                vectorDeEstado.evento = eventoActual.tipo + "_" + eventoActual.servicio;


                // procesamos una llegada
                if (eventoActual.tipo.Equals(TipoEvento.Llegada))
                {
                    double random = rnd.NextDouble();
                    double tiempoEntreLlegadas = calcularTiempo(random, eventoActual.media);
                    double proximaLlegada = vectorDeEstado.reloj + tiempoEntreLlegadas;

                    var proxLLegada = new Evento(TipoEvento.Llegada, eventoActual.servicio, proximaLlegada, eventoActual.media);
                    colaEventos.Add(proxLLegada);

                    // actualizamos vector
                    if (eventoActual.servicio.Equals(TipoServicio.Prestamos))
                    {
                        vectorDeEstado.RNDLLegadaClientePrestamos = random;
                        vectorDeEstado.tiempoEntreLlegadasClientesPrestamos = tiempoEntreLlegadas;
                        vectorDeEstado.proximaLlegadaClientesPrestamos = proximaLlegada;

                        // Resetear otros atributos a 0 que no pertenecen a Prestamos
                        vectorDeEstado.tiempoAtencionPrestamo = 0.0;
                        vectorDeEstado.RNDFinAtencionPrestamo = 0.0;
                        vectorDeEstado.RNDLLegadaClienteDevolucion = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesDevolucion = 0.0;
                        vectorDeEstado.RNDLLegadaClienteConsulta = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesConsulta = 0.0;
                        vectorDeEstado.RNDLLegadaClientePC = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesPC = 0.0;
                        vectorDeEstado.RNDLLegadaClienteInfoGral = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesInfoGral = 0.0;
                        
                        // generamos fin de atencion
                        

                        // buscamos servidor libre
                        int servidor = buscarServidorLibre(vectorDeEstado.servidoresPrestamo);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinPrestamo);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Prestamos, tiempoFinAtencion, mediaFinPrestamo);
                            colaEventos.Add(finAtencion);
                          
                            vectorDeEstado.finAtencionPrestamo[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionPrestamo = random;
                            vectorDeEstado.tiempoAtencionPrestamo = tiempoAtencion;

                            // generamos estudiante y lo añadimos al vector de estado
                            var estudiante = new Estudiante("Siendo Atendido (Prestamos)", vectorDeEstado.reloj, TipoServicio.Prestamos);
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = vectorDeEstado.reloj;
                            vectorDeEstado.estudiantes.Add(estudiante);


                            // variables estadisticas
                            vectorDeEstado.atendidosPrestamos += 1;


                        } else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante($"Esperando Atencion Prestamos ({servidor})", vectorDeEstado.reloj, TipoServicio.Prestamos);
                            vectorDeEstado.colaPrestamos.Enqueue(Estudiante);

                            vectorDeEstado.mayorNumeroDeGenteEnCola = chequearColaMasGrande(vectorDeEstado.colaPrestamos.Count, vectorDeEstado.mayorNumeroDeGenteEnCola);
                        }


                    }
                    else if (eventoActual.servicio.Equals(TipoServicio.Devolucion))
                    {
                        vectorDeEstado.RNDLLegadaClienteDevolucion = random;
                        vectorDeEstado.tiempoEntreLlegadasClientesDevolucion = tiempoEntreLlegadas;
                        vectorDeEstado.proximaLlegadaClientesDevolucion = proximaLlegada;

                        // Resetear otros atributos a 0 que no pertenecen a Devolucion
                        vectorDeEstado.RNDLLegadaClientePrestamos = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesPrestamos = 0.0;
                        vectorDeEstado.RNDLLegadaClienteConsulta = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesConsulta = 0.0;
                        vectorDeEstado.RNDLLegadaClientePC = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesPC = 0.0;
                        vectorDeEstado.RNDLLegadaClienteInfoGral = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesInfoGral = 0.0;


                        int servidor = buscarServidorLibre(vectorDeEstado.servidoresDevolucion);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinDevolucion);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Devolucion, tiempoFinAtencion, mediaFinDevolucion);
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionDevolucion[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionDevolucion = random;
                            vectorDeEstado.tiempoAtencionDevolucion = tiempoAtencion;

                            // generamos estudiante y lo añadimos al vector de estado
                            var estudiante = new Estudiante($"Siendo Atendido Devolucion ({servidor})", vectorDeEstado.reloj, TipoServicio.Devolucion);
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = vectorDeEstado.reloj;
                            vectorDeEstado.estudiantes.Add(estudiante);

                            // variables estadisticas
                            vectorDeEstado.atendidosDevoluciones += 1;

                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante($"Esperando Atencion Devolucion ({servidor})", vectorDeEstado.reloj, TipoServicio.Devolucion);
                            vectorDeEstado.colaDevolucion.Enqueue(Estudiante);
                            vectorDeEstado.mayorNumeroDeGenteEnCola = chequearColaMasGrande(vectorDeEstado.colaDevolucion.Count, vectorDeEstado.mayorNumeroDeGenteEnCola);
                        }

                    }
                    else if (eventoActual.servicio.Equals(TipoServicio.Consulta))
                    {
                        vectorDeEstado.RNDLLegadaClienteConsulta = random;
                        vectorDeEstado.tiempoEntreLlegadasClientesConsulta = tiempoEntreLlegadas;
                        vectorDeEstado.proximaLlegadaClientesConsulta = proximaLlegada;

                        // Resetear otros atributos a 0 que no pertenecen a Consulta
                        vectorDeEstado.RNDLLegadaClientePrestamos = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesPrestamos = 0.0;
                        vectorDeEstado.RNDLLegadaClienteDevolucion = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesDevolucion = 0.0;
                        vectorDeEstado.RNDLLegadaClientePC = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesPC = 0.0;
                        vectorDeEstado.RNDLLegadaClienteInfoGral = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesInfoGral = 0.0;

                        // generamos fin de atencion


                        // buscamos servidor libre
                        int servidor = buscarServidorLibre(vectorDeEstado.servidoresConsulta);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinConsulta);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Consulta, tiempoFinAtencion, mediaFinConsulta);
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionConsulta[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionConsulta = random;
                            vectorDeEstado.tiempoAtencionConsulta = tiempoAtencion;

                            // generamos estudiante y lo aÃ±adimos al vector de estado
                            var estudiante = new Estudiante($"Siendo Atendido Consulta ({servidor})", vectorDeEstado.reloj, TipoServicio.Consulta);
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = vectorDeEstado.reloj;
                            vectorDeEstado.estudiantes.Add(estudiante);

                            // variables estadisticas
                            vectorDeEstado.atendidosConsultas += 1;

                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante("Esperando Atencion", vectorDeEstado.reloj, TipoServicio.Consulta);
                            vectorDeEstado.colaConsultas.Enqueue(Estudiante);
                            vectorDeEstado.mayorNumeroDeGenteEnCola = chequearColaMasGrande(vectorDeEstado.colaConsultas.Count, vectorDeEstado.mayorNumeroDeGenteEnCola);
                        }


                    }
                    else if (eventoActual.servicio.Equals(TipoServicio.PC))
                    {
                        vectorDeEstado.RNDLLegadaClientePC = random;
                        vectorDeEstado.tiempoEntreLlegadasClientesPC = tiempoEntreLlegadas;
                        vectorDeEstado.proximaLlegadaClientesPC = proximaLlegada;

                        // Resetear otros atributos a 0 que no pertenecen a PC
                        vectorDeEstado.RNDLLegadaClientePrestamos = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesPrestamos = 0.0;
                        vectorDeEstado.RNDLLegadaClienteDevolucion = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesDevolucion = 0.0;
                        vectorDeEstado.RNDLLegadaClienteConsulta = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesConsulta = 0.0;
                        vectorDeEstado.RNDLLegadaClienteInfoGral = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesInfoGral = 0.0;

                        // generamos fin de atencion


                        // buscamos servidor libre
                        int servidor = buscarServidorLibre(vectorDeEstado.servidoresPC);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinPC);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.PC, tiempoFinAtencion, mediaFinPC);
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionPC[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionPC = random;
                            vectorDeEstado.tiempoAtencionPC = tiempoAtencion;

                            // generamos estudiante y lo aÃ±adimos al vector de estado
                            var estudiante = new Estudiante($"Siendo Atendido PCs ({servidor})", vectorDeEstado.reloj, TipoServicio.PC);
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = vectorDeEstado.reloj;
                            vectorDeEstado.estudiantes.Add(estudiante);

                            //variables estadisticas
                            vectorDeEstado.atendidosPC += 1;

                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante("Esperando Atencion", vectorDeEstado.reloj, TipoServicio.PC);
                            vectorDeEstado.colaPC.Enqueue(Estudiante);
                            vectorDeEstado.mayorNumeroDeGenteEnCola = chequearColaMasGrande(vectorDeEstado.colaPC.Count, vectorDeEstado.mayorNumeroDeGenteEnCola);
                        }

                    }
                    else if (eventoActual.servicio.Equals(TipoServicio.InfoGral))
                    {
                        vectorDeEstado.RNDLLegadaClienteInfoGral = random;
                        vectorDeEstado.tiempoEntreLlegadasClientesInfoGral = tiempoEntreLlegadas;
                        vectorDeEstado.proximaLlegadaClientesInfoGral = proximaLlegada;

                        // Resetear otros atributos a 0 que no pertenecen a InfoGral
                        vectorDeEstado.RNDLLegadaClientePrestamos = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesPrestamos = 0.0;
                        vectorDeEstado.RNDLLegadaClienteDevolucion = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesDevolucion = 0.0;
                        vectorDeEstado.RNDLLegadaClienteConsulta = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesConsulta = 0.0;
                        vectorDeEstado.RNDLLegadaClientePC = 0.0;
                        vectorDeEstado.tiempoEntreLlegadasClientesPC = 0.0;

                        // generamos fin de atencion


                        // buscamos servidor libre
                        int servidor = buscarServidorLibre(vectorDeEstado.servidoresInfoGeneral);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinInfoGral);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.InfoGral, tiempoFinAtencion, mediaFinInfoGral);
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionInfoGeneral[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionInfoGeneral = random;
                            vectorDeEstado.tiempoAtencionInfoGeneral = tiempoAtencion;

                            // generamos estudiante y lo aÃ±adimos al vector de estado
                            var estudiante = new Estudiante($"Siendo Atendido InfoGral ({servidor})", vectorDeEstado.reloj, TipoServicio.InfoGral);
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = vectorDeEstado.reloj;
                            vectorDeEstado.estudiantes.Add(estudiante);

                            // variables estadisticas
                            vectorDeEstado.atendidosInfoGral += 1;

                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante("Esperando Atencion", vectorDeEstado.reloj, TipoServicio.InfoGral);
                            vectorDeEstado.colaInfoGeneral.Enqueue(Estudiante);
                            vectorDeEstado.mayorNumeroDeGenteEnCola = chequearColaMasGrande(vectorDeEstado.colaInfoGeneral.Count, vectorDeEstado.mayorNumeroDeGenteEnCola);
                        }


                    }

                }




                //
                //
                // procesamos un fin atención
                else
                {
                    // Traemos el servidor que terminó
                    Estudiante estudiante = vectorDeEstado.estudiantes.Find(e => e.tiempoFinAtencion == vectorDeEstado.reloj);
                    int servidor = estudiante.servidor;
                    

                    if (eventoActual.servicio.Equals(TipoServicio.Prestamos)) 
                    {
                        if (vectorDeEstado.colaPrestamos.Count != 0) // Traemos un nuevo cliente al servidor
                        {
                            Estudiante estudianteNuevo = vectorDeEstado.colaPrestamos.Dequeue();

                            vectorDeEstado.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinPrestamo);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Prestamos, tiempoFinAtencion, mediaFinPrestamo); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionPrestamo[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionPrestamo = random;
                            vectorDeEstado.tiempoAtencionPrestamo = tiempoAtencion;

                            estudianteNuevo.estado = "Siendo Atendido";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.servidor = servidor;
                            estudianteNuevo.horaInicioAtencion = vectorDeEstado.reloj;

                            vectorDeEstado.RNDLLegadaClientePrestamos = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesPrestamos = 0.0;

                            // variables estadisticas
                            vectorDeEstado.acTiempoEsperaPrestamos += vectorDeEstado.reloj - estudianteNuevo.tiempoLlegada;
                            vectorDeEstado.atendidosPrestamos += 1;
                            //vectorDeEstado.acTiempoServicioPrestamos += vectorDeEstado.reloj - estudiante.horaInicioAtencion;

                       



                        } else //cuando no hay nadie en cola
                        {
                            vectorDeEstado.servidoresPrestamo[servidor] = true;
                            vectorDeEstado.tiempoAtencionPrestamo = 0.0;
                            vectorDeEstado.RNDFinAtencionPrestamo = 0.0;
                            vectorDeEstado.RNDLLegadaClientePrestamos = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesPrestamos = 0.0;
                            //vectorDeEstado.acTiempoServicioPrestamos += vectorDeEstado.reloj - estudiante.horaInicioAtencion;
                        }


                    }
                    if (eventoActual.servicio.Equals(TipoServicio.Devolucion))
                    {
                        if (vectorDeEstado.colaDevolucion.Count != 0)
                        {
                            Estudiante estudianteNuevo = vectorDeEstado.colaDevolucion.Dequeue();

                            vectorDeEstado.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinDevolucion);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Devolucion, tiempoFinAtencion, mediaFinDevolucion); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionDevolucion[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionDevolucion = random;
                            vectorDeEstado.tiempoAtencionDevolucion = tiempoAtencion;

                            estudianteNuevo.estado = "Siendo Atendido";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.horaInicioAtencion = vectorDeEstado.reloj;
                            estudianteNuevo.servidor = servidor;


                            vectorDeEstado.RNDLLegadaClienteDevolucion = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesDevolucion = 0.0;

                            // variables estadisticas
                            vectorDeEstado.acTiempoEsperaDevoluciones += vectorDeEstado.reloj - estudianteNuevo.tiempoLlegada;
                            vectorDeEstado.atendidosDevoluciones += 1;
                            //vectorDeEstado.acTiempoServicioDevoluciones += vectorDeEstado.reloj - estudiante.horaInicioAtencion;

                        }
                        else
                        {
                            vectorDeEstado.servidoresDevolucion[servidor] = true;
                            vectorDeEstado.tiempoAtencionDevolucion = 0.0;
                            vectorDeEstado.RNDFinAtencionDevolucion = 0.0;
                            vectorDeEstado.RNDLLegadaClienteDevolucion = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesDevolucion = 0.0;
                            //vectorDeEstado.acTiempoServicioDevoluciones += vectorDeEstado.reloj - estudiante.horaInicioAtencion;
                        }
                    }
                    if (eventoActual.servicio.Equals(TipoServicio.Consulta))
                    {
                        if (vectorDeEstado.colaConsultas.Count != 0)
                        {
                            Estudiante estudianteNuevo = vectorDeEstado.colaConsultas.Dequeue();

                            vectorDeEstado.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinConsulta);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Consulta, tiempoFinAtencion, mediaFinConsulta); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionConsulta[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionConsulta = random;
                            vectorDeEstado.tiempoAtencionConsulta = tiempoAtencion;

                            estudianteNuevo.estado = "Siendo Atendido";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.horaInicioAtencion = vectorDeEstado.reloj;
                            estudianteNuevo.servidor = servidor;

                            vectorDeEstado.RNDLLegadaClienteConsulta = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesConsulta = 0.0;

                            // variables estadisticas
                            vectorDeEstado.atendidosConsultas += 1;
                            //vectorDeEstado.acTiempoServicioConsultas += vectorDeEstado.reloj - estudiante.horaInicioAtencion;
                            vectorDeEstado.acTiempoEsperaConsultas += vectorDeEstado.reloj - estudianteNuevo.tiempoLlegada;

                        }
                        else
                        {
                            vectorDeEstado.servidoresConsulta[servidor] = true;
                            vectorDeEstado.tiempoAtencionConsulta = 0.0;
                            vectorDeEstado.RNDFinAtencionConsulta = 0.0;
                            vectorDeEstado.RNDLLegadaClienteConsulta = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesConsulta = 0.0;
                            //vectorDeEstado.acTiempoServicioConsultas += vectorDeEstado.reloj - estudiante.horaInicioAtencion;
                        }
                    }
                    if (eventoActual.servicio.Equals(TipoServicio.PC))
                    {
                        if (vectorDeEstado.colaPC.Count != 0)
                        {
                            Estudiante estudianteNuevo = vectorDeEstado.colaPC.Dequeue();

                            vectorDeEstado.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinPC);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.PC, tiempoFinAtencion, mediaFinPC); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionPC[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionPC = random;
                            vectorDeEstado.tiempoAtencionPC = tiempoAtencion;

                            estudianteNuevo.estado = "Siendo Atendido";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.horaInicioAtencion = vectorDeEstado.reloj;
                            estudianteNuevo.servidor = servidor;

                            vectorDeEstado.RNDLLegadaClientePC = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesPC = 0.0;

                            //variables estadisticas
                            vectorDeEstado.atendidosPC += 1;
                            vectorDeEstado.acTiempoEsperaPC += vectorDeEstado.reloj - estudianteNuevo.tiempoLlegada;
                            //vectorDeEstado.acTiempoServicioPC += vectorDeEstado.reloj - estudiante.horaInicioAtencion;

                        }
                        else
                        {
                            vectorDeEstado.servidoresPC[servidor] = true;
                            vectorDeEstado.tiempoAtencionPC = 0.0;
                            vectorDeEstado.RNDFinAtencionPC = 0.0;
                            vectorDeEstado.RNDLLegadaClientePC = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesPC = 0.0;
                            //vectorDeEstado.acTiempoServicioPC += vectorDeEstado.reloj - estudiante.horaInicioAtencion;
                        }
                    }         
                    if (eventoActual.servicio.Equals(TipoServicio.InfoGral))
                    {
                        if (vectorDeEstado.colaInfoGeneral.Count != 0)
                        {
                            Estudiante estudianteNuevo = vectorDeEstado.colaInfoGeneral.Dequeue();

                            vectorDeEstado.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinInfoGral);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.InfoGral, tiempoFinAtencion, mediaFinInfoGral); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionInfoGeneral[servidor] = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionInfoGeneral = random;
                            vectorDeEstado.tiempoAtencionInfoGeneral = tiempoAtencion;

                            estudianteNuevo.estado = "Siendo Atendido";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.horaInicioAtencion = vectorDeEstado.reloj;
                            estudianteNuevo.servidor = servidor;

                            vectorDeEstado.RNDLLegadaClienteInfoGral = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesInfoGral = 0.0;

                            // variables estadisticas
                            vectorDeEstado.atendidosInfoGral += 1;
                            vectorDeEstado.acTiempoEsperaInfoGral += vectorDeEstado.reloj - estudianteNuevo.tiempoLlegada;
                            //vectorDeEstado.acTiempoServicioInfoGral += vectorDeEstado.reloj - estudiante.horaInicioAtencion;
                        }
                        else
                        {
                            vectorDeEstado.servidoresInfoGeneral[servidor] = true;
                            vectorDeEstado.tiempoAtencionInfoGeneral = 0.0;
                            vectorDeEstado.RNDFinAtencionInfoGeneral = 0.0;
                            vectorDeEstado.RNDLLegadaClienteInfoGral = 0.0;
                            vectorDeEstado.tiempoEntreLlegadasClientesInfoGral = 0.0;
                            //vectorDeEstado.acTiempoServicioInfoGral += vectorDeEstado.reloj - estudiante.horaInicioAtencion;
                        }
                    }
                    if(eventoActual.servicio.Equals(TipoServicio.GestionarMembresia))
                    {
                        if (vectorDeEstado.colaGestionarMembresia.Count != 0) // Traemos un nuevo cliente al servidor
                        {
                            Estudiante estudianteNuevo = vectorDeEstado.colaGestionarMembresia.Dequeue();

                            vectorDeEstado.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinGestionarMembresia);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.GestionarMembresia, tiempoFinAtencion, mediaFinGestionarMembresia);
                            colaEventos.Add(finAtencion);

                            vectorDeEstado.finAtencionMembresia = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionMembresia = random;
                            vectorDeEstado.tiempoAtencionMembresia = tiempoAtencion;

                            estudianteNuevo.estado = "Siendo Atendido";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.servidor = servidor;
                            estudianteNuevo.horaInicioAtencion = vectorDeEstado.reloj;


                            // variables estadisticas
                            vectorDeEstado.acTiempoEsperaGestionarMembresia += vectorDeEstado.reloj - estudianteNuevo.tiempoLlegada;
                            vectorDeEstado.atendidosGestionarMembresia += 1;
                            //vectorDeEstado.acTiempoServicioGestionarMembresia += vectorDeEstado.reloj - estudiante.horaInicioAtencion;

                        }
                        else //cuando no hay nadie en cola
                        {
                            vectorDeEstado.servidoresMembresia = true;
                            vectorDeEstado.tiempoAtencionMembresia = 0.0;
                            vectorDeEstado.RNDFinAtencionMembresia = 0.0;
                            //vectorDeEstado.acTiempoServicioGestionarMembresia += vectorDeEstado.reloj - estudiante.horaInicioAtencion;
                        }

                    }

                    // vemos si se accede al servicio nuevo

                    double rndServicioNuevo = rnd.NextDouble();
                    if (rndServicioNuevo < probMemb)
                    {
                        if (vectorDeEstado.servidoresMembresia)
                        {
                            vectorDeEstado.servidoresMembresia = false;
                            
                            rndServicioNuevo = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(rndServicioNuevo, mediaFinPrestamo);
                            double tiempoFinAtencion = vectorDeEstado.reloj + tiempoAtencion;

                            Evento finAtencionNuevo = new Evento(TipoEvento.FinAtencion, TipoServicio.GestionarMembresia, tiempoFinAtencion, mediaFinGestionarMembresia);
                            colaEventos.Add(finAtencionNuevo);

                            vectorDeEstado.finAtencionMembresia = tiempoFinAtencion;
                            vectorDeEstado.RNDFinAtencionMembresia = rndServicioNuevo;
                            vectorDeEstado.tiempoAtencionMembresia = tiempoAtencion;

                            estudiante.estado = $"Siendo Atendido Gestion Membresia";
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = vectorDeEstado.reloj;

                            // variables estadisticas
                            vectorDeEstado.atendidosGestionarMembresia += 1;
                    
                        } else
                        {
                            vectorDeEstado.colaGestionarMembresia.Enqueue(estudiante);
                            estudiante.tiempoLlegada = vectorDeEstado.reloj;
                            vectorDeEstado.mayorNumeroDeGenteEnCola = chequearColaMasGrande(vectorDeEstado.colaGestionarMembresia.Count, vectorDeEstado.mayorNumeroDeGenteEnCola);
                            vectorDeEstado.estudiantes.Remove(estudiante);


                        }
                    }
                }
                i++;

                // chequeamos la disponibilidad de los servidores para calcular el tiempo de ocupacion
                for (int j = 0; j < vectorDeEstado.servidoresPrestamo.Length; j++)
                {
                    if (vectorDeEstado.servidoresPrestamo[j]) { break; }
                    else
                    {
                        vectorDeEstado.acTiempoServicioPrestamos += (vectorDeEstado.reloj - vectorDeEstado.relojAnterior);
                        break;
                    }
                }
                for (int j = 0; j < vectorDeEstado.servidoresDevolucion.Length; j++)
                {
                    if (vectorDeEstado.servidoresDevolucion[j]) { break; }
                    else
                    {
                        vectorDeEstado.acTiempoServicioDevoluciones += (vectorDeEstado.reloj - vectorDeEstado.relojAnterior);
                        break;
                    }
                }
                for (int j = 0; j < vectorDeEstado.servidoresConsulta.Length; j++)
                {
                    if (vectorDeEstado.servidoresConsulta[j]) { break; }
                    else
                    {
                        vectorDeEstado.acTiempoServicioConsultas += (vectorDeEstado.reloj - vectorDeEstado.relojAnterior);
                        break;
                    }
                }
                for (int j = 0; j < vectorDeEstado.servidoresPC.Length; j++)
                {
                    if (vectorDeEstado.servidoresPC[j]) { break; }
                    else
                    {
                        vectorDeEstado.acTiempoServicioPC += (vectorDeEstado.reloj - vectorDeEstado.relojAnterior);
                        break;
                    }
                }
                for (int j = 0; j < vectorDeEstado.servidoresInfoGeneral.Length; j++)
                {
                    if (vectorDeEstado.servidoresInfoGeneral[j]) { break; }
                    else
                    {
                        vectorDeEstado.acTiempoServicioInfoGral += (vectorDeEstado.reloj - vectorDeEstado.relojAnterior);
                        break;
                    }
                }

                if (vectorDeEstado.servidoresMembresia) { continue; }
                else
                {
                    vectorDeEstado.acTiempoServicioGestionarMembresia += (vectorDeEstado.reloj - vectorDeEstado.relojAnterior);
                }  
            }
            cargarDataGrid(cantIteraciones, dt);

            return dt;
        }

        public double calcularTiempo(double rnd, double media)
        {
            //habria que tabular?
            double tiempo = -media * Math.Log(1 - rnd);


            return tiempo;

        }

        public double calcularProximaLlegada(double tiempo, double reloj)
        {

            double proxLlegada = reloj + tiempo;


            return proxLlegada;

        }

        public int buscarServidorLibre(bool[] servidores)
        {
            int indiceServidor = -1;
            for (int i = 0; i < servidores.Length ; i++)
            {
                
                if (servidores[i])
                {
                    servidores[i] = false;
                    indiceServidor = i;
                    break;
                }
            }

            return indiceServidor;
        }

        public void cargarDataGrid(int i, DataTable dt)
        {

            dt.Rows.Add(i, vectorDeEstado.evento,
                TruncateDecimal(vectorDeEstado.reloj, 2),
                TruncateDecimal(vectorDeEstado.RNDLLegadaClientePrestamos, 2),
                TruncateDecimal(vectorDeEstado.tiempoEntreLlegadasClientesPrestamos, 2),
                TruncateDecimal(vectorDeEstado.proximaLlegadaClientesPrestamos, 2),

                TruncateDecimal(vectorDeEstado.RNDLLegadaClienteDevolucion, 2),
                TruncateDecimal(vectorDeEstado.tiempoEntreLlegadasClientesDevolucion, 2),
                TruncateDecimal(vectorDeEstado.proximaLlegadaClientesDevolucion, 2),

                TruncateDecimal(vectorDeEstado.RNDLLegadaClienteConsulta, 2),
                TruncateDecimal(vectorDeEstado.tiempoEntreLlegadasClientesConsulta, 2),
                TruncateDecimal(vectorDeEstado.proximaLlegadaClientesConsulta, 2),

                TruncateDecimal(vectorDeEstado.RNDLLegadaClientePC, 2),
                TruncateDecimal(vectorDeEstado.tiempoEntreLlegadasClientesPC, 2),
                TruncateDecimal(vectorDeEstado.proximaLlegadaClientesPC, 2),

                TruncateDecimal(vectorDeEstado.RNDLLegadaClienteInfoGral, 2),
                TruncateDecimal(vectorDeEstado.tiempoEntreLlegadasClientesInfoGral, 2),
                TruncateDecimal(vectorDeEstado.proximaLlegadaClientesInfoGral, 2),

                TruncateDecimal(vectorDeEstado.RNDFinAtencionPrestamo, 2),
                TruncateDecimal(vectorDeEstado.tiempoAtencionPrestamo, 2),
                TruncateDecimal(vectorDeEstado.finAtencionPrestamo[0], 2),
                TruncateDecimal(vectorDeEstado.finAtencionPrestamo[1], 2),
                TruncateDecimal(vectorDeEstado.finAtencionPrestamo[2], 2),
                vectorDeEstado.servidoresPrestamo[0] ? "Libre":"Ocupado",
                vectorDeEstado.servidoresPrestamo[1] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresPrestamo[2] ? "Libre" : "Ocupado",
                TruncateDecimal(vectorDeEstado.colaPrestamos.Count, 2),

                TruncateDecimal(vectorDeEstado.RNDFinAtencionDevolucion, 2),
                TruncateDecimal(vectorDeEstado.tiempoAtencionDevolucion, 2),
                TruncateDecimal(vectorDeEstado.finAtencionDevolucion[0], 2),
                TruncateDecimal(vectorDeEstado.finAtencionDevolucion[1], 2),
                vectorDeEstado.servidoresDevolucion[0] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresDevolucion[1] ? "Libre" : "Ocupado",
                TruncateDecimal(vectorDeEstado.colaDevolucion.Count, 2),

                TruncateDecimal(vectorDeEstado.RNDFinAtencionConsulta, 2),
                TruncateDecimal(vectorDeEstado.tiempoAtencionConsulta, 2),
                TruncateDecimal(vectorDeEstado.finAtencionConsulta[0], 2),
                TruncateDecimal(vectorDeEstado.finAtencionConsulta[1], 2),
                vectorDeEstado.servidoresConsulta[0] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresConsulta[1] ? "Libre" : "Ocupado",
                TruncateDecimal(vectorDeEstado.colaConsultas.Count, 2),

                TruncateDecimal(vectorDeEstado.RNDFinAtencionPC, 2),
                TruncateDecimal(vectorDeEstado.tiempoAtencionPC, 2),
                TruncateDecimal(vectorDeEstado.finAtencionPC[0], 2),
                TruncateDecimal(vectorDeEstado.finAtencionPC[1], 2),
                TruncateDecimal(vectorDeEstado.finAtencionPC[2], 2),
                TruncateDecimal(vectorDeEstado.finAtencionPC[3], 2),
                TruncateDecimal(vectorDeEstado.finAtencionPC[4], 2),
                TruncateDecimal(vectorDeEstado.finAtencionPC[5], 2),
                vectorDeEstado.servidoresPC[0] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresPC[1] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresPC[2] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresPC[3] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresPC[4] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresPC[5] ? "Libre" : "Ocupado",
                TruncateDecimal(vectorDeEstado.colaPC.Count, 2),

                TruncateDecimal(vectorDeEstado.RNDFinAtencionInfoGeneral, 2),
                TruncateDecimal(vectorDeEstado.tiempoAtencionInfoGeneral, 2),
                TruncateDecimal(vectorDeEstado.finAtencionInfoGeneral[0], 2),
                TruncateDecimal(vectorDeEstado.finAtencionInfoGeneral[1], 2),
                vectorDeEstado.servidoresInfoGeneral[0] ? "Libre" : "Ocupado",
                vectorDeEstado.servidoresInfoGeneral[1] ? "Libre" : "Ocupado",

                TruncateDecimal(vectorDeEstado.colaInfoGeneral.Count, 2),

                TruncateDecimal(vectorDeEstado.RNDFinAtencionMembresia, 2),
                TruncateDecimal(vectorDeEstado.tiempoAtencionMembresia, 2),
                TruncateDecimal(vectorDeEstado.finAtencionMembresia, 2),
                TruncateDecimal(vectorDeEstado.colaGestionarMembresia.Count, 2),

                TruncateDecimal(vectorDeEstado.acTiempoEsperaPrestamos, 2),
                TruncateDecimal(vectorDeEstado.atendidosPrestamos, 2),

                TruncateDecimal(vectorDeEstado.acTiempoEsperaDevoluciones, 2),
                TruncateDecimal(vectorDeEstado.atendidosDevoluciones, 2),

                TruncateDecimal(vectorDeEstado.acTiempoEsperaConsultas, 2),
                TruncateDecimal(vectorDeEstado.atendidosConsultas, 2),

                TruncateDecimal(vectorDeEstado.acTiempoEsperaPC, 2),
                TruncateDecimal(vectorDeEstado.atendidosPC, 2),

                TruncateDecimal(vectorDeEstado.acTiempoEsperaInfoGral, 2),
                TruncateDecimal(vectorDeEstado.atendidosInfoGral, 2),

                TruncateDecimal(vectorDeEstado.acTiempoEsperaGestionarMembresia, 2),
                TruncateDecimal(vectorDeEstado.atendidosGestionarMembresia, 2),

                TruncateDecimal(vectorDeEstado.acTiempoServicioPrestamos, 2),
                TruncateDecimal(vectorDeEstado.acTiempoServicioDevoluciones, 2),
                TruncateDecimal(vectorDeEstado.acTiempoServicioConsultas, 2),
                TruncateDecimal(vectorDeEstado.acTiempoServicioPC, 2),
                TruncateDecimal(vectorDeEstado.acTiempoServicioInfoGral, 2),
                TruncateDecimal(vectorDeEstado.acTiempoServicioGestionarMembresia, 2),

                TruncateDecimal(vectorDeEstado.mayorNumeroDeGenteEnCola, 2));
        }


        public static double TruncateDecimal(double value, byte decimalPlaces)
        {
            if (decimalPlaces <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces), "The number of decimal places must be greater than zero.");
            }

            double scaleFactor = (double)Math.Pow(10, decimalPlaces);
            return Math.Floor(value * scaleFactor) / scaleFactor;
        }


            public DataTable crearDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Iteración");
            dt.Columns.Add("Evento");
            dt.Columns.Add("Reloj");

            dt.Columns.Add("RND prestamos");
            dt.Columns.Add("tiempo entre llegadas prestamos");
            dt.Columns.Add("proxima llegada estudiante prestamos");

            dt.Columns.Add("RND devolucion");
            dt.Columns.Add("tiempo entre llegadas devolucion");
            dt.Columns.Add("proxima llegada estudiante devolucion");

            dt.Columns.Add("RND consulta");
            dt.Columns.Add("tiempo entre llegadas consulta");
            dt.Columns.Add("proxima llegada estudiante consulta");

            dt.Columns.Add("RND PC");
            dt.Columns.Add("tiempo entre llegadas PC");
            dt.Columns.Add("proxima llegada estudiante PC");

            dt.Columns.Add("RND Info");
            dt.Columns.Add("tiempo entre llegadas info");
            dt.Columns.Add("proxima llegada estudiante info");

            dt.Columns.Add("RND fin atencion prestamo");
            dt.Columns.Add("tiempo atencion prestamo");
            dt.Columns.Add("fin atencion prestamo S1");
            dt.Columns.Add("fin atencion prestamo S2");
            dt.Columns.Add("fin atencion prestamo S3");
            dt.Columns.Add("servidores prestamo 1");
            dt.Columns.Add("servidores prestamo 2");
            dt.Columns.Add("servidores prestamo 3");
            dt.Columns.Add("cola prestamos");

            dt.Columns.Add("RND fin atencion devolución");
            dt.Columns.Add("tiempo atencion devolución");
            dt.Columns.Add("fin atencion devolución S1");
            dt.Columns.Add("fin atencion devolución S2");
            dt.Columns.Add("servidores devolución 1");
            dt.Columns.Add("servidores devolución 2");
            dt.Columns.Add("cola devoluciones");

            dt.Columns.Add("RND fin atencion consulta");
            dt.Columns.Add("tiempo atencion consulta");
            dt.Columns.Add("fin atencion consulta S1");
            dt.Columns.Add("fin atencion consulta S2");
            dt.Columns.Add("servidores consulta 1");
            dt.Columns.Add("servidores consulta 2");
            dt.Columns.Add("cola consultas");

            dt.Columns.Add("RND fin atencion PC");
            dt.Columns.Add("tiempo atencion PC");
            dt.Columns.Add("fin atencion PC 1");
            dt.Columns.Add("fin atencion PC 2");
            dt.Columns.Add("fin atencion PC 3");
            dt.Columns.Add("fin atencion PC 4");
            dt.Columns.Add("fin atencion PC 5");
            dt.Columns.Add("fin atencion PC 6");
            dt.Columns.Add("PC 1");
            dt.Columns.Add("PC 2");
            dt.Columns.Add("PC 3");
            dt.Columns.Add("PC 4");
            dt.Columns.Add("PC 5");
            dt.Columns.Add("PC 6");
            dt.Columns.Add("cola PCs");

            dt.Columns.Add("RND fin atencion Info Gral");
            dt.Columns.Add("tiempo atencion Info Gral");
            dt.Columns.Add("fin atencion Info Gral 1");
            dt.Columns.Add("fin atencion Info Gral 2");
            dt.Columns.Add("Servidor Info Gral 1");
            dt.Columns.Add("Servidor Info Gral 2");

            dt.Columns.Add("cola Info Gral");

            dt.Columns.Add("RND fin atencion gestionar membresia");
            dt.Columns.Add("tiempo atencion gestionar membresia");
            dt.Columns.Add("proximo fin atencion gestionar membresia");
            dt.Columns.Add("cola gestionar membresias");

            dt.Columns.Add("AC Tiempo espera prestamos");
            dt.Columns.Add("Atendidos prestamos");

            dt.Columns.Add("AC Tiempo espera devolucion");
            dt.Columns.Add("Atendidos devolucion");

            dt.Columns.Add("AC Tiempo espera info consulta");
            dt.Columns.Add("Atendidos info consulta");

            dt.Columns.Add("AC Tiempo espera acceso a pc");
            dt.Columns.Add("Atendidos pc");

            dt.Columns.Add("AC Tiempo espera info general");
            dt.Columns.Add("Atendidos info general");

            dt.Columns.Add("AC Tiempo espera gestionar membresia");
            dt.Columns.Add("Atendidos gestionar membresía");

            dt.Columns.Add("tiempo de ocupacion de prestamos");
            dt.Columns.Add("tiempo de ocupacion de devoluciones");
            dt.Columns.Add("tiempo de ocupacion de consultas");
            dt.Columns.Add("tiempo de ocupacion de PC");
            dt.Columns.Add("tiempo de ocupacion de Info General");
            dt.Columns.Add("tiempo de ocupacion gestionar membresias");

            dt.Columns.Add("Mayor numero de gente en cola");

            return dt;
        }

        public int chequearColaMasGrande(int tamañoCola, int colaMasGrande)
        {
            if (tamañoCola > colaMasGrande) { return tamañoCola; }
            else return colaMasGrande;
        }

        public string[] calcularVariablesEstadisticas()
        {
            var ve = vectorDeEstado;
            var resultados = new string[16];

            // 1 promedios esperas
            resultados[0] = TruncateDecimal(ve.acTiempoEsperaPrestamos / ve.atendidosPrestamos,2).ToString();
            resultados[1] = TruncateDecimal((ve.acTiempoEsperaDevoluciones / ve.atendidosDevoluciones),2).ToString();
            resultados[2] = TruncateDecimal((ve.acTiempoEsperaConsultas / ve.atendidosConsultas), 2).ToString();
            resultados[3] = TruncateDecimal((ve.acTiempoEsperaPC / ve.atendidosPC), 2).ToString();
            resultados[4] = TruncateDecimal((ve.acTiempoEsperaInfoGral / ve.atendidosInfoGral), 2).ToString();
            resultados[5] = TruncateDecimal((ve.acTiempoEsperaGestionarMembresia / ve.atendidosGestionarMembresia), 2).ToString();

            // 1 porcentajes ocupacion servicios
            resultados[6] = TruncateDecimal((ve.acTiempoServicioPrestamos / ve.reloj * 100), 2).ToString();
            resultados[7] = TruncateDecimal((ve.acTiempoServicioDevoluciones / ve.reloj * 100), 2).ToString();
            resultados[8] = TruncateDecimal((ve.acTiempoServicioConsultas / ve.reloj * 100), 2).ToString();
            resultados[9] = TruncateDecimal((ve.acTiempoServicioPC / ve.reloj * 100), 2).ToString();
            resultados[10] =TruncateDecimal((ve.acTiempoServicioInfoGral / ve.reloj * 100), 2).ToString();
            resultados[11] =TruncateDecimal((ve.acTiempoServicioGestionarMembresia / ve.reloj * 100), 2).ToString();

            // 2 menor tiempo servicio
            string menorTiempoServicio = "A";

            // Comparar y encontrar el menor valor
            double menorTiempo = Math.Min(
                Math.Min(ve.acTiempoServicioPrestamos, ve.acTiempoServicioDevoluciones),
                Math.Min(ve.acTiempoServicioPC, ve.acTiempoServicioInfoGral));
                menorTiempo = Math.Min(ve.acTiempoServicioGestionarMembresia, menorTiempo);

            // Determinar cuál servicio corresponde al menor tiempo
            if (menorTiempo == ve.acTiempoServicioPrestamos)
            {
                menorTiempoServicio = ("Prestamos");
            }
            else if (menorTiempo == ve.acTiempoServicioDevoluciones)
            {
                menorTiempoServicio = ("Devoluciones");
            }
            else if (menorTiempo == ve.acTiempoServicioPC)
            {
                menorTiempoServicio = ("Acceso a PC");
            }
            else if (menorTiempo == ve.acTiempoServicioInfoGral)
            {
                menorTiempoServicio = ("Información General");
            }
            else if (menorTiempo == ve.acTiempoServicioGestionarMembresia)
            {
                menorTiempoServicio = ("Gestionar Membresía");
            }
            resultados[12] = menorTiempoServicio;

            // 4 mayor cola
            resultados[13] = TruncateDecimal(ve.mayorNumeroDeGenteEnCola, 2).ToString();

            // 5 cantidad estudiantes atendidos
            var totalAtendidos = (ve.atendidosPrestamos + ve.atendidosConsultas + ve.atendidosDevoluciones + ve.atendidosPC + ve.atendidosInfoGral + ve.atendidosGestionarMembresia);
            resultados[14] = TruncateDecimal(totalAtendidos , 2).ToString();

            // 6 cantidad estudiantes atendidos por hora
            resultados[15] = TruncateDecimal((totalAtendidos / 60), 2).ToString();

            return resultados;

        }

    }




}
