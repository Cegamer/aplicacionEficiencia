﻿using AplicacionEficiencia.Modelos;
using AplicacionEficiencia.Vistas;
using AplicacionEficiencia.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AplicacionEficiencia.Controladores
{
    public class PerfilesController
    {
        public readonly static Dictionary<int, Perfil> perfiles = new Dictionary<int, Perfil>();
        public readonly Perfiles view;
        public readonly ExpandableGrid manager;

        public PerfilesController(Perfiles view)
        {
            this.view = view;
            this.view.btn_new_profile.Click += Btn_new_profile_Click;   
            this.manager = new ExpandableGrid(ref view.profiles_grid, 3, 400);
            PerfilDePrueva();
            MostrarPerfiles();
        }

        private void PerfilDePrueva()
        {
            if (perfiles.Count > 0) return;
            var perfil1 = new Perfil(1, "Trabajo 1", "Para trabajar");
            var perfil2 = new Perfil(2, "Juego", "Para jugar GTA");

            perfil1.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[0]);
            perfil1.bloquearPrograma(LectorProgramas.GetProgramas()[8]);

            perfil2.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[0]);
            perfil2.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[1]);
            perfil2.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[2]);
            perfil2.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[3]);
            perfil2.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[4]);
            perfil2.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[5]);
            perfil2.bloquearPrograma(LectorProgramas.GetProgramas()[6]);

            PerfilesController.perfiles.Add(perfil1.id, perfil1);
            PerfilesController.perfiles.Add(perfil2.id, perfil2);
        }

        private void Btn_new_profile_Click(object sender, RoutedEventArgs e)
        {
            int id = PerfilesController.perfiles.Count + 1;
            PerfilesController.perfiles.Add(id, new Perfil(id, $"Nuevo Perfil {id}", ""));
            MostrarPerfiles();
        }

        public void MostrarPerfiles() 
        {
            manager.Reset();
            foreach (var perfil in PerfilesController.perfiles.Values)
            {
                var UICard = CreateProfileCard(perfil);
                manager.Insert(UICard);
            }
        }

        public UIElement CreateProfileCard(Perfil perfil)
        {
            var b = new Border() { 
                Background = new SolidColorBrush(Color.FromRgb(28, 28, 30)), 
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(5)
            };
            var grid = new Grid();
            var r1 = new RowDefinition() {Height = new GridLength(1, GridUnitType.Star)}; //Imagen
            var r2 = new RowDefinition() {Height = new GridLength(1, GridUnitType.Auto)}; //Nombre
            var r3 = new RowDefinition() {Height = new GridLength(1, GridUnitType.Auto) };//Descripcion
            var r4 = new RowDefinition() {Height = new GridLength(1, GridUnitType.Auto)}; //Boton Editar
            var r5 = new RowDefinition() {Height = new GridLength(1, GridUnitType.Auto)}; //Boton Iniciar
            grid.RowDefinitions.Add(r1);
            grid.RowDefinitions.Add(r2);
            grid.RowDefinitions.Add(r3);
            grid.RowDefinitions.Add(r4);
            grid.RowDefinitions.Add(r5);

            UIElement image = CreateProfileImage(perfil);
            grid.Children.Add(image);
            Grid.SetColumn(image, 0);
            Grid.SetRow(image, 0);

            Label nombre = new Label() { 
                Content = perfil.nombre,
                FontSize = 17,
                Foreground = new SolidColorBrush(Colors.White),
                Padding = new Thickness(5, 5, 5, 0),
                FontWeight = FontWeights.SemiBold
            };
            grid.Children.Add(nombre);
            Grid.SetRow(nombre, 1);

            Label info = new Label()
            {
                Content = perfil.descripcion,
                FontSize = 15,
                Foreground = new SolidColorBrush(Color.FromRgb(152, 152, 153)),
                Padding = new Thickness(5, 0, 5, 5),
            };
            grid.Children.Add(info);
            Grid.SetRow(info, 2);

            Button btn_edit = new Button { 
                Content = "Editar", 
                Height = 36,
                FontSize = 17,
                Style = Application.Current.Resources["MediumEnfasisCardButton"] as Style,
                Margin = new Thickness(0, 0, 0, 5)
            };
            btn_edit.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
            {
                LectorProgramas.MainWindow.frame.Content = new ModificarPerfil(perfil);
            }));
            grid.Children.Add(btn_edit);
            Grid.SetRow(btn_edit, 3);

            Button btn_start = new Button { 
                Content = "Iniciar", 
                Height = 36,
                FontSize = 17,
                Style = Application.Current.Resources["HighEnfasisButton"] as Style,
            };
            btn_start.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
            {
                if (SesionActual.sesionActual == null)
                {
                    perfil.iniciar();
                    MainWindow.mainWindow.frame.Content = new SesionActual(new Sesion(perfil));
                }
                else
                {
                    MessageBox.Show($"Ya hay un perfil activo, debe finalizar la sesión del perfil {SesionActual.sesionActual.Perfil.nombre} antes de iniciar otro perfil", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }));
            grid.Children.Add(btn_start);
            Grid.SetRow(btn_start, 4);
            b.Child = grid;

            return b;
        }

        //mejorar
        private UIElement CreateProfileImage(Perfil perfil)
        {
            Grid grid = new Grid() { Height = 240, Width = 240 };
            int columna = 0;
            int fila = 0;

            for (int i = 0; i < perfil.programasAEjecutar.Count; i++)
            {
                Image image = new Image();
                image.Name = "image" + perfil.programasAEjecutar[i].id;
                image.Height = 50;
                image.Width = 50;
                image.Source = perfil.programasAEjecutar[i].getIcon();
                grid.Children.Add(image);

                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Grid.SetColumn(image, columna);
                Grid.SetRow(image, fila);
                columna++;
                if (columna == 3)
                {
                    columna = 0; fila++;
                }
            }
            for (int i = 0; i <= fila; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            return grid;
        }
    }
}
