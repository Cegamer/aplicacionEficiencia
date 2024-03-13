using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.ConditionalDraw;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using SkiaSharp;

namespace AplicacionEficiencia.Controladores.Graficos
{
    public class PilotoInfo : ObservableValue
    {
        public string Nombre { get; set; }
        public SolidColorPaint Paint { get; set; }
        public PilotoInfo(String nombre, int value, SolidColorPaint paint) 
        {
            Nombre = nombre;
            Paint = paint;
            Value = value;
        }

    }
    public partial class GraficoLista : ObservableObject
    {
        
        private readonly Random _r = new();
        private readonly PilotoInfo[] _data;
        
        public GraficoLista()
        {
            var paints = Enumerable.Range(0, 7)
                .Select(i => new SolidColorPaint(ColorPalletes.MaterialDesign500[i].AsSKColor()))
                .ToArray();

            _data = new PilotoInfo[]
            {
                new("Tsunoda",   500,  paints[0]),
                new("Sainz",     450,  paints[1]),
                new("Riccardo",  520,  paints[2]),
                new("Bottas",    550,  paints[3]),
                new("Perez",     660,  paints[4]),
                new("Verstapen", 920,  paints[5]),
                new("Hamilton",  1000, paints[6])
            };

            var rowSeries = new RowSeries<PilotoInfo>
            {
                Values = _data.OrderBy(x => x.Value).ToArray(),
                DataLabelsPaint = new SolidColorPaint(new SKColor(245, 245, 245)),
                DataLabelsPosition = DataLabelsPosition.End,
                DataLabelsTranslate = new(-1, 0),
                DataLabelsFormatter = point => $"{point.Model!.Nombre} {point.Coordinate.PrimaryValue}",
                MaxBarWidth = 50,
                Padding = 10,
            }
            .OnPointMeasured(point =>
            {
                if (point.Visual is null) return;
                point.Visual.Fill = point.Model!.Paint;
            });

            _series = new[] { rowSeries };

            _ = StartRace();
        }

        [ObservableProperty]
        private ISeries[] _series;

        [ObservableProperty]
        private Axis[] _xAxes = { new Axis { SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 220)) } };

        [ObservableProperty]
        private Axis[] _yAxes = { new Axis { IsVisible = false } };

        public bool IsReading { get; set; } = true;

        public async Task StartRace()
        {
            await Task.Delay(1000);


            while (IsReading)
            {

                foreach (var item in _data)
                    item.Value += _r.Next(0, 100);

                Series[0].Values =
                    _data.OrderBy(x => x.Value).ToArray();

                await Task.Delay(100);
            }
        }
        
    }
}
