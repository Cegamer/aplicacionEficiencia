using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionEficiencia.Modelos
{
    internal class Programa
    {
        public string nombre;
        public string ruta;
        public string rutaIcono;
        public string nombreProceso;

        public Programa(string nombre, string ruta, string rutaIcono)
        {
            this.nombre = nombre;
            this.ruta = ruta;
            this.rutaIcono = rutaIcono;
        }
    }
}
