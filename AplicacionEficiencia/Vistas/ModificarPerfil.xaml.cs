using AplicacionEficiencia.Controladores;
using AplicacionEficiencia.Modelos;
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

namespace AplicacionEficiencia.Vistas
{
    /// <summary>
    /// Lógica de interacción para ModificarPerfil.xaml
    /// </summary>
    public partial class ModificarPerfil : Page
    {
        public ModificarPerfil(Perfil perfil)
        {
            InitializeComponent();

            textBoxNombrePerfil.Text = perfil.nombre;
            ListaAplicacionesModificarPerfil LAMP = new ListaAplicacionesModificarPerfil(this);

        }
    }
}
