using AplicacionEficiencia.Vistas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AplicacionEficiencia.Modelos
{
    public class Sesion
    {
        public int id { get; set; }
        public Perfil Perfil { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFin { get; set; }
        public bool activa { get; set; }
        public List<SesionPrograma> programasMonitoreo { get; set; }

        public Sesion(Perfil perfil)
        {
            Perfil = perfil;
            horaInicio = DateTime.Now;
            programasMonitoreo = obtenerListaDeProgramasMonitoreados();
            activa = true;
        }

        private List<SesionPrograma> obtenerListaDeProgramasMonitoreados()
        {
            List<SesionPrograma> lista = new List<SesionPrograma>();

            foreach (var programa in Perfil.programasAEjecutar)
            {
                var sesionPrograma = new SesionPrograma(this, programa, DateTime.Now);
                lista.Add(sesionPrograma);
            }
            return lista;
        }

        public void IniciarMonitoreo()
        {
            Task.Run(() => Monitorear());
        }

        private void Monitorear()
        {
            System.Threading.Thread.Sleep(10000); // Esperar 10 segundos

            while (activa)
            {
                System.Threading.Thread.Sleep(1000); // Esperar 1 segundo

                SesionActual.sesionActualVista.Dispatcher.Invoke(() =>
                {
                    SesionActual.sesionActualVista.testSesion.Content = "";
                    SesionActual.sesionActualVista.label.Content = calcularTiempoTranscurrido();
                    foreach (var item in programasMonitoreo)
                    {
                        Debug.WriteLine( item.programa.nombreProceso + "  "+ Process.GetProcessesByName(item.programa.nombre).Length);
                        if (Process.GetProcessesByName(item.programa.nombreProceso).Length <= 0)
                            if (item.activa) item.finalizar();
                        SesionActual.sesionActualVista.testSesion.Content += $"{item.programa.nombre} | {item.calcularTiempoTranscurrido(DateTime.Now)}\n";
                    }
                });
            }
        }

        public TimeSpan calcularTiempoTranscurrido()
        {
            if (!activa)
                return horaFin - horaInicio;
            return DateTime.Now - horaInicio;
        }

        public void Finalizar()
        {

            foreach (var item in programasMonitoreo) {
                item.finalizar();
                horaFin = DateTime.Now;
                activa = false;

            }
            SesionActual.sesionActual = null;
            ///Aquí hay que enviar los datos de la sesión a la database

        }


    }
}
