using AplicacionEficiencia.Modelos;
using AplicacionEficiencia.utils;
using AplicacionEficiencia.Vistas;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AplicacionEficiencia.Controladores
{
    internal class PerfilesController
    {
        public Perfiles perfiles;
        public List<Perfil> perfilesLista = new List<Perfil>();
        public Perfil prueba1 = new Perfil(0, "Juego", "aaaaaaa");
        public Perfil prueba2 = new Perfil(1, "Trabajo", "aaaaaaa");
        public Perfil prueba3 = new Perfil(2, "Es", "aaaaaaa");
        public Perfil prueba4 = new Perfil(3, "Via", "aaaaaaa");


        public PerfilesController(Perfiles perfiles)
        {
            this.perfiles = perfiles;
            GridManager manager = new GridManager(ref perfiles.profiles_grid, 3, 400);

            PerfilDePrueva();
            foreach (var perfil in perfilesLista)
            {
                manager.Insert(CreateProfileCard(perfil));
            }
        }

        public UIElement CreateProfileCard(Perfil perfil)
        {
            Border b = new Border() { Background = Brushes.Gray };
            Grid grid = new Grid();
            RowDefinition r1 = new RowDefinition();
            RowDefinition r2 = new RowDefinition();
            RowDefinition r3 = new RowDefinition();
            RowDefinition r4 = new RowDefinition();
            r1.Height = new GridLength(1, GridUnitType.Star); //Imagen
            r2.Height = new GridLength(1, GridUnitType.Auto); //Nombre
            r3.Height = new GridLength(1, GridUnitType.Auto); //Boton Editar
            r4.Height = new GridLength(1, GridUnitType.Auto); //Boton Iniciar
            grid.RowDefinitions.Add(r1);
            grid.RowDefinitions.Add(r2);
            grid.RowDefinitions.Add(r3);
            grid.RowDefinitions.Add(r4);

            UIElement image = CreateProfileImage(perfil);
            grid.Children.Add(image);
            Grid.SetColumn(image, 0);
            Grid.SetRow(image, 0);

            Label nombre = new Label() { Content = perfil.nombre };
            grid.Children.Add(nombre);
            Grid.SetRow(nombre, 1);

            Button btn_edit = new Button { Content = "Editar", Height = 36 };
            btn_edit.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
            {
                LectorProgramas.MainWindow.frame.Content = new ModificarPerfil(perfil);
            }));
            grid.Children.Add(btn_edit);
            Grid.SetRow(btn_edit, 2);

            Button btn_start = new Button { Content = "Iniciar", Height = 36 };
            btn_start.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
            {
                perfil.iniciar();
                MainWindow.mainWindow.frame.Content = new SesionActual(new Sesion(perfil));
            }));
            grid.Children.Add(btn_start);
            Grid.SetRow(btn_start, 3);
            b.Child = grid;
            return b;
        }

        private UIElement CreateProfileImage(Perfil perfil)
        {
            Grid grid = new Grid() {Height = 240, Width = 240};
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

        private void PerfilDePrueva()
        {
            perfilesLista.Add(prueba1);
            prueba1.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[0]);
            prueba1.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[1]);
            prueba1.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[2]);
            prueba1.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[3]);
            prueba1.bloquearPrograma(LectorProgramas.GetProgramas()[4]);

            perfilesLista.Add(prueba2);
            prueba2.bloquearPrograma(LectorProgramas.GetProgramas()[0]);
            prueba2.bloquearPrograma(LectorProgramas.GetProgramas()[1]);
            prueba2.bloquearPrograma(LectorProgramas.GetProgramas()[2]);
            prueba2.bloquearPrograma(LectorProgramas.GetProgramas()[3]);
            prueba2.bloquearPrograma(LectorProgramas.GetProgramas()[4]);

            perfilesLista.Add(prueba3);
            perfilesLista.Add(prueba4);
        }

        /*
        public void mostrarPerfiles()
        {
            StackPanel stackPanelPrincipal = new StackPanel();
            stackPanelPrincipal.Margin = new Thickness(10, 10, 10, 10);
            stackPanelPrincipal.Width = 240;
            foreach (Perfil perfil in perfilesLista)
            {

                Label label = new Label();
                label.Name = "label" + perfil.nombre;
                label.Content = perfil.nombre;
                stackPanelPrincipal.Children.Add(label);

                Grid grid = new Grid();
                grid.Height = 240;
                grid.Width = 240;
                stackPanelPrincipal.Children.Add(grid);

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

                // Botón Editar
                Button buttonEditar = new Button();
                buttonEditar.Name = "botonEditar" + perfil.id;
                buttonEditar.Content = "Editar";
                buttonEditar.Width = 240;

                buttonEditar.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                {
                    LectorProgramas.MainWindow.frame.Content = new ModificarPerfil(perfil);
                }));

                stackPanelPrincipal.Children.Add(buttonEditar);

                // Botón Iniciar
                Button button = new Button();
                button.Name = "button" + perfil.id;
                button.Content = "Iniciar";
                button.Width = 240;

                button.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, e) =>
                {
                    perfil.iniciar();
                    MainWindow.mainWindow.frame.Content = new SesionActual(new Sesion(perfil));
                }));

                stackPanelPrincipal.Children.Add(button);

                // Agregar el stackPanelPrincipal a la ventana o al contenedor deseado
                perfiles.gridPerfiles.Children.Add(stackPanelPrincipal);
            }
        }
        */
    }
}
