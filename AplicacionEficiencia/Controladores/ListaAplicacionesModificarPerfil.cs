using AplicacionEficiencia.Modelos;
using AplicacionEficiencia.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AplicacionEficiencia.Controladores
{

    internal class ListaAplicacionesModificarPerfil
    {
        public ModificarPerfil modificarPerfil;
        public LectorProgramas lectorProgramas;

        public ListaAplicacionesModificarPerfil(ModificarPerfil modificarPerfil, LectorProgramas lectorProgramas)
        {
            this.modificarPerfil = modificarPerfil;
            this.lectorProgramas = lectorProgramas;
        }

        public void mostrarListaAplicaciones() {

            StackPanel stackPanelPrincipal = new StackPanel();
            stackPanelPrincipal.Margin = new Thickness(10, 10, 144, 276);

            foreach (Programa programa in lectorProgramas.programas)
            {
                StackPanel stackPanel1 = new StackPanel();
                stackPanel1.Height = 100;

                Image image = new Image();
                image.Name = "image" + programa.nombre;
                image.Height = 75;
                image.Width = 75;
                image.Source = new BitmapImage(new Uri(programa.rutaIcono, UriKind.Relative));
                stackPanel1.Children.Add(image);

                Label label = new Label();
                label.Name = "label" + programa.nombre;
                label.Content = programa.nombre;
                stackPanel1.Children.Add(label);

                stackPanelPrincipal.Children.Add(stackPanel1);

                StackPanel stackPanel2 = new StackPanel();
                stackPanel2.Height = 63;


                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button();

                    button.Name = "button" + programa.nombre +j.ToString();
                    button.Content = "Button " + j.ToString();
                    stackPanel2.Children.Add(button);
                }

                stackPanelPrincipal.Children.Add(stackPanel2);
            }

            modificarPerfil.panelAplicaciones.Children.Add(stackPanelPrincipal);

        }
    }
}
