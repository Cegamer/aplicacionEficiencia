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
        public ModificarPerfil modificarPerfil = new ModificarPerfil();
        public MainWindow()
        {
            InitializeComponent();
            LectorProgramas lectorProcesos = new LectorProgramas();
            lectorProcesos.MainWindow = this;
            lectorProcesos.obtenerProgramasInstalados(); 
            ListaAplicacionesModificarPerfil LAMP = new ListaAplicacionesModificarPerfil(modificarPerfil,lectorProcesos);
        }

  

        private void button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = modificarPerfil;
        }
    }
}
