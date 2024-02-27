using AplicacionEficiencia.Controladores;
using AplicacionEficiencia.Vistas;
using System.Windows;
using System.Windows.Media;

namespace AplicacionEficiencia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Perfiles perfilesVista = new Perfiles();
        public static MainWindow mainWindow;
        public MainWindow()
        {

            InitializeComponent();
            mainWindow = this;
            /*Esto hay que arreglarlo, pero aun no, por ahora funciona */
            LectorProgramas.MainWindow = this;
            LectorProgramas.obtenerProgramasInstalados();
            PerfilesController perfiles = new PerfilesController(perfilesVista);
            frame.Content = perfilesVista;
            /* --------------------------------------------------------- */
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = perfilesVista;
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            var color = SystemParameters.WindowGlassColor;
            var lighter = Color.FromArgb(color.A, (byte)(color.R + 30), (byte)(color.G + 30), (byte)(color.B + 30));
            
            var bursh = new SolidColorBrush(lighter);
            Application.Current.Resources["HButtonLigther"] = bursh;
        }
    }
}
