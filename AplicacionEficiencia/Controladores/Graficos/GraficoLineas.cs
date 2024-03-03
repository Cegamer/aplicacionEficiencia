using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace AplicacionEficiencia.Modelos
{
    public class GraficoLineas
    {
        public ISeries[] Series { get; set; }
        = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = new double[] {2, 1, 3, 5, 4, 5}
            }
        };

        public Axis[] XAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                Name = "Aplicación",
                NameTextSize = 15,
                Labels = new String[] {"VS", "NeatBeans", "LoL", "VSC"},
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.White),
                TextSize = 10
                
            }
        };



        public Axis[] YAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                Name = "Numero de Horas",
                NameTextSize = 13,
                NamePaint = new SolidColorPaint(SKColors.White),
                LabelsPaint = new SolidColorPaint(SKColors.White),
                TextSize = 10
            }
        };
    }
}
