using AplicacionEficiencia.Modelos;
using AplicacionEficiencia.Vistas;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AplicacionEficiencia.Controladores
{

    internal class ListaAplicacionesModificarPerfil
    {
        public ModificarPerfil modificarPerfil;
        private HashSet<Programa> programasBlock = new HashSet<Programa>();
        private HashSet<Programa> programasAuto = new HashSet<Programa>();


        public ListaAplicacionesModificarPerfil(ModificarPerfil modificarPerfil)
        {
            this.modificarPerfil = modificarPerfil;
            modificarPerfil.btn_agregarapp.Click += Btn_agregarapp_Click;
            modificarPerfil.list_app_ejecutar.PreviewMouseRightButtonDown += List_app_ejecutar_PreviewMouseRightButtonDown;
            modificarPerfil.list_applicaciones_bloqueadas.PreviewMouseRightButtonDown += List_app_block_PreviewMouseRightButtonDown;
            mostrarListaAplicaciones();
        }

        public void mostrarListaAplicaciones() {

            StackPanel stackPanelPrincipal = new StackPanel();
            stackPanelPrincipal.Margin = new Thickness(10, 10, 10, 10);
            string textoMostrar = "";

            foreach (Programa programa in LectorProgramas.programas)
            {
                textoMostrar += programa.rutaIcono+ "\n";

                StackPanel stackPanel1 = new StackPanel();
                stackPanel1.Background = new SolidColorBrush(Color.FromRgb(20,20,20));
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

                Button btn_open = new Button();
                btn_open.Content = "Abrir";
                btn_open.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                {
                    string executablePath = programa.ruta;

                    try { Process.Start(executablePath); }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al ejecutar el programa: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
                stackPanel2.Children.Add(btn_open);

                Button btn_autoinicio = new Button();
                btn_autoinicio.Content = $"Inicio Automatico";
                btn_autoinicio.AddHandler(Button.ClickEvent, new RoutedEventHandler((_, e) =>
                {
                    if (!programasAuto.Contains(programa) && !programasBlock.Contains(programa)) { 
                        modificarPerfil.list_app_ejecutar.Items.Add(programa);
                        programasAuto.Add(programa);
                    }
                }));
                stackPanel2.Children.Add(btn_autoinicio);

                Button btn_bloquear = new Button();
                btn_bloquear.Content = "Bloquear Uso";
                btn_bloquear.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                {
                    if (!programasAuto.Contains(programa) && !programasBlock.Contains(programa))
                    {
                        modificarPerfil.list_applicaciones_bloqueadas.Items.Add(programa);
                        programasBlock.Add(programa);
                    }
                }));
                stackPanel2.Children.Add(btn_bloquear);
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

        private void List_app_ejecutar_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var programa = (Programa) modificarPerfil.list_app_ejecutar.SelectedItem;
            modificarPerfil.list_app_ejecutar.Items.Remove(programa);
            programasAuto.Remove(programa);
        }

        private void List_app_block_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var programa = (Programa)modificarPerfil.list_applicaciones_bloqueadas.SelectedItem;
            modificarPerfil.list_applicaciones_bloqueadas.Items.Remove(programa);
            programasBlock.Remove(programa);
        }
    }
}
