using AplicacionEficiencia.Modelos;
using AplicacionEficiencia.Vistas;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AplicacionEficiencia.Controladores
{

    internal class ListaAplicacionesModificarPerfil
    {
        public ModificarPerfil modificarPerfil;

        public ListaAplicacionesModificarPerfil(ModificarPerfil modificarPerfil)
        {
            this.modificarPerfil = modificarPerfil;
            modificarPerfil.btn_agregarapp.Click += Btn_agregarapp_Click;
            mostrarListaAplicaciones();
        }

        public void mostrarListaAplicaciones() {

            StackPanel stackPanelPrincipal = new StackPanel();
            stackPanelPrincipal.Margin = new Thickness(10, 10, 144, 276);
            string textoMostrar = "";

            foreach (Programa programa in LectorProgramas.programas)
            {
                textoMostrar += programa.rutaIcono+ "\n";
                StackPanel stackPanel1 = new StackPanel();
                stackPanel1.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 180, 0));
                stackPanel1.Height = 100;

                Image image = new Image();
                image.Name = "image" + programa.id;
                image.Height = 75;
                image.Width = 75;
                image.Source = programa.getIcon();
                stackPanel1.Children.Add(image);

                Label label = new Label();
                label.Name = "label" + programa.id;
                label.Content = programa.nombre;
                stackPanel1.Children.Add(label);

                stackPanelPrincipal.Children.Add(stackPanel1);

                StackPanel stackPanel2 = new StackPanel();
                stackPanel2.Height = 63;


                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button();

                    button.Name = "button" + programa.id +j.ToString();
                    button.Content = "Button " + j.ToString();
                    stackPanel2.Children.Add(button);
                    button.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                    {
                        string executablePath = programa.ruta;

                        try { Process.Start(executablePath); }
                        catch (Exception ex) {
                            MessageBox.Show($"Error al ejecutar el programa: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }));
                }

                stackPanelPrincipal.Children.Add(stackPanel2);
            }

            modificarPerfil.panelAplicaciones.Children.Add(stackPanelPrincipal);
        }

        private void Btn_agregarapp_Click(object sender, RoutedEventArgs e)
        {
            var fileChoser = new OpenFileDialog();
            fileChoser.DefaultExt = ".exe";
            fileChoser.Filter = "Executable Files (*.exe)|*.exe";
            fileChoser.InitialDirectory = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs";

            if (fileChoser.ShowDialog() ?? false)
            {
                string path = fileChoser.FileName;
                string name = System.IO.Path.GetFileName(path);
                var app = new Programa(999, name, path); //Ingresar ID autogenerado por la base de datos

                LectorProgramas.programas.Add(app);
                modificarPerfil.panelAplicaciones.Children.Clear();
                mostrarListaAplicaciones();
            }
        }

    }
}
