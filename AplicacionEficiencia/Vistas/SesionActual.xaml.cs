using AplicacionEficiencia.Modelos;
using System.Windows.Controls;

namespace AplicacionEficiencia.Vistas
{
    /// <summary>
    /// Lógica de interacción para SesionActual.xaml
    /// </summary>
    public partial class SesionActual : Page
    {
        public static SesionActual sesionActual;
        public SesionActual(Sesion sesion)

        {
            InitializeComponent();
            sesionActual = this;
            sesion.IniciarMonitoreo();
        }
    }
}
