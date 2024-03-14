using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AplicacionEficiencia.Dal;
using AplicacionEficiencia.Modelos;
using AplicacionEficiencia.Vistas;

namespace AplicacionEficiencia.Controladores
{
    internal sealed class StatsController
    {
        private readonly Estadistica _view;
        private Dictionary<DateTime, DateTime> activityPeriods = new Dictionary<DateTime, DateTime>();
        private int[] dailyUsageSegments = {0, 0, 0, 0, 0, 0, 0};

        public StatsController(Estadistica view) {
            this._view = view;
            FetchData();
            FilterData();
            UpdateProfileTime();
            LoadProfileChart();
        }

        private void LoadProfileChart() {
            var chart = new GraficoBarras(new string[] {"1", "2", "3", "4", "5", "6", "7"}, dailyUsageSegments);
            _view.profile_stats_chart.DataContext = chart;
        }

        private void UpdateProfileTime() {
            var time = GetAverageTime(activityPeriods);
            _view.txt_profile_time.Text = $"{time:f2} m";
        }

        private float GetAverageTime(Dictionary<DateTime, DateTime> period) {
            float time = 0;

            foreach(var kvp in period) 
            {
                time += CalculateTime(kvp.Key, kvp.Value);
            }
            return time / period.Count();
        }
 
        private float CalculateTime(DateTime start, DateTime end)
        {
            TimeSpan difference = end - start;
            return (float) difference.TotalMinutes;
        }

        private void FetchData() 
        {
            using (var conn = new ConexionContext())
            {
                activityPeriods = conn.Sesiones!
                    .Select(x => new {x.horaInicio, x.horaFin})
                    .ToDictionary(k => k.horaInicio, v => v.horaFin);
            }
        }

        private void FilterData() {
            var date6 = DateTime.Today;
            var date5 = DateTime.Today.AddDays(-1);
            var date4 = DateTime.Today.AddDays(-2);
            var date3 = DateTime.Today.AddDays(-3);
            var date2 = DateTime.Today.AddDays(-4);
            var date1 = DateTime.Today.AddDays(-5);
            var date0 = DateTime.Today.AddDays(-6);

            dailyUsageSegments[6] = (int) GetTime(activityPeriods.Where(kvp => CompareDates(kvp.Key, date6)).ToDictionary(k => k.Key, v => v.Value));
            dailyUsageSegments[5] = (int) GetTime(activityPeriods.Where(kvp => CompareDates(kvp.Key, date5)).ToDictionary(k => k.Key, v => v.Value));
            dailyUsageSegments[4] = (int) GetTime(activityPeriods.Where(kvp => CompareDates(kvp.Key, date4)).ToDictionary(k => k.Key, v => v.Value));
            dailyUsageSegments[3] = (int) GetTime(activityPeriods.Where(kvp => CompareDates(kvp.Key, date3)).ToDictionary(k => k.Key, v => v.Value));
            dailyUsageSegments[2] = (int) GetTime(activityPeriods.Where(kvp => CompareDates(kvp.Key, date2)).ToDictionary(k => k.Key, v => v.Value));
            dailyUsageSegments[1] = (int) GetTime(activityPeriods.Where(kvp => CompareDates(kvp.Key, date1)).ToDictionary(k => k.Key, v => v.Value));
            dailyUsageSegments[0] = (int) GetTime(activityPeriods.Where(kvp => CompareDates(kvp.Key, date0)).ToDictionary(k => k.Key, v => v.Value));
        }

        private bool CompareDates(DateTime dateA, DateTime dateB) {
            return dateA.Year == dateB.Year && dateA.Month == dateB.Month && dateA.Day == dateB.Day;
        }

        private bool IsTimeBetween(TimeSpan start, TimeSpan end, DateTime dateToCheck)
        {
            var dateSpan = dateToCheck.TimeOfDay;
            return start <= dateSpan && dateSpan <= end;
        }

        private float GetTime(Dictionary<DateTime, DateTime> period) {
            float time = 0;

            foreach(var kvp in period) 
            {
                time += CalculateTime(kvp.Key, kvp.Value);
            }
            return time;
        }
    }
}
