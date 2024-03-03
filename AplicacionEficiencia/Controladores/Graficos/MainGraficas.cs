using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionEficiencia.Modelos
{
    public class MainGraficas : INotifyPropertyChanged
    {
        private GraficoLineas graficoLineas;
        private GraficoRosquilla graficoRosquilla;
        private GraficoBarras graficoBarras;

        public GraficoBarras GraficoBarras
        {
            get { return graficoBarras; }
            set 
            { 
                graficoBarras = value;
                OnPropertyChanged(nameof(GraficoBarras));
            }
        }

        public GraficoLineas GraficoLineas
        {
            get { return graficoLineas; }
            set
            {
                graficoLineas = value;
                OnPropertyChanged(nameof(GraficoLineas));
            }
        }

        public GraficoRosquilla GraficoRosquilla
        {
            get { return graficoRosquilla; }
            set
            {
                graficoRosquilla = value;
                OnPropertyChanged(nameof(GraficoRosquilla));
            }
        }

        public MainGraficas()
        {
            GraficoBarras = new GraficoBarras();
            GraficoLineas = new GraficoLineas();
            GraficoRosquilla = new GraficoRosquilla();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
