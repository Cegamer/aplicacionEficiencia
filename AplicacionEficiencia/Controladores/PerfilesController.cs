using AplicacionEficiencia.Modelos;
using AplicacionEficiencia.Vistas;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AplicacionEficiencia.Controladores
{
    internal class PerfilesController
    {
        public Perfiles perfiles;
        public Perfil prueba = new Perfil(0, "aaaa", "aaaaaaa");
        List<Perfil> perfilesLista = new List<Perfil>();

        public PerfilesController(Perfiles perfiles)
        {

            this.perfiles = perfiles;
            perfilesLista.Add(prueba);
            prueba.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[8]);
            prueba.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[0]);
            prueba.bloquearPrograma(LectorProgramas.GetProgramas()[4]);

            Perfil prueba2 = new Perfil(1, "prueba2", "aaaaaaaa");
            prueba2.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[8]);
            prueba2.agregarProgramaEjecutar(LectorProgramas.GetProgramas()[0]);
            perfilesLista.Add(prueba2);
            mostrarPerfiles();
        }

        //Corrgir
        public void mostrarPerfiles()
        {
            // Crear un solo StackPanel para todos los perfiles
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

                stackPanelPrincipal.Children.Add(button);
            }

            // Agregar el stackPanelPrincipal con todos los perfiles al contenedor deseado
            perfiles.gridPerfiles.Children.Add(stackPanelPrincipal);
        }

    }
}
