using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveChartsCore;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView.Extensions;

namespace AplicacionEficiencia.Modelos
{
    public partial class GraficoRosquilla : ObservableObject
    {
        public IEnumerable<ISeries> Series { get; set; } =
            new[] { 2, 4, 1, 4, 3 }.AsPieSeries((value, series) =>
            {
                series.MaxRadialColumnWidth = 60;
            });
    }
}
