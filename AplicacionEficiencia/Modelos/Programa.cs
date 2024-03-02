using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace AplicacionEficiencia.Modelos
{
    public class Programa
    {
        public int id { get; }
        public string nombre { get; set; }
        public string ruta { get; set; }
        public string rutaIcono { get; set; }
        public string nombreProceso { get; set; }


        public Programa(int id, string nombre, string ruta)
        {
            this.id = id;
            this.nombre = nombre;
            this.ruta = ruta;
            this.nombreProceso = obtenerNombreProceso();
        }
        public Process iniciarPrograma()
        {
            var proceso = Process.Start(ruta);
            nombreProceso = proceso.ProcessName;
            Debug.WriteLine(nombreProceso);
            return proceso;
        }
        private string obtenerNombreProceso() {
            ProcessStartInfo startInfo = new ProcessStartInfo(ruta);

            try
            {
                string nombreArchivo = System.IO.Path.GetFileNameWithoutExtension(ruta);
                startInfo.FileName = nombreArchivo;

                using (Process proceso = new Process())
                {
                    proceso.StartInfo = startInfo;
                    return proceso.StartInfo.FileName;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener el nombre del proceso: {ex.Message}");
                return null;
            }
        }
        public BitmapSource getIcon()
        {
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
