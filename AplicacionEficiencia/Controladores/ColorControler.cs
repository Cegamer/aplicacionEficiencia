using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AplicacionEficiencia.Controladores
{
    public class ColorControler
    {
        public ColorControler() {

        }

        public static SKColor GetWindowGlassSKColor()
        {
            var color = SystemParameters.WindowGlassColor;
            return new SKColor(color.R, color.G, color.B, color.A);
        }
    }
}
