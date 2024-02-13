using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;

namespace AplicacionEficiencia.Modelos
{
    public class Perfil
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public List<Programa> programasAEjecutar { get; set; }
        public List<Programa> programasBloqueados { get; set; }

        public Perfil(int id, string nombre, string descripcion)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            programasAEjecutar = new List<Programa>();
            programasBloqueados = new List<Programa>();
        }

        public void agregarProgramaEjecutar(Programa programa) { programasAEjecutar.Add(programa); }
        public void bloquearPrograma(Programa programa) { programasBloqueados.Add(programa); }
        public void iniciar() {
            foreach(Programa programa in programasAEjecutar) { 
                try { Process.Start(programa.ruta); }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al ejecutar el programa: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
