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
        public List<Programa> ProgramasBloqueados { get; private set; }
        public List<Programa> ProgramasAutostart  { get; private set; }

        public ListaAplicacionesModificarPerfil(ModificarPerfil modificarPerfil)
        {
            this.modificarPerfil = modificarPerfil;
            this.modificarPerfil.btn_agregarapp.Click += Btn_agregarapp_Click;
            this.ProgramasBloqueados = new List<Programa>();
            this.ProgramasAutostart = new List<Programa>();

            mostrarListaAplicaciones();
        }

        public void mostrarListaAplicaciones() {
            //Contenedor de la lista
            StackPanel stackPanelPrincipal = new StackPanel();
            int counter = 0;

            foreach (Programa programa in LectorProgramas.programas)
            {
                var brush = (counter % 2 == 0) ? new SolidColorBrush(Color.FromRgb(28, 28, 30)) : Brushes.Transparent;
                //
                Border border = new Border();
                StackPanel panel = new StackPanel();
                Grid grid = new Grid();
                Button btn_open = new Button();
                Button btn_autostart = new Button();
                Button btn_block = new Button();

                //
                grid.Margin = new Thickness(5, 0, 10, 5);
                border.Background = brush;
                border.Margin =  new Thickness(0, (counter == 0) ? 0 : 5, 0, 0);
                border.CornerRadius = new CornerRadius(4); 
                border.BorderThickness = new Thickness(1); 
                border.BorderBrush = new SolidColorBrush(Color.FromRgb(28, 28, 30)); 
                border.Child = grid;
                
                //
                ColumnDefinition image_container = new ColumnDefinition();
                ColumnDefinition space_5p = new ColumnDefinition();
                ColumnDefinition button_container = new ColumnDefinition();
                image_container.Width = new GridLength(25);
                space_5p.Width = new GridLength(5);
                button_container.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(image_container);
                grid.ColumnDefinitions.Add(space_5p);
                grid.ColumnDefinitions.Add(button_container);

                RowDefinition imagey_container = new RowDefinition();
                RowDefinition name_container = new RowDefinition();
                imagey_container.Height = new GridLength(1, GridUnitType.Auto);
                name_container.Height = new GridLength(1, GridUnitType.Auto);
                grid.RowDefinitions.Add(name_container);
                grid.RowDefinitions.Add(imagey_container);
                

                //
                Image icon = new Image();
                icon.Name = "image" + programa.id;
                icon.Height = 25;
                icon.Width = 25;
                icon.Source = programa.getIcon();
                grid.Children.Add(icon);
                Grid.SetColumn(icon, 0);
                Grid.SetRow(icon, 1);

                //
                Label label = new Label();
                label.Content = programa.nombre;
                label.FontSize = 15;
                label.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                label.Width = 150;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                grid.Children.Add(label);
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, 0);
                Grid.SetColumnSpan(label, 3);


                //
                btn_open.Content = "A";
                btn_open.Width = 25;
                btn_open.Height = 25;
                btn_open.FontSize = 13;
                btn_open.Background = Brushes.Transparent;
                btn_open.Foreground = new SolidColorBrush(Color.FromRgb(141, 141, 147));
                btn_open.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                {
                    string executablePath = programa.ruta;

                    try { Process.Start(executablePath); }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al ejecutar el programa: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
                panel.Children.Add(btn_open);

                btn_autostart.Content = "Inicio Automatico";
                btn_autostart.Width = 25;
                btn_autostart.Height = 25;
                btn_autostart.Background = Brushes.Transparent;
                btn_autostart.FontSize = 13;
                btn_autostart.Foreground = new SolidColorBrush(Color.FromRgb(141, 141, 147));
                btn_autostart.AddHandler(Button.ClickEvent, new RoutedEventHandler((_, e) =>
                {
                    var item = new ProgramaItem(programa, modificarPerfil, this);
                    if (!AplicacionEnListas(programa)) item.AddProgramToAutostartList();
                }));
                panel.Children.Add(btn_autostart);

                btn_block.Content = "Bloquear Uso";
                btn_block.Width = 25;
                btn_block.Height = 25;
                btn_block.FontSize = 13;
                btn_block.Background = Brushes.Transparent;
                btn_block.Foreground = new SolidColorBrush(Color.FromRgb(141, 141, 147));
                btn_block.AddHandler(Button.ClickEvent, new RoutedEventHandler((_, e) =>
                {
                    var item = new ProgramaItem(programa, modificarPerfil, this);
                    if (!AplicacionEnListas(programa)) item.AddProgramToBloquedList();
                }));
                panel.Children.Add(btn_block);
                panel.Orientation = Orientation.Horizontal;
                panel.HorizontalAlignment = HorizontalAlignment.Center;
                grid.Children.Add(panel);
                Grid.SetColumn(panel, 2);
                Grid.SetRow(panel, 1);

                stackPanelPrincipal.Children.Add(border);
                counter++;

                /*
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
                btn_autoinicio.Content = "Inicio Automatico";
                btn_autoinicio.AddHandler(Button.ClickEvent, new RoutedEventHandler((_, e) =>
                {
                    var item = new ProgramaItem(programa, modificarPerfil, this);
                    if (!AplicacionEnListas(programa)) item.AddProgramToAutostartList();
                }));
                stackPanel2.Children.Add(btn_autoinicio);

                Button btn_bloquear = new Button();
                btn_bloquear.Content = "Bloquear Uso";
                btn_bloquear.AddHandler(Button.ClickEvent, new RoutedEventHandler((_, e) =>
                {
                    var item = new ProgramaItem(programa, modificarPerfil, this);
                    if (!AplicacionEnListas(programa)) item.AddProgramToBloquedList();
                }));
                stackPanel2.Children.Add(btn_bloquear);

                stackPanelPrincipal.Children.Add(stackPanel2);
                */
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

        private bool AplicacionEnListas(Programa p) => (ProgramasAutostart.Contains(p) || ProgramasBloqueados.Contains(p));
    }
}
