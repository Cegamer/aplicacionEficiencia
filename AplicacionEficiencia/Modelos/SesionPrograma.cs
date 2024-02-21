using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionEficiencia.Modelos
{
    public class SesionPrograma
    {
        public Sesion sesion { get; set; }
        public Programa programa { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFin { get; set; }
        public bool activa;

        public SesionPrograma(Sesion sesion, Programa programa, DateTime horaInicio)
        {
            this.sesion = sesion;
            this.programa = programa;
            this.horaInicio = horaInicio;
            this.activa = true;
        }

        public TimeSpan calcularTiempoTranscurrido(DateTime fin) {
            if (!activa)
                return horaFin - horaInicio;
            return fin - horaInicio;
        }
        public void finalizar() {
            horaFin = DateTime.Now; 
            Debug.WriteLine("Finalizado proceso" + programa.nombre);
            activa = false; 
        }
    }
}
