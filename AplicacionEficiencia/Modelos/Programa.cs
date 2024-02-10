using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AplicacionEficiencia.Modelos
{
    internal class Programa
    {
        public int id;
        public string nombre;
        public string ruta;
        public string rutaIcono;
        public string nombreProceso;

        public Programa(int id,string nombre, string ruta)
        {
            this.id = id;
            this.nombre = nombre;
            this.ruta = ruta;
        }

        public BitmapSource getIcon() {
            Icon icon = Icon.ExtractAssociatedIcon(ruta);
            Bitmap bitmap = icon.ToBitmap();

            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                bitmap.GetHicon(),
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return bitmapSource;
        }
    }
}
