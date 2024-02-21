using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace AplicacionEficiencia.Modelos
{
    public class Programa
    {
        public int id { get; }
        public string nombre { get; set; }
        public string ruta { get; set; }
        public string rutaIcono { get; set; }
        public string nombreProceso { get; set; }


        public Programa(int id,string nombre, string ruta)
        {
            this.id = id;
            this.nombre = nombre;
            this.ruta = ruta;
        }
        public Process iniciarPrograma() {
            var proceso =  Process.Start(ruta);
            nombreProceso = proceso.ProcessName;
            Debug.WriteLine(nombreProceso);
            return proceso;
        }
        public BitmapSource getIcon() {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(ruta);
                Bitmap bitmap = icon.ToBitmap();

                BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                    bitmap.GetHicon(),
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

                return bitmapSource;
            }
            catch { return null; }
        }
    }
}
