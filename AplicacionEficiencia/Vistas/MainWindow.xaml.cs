using AplicacionEficiencia.Controladores;
using AplicacionEficiencia.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AplicacionEficiencia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Perfiles perfilesVista = new Perfiles();
        public MainWindow()
        {

            InitializeComponent();

            /*Esto hay que arreglarlo, pero aun no, por ahora funciona */
            LectorProgramas.MainWindow = this;
            LectorProgramas.obtenerProgramasInstalados();
            PerfilesController perfiles = new PerfilesController(perfilesVista);
            frame.Content = perfilesVista;
            /* --------------------------------------------------------- */
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //frame.Content = perfilesVista;
        }
    }
}
