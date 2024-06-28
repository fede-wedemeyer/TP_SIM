using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TP4
{
    public class Simulacion
    {
        public VectorDeEstado v { get; set; }

        public Simulacion()
        {
            v = new VectorDeEstado();
        }

        public (DataTable, DataTable) simular(int cantIteraciones, double mediaPrestamo, double mediaDevolucion, double mediaConsulta,
            double mediaPC, double mediaInfoGral, int mostrarDesde, double mediaFinPrestamo, double mediaFinDevolucion, 
            double mediaFinConsulta, double mediaFinPC, double mediaFinInfoGral, double mediaFinGestionarMembresia, double probMemb, double t, bool tipoSim)
        {
            if(tipoSim) 
            { 
                this.v = new VectorDeEstado();
                v.servidoresDevolucion = [true];
            }

            DataTable dt = new DataTable();
            DataTable dtRk = new DataTable();
            v.N = 0;
            v.evento = "Inicializacion";
            v.reloj = 0.00;
            int idClientes = 0;

            Random rnd = new Random();
            v.RNDLLegadaClientePrestamos = rnd.NextDouble();
            v.tiempoEntreLlegadasClientesPrestamos = calcularTiempo(v.RNDLLegadaClientePrestamos, mediaPrestamo);
            v.proximaLlegadaClientesPrestamos = v.tiempoEntreLlegadasClientesPrestamos;
            v.RNDLLegadaClienteDevolucion = rnd.NextDouble();
            v.tiempoEntreLlegadasClientesDevolucion = calcularTiempo(v.RNDLLegadaClienteDevolucion, mediaDevolucion);
            v.proximaLlegadaClientesDevolucion = v.tiempoEntreLlegadasClientesDevolucion;
            v.RNDLLegadaClienteConsulta = rnd.NextDouble();
            v.tiempoEntreLlegadasClientesConsulta = rnd.NextDouble();
            v.proximaLlegadaClientesConsulta = calcularTiempo(v.RNDLLegadaClienteConsulta, mediaConsulta);
            v.RNDLLegadaClientePC = rnd.NextDouble();
            v.tiempoEntreLlegadasClientesPC = calcularTiempo(v.RNDLLegadaClientePC, mediaPC);
            v.proximaLlegadaClientesPC = v.tiempoEntreLlegadasClientesPC;
            v.RNDLLegadaClienteInfoGral = rnd.NextDouble();
            v.tiempoEntreLlegadasClientesInfoGral = calcularTiempo(v.RNDLLegadaClienteInfoGral, mediaInfoGral);
            v.proximaLlegadaClientesInfoGral = v.tiempoEntreLlegadasClientesInfoGral;
            v.RNDProximaInterrupcion = rnd.NextDouble();
            v.proximaInterrupcion = calcularProximaInterrupcion(v.RNDProximaInterrupcion, t);

            SortedSet<Evento> colaEventos = new SortedSet<Evento> { };

            Evento llegadaPrestamo = new Evento(TipoEvento.Llegada, TipoServicio.Prestamos, v.proximaLlegadaClientesPrestamos, mediaPrestamo);
            Evento llegadaDevolucion = new Evento(TipoEvento.Llegada, TipoServicio.Devolucion, v.proximaLlegadaClientesDevolucion, mediaDevolucion);
            Evento llegadaConsulta = new Evento(TipoEvento.Llegada, TipoServicio.Consulta, v.proximaLlegadaClientesConsulta, mediaConsulta);
            Evento llegadaPC = new Evento(TipoEvento.Llegada, TipoServicio.PC, v.proximaLlegadaClientesPC, mediaPC);
            Evento llegadaInfoGral = new Evento(TipoEvento.Llegada, TipoServicio.InfoGral, v.proximaLlegadaClientesInfoGral, mediaInfoGral);
            Evento interrupcion = new Evento(TipoEvento.Interrupcion, TipoServicio.GestionarMembresia, v.proximaInterrupcion, t);

            colaEventos.Add(llegadaPrestamo);
            colaEventos.Add(llegadaDevolucion);
            colaEventos.Add(llegadaConsulta);
            colaEventos.Add(llegadaPC);
            colaEventos.Add(llegadaInfoGral);
            colaEventos.Add(interrupcion);

            if (tipoSim) { dt = crearDataTable2(); agregarFilaInicializacion2(0, dt); }
            else { dt = crearDataTable(); agregarFilaInicializacion(0, dt); }

            //Agregamos la fila de inicialización a la datatable


            int i = 1;
            bool primeraEntrada = true;
            while (i < cantIteraciones)
            {
                limpiarAtributos();
                
                Evento eventoActual = colaEventos.Min;
                colaEventos.Remove(eventoActual);

                v.relojAnterior = v.reloj;
                v.reloj = eventoActual.tiempo;
                v.evento = eventoActual.tipo + "_" + eventoActual.servicio;


                // procesamos una llegada
                if (eventoActual.tipo.Equals(TipoEvento.Llegada))
                {
                    double random = rnd.NextDouble();
                    double tiempoEntreLlegadas = calcularTiempo(random, eventoActual.media);
                    double proximaLlegada = v.reloj + tiempoEntreLlegadas;

                    var proxLLegada = new Evento(TipoEvento.Llegada, eventoActual.servicio, proximaLlegada, eventoActual.media);
                    colaEventos.Add(proxLLegada);

                    // actualizamos vector
                    if (eventoActual.servicio.Equals(TipoServicio.Prestamos))
                    {
                        v.RNDLLegadaClientePrestamos = random;
                        v.tiempoEntreLlegadasClientesPrestamos = tiempoEntreLlegadas;
                        v.proximaLlegadaClientesPrestamos = proximaLlegada;

                        // generamos fin de atencion
                        // buscamos servidor libre
                        int servidor = buscarServidorLibre(v.servidoresPrestamo);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinPrestamo);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Prestamos, tiempoFinAtencion, mediaPrestamo);
                            colaEventos.Add(finAtencion);

                            v.finAtencionPrestamo[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionPrestamo = random;
                            v.tiempoAtencionPrestamo = tiempoAtencion;

                            // generamos estudiante y lo añadimos al vector de estado
                            var estudiante = new Estudiante(idClientes, $"Atendido Prestamos ({servidor})" , v.reloj, TipoServicio.Prestamos);
                            idClientes++;
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = v.reloj;
                            v.estudiantes.Add(estudiante);


                            // variables estadisticas
                            v.atendidosPrestamos += 1;


                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante(idClientes, $"Esperando Atencion Prestamos)", v.reloj, TipoServicio.Prestamos);
                            idClientes++;
                            v.colaPrestamos.Enqueue(Estudiante);

                            v.mayorNumeroDeGenteEnCola = chequearColaMasGrande(v.colaPrestamos.Count, v.mayorNumeroDeGenteEnCola);
                        }


                    }
                    else if (eventoActual.servicio.Equals(TipoServicio.Devolucion))
                    {
                        v.RNDLLegadaClienteDevolucion = random;
                        v.tiempoEntreLlegadasClientesDevolucion = tiempoEntreLlegadas;
                        v.proximaLlegadaClientesDevolucion = proximaLlegada;

                        // generamos fin de atencion
                        // buscamos servidor libre
                        int servidor = buscarServidorLibre(v.servidoresDevolucion);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinDevolucion);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Devolucion, tiempoFinAtencion, mediaDevolucion);
                            colaEventos.Add(finAtencion);

                            v.finAtencionDevolucion[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionDevolucion = random;
                            v.tiempoAtencionDevolucion = tiempoAtencion;

                            // generamos estudiante y lo añadimos al vector de estado
                            var estudiante = new Estudiante(idClientes, $"Atendido Devolucion ({servidor})", v.reloj, TipoServicio.Devolucion);
                            idClientes++;
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = v.reloj;
                            v.estudiantes.Add(estudiante);

                            // variables estadisticas
                            v.atendidosDevoluciones += 1;

                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante(idClientes, $"Esperando Atencion Devolucion", v.reloj, TipoServicio.Devolucion);
                            idClientes++;
                            v.colaDevolucion.Enqueue(Estudiante);
                            v.mayorNumeroDeGenteEnCola = chequearColaMasGrande(v.colaDevolucion.Count, v.mayorNumeroDeGenteEnCola);
                        }

                    }
                    else if (eventoActual.servicio.Equals(TipoServicio.Consulta))
                    {
                        v.RNDLLegadaClienteConsulta = random;
                        v.tiempoEntreLlegadasClientesConsulta = tiempoEntreLlegadas;
                        v.proximaLlegadaClientesConsulta = proximaLlegada;

                       // generamos fin de atencion
                       // buscamos servidor libre
                        int servidor = buscarServidorLibre(v.servidoresConsulta);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinConsulta);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Consulta, tiempoFinAtencion, mediaConsulta);
                            colaEventos.Add(finAtencion);

                            v.finAtencionConsulta[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionConsulta = random;
                            v.tiempoAtencionConsulta = tiempoAtencion;

                            // generamos estudiante y lo aÃ±adimos al vector de estado
                            var estudiante = new Estudiante(idClientes, $"Atendido Consulta ({servidor})", v.reloj, TipoServicio.Consulta);
                            idClientes++;
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = v.reloj;
                            v.estudiantes.Add(estudiante);

                            // variables estadisticas
                            v.atendidosConsultas += 1;

                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante(idClientes, "Esperando Atencion Consulta", v.reloj, TipoServicio.Consulta);
                            idClientes++;
                            v.colaConsultas.Enqueue(Estudiante);
                            v.mayorNumeroDeGenteEnCola = chequearColaMasGrande(v.colaConsultas.Count, v.mayorNumeroDeGenteEnCola);
                        }


                    }
                    else if (eventoActual.servicio.Equals(TipoServicio.PC))
                    {
                        v.RNDLLegadaClientePC = random;
                        v.tiempoEntreLlegadasClientesPC = tiempoEntreLlegadas;
                        v.proximaLlegadaClientesPC = proximaLlegada;

                        // generamos fin de atencion
                        // buscamos servidor libre
                        int servidor = buscarServidorLibre(v.servidoresPC);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinPC);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.PC, tiempoFinAtencion, mediaPC);
                            colaEventos.Add(finAtencion);

                            v.finAtencionPC[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionPC = random;
                            v.tiempoAtencionPC = tiempoAtencion;

                            // generamos estudiante y lo aÃ±adimos al vector de estado
                            var estudiante = new Estudiante(idClientes, $"Atendido PCs ({servidor})", v.reloj, TipoServicio.PC);;
                            idClientes++;
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = v.reloj;
                            v.estudiantes.Add(estudiante);

                            //variables estadisticas
                            v.atendidosPC += 1;

                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante(idClientes, "Esperando Atencion PC", v.reloj, TipoServicio.PC);
                            idClientes++;
                            v.colaPC.Enqueue(Estudiante);
                            v.mayorNumeroDeGenteEnCola = chequearColaMasGrande(v.colaPC.Count, v.mayorNumeroDeGenteEnCola);
                        }

                    }
                    else if (eventoActual.servicio.Equals(TipoServicio.InfoGral))
                    {
                        v.RNDLLegadaClienteInfoGral = random;
                        v.tiempoEntreLlegadasClientesInfoGral = tiempoEntreLlegadas;
                        v.proximaLlegadaClientesInfoGral = proximaLlegada;

                        // generamos fin de atencion
                        // buscamos servidor libre
                        int servidor = buscarServidorLibre(v.servidoresInfoGeneral);
                        if (servidor != -1)
                        {

                            // si hay servidor libre, generamos el evento
                            random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinInfoGral);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.InfoGral, tiempoFinAtencion, mediaInfoGral);
                            colaEventos.Add(finAtencion);

                            v.finAtencionInfoGeneral[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionInfoGeneral = random;
                            v.tiempoAtencionInfoGeneral = tiempoAtencion;

                            // generamos estudiante y lo aÃ±adimos al vector de estado
                            var estudiante = new Estudiante(idClientes, $"Atendido InfoGral ({servidor})", v.reloj, TipoServicio.InfoGral);
                            idClientes++;
                            estudiante.servidor = servidor;
                            estudiante.tiempoFinAtencion = tiempoFinAtencion;
                            estudiante.horaInicioAtencion = v.reloj;
                            v.estudiantes.Add(estudiante);

                            // variables estadisticas
                            v.atendidosInfoGral += 1;

                        }
                        else
                        {
                            // si no hay servidor libre, generamos el estudiante y lo ponemos en la cola del servicio
                            var Estudiante = new Estudiante(idClientes, "Esperando Atencion Info", v.reloj, TipoServicio.InfoGral);
                            idClientes++;
                            v.colaInfoGeneral.Enqueue(Estudiante);
                            v.mayorNumeroDeGenteEnCola = chequearColaMasGrande(v.colaInfoGeneral.Count, v.mayorNumeroDeGenteEnCola);
                        }


                    }

                }

                // procesamos un fin atención
                else if (eventoActual.tipo.Equals(TipoEvento.FinAtencion))
                {
                    // Traemos el servidor que terminó
                    Estudiante estudiante = v.estudiantes.Find(e => e.tiempoFinAtencion == v.reloj);
                    int servidor = estudiante.servidor;


                    if (eventoActual.servicio.Equals(TipoServicio.Prestamos))
                    {
                        if (v.colaPrestamos.Count != 0) // Traemos un nuevo cliente al servidor
                        {
                            Estudiante estudianteNuevo = v.colaPrestamos.Dequeue();

                            v.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinPrestamo);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Prestamos, tiempoFinAtencion, mediaFinPrestamo); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            v.finAtencionPrestamo[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionPrestamo = random;
                            v.tiempoAtencionPrestamo = tiempoAtencion;

                            estudianteNuevo.estado = $"Atendido Prestamos {servidor}";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.servidor = servidor;
                            estudianteNuevo.horaInicioAtencion = v.reloj;

                            // variables estadisticas
                            v.acTiempoEsperaPrestamos += v.reloj - estudianteNuevo.tiempoLlegada;
                            v.atendidosPrestamos += 1;

                        }
                        else //cuando no hay nadie en cola
                        {
                            v.servidoresPrestamo[servidor] = true;
                            v.finAtencionPrestamo[servidor] = 0.0;
                        }
                    }
                    if (eventoActual.servicio.Equals(TipoServicio.Devolucion))
                    {
                        if (v.colaDevolucion.Count != 0)
                        {
                            Estudiante estudianteNuevo = v.colaDevolucion.Dequeue();

                            v.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinDevolucion);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Devolucion, tiempoFinAtencion, mediaFinDevolucion); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            v.finAtencionDevolucion[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionDevolucion = random;
                            v.tiempoAtencionDevolucion = tiempoAtencion;

                            estudianteNuevo.estado = $"Atendido Devolucion {servidor}";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.horaInicioAtencion = v.reloj;
                            estudianteNuevo.servidor = servidor;


                            v.RNDLLegadaClienteDevolucion = 0.0;
                            v.tiempoEntreLlegadasClientesDevolucion = 0.0;

                            // variables estadisticas
                            v.acTiempoEsperaDevoluciones += v.reloj - estudianteNuevo.tiempoLlegada;
                            v.atendidosDevoluciones += 1;

                        }
                        else
                        {
                            v.servidoresDevolucion[servidor] = true;
                            v.finAtencionDevolucion[servidor] = 0.0;
                        }
                    }
                    if (eventoActual.servicio.Equals(TipoServicio.Consulta))
                    {
                        if (v.colaConsultas.Count != 0)
                        {
                            Estudiante estudianteNuevo = v.colaConsultas.Dequeue();

                            v.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinConsulta);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.Consulta, tiempoFinAtencion, mediaFinConsulta); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            v.finAtencionConsulta[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionConsulta = random;
                            v.tiempoAtencionConsulta = tiempoAtencion;

                            estudianteNuevo.estado = $"Atendido Consulta {servidor}";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.horaInicioAtencion = v.reloj;
                            estudianteNuevo.servidor = servidor;

                            v.RNDLLegadaClienteConsulta = 0.0;
                            v.tiempoEntreLlegadasClientesConsulta = 0.0;

                            // variables estadisticas
                            v.atendidosConsultas += 1;
                            v.acTiempoEsperaConsultas += v.reloj - estudianteNuevo.tiempoLlegada;

                        }
                        else
                        {
                            v.servidoresConsulta[servidor] = true;
                            v.finAtencionConsulta[servidor] = 0.0;
                        }
                    }
                    if (eventoActual.servicio.Equals(TipoServicio.PC))
                    {
                        if (v.colaPC.Count != 0)
                        {
                            Estudiante estudianteNuevo = v.colaPC.Dequeue();

                            v.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinPC);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.PC, tiempoFinAtencion, mediaFinPC); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            v.finAtencionPC[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionPC = random;
                            v.tiempoAtencionPC = tiempoAtencion;

                            estudianteNuevo.estado = $"Atendido PC {servidor}";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.horaInicioAtencion = v.reloj;
                            estudianteNuevo.servidor = servidor;

                            v.RNDLLegadaClientePC = 0.0;
                            v.tiempoEntreLlegadasClientesPC = 0.0;

                            //variables estadisticas
                            v.atendidosPC += 1;
                            v.acTiempoEsperaPC += v.reloj - estudianteNuevo.tiempoLlegada;

                        }
                        else
                        {
                            v.servidoresPC[servidor] = true;
                            v.finAtencionPC[servidor] = 0.0;
                        }
                    }
                    if (eventoActual.servicio.Equals(TipoServicio.InfoGral))
                    {
                        if (v.colaInfoGeneral.Count != 0)
                        {
                            Estudiante estudianteNuevo = v.colaInfoGeneral.Dequeue();

                            v.estudiantes.Add(estudianteNuevo);

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinInfoGral);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.InfoGral, tiempoFinAtencion, mediaFinInfoGral); // recordar pasar la media correcta
                            colaEventos.Add(finAtencion);

                            v.finAtencionInfoGeneral[servidor] = tiempoFinAtencion;
                            v.RNDFinAtencionInfoGeneral = random;
                            v.tiempoAtencionInfoGeneral = tiempoAtencion;

                            estudianteNuevo.estado = $"Atendido Info General ({servidor})";
                            estudianteNuevo.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteNuevo.horaInicioAtencion = v.reloj;
                            estudianteNuevo.servidor = servidor;

                            v.RNDLLegadaClienteInfoGral = 0.0;
                            v.tiempoEntreLlegadasClientesInfoGral = 0.0;

                            // variables estadisticas
                            v.atendidosInfoGral += 1;
                            v.acTiempoEsperaInfoGral += v.reloj - estudianteNuevo.tiempoLlegada;
                        }
                        else
                        {
                            v.servidoresInfoGeneral[servidor] = true;
                            v.finAtencionInfoGeneral[servidor] = 0.0;
                        }
                    }
                    if (eventoActual.servicio.Equals(TipoServicio.GestionarMembresia))
                    {
                        if (v.colaGestionarMembresia.Count != 0) // Traemos un nuevo cliente al servidor si habia cola
                        {
                            Estudiante estudianteMembresia = v.colaGestionarMembresia.Dequeue();

                            double random = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(random, mediaFinGestionarMembresia);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.GestionarMembresia, tiempoFinAtencion, mediaFinGestionarMembresia);
                            colaEventos.Add(finAtencion);

                            v.finAtencionMembresia = tiempoFinAtencion;
                            v.RNDFinAtencionMembresia = random;
                            v.tiempoAtencionMembresia = tiempoAtencion;

                            estudianteMembresia.estado = ("Gestionando membresía");
                            estudianteMembresia.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteMembresia.servidor = 0;
                            estudianteMembresia.horaInicioAtencion = v.reloj;
                            v.estudiantes.Add(estudianteMembresia);


                            // variables estadisticas
                            v.acTiempoEsperaGestionarMembresia += v.reloj - estudianteMembresia.tiempoLlegada;
                            v.atendidosGestionarMembresia += 1;

                        }
                        else if (v.servidoresMembresia.estado != Estado.Interrumpido) //cuando no hay nadie en cola
                        {
                            v.servidoresMembresia.estado = Estado.Libre;
                            v.finAtencionMembresia = 0.0;

                        }


                    }

                    // Vemos si se ejecuta el nuevo servicio

                    double rndServicioNuevo = rnd.NextDouble();
                    v.RNDGestionarMembresia = rndServicioNuevo;
                    if (rndServicioNuevo < probMemb)
                    {
                        v.booleanoGestiona = true;
                        if (v.servidoresMembresia.estado == Estado.Libre)
                        {
                            

                            Estudiante estudianteMembresia = estudiante;
                            v.servidoresMembresia.estado = Estado.Ocupado;

                            rndServicioNuevo = rnd.NextDouble();
                            double tiempoAtencion = calcularTiempo(rndServicioNuevo, mediaFinPrestamo);
                            double tiempoFinAtencion = v.reloj + tiempoAtencion;

                            Evento finAtencionNuevo = new Evento(TipoEvento.FinAtencion, TipoServicio.GestionarMembresia, tiempoFinAtencion, mediaFinGestionarMembresia);
                            colaEventos.Add(finAtencionNuevo);

                            v.finAtencionMembresia = tiempoFinAtencion;
                            v.RNDFinAtencionMembresia = rndServicioNuevo;
                            v.tiempoAtencionMembresia = tiempoAtencion;

                            estudianteMembresia.estado = ("Gestionando membresía");
                            estudianteMembresia.tiempoFinAtencion = tiempoFinAtencion;
                            estudianteMembresia.servidor = 0;
                            estudianteMembresia.horaInicioAtencion = v.reloj;

                            v.estudiantes.Add(estudianteMembresia);


                            // variables estadisticas
                            v.atendidosGestionarMembresia += 1;

                        }
                        else
                        {
                            Estudiante estudianteMembresia = estudiante;
                            estudianteMembresia.estado = "Esperando gestion membresía";
                            v.colaGestionarMembresia.Enqueue(estudianteMembresia);

                            // Variables estadisticas
                            v.mayorNumeroDeGenteEnCola = chequearColaMasGrande(v.colaGestionarMembresia.Count, v.mayorNumeroDeGenteEnCola);
                            estudianteMembresia.tiempoLlegada = v.reloj;

                            v.RNDGestionarMembresia = 0.0;
                            v.tiempoAtencionMembresia = 0.0;

                        }
                    }

                    else 
                    { 
                        v.booleanoGestiona = false; 
                    } // Seteamos el booleano en falso

                    v.estudiantes.Remove(estudiante); // Eliminamos al cliente viejo
                }

                // Procesamos la interrupcion
                else if (eventoActual.tipo == TipoEvento.Interrupcion)
                {
                    
                    Evento eventoAInterrumpir = colaEventos.FirstOrDefault(e => e.tipo == TipoEvento.FinAtencion && e.servicio == TipoServicio.GestionarMembresia);
                    v.servidoresMembresia.estado = Estado.Interrumpido;

                    if (eventoAInterrumpir != null) 
                    {
                        colaEventos.Remove(eventoAInterrumpir);
                        v.tiempoRestanteAtencionMembresia = eventoAInterrumpir.tiempo - v.reloj;
                        v.finAtencionMembresia = 0;
                        v.tiempoAtencionMembresia = 0;
                        
                        
                        Estudiante estudianteACambiar = v.estudiantes.Find(e => e.tiempoFinAtencion == eventoAInterrumpir.tiempo);
                        estudianteACambiar.estado = "Esperando fin interrupción";

                    }

                    if (i >= mostrarDesde && i <= mostrarDesde + 300 && primeraEntrada)
                    {
                        primeraEntrada = false;
                        (v.tiempoEnfriamiento, dtRk) = rkGuardar(v.reloj);
                    }

                    v.tiempoEnfriamiento = rk(v.reloj);
                    v.finEnfriamiento = v.tiempoEnfriamiento + v.reloj;
                    Evento finInterrupcion = new Evento(TipoEvento.FinInterrupcion, TipoServicio.GestionarMembresia, v.finEnfriamiento, 0);

                    colaEventos.Add(finInterrupcion);

                }

                // Procesamos el fin de interrupcion
                else
                {

                    Estudiante estudianteInterrumpido = v.estudiantes.Find(e => e.estado == "Esperando fin interrupción");
                    if (estudianteInterrumpido != null)
                    {
                        v.servidoresMembresia.estado = Estado.Ocupado;

                        v.finAtencionMembresia = v.tiempoRestanteAtencionMembresia + v.reloj;
                        Evento nuevoFinGestionMembresia = new Evento(TipoEvento.FinAtencion, TipoServicio.GestionarMembresia, v.finAtencionMembresia, mediaFinGestionarMembresia);
                        estudianteInterrumpido.tiempoFinAtencion = v.finAtencionMembresia;

                        colaEventos.Add(nuevoFinGestionMembresia);
                        v.tiempoRestanteAtencionMembresia = 0.0;
                    }

                    else if (estudianteInterrumpido == null && v.colaGestionarMembresia.Count != 0)  // Si no interrumpió a nadie antes
                    {
                        v.servidoresMembresia.estado = Estado.Ocupado;

                        Estudiante estudianteMembresia = v.colaGestionarMembresia.Dequeue();

                        double random = rnd.NextDouble();
                        double tiempoAtencion = calcularTiempo(random, mediaFinGestionarMembresia);
                        double tiempoFinAtencion = v.reloj + tiempoAtencion;

                        Evento finAtencion = new Evento(TipoEvento.FinAtencion, TipoServicio.GestionarMembresia, tiempoFinAtencion, mediaFinGestionarMembresia);
                        colaEventos.Add(finAtencion);

                        v.finAtencionMembresia = tiempoFinAtencion;
                        v.RNDFinAtencionMembresia = random;
                        v.tiempoAtencionMembresia = tiempoAtencion;

                        estudianteMembresia.estado = ("Gestionando membresía");
                        estudianteMembresia.tiempoFinAtencion = tiempoFinAtencion;
                        estudianteMembresia.servidor = 0;
                        estudianteMembresia.horaInicioAtencion = v.reloj;
                        v.estudiantes.Add(estudianteMembresia);


                        // variables estadisticas
                        v.acTiempoEsperaGestionarMembresia += v.reloj - estudianteMembresia.tiempoLlegada;
                        v.atendidosGestionarMembresia += 1;

                    }

                    else 
                    {
                        v.servidoresMembresia.estado = Estado.Libre;
                        // REVISAR VARIABLES ESTADISTICAS
                    }

                    // Generamos la proxima interrupcion
                    v.RNDProximaInterrupcion = rnd.NextDouble();
                    v.tiempoEntreInterupciones = calcularProximaInterrupcion(v.RNDProximaInterrupcion, t);
                    v.proximaInterrupcion = v.tiempoEntreInterupciones + v.reloj;
                    Evento nuevaInterrupcion = new Evento(TipoEvento.Interrupcion, TipoServicio.GestionarMembresia, v.proximaInterrupcion, t);
                    colaEventos.Add(nuevaInterrupcion);
                }

                
                if (i >= mostrarDesde && i <= mostrarDesde + 300)
                {
                    if (tipoSim) { cargarDataGrid2(i, dt); }
                    else { cargarDataGrid(i, dt); }
                }

                i++;

                // chequeamos la disponibilidad de los servidores para calcular el tiempo de ocupacion
                for (int j = 0; j < v.servidoresPrestamo.Length; j++)
                {
                    if (v.servidoresPrestamo[j]) { break; }
                    else
                    {
                        v.acTiempoServicioPrestamos += (v.reloj - v.relojAnterior);
                        break;
                    }
                }
                for (int j = 0; j < v.servidoresDevolucion.Length; j++)
                {
                    if (v.servidoresDevolucion[j]) { break; }
                    else
                    {
                        v.acTiempoServicioDevoluciones += (v.reloj - v.relojAnterior);
                        break;
                    }
                }
                for (int j = 0; j < v.servidoresConsulta.Length; j++)
                {
                    if (v.servidoresConsulta[j]) { break; }
                    else
                    {
                        v.acTiempoServicioConsultas += (v.reloj - v.relojAnterior);
                        break;
                    }
                }
                for (int j = 0; j < v.servidoresPC.Length; j++)
                {
                    if (v.servidoresPC[j]) { break; }
                    else
                    {
                        v.acTiempoServicioPC += (v.reloj - v.relojAnterior);
                        break;
                    }
                }
                for (int j = 0; j < v.servidoresInfoGeneral.Length; j++)
                {
                    if (v.servidoresInfoGeneral[j]) { break; }
                    else
                    {
                        v.acTiempoServicioInfoGral += (v.reloj - v.relojAnterior);
                        break;
                    }
                }
                if (v.servidoresMembresia.estado == Estado.Libre) 
                { 
                    continue; 
                }
                else
                {
                    v.acTiempoServicioGestionarMembresia += (v.reloj - v.relojAnterior);
                }
            }

            if (tipoSim) { cargarDataGrid2(i, dt); }
            else { cargarDataGrid(i, dt); }

            return (dt, dtRk);
        }

        private double calcularProximaInterrupcion(double RNDProximaInterrupcion, double t)
        {
            double res = 0;
            if (RNDProximaInterrupcion < 0.2) 
            {

                res = t * 4;

            }

            else if (RNDProximaInterrupcion < 0.8) 
            {
                res = t * 6;
            }

            else 
            {

                res = t * 8;

            }


            return res;
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
            List<Object> fila = new List<Object>();

            fila.Add(i);
            fila.Add(v.evento);
            fila.Add(TruncateDecimal(v.reloj, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClientePrestamos, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesPrestamos, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesPrestamos, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteDevolucion, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesDevolucion, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesDevolucion, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteConsulta, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesConsulta, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesConsulta, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClientePC, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesPC, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesPC, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteInfoGral, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesInfoGral, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesInfoGral, 2));

            fila.Add(TruncateDecimal(v.RNDProximaInterrupcion, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreInterupciones, 2));
            fila.Add(TruncateDecimal(v.proximaInterrupcion, 2));

            fila.Add(TruncateDecimal(v.reloj, 2));
            fila.Add(TruncateDecimal(v.tiempoEnfriamiento, 2));
            fila.Add(TruncateDecimal(v.finEnfriamiento, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionPrestamo, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionPrestamo, 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[1], 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[2], 2));
            fila.Add(v.servidoresPrestamo[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPrestamo[1] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPrestamo[2] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaPrestamos.Count, 2));
  
            fila.Add(TruncateDecimal(v.RNDFinAtencionDevolucion, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionDevolucion, 2));
            fila.Add(TruncateDecimal(v.finAtencionDevolucion[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionDevolucion[1], 2));
            fila.Add(v.servidoresDevolucion[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresDevolucion[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaDevolucion.Count, 2));
 
            fila.Add(TruncateDecimal(v.RNDFinAtencionConsulta, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionConsulta, 2));
            fila.Add(TruncateDecimal(v.finAtencionConsulta[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionConsulta[1], 2));
            fila.Add(v.servidoresConsulta[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresConsulta[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaConsultas.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionPC, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionPC, 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[1], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[2], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[3], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[4], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[5], 2));
            fila.Add(v.servidoresPC[1] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[2] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[3] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[4] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[5] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaPC.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionInfoGeneral, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionInfoGeneral, 2));
            fila.Add(TruncateDecimal(v.finAtencionInfoGeneral[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionInfoGeneral[1], 2));
            fila.Add(v.servidoresInfoGeneral[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresInfoGeneral[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaInfoGeneral.Count, 2));

            fila.Add(TruncateDecimal(v.RNDGestionarMembresia, 2));
            fila.Add(v.booleanoGestiona ? "Si" : "No");

            fila.Add(TruncateDecimal(v.RNDFinAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.finAtencionMembresia, 2));
            fila.Add(v.servidoresMembresia.estado.ToString());
            fila.Add(TruncateDecimal(v.tiempoRestanteAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.colaGestionarMembresia.Count, 2));
 
            fila.Add(TruncateDecimal(v.acTiempoEsperaPrestamos, 2));
            fila.Add(TruncateDecimal(v.atendidosPrestamos, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioPrestamos, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaDevoluciones, 2));
            fila.Add(TruncateDecimal(v.atendidosDevoluciones, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioDevoluciones, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaConsultas, 2));
            fila.Add(TruncateDecimal(v.atendidosConsultas, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioConsultas, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaPC, 2));
            fila.Add(TruncateDecimal(v.atendidosPC, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioPC, 2));
   
            fila.Add(TruncateDecimal(v.acTiempoEsperaInfoGral, 2));
            fila.Add(TruncateDecimal(v.atendidosInfoGral, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioInfoGral, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaGestionarMembresia, 2));
            fila.Add(TruncateDecimal(v.atendidosGestionarMembresia, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioGestionarMembresia, 2));

            fila.Add(TruncateDecimal(v.mayorNumeroDeGenteEnCola, 2));

            foreach (Estudiante e in v.estudiantes) { fila.Add((e.id, e.estado)); } //96 filas

            DataRow newRow = dt.NewRow();
            for (int j = 0; j < fila.Count && j < 106; j++) { newRow[j] = fila[j]; }
            dt.Rows.Add(newRow);


        }

        public void cargarDataGrid2(int i, DataTable dt)
        {
            List<Object> fila = new List<Object>();

            fila.Add(i);
            fila.Add(v.evento);
            fila.Add(TruncateDecimal(v.reloj, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClientePrestamos, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesPrestamos, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesPrestamos, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteDevolucion, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesDevolucion, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesDevolucion, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteConsulta, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesConsulta, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesConsulta, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClientePC, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesPC, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesPC, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteInfoGral, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesInfoGral, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesInfoGral, 2));

            fila.Add(TruncateDecimal(v.RNDProximaInterrupcion, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreInterupciones, 2));
            fila.Add(TruncateDecimal(v.proximaInterrupcion, 2));

            fila.Add(TruncateDecimal(v.reloj, 2));
            fila.Add(TruncateDecimal(v.tiempoEnfriamiento, 2));
            fila.Add(TruncateDecimal(v.finEnfriamiento, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionPrestamo, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionPrestamo, 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[1], 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[2], 2));
            fila.Add(v.servidoresPrestamo[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPrestamo[1] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPrestamo[2] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaPrestamos.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionDevolucion, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionDevolucion, 2));
            fila.Add(TruncateDecimal(v.finAtencionDevolucion[0], 2));
            fila.Add(v.servidoresDevolucion[0] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaDevolucion.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionConsulta, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionConsulta, 2));
            fila.Add(TruncateDecimal(v.finAtencionConsulta[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionConsulta[1], 2));
            fila.Add(v.servidoresConsulta[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresConsulta[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaConsultas.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionPC, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionPC, 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[1], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[2], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[3], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[4], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[5], 2));
            fila.Add(v.servidoresPC[1] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[2] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[3] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[4] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[5] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaPC.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionInfoGeneral, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionInfoGeneral, 2));
            fila.Add(TruncateDecimal(v.finAtencionInfoGeneral[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionInfoGeneral[1], 2));
            fila.Add(v.servidoresInfoGeneral[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresInfoGeneral[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaInfoGeneral.Count, 2));

            fila.Add(TruncateDecimal(v.RNDGestionarMembresia, 2));
            fila.Add(v.booleanoGestiona ? "Si" : "No");

            fila.Add(TruncateDecimal(v.RNDFinAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.finAtencionMembresia, 2));
            fila.Add(v.servidoresMembresia.estado.ToString());
            fila.Add(TruncateDecimal(v.tiempoRestanteAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.colaGestionarMembresia.Count, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaPrestamos, 2));
            fila.Add(TruncateDecimal(v.atendidosPrestamos, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioPrestamos, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaDevoluciones, 2));
            fila.Add(TruncateDecimal(v.atendidosDevoluciones, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioDevoluciones, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaConsultas, 2));
            fila.Add(TruncateDecimal(v.atendidosConsultas, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioConsultas, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaPC, 2));
            fila.Add(TruncateDecimal(v.atendidosPC, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioPC, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaInfoGral, 2));
            fila.Add(TruncateDecimal(v.atendidosInfoGral, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioInfoGral, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaGestionarMembresia, 2));
            fila.Add(TruncateDecimal(v.atendidosGestionarMembresia, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioGestionarMembresia, 2));

            fila.Add(TruncateDecimal(v.mayorNumeroDeGenteEnCola, 2));

            foreach (Estudiante e in v.estudiantes) { fila.Add((e.id, e.estado)); } //96 filas

            DataRow newRow = dt.NewRow();
            for (int j = 0; j < fila.Count && j < 104; j++) { newRow[j] = fila[j]; }
            dt.Rows.Add(newRow);


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


            dt.Columns.Add("RND Prestamo");
            dt.Columns.Add("Tiempo entre llegadas (Prestamos)");
            dt.Columns.Add("Proxima llegada estudiante (Prestamos)");

            dt.Columns.Add("RND Devolución");
            dt.Columns.Add("Tiempo entre llegadas (Devoluciones)");
            dt.Columns.Add("Proxima llegada estudiante (Devoluciones)");

            dt.Columns.Add("RND Consulta");
            dt.Columns.Add("Tiempo entre llegadas (Consultas)");
            dt.Columns.Add("Proxima llegada estudiante (Consultas)");

            dt.Columns.Add("RND PC");
            dt.Columns.Add("Tiempo entre llegadas (Servicio PC)");
            dt.Columns.Add("Proxima llegada estudiante (Servicio PC)");

            dt.Columns.Add("RND Info General");
            dt.Columns.Add("Tiempo entre llegadas (Información General)");
            dt.Columns.Add("Proxima llegada estudiante (Información General)");

            dt.Columns.Add("RND Próximo corte de luz");
            dt.Columns.Add("Tiempo entre cortes");
            dt.Columns.Add("Próximo corte de luz");

            dt.Columns.Add("Valor de 'C'");
            dt.Columns.Add("Tiempo de enfriación de térmica");
            dt.Columns.Add("Proximo fin de corte de luz");

            dt.Columns.Add("RND Fin (Prestamo)");
            dt.Columns.Add("Tiempo atendiendo (Prestamos)");
            dt.Columns.Add("Fin atención 1 (Prestamos)");
            dt.Columns.Add("Fin atención 2 (Prestamos)");
            dt.Columns.Add("Fin atención 3 (Prestamos)");
            dt.Columns.Add("Bibliotecario 1 (Prestamos)");
            dt.Columns.Add("Bibliotecario 2 (Prestamos)");
            dt.Columns.Add("Bibliotecario 3 (Prestamos)");
            dt.Columns.Add("Cola prestamos");

            dt.Columns.Add("RND Fin (Devolución)");
            dt.Columns.Add("Tiempo atendiendo (Devolución)");
            dt.Columns.Add("Fin atención 1 (Devolución)");
            dt.Columns.Add("Fin atención 2 (Devolución)");
            dt.Columns.Add("Bibliotecario 1 (Devolución)");
            dt.Columns.Add("Bibliotecario 2 (Devolución)");
            dt.Columns.Add("Cola devoluciones");

            dt.Columns.Add("RND Fin (Consulta)");
            dt.Columns.Add("Tiempo atendiendo (Consulta)");
            dt.Columns.Add("Fin atención 1 (Consultas)");
            dt.Columns.Add("Fin atención 2 (Consultas)");
            dt.Columns.Add("Bibliotecario 1 (Consulta)");
            dt.Columns.Add("Bibliotecario 2 (Consulta)");
            dt.Columns.Add("Cola consultas");

            dt.Columns.Add("RND Fin (PC)");
            dt.Columns.Add("Tiempo de uso (PC)");
            dt.Columns.Add("Fin Atencion PC 1");
            dt.Columns.Add("Fin Atencion PC 2");
            dt.Columns.Add("Fin Atencion PC 3");
            dt.Columns.Add("Fin Atencion PC 4");
            dt.Columns.Add("Fin Atencion PC 5");
            dt.Columns.Add("Fin Atencion PC 6");
            dt.Columns.Add("PC 1");
            dt.Columns.Add("PC 2");
            dt.Columns.Add("PC 3");
            dt.Columns.Add("PC 4");
            dt.Columns.Add("PC 5");
            dt.Columns.Add("PC 6");
            dt.Columns.Add("Cola PCs");

            dt.Columns.Add("RND Fin (Info General)");
            dt.Columns.Add("Tiempo atendiendo (Info General)");
            dt.Columns.Add("Fin atención 1 (Info General)");
            dt.Columns.Add("Fin atención 2 (Info General)");
            dt.Columns.Add("Bibliotecario 1 (Info General)");
            dt.Columns.Add("Bibliotecario 2 (Info General)");
            dt.Columns.Add("Cola Info General");

            dt.Columns.Add("RND Servicio Membresía");
            dt.Columns.Add("¿Va a gestionar su membresía?");

            dt.Columns.Add("RND Fin (Gestionar Membresía)");
            dt.Columns.Add("Tiempo atendiendo (Gestionar membresía)");
            dt.Columns.Add("Fin atención (Membresía)");
            dt.Columns.Add("Bibliotecario");
            dt.Columns.Add("Tiempo restante");
            dt.Columns.Add("Cola gestionar membresias");

            dt.Columns.Add("Tiempo acumulado de espera (Prestamos)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Prestamos)");
            dt.Columns.Add("Acumulador tiempo ocupado (Prestamos)");

            dt.Columns.Add("Tiempo acumulado de espera (Devoluciones)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Devoluciones)");
            dt.Columns.Add("Acumulador tiempo ocupado (Devoluciones)");

            dt.Columns.Add("Tiempo acumulado de espera (Consultas)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Consultas)");
            dt.Columns.Add("Acumulador tiempo ocupado (Consultas)");

            dt.Columns.Add("Tiempo acumulado de espera (PC)");
            dt.Columns.Add("Acumulado estudiantes atendidos (PC)");
            dt.Columns.Add("Acumulador tiempo ocupado (PCs)");

            dt.Columns.Add("Tiempo acumulado de espera (Info General)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Info General)");
            dt.Columns.Add("Acumulador tiempo ocupado (Info General)");

            dt.Columns.Add("Tiempo acumulado de espera (Gestionar Membresía)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Gestionar Membresía)");
            dt.Columns.Add("Acumulador tiempo ocupado (Gestionar membresía)");

            dt.Columns.Add("Mayor numero de gente en cola");
            dt.Columns.Add("Estudiante 1 (ID, Estado)");
            dt.Columns.Add("Estudiante 2 (ID, Estado)");
            dt.Columns.Add("Estudiante 3 (ID, Estado)");
            dt.Columns.Add("Estudiante 4 (ID, Estado)");
            dt.Columns.Add("Estudiante 5 (ID, Estado)");
            dt.Columns.Add("Estudiante 6 (ID, Estado)");
            dt.Columns.Add("Estudiante 7 (ID, Estado)");
            dt.Columns.Add("Estudiante 8 (ID, Estado)");
            dt.Columns.Add("Estudiante 9 (ID, Estado)");
            dt.Columns.Add("Estudiante N (ID, Estado)");

            return dt;
        }

        public DataTable crearDataTable2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Iteración");
            dt.Columns.Add("Evento");
            dt.Columns.Add("Reloj");


            dt.Columns.Add("RND Prestamo");
            dt.Columns.Add("Tiempo entre llegadas (Prestamos)");
            dt.Columns.Add("Proxima llegada estudiante (Prestamos)");

            dt.Columns.Add("RND Devolución");
            dt.Columns.Add("Tiempo entre llegadas (Devoluciones)");
            dt.Columns.Add("Proxima llegada estudiante (Devoluciones)");

            dt.Columns.Add("RND Consulta");
            dt.Columns.Add("Tiempo entre llegadas (Consultas)");
            dt.Columns.Add("Proxima llegada estudiante (Consultas)");

            dt.Columns.Add("RND PC");
            dt.Columns.Add("Tiempo entre llegadas (Servicio PC)");
            dt.Columns.Add("Proxima llegada estudiante (Servicio PC)");

            dt.Columns.Add("RND Info General");
            dt.Columns.Add("Tiempo entre llegadas (Información General)");
            dt.Columns.Add("Proxima llegada estudiante (Información General)");

            dt.Columns.Add("RND Próximo corte de luz");
            dt.Columns.Add("Tiempo entre cortes");
            dt.Columns.Add("Próximo corte de luz");

            dt.Columns.Add("Valor de 'C'");
            dt.Columns.Add("Tiempo de enfriación de térmica");
            dt.Columns.Add("Proximo fin de corte de luz");

            dt.Columns.Add("RND Fin (Prestamo)");
            dt.Columns.Add("Tiempo atendiendo (Prestamos)");
            dt.Columns.Add("Fin atención 1 (Prestamos)");
            dt.Columns.Add("Fin atención 2 (Prestamos)");
            dt.Columns.Add("Fin atención 3 (Prestamos)");
            dt.Columns.Add("Bibliotecario 1 (Prestamos)");
            dt.Columns.Add("Bibliotecario 2 (Prestamos)");
            dt.Columns.Add("Bibliotecario 3 (Prestamos)");
            dt.Columns.Add("Cola prestamos");

            dt.Columns.Add("RND Fin (Devolución)");
            dt.Columns.Add("Tiempo atendiendo (Devolución)");
            dt.Columns.Add("Fin atención 1 (Devolución)");
            dt.Columns.Add("Bibliotecario 1 (Devolución)");
            dt.Columns.Add("Cola devoluciones");

            dt.Columns.Add("RND Fin (Consulta)");
            dt.Columns.Add("Tiempo atendiendo (Consulta)");
            dt.Columns.Add("Fin atención 1 (Consultas)");
            dt.Columns.Add("Fin atención 2 (Consultas)");
            dt.Columns.Add("Bibliotecario 1 (Consulta)");
            dt.Columns.Add("Bibliotecario 2 (Consulta)");
            dt.Columns.Add("Cola consultas");

            dt.Columns.Add("RND Fin (PC)");
            dt.Columns.Add("Tiempo de uso (PC)");
            dt.Columns.Add("Fin Atencion PC 1");
            dt.Columns.Add("Fin Atencion PC 2");
            dt.Columns.Add("Fin Atencion PC 3");
            dt.Columns.Add("Fin Atencion PC 4");
            dt.Columns.Add("Fin Atencion PC 5");
            dt.Columns.Add("Fin Atencion PC 6");
            dt.Columns.Add("PC 1");
            dt.Columns.Add("PC 2");
            dt.Columns.Add("PC 3");
            dt.Columns.Add("PC 4");
            dt.Columns.Add("PC 5");
            dt.Columns.Add("PC 6");
            dt.Columns.Add("Cola PCs");

            dt.Columns.Add("RND Fin (Info General)");
            dt.Columns.Add("Tiempo atendiendo (Info General)");
            dt.Columns.Add("Fin atención 1 (Info General)");
            dt.Columns.Add("Fin atención 2 (Info General)");
            dt.Columns.Add("Bibliotecario 1 (Info General)");
            dt.Columns.Add("Bibliotecario 2 (Info General)");
            dt.Columns.Add("Cola Info General");

            dt.Columns.Add("RND Servicio Membresía");
            dt.Columns.Add("¿Va a gestionar su membresía?");

            dt.Columns.Add("RND Fin (Gestionar Membresía)");
            dt.Columns.Add("Tiempo atendiendo (Gestionar membresía)");
            dt.Columns.Add("Fin atención (Membresía)");
            dt.Columns.Add("Bibliotecario");
            dt.Columns.Add("Tiempo restante");
            dt.Columns.Add("Cola gestionar membresias");

            dt.Columns.Add("Tiempo acumulado de espera (Prestamos)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Prestamos)");
            dt.Columns.Add("Acumulador tiempo ocupado (Prestamos)");

            dt.Columns.Add("Tiempo acumulado de espera (Devoluciones)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Devoluciones)");
            dt.Columns.Add("Acumulador tiempo ocupado (Devoluciones)");

            dt.Columns.Add("Tiempo acumulado de espera (Consultas)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Consultas)");
            dt.Columns.Add("Acumulador tiempo ocupado (Consultas)");

            dt.Columns.Add("Tiempo acumulado de espera (PC)");
            dt.Columns.Add("Acumulado estudiantes atendidos (PC)");
            dt.Columns.Add("Acumulador tiempo ocupado (PCs)");

            dt.Columns.Add("Tiempo acumulado de espera (Info General)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Info General)");
            dt.Columns.Add("Acumulador tiempo ocupado (Info General)");

            dt.Columns.Add("Tiempo acumulado de espera (Gestionar Membresía)");
            dt.Columns.Add("Acumulado estudiantes atendidos (Gestionar Membresía)");
            dt.Columns.Add("Acumulador tiempo ocupado (Gestionar membresía)");

            dt.Columns.Add("Mayor numero de gente en cola");
            dt.Columns.Add("Estudiante 1 (ID, Estado)");
            dt.Columns.Add("Estudiante 2 (ID, Estado)");
            dt.Columns.Add("Estudiante 3 (ID, Estado)");
            dt.Columns.Add("Estudiante 4 (ID, Estado)");
            dt.Columns.Add("Estudiante 5 (ID, Estado)");
            dt.Columns.Add("Estudiante 6 (ID, Estado)");
            dt.Columns.Add("Estudiante 7 (ID, Estado)");
            dt.Columns.Add("Estudiante 8 (ID, Estado)");
            dt.Columns.Add("Estudiante 9 (ID, Estado)");
            dt.Columns.Add("Estudiante N (ID, Estado)");

            return dt;
        }

        public void cargarEstudiantes(List<Estudiante> estudiantes, DataTable dt)
        {
            foreach (Estudiante e in estudiantes)
            {
                int i = 0;
                dt.Columns.Add($"Estudiante {i}");
                dt.Columns.Add($"Estado {i}");
                i++;
            }

        }

        public int chequearColaMasGrande(int tamañoCola, int colaMasGrande)
        {
            if (tamañoCola > colaMasGrande) { return tamañoCola; }
            else return colaMasGrande;
        }

        public string[] calcularVariablesEstadisticas()
        {
            var ve = v;
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
            resultados[15] = TruncateDecimal((totalAtendidos / (v.reloj /60)), 2).ToString();

            return resultados;

        }

        double rk(double horaReloj)
        {
            var h = 0.1;

            // inicializaciÃ³n de variables
            var x = -h;
            var y = 0.0;
            double k1;
            double k2;
            double k3;
            double k4;
            var ySiguiente = horaReloj;

            // calcula cada fila de rk hasta que y sea negativo
            while (y >= 0)
            {
                x += h;
                y = ySiguiente;

                k1 = funcionDif(x, y);
                k2 = funcionDif(x + h / 2, y + h / 2 * k1);
                k3 = h * funcionDif(x + h / 2, y + h / 2 * k2);
                k4 = h * funcionDif(x, y + h * k3);
                ySiguiente = y + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
                //x += h;
                //y = ySiguiente;
            }

            // t = 1 equivale 30 segundos y transforma a minutos.
            return x * 30 / 60;
        }

        (double, DataTable) rkGuardar(double horaReloj)
        {
            DataTable resultados = new DataTable();
            resultados.Columns.Add("t");
            resultados.Columns.Add("c");
            resultados.Columns.Add("k1");
            resultados.Columns.Add("k2");
            resultados.Columns.Add("k3");
            resultados.Columns.Add("k4");
            resultados.Columns.Add("tSiguiente");
            resultados.Columns.Add("cSiguiente");

            var h = 0.1;

            // inicializaciÃ³n de variables
            var x = -h;
            var y = 0.0;
            double k1 = 0;
            double k2 = 0;
            double k3 = 0;
            double k4 = 0;
            var ySiguiente = horaReloj;

            resultados.Rows.Add(x, y, k1, k2, k3, k4, x+h, ySiguiente);

            // calcula cada fila de rk hasta que y sea negativo
            while (y >= 0)
            {
                x += h;
                y = ySiguiente;

                k1 = funcionDif(x, y);
                k2 = funcionDif(x + h / 2, y + h / 2 * k1);
                k3 = h * funcionDif(x + h / 2, y + h / 2 * k2);
                k4 = h * funcionDif(x, y + h * k3);
                ySiguiente = y + (h / 6) * (k1 + 2 * k2 + 2 * k3 + k4);
                //x += h;
                //y = ySiguiente;

                resultados.Rows.Add(x, y, k1, k2, k3, k4, x+h, ySiguiente);
            }
            return (x * 0.5, resultados);
        }

        public double funcionDif(double x, double y)
        {
            return 0.025 * x - 0.5 * y - 12.85;
        }
        public void limpiarAtributos() 
        {
            v.RNDLLegadaClientePrestamos = 0.0;
            v.tiempoEntreLlegadasClientesPrestamos = 0.0;

            v.RNDLLegadaClienteDevolucion = 0.0;
            v.tiempoEntreLlegadasClientesDevolucion = 0.0;

            v.RNDLLegadaClienteConsulta = 0.0;
            v.tiempoEntreLlegadasClientesConsulta = 0.0;

            v.RNDLLegadaClientePC = 0.0;
            v.tiempoEntreLlegadasClientesPC = 0.0;

            v.RNDLLegadaClienteInfoGral = 0.0;
            v.tiempoEntreLlegadasClientesInfoGral = 0.0;

            v.RNDGestionarMembresia = 0.0;
            v.tiempoAtencionMembresia = 0.0;
            v.RNDFinAtencionMembresia = 0.0;

            v.RNDProximaInterrupcion = 0.0;
            v.tiempoEntreInterupciones = 0.0;

            v.c = 0.0;
            v.tiempoEnfriamiento = 0.0;

            v.RNDFinAtencionConsulta = 0.0;
            v.tiempoAtencionConsulta = 0.0;

            v.RNDFinAtencionPrestamo = 0.0;
            v.tiempoAtencionPrestamo = 0.0;

            v.RNDFinAtencionDevolucion = 0.0;
            v.tiempoAtencionDevolucion = 0.0;

            v.RNDFinAtencionConsulta = 0.0;
            v.tiempoAtencionConsulta = 0.0;

            v.RNDFinAtencionPC = 0.0;
            v.tiempoAtencionPC = 0.0;


        }

        public void agregarFilaInicializacion(int i, DataTable dt) 
        {
            List<Object> fila = new List<Object>();

            fila.Add(i);
            fila.Add(v.evento);
            fila.Add(TruncateDecimal(v.reloj, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClientePrestamos, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesPrestamos, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesPrestamos, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteDevolucion, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesDevolucion, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesDevolucion, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteConsulta, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesConsulta, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesConsulta, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClientePC, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesPC, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesPC, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteInfoGral, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesInfoGral, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesInfoGral, 2));

            fila.Add(TruncateDecimal(v.RNDProximaInterrupcion, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreInterupciones, 2));
            fila.Add(TruncateDecimal(v.proximaInterrupcion, 2));

            fila.Add(TruncateDecimal(v.reloj, 2));
            fila.Add(TruncateDecimal(v.tiempoEnfriamiento, 2));
            fila.Add(TruncateDecimal(v.finEnfriamiento, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionPrestamo, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionPrestamo, 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[1], 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[2], 2));
            fila.Add(v.servidoresPrestamo[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPrestamo[1] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPrestamo[2] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaPrestamos.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionDevolucion, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionDevolucion, 2));
            fila.Add(TruncateDecimal(v.finAtencionDevolucion[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionDevolucion[1], 2));
            fila.Add(v.servidoresDevolucion[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresDevolucion[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaDevolucion.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionConsulta, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionConsulta, 2));
            fila.Add(TruncateDecimal(v.finAtencionConsulta[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionConsulta[1], 2));
            fila.Add(v.servidoresConsulta[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresConsulta[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaConsultas.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionPC, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionPC, 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[1], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[2], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[3], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[4], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[5], 2));
            fila.Add(v.servidoresPC[1] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[2] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[3] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[4] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[5] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaPC.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionInfoGeneral, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionInfoGeneral, 2));
            fila.Add(TruncateDecimal(v.finAtencionInfoGeneral[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionInfoGeneral[1], 2));
            fila.Add(v.servidoresInfoGeneral[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresInfoGeneral[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaInfoGeneral.Count, 2));

            fila.Add(TruncateDecimal(v.RNDGestionarMembresia, 2));
            fila.Add(v.booleanoGestiona ? "Si" : "No");

            fila.Add(TruncateDecimal(v.RNDFinAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.finAtencionMembresia, 2));
            fila.Add(v.servidoresMembresia.estado.ToString());
            fila.Add(TruncateDecimal(v.tiempoRestanteAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.colaGestionarMembresia.Count, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaPrestamos, 2));
            fila.Add(TruncateDecimal(v.atendidosPrestamos, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioPrestamos, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaDevoluciones, 2));
            fila.Add(TruncateDecimal(v.atendidosDevoluciones, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioDevoluciones, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaConsultas, 2));
            fila.Add(TruncateDecimal(v.atendidosConsultas, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioConsultas, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaPC, 2));
            fila.Add(TruncateDecimal(v.atendidosPC, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioPC, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaInfoGral, 2));
            fila.Add(TruncateDecimal(v.atendidosInfoGral, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioInfoGral, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaGestionarMembresia, 2));
            fila.Add(TruncateDecimal(v.atendidosGestionarMembresia, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioGestionarMembresia, 2));

            fila.Add(TruncateDecimal(v.mayorNumeroDeGenteEnCola, 2));

            foreach (Estudiante e in v.estudiantes) { fila.Add((e.id, e.estado)); } //96 filas

            DataRow newRow = dt.NewRow();
            for (int j = 0; j < fila.Count && j < 106; j++) { newRow[j] = fila[j]; }
            dt.Rows.Add(newRow);


        }

        public void agregarFilaInicializacion2(int i, DataTable dt) 
        {
            List<Object> fila = new List<Object>();

            fila.Add(i);
            fila.Add(v.evento);
            fila.Add(TruncateDecimal(v.reloj, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClientePrestamos, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesPrestamos, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesPrestamos, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteDevolucion, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesDevolucion, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesDevolucion, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteConsulta, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesConsulta, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesConsulta, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClientePC, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesPC, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesPC, 2));

            fila.Add(TruncateDecimal(v.RNDLLegadaClienteInfoGral, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreLlegadasClientesInfoGral, 2));
            fila.Add(TruncateDecimal(v.proximaLlegadaClientesInfoGral, 2));

            fila.Add(TruncateDecimal(v.RNDProximaInterrupcion, 2));
            fila.Add(TruncateDecimal(v.tiempoEntreInterupciones, 2));
            fila.Add(TruncateDecimal(v.proximaInterrupcion, 2));

            fila.Add(TruncateDecimal(v.reloj, 2));
            fila.Add(TruncateDecimal(v.tiempoEnfriamiento, 2));
            fila.Add(TruncateDecimal(v.finEnfriamiento, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionPrestamo, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionPrestamo, 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[1], 2));
            fila.Add(TruncateDecimal(v.finAtencionPrestamo[2], 2));
            fila.Add(v.servidoresPrestamo[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPrestamo[1] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPrestamo[2] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaPrestamos.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionDevolucion, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionDevolucion, 2));
            fila.Add(TruncateDecimal(v.finAtencionDevolucion[0], 2));
            fila.Add(v.servidoresDevolucion[0] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaDevolucion.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionConsulta, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionConsulta, 2));
            fila.Add(TruncateDecimal(v.finAtencionConsulta[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionConsulta[1], 2));
            fila.Add(v.servidoresConsulta[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresConsulta[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaConsultas.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionPC, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionPC, 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[1], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[2], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[3], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[4], 2));
            fila.Add(TruncateDecimal(v.finAtencionPC[5], 2));
            fila.Add(v.servidoresPC[1] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[2] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[3] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[4] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresPC[5] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaPC.Count, 2));

            fila.Add(TruncateDecimal(v.RNDFinAtencionInfoGeneral, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionInfoGeneral, 2));
            fila.Add(TruncateDecimal(v.finAtencionInfoGeneral[0], 2));
            fila.Add(TruncateDecimal(v.finAtencionInfoGeneral[1], 2));
            fila.Add(v.servidoresInfoGeneral[0] ? "Libre" : "Ocupado");
            fila.Add(v.servidoresInfoGeneral[1] ? "Libre" : "Ocupado");
            fila.Add(TruncateDecimal(v.colaInfoGeneral.Count, 2));

            fila.Add(TruncateDecimal(v.RNDGestionarMembresia, 2));
            fila.Add(v.booleanoGestiona ? "Si" : "No");

            fila.Add(TruncateDecimal(v.RNDFinAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.tiempoAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.finAtencionMembresia, 2));
            fila.Add(v.servidoresMembresia.estado.ToString());
            fila.Add(TruncateDecimal(v.tiempoRestanteAtencionMembresia, 2));
            fila.Add(TruncateDecimal(v.colaGestionarMembresia.Count, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaPrestamos, 2));
            fila.Add(TruncateDecimal(v.atendidosPrestamos, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioPrestamos, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaDevoluciones, 2));
            fila.Add(TruncateDecimal(v.atendidosDevoluciones, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioDevoluciones, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaConsultas, 2));
            fila.Add(TruncateDecimal(v.atendidosConsultas, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioConsultas, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaPC, 2));
            fila.Add(TruncateDecimal(v.atendidosPC, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioPC, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaInfoGral, 2));
            fila.Add(TruncateDecimal(v.atendidosInfoGral, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioInfoGral, 2));

            fila.Add(TruncateDecimal(v.acTiempoEsperaGestionarMembresia, 2));
            fila.Add(TruncateDecimal(v.atendidosGestionarMembresia, 2));
            fila.Add(TruncateDecimal(v.acTiempoServicioGestionarMembresia, 2));

            fila.Add(TruncateDecimal(v.mayorNumeroDeGenteEnCola, 2));

            foreach (Estudiante e in v.estudiantes) { fila.Add((e.id, e.estado)); } //96 filas

            DataRow newRow = dt.NewRow();
            for (int j = 0; j < fila.Count && j < 106; j++) { newRow[j] = fila[j]; }
            dt.Rows.Add(newRow);

        }


    }




}
