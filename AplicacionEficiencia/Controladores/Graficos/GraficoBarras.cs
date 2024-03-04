using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionEficiencia.Modelos
{
    public partial class GraficoBarras : ObservableObject
    {
        public ISeries[] Series { get; set; } =
        {
            new ColumnSeries<int>
            {
                Values = new int[] {1,4, 3, 5}
            }
        };

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                Labels = new string[] {"App1", "App2", "App3", "App4"},
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200,200,200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35,35,35)),
                TicksAtCenter = true,
                ForceStepToMin = true,
                MinStep = 1
            }
        };
    }
}
