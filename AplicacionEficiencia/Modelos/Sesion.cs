using AplicacionEficiencia.Dal;
using AplicacionEficiencia.Vistas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            horaFin = DateTime.Now;
            activa = true;
            GuardarDatosSesion(this);
            programasMonitoreo = obtenerListaDeProgramasMonitoreados();
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
            while (activa)
            {
                System.Threading.Thread.Sleep(1000); // Esperar 1 segundo (ampliar este número podría mejorar el rendimiento del programa)

                SesionActual.sesionActualVista.Dispatcher.Invoke(() =>
                {
                    SesionActual.sesionActualVista.testSesion.Content = "";
                    SesionActual.sesionActualVista.label.Content = calcularTiempoTranscurrido();

                    foreach (var item in programasMonitoreo)
                    {
                        if (item.activa)
                        {
                            if (Process.GetProcessesByName(item.programa.nombreProceso).Length <= 0)
                                item.finalizar();
                            item.calcularTiempoTranscurrido(DateTime.Now);
                        }
                    }
                    MostrarInformacionEnPantalla();
                });
                monitorearReaperturaProgramas();
                bloquearProgramas();
            }
        }

        void bloquearProgramas() { 
            foreach(Programa programaABloquear in Perfil.programasBloqueados) {
                if (Process.GetProcessesByName(programaABloquear.nombreProceso).Length > 0) {
                   
                    foreach (var proceso in Process.GetProcessesByName(programaABloquear.nombreProceso)) { 
                        proceso.Kill();
                    }
                    MessageBox.Show($"No puede iniciar {programaABloquear.nombre} porque está en la lista de programas bloquados");
                }
            }
        }

        public bool VerificarProcesoActivo(string nombreProceso)
        {
            bool existeSesionProgramaEspecifico = programasMonitoreo.Any(sesionPrograma => sesionPrograma.programa.nombreProceso == nombreProceso);

            if (existeSesionProgramaEspecifico)
            {
                bool hayActivo = programasMonitoreo.Any(sesionPrograma => sesionPrograma.programa.nombreProceso == nombreProceso && sesionPrograma.activa);
                if (hayActivo)
                    return true;
            }
            return false;
        }
        public void monitorearReaperturaProgramas() {
            foreach (Programa programa in Perfil.programasAEjecutar) {
                if (!VerificarProcesoActivo(programa.nombreProceso)) {
                    if (Process.GetProcessesByName(programa.nombreProceso).Length > 0)
                        programasMonitoreo.Add(new SesionPrograma(this, programa, DateTime.Now));
                }
            }
        }

        /// <summary>
        /// El que vaya a manejar la vista de Sesiones, debe tener en cuenta esta función porque es la que muestra la suma de 
        /// todos los tiempos del programa monitoreado (si se cierra y se vuelve a abrir).
        /// El tiempo total de cada programa se almacena en tiempoTotalProgramas.
        /// </summary>
        public void MostrarInformacionEnPantalla() {
            var tiempoTotalProgramas = programasMonitoreo.GroupBy(obj => obj.programa.nombreProceso)
                .Select(grupo => new
                {
                    NombreProceso = grupo.Key, 
                    TiempoTotal = TimeSpan.FromTicks(grupo.Sum(obj => obj.tiempoTranscurrido.Ticks)),
                    Programa = grupo.First().programa
                });
            foreach (var item in tiempoTotalProgramas)
            {
                SesionActual.sesionActualVista.testSesion.Content += $"{item.NombreProceso} | {item.TiempoTotal}\n";
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
            ActualizarDatosSesion(this);
            SesionActual.sesionActual = null;
            
        }
        public static void GuardarDatosSesion(Sesion sesion)
        {
            using (var conn = new ConexionContext())
            {
                SesionDTO dto = new SesionDTO();
                dto.horaFin = sesion.horaFin;
                dto.horaInicio = sesion.horaInicio;
                dto.PerfilId = sesion.Perfil.id;


                conn.Sesiones!.Add(dto);
                conn.SaveChanges();

                sesion.id = dto.id;
            }
        }

        public static void ActualizarDatosSesion(Sesion sesion)
        {
            using (var conn = new ConexionContext())
            {
                var existingSesion = conn.Sesiones.FirstOrDefault(s => s.id == sesion.id);

                if (existingSesion != null)
                {
                    // Actualiza los datos de la sesión existente
                    existingSesion.horaInicio = sesion.horaInicio;
                    existingSesion.horaFin = sesion.horaFin;
                }
                conn.SaveChanges();
            }
        }

    }
}
