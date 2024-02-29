using AplicacionEficiencia.Modelos;
using System.Windows.Controls;

namespace AplicacionEficiencia.Vistas
{
    /// <summary>
    /// Lógica de interacción para SesionActual.xaml
    /// </summary>
    public partial class SesionActual : Page
    {
        public static SesionActual sesionActualVista;
        public static Sesion sesionActual;
        public SesionActual(Sesion sesion)

        {
            InitializeComponent();
            sesionActualVista = this;
            sesion.IniciarMonitoreo();
            sesionActual = sesion;
        }

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            sesionActual.Finalizar();
            MainWindow.mainWindow.frame.Content = MainWindow.perfilesVista;
        }
    }
}
