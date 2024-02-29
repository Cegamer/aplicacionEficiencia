using System;
using System.Diagnostics;

namespace AplicacionEficiencia.Modelos
{
    public class SesionPrograma
    {
        public Sesion sesion { get; set; }
        public Programa programa { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFin { get; set; }
        public TimeSpan tiempoTranscurrido { get; set; }

        public bool activa;

        public SesionPrograma(Sesion sesion, Programa programa, DateTime horaInicio)
        {
            this.sesion = sesion;
            this.programa = programa;
            this.horaInicio = horaInicio;
            this.activa = true;
        }

        public TimeSpan calcularTiempoTranscurrido(DateTime fin)
        { 
            if (!activa)
                tiempoTranscurrido =  horaFin - horaInicio;
            else
                tiempoTranscurrido = fin - horaInicio;

            return tiempoTranscurrido;
        }
        public void finalizar()
        {
            horaFin = DateTime.Now;
            Debug.WriteLine("Finalizado proceso" + programa.nombre);
            activa = false;
            ///Aquí hay que enviar los datos de la sesión a la database
        }

    }
}
