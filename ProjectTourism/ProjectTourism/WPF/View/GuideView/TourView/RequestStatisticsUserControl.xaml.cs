using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.WPF.ViewModel;
using static ProjectTourism.WPF.View.GuideView.TourView.RequestStatisticsUserControl;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class RequestStatisticsUserControl : UserControl
    {
        public struct Stat
        {
            public int Year { get; set; }
            public int StatsForThatYear { get; set; }
        }
        public TourRequestVM TourRequest { get; set; }
        public ObservableCollection<Stat> Stats { get; set; }
        public ObservableCollection<int> MonthlyStats { get; set; }
        public Stat SelectedStat { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public string SelectedLanguage { get; set; }
        public ObservableCollection<string> Locations { get; set; }
        public string SelectedLocation { get; set; }
        public bool WasLanguageChosen { get; set; }
        public bool WasLocationChosen { get; set; }

        public RequestStatisticsUserControl()
        {
            InitializeComponent();
            DataContext = this;

            SetAttributes();
            CalculateYearStats();
            SetLanguagesAndLocations();
        }
        private void SetAttributes()
        {
            TourRequest = new TourRequestVM();
            Stats = new ObservableCollection<Stat>();
            Languages = new ObservableCollection<string>();
            Locations = new ObservableCollection<string>();
            WasLocationChosen = false;
            WasLocationChosen = false;
        }
        private void SetLanguagesAndLocations()
        {
            TourRequest.GetAllLanguages().ToList().ForEach(language => Languages.Add(language));
            TourRequest.GetAllLocations().ToList().ForEach(location => Locations.Add(location));
        }
        private void LanguagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStats();
        }
        private void LocationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStats();
        }
        private void UpdateStats()
        {
            StatsCanvas.Children.Clear();

            if (SelectedLanguage != null || SelectedLocation != null)
            {
                WasLanguageChosen = SelectedLanguage != null;
                WasLocationChosen = SelectedLocation != null;

                CalculateStats();
            }
        }

        private void CalculateStats()
        {
            Stats.Clear();
            foreach (var year in TourRequest.Years())
            {
                Stat stat = new Stat();
                stat.Year = year;

                if (WasLanguageChosen && !WasLocationChosen)
                    stat.StatsForThatYear = TourRequest.StatForYearLanguageFiltered(year, SelectedLanguage);
                else if (!WasLanguageChosen && WasLocationChosen)
                    stat.StatsForThatYear = TourRequest.StatForYearLocationFiltered(year, SelectedLocation);
                else
                    stat.StatsForThatYear = TourRequest.StatForYearLanguageAndLocationFiltered(year, SelectedLanguage, SelectedLocation);

                Stats.Add(stat);
            }
        }
        private void CalculateYearStats()
        {
            foreach (var year in TourRequest.Years())
            {
                Stat stat = new Stat();
                stat.Year = year;
                stat.StatsForThatYear = TourRequest.StatForYear(year);
                Stats.Add(stat);
            }
        }
        private void DataGridStats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatsCanvas.Children.Clear();
            MonthlyStats = new ObservableCollection<int>();
            List<int> CalculatedList = new List<int>();
            if (WasLanguageChosen && !WasLocationChosen)
                CalculatedList = TourRequest.MonthlyStatsForLanguage(SelectedStat.Year, SelectedLanguage);
            else if (WasLocationChosen && !WasLanguageChosen)
                CalculatedList = TourRequest.MonthlyStatsForLocation(SelectedStat.Year, SelectedLocation);
            else if(WasLanguageChosen && WasLocationChosen)
                CalculatedList = TourRequest.MonthlyStatsForLanguageAndLocation(SelectedStat.Year, SelectedLanguage, SelectedLocation);
            else
                CalculatedList = TourRequest.MonthlyStats(SelectedStat.Year);

            CalculatedList.ToList().ForEach(stat => MonthlyStats.Add(stat));

            if (!MonthlyStats.All(item => item == 0))
                DrawBlockChart(MonthlyStats);
        }
        private void DrawBlockChart(ObservableCollection<int> monthlyStats)
        {
            StatsCanvas.Children.Clear();

            int chartWidth = 300;
            int chartHeight = 120;
            int barWidth = chartWidth / monthlyStats.Count;
            int maxValue = monthlyStats.Max();
            double scale = (double)chartHeight / maxValue;

            DrawBars(monthlyStats, barWidth, chartHeight, scale);
            DrawLabels(monthlyStats, barWidth, chartHeight);
            DrawAxes(chartHeight, chartWidth);
        }
        private void DrawBars(ObservableCollection<int> monthlyStats, int barWidth, int chartHeight, double scale)
        {
            for (int i = 0; i < monthlyStats.Count; i++)
            {
                Rectangle bar = new Rectangle();
                bar.Width = barWidth - 2;
                bar.Height = monthlyStats[i] * scale;
                bar.Fill = Brushes.Blue;
                bar.Stroke = Brushes.Black;
                bar.StrokeThickness = 1;
                Canvas.SetLeft(bar, i * barWidth + 1);
                Canvas.SetTop(bar, chartHeight - bar.Height + 20);
                StatsCanvas.Children.Add(bar);

                TextBlock label = new TextBlock();
                label.Text = monthlyStats[i].ToString();
                if (label.Text.Equals("0"))
                    label.Text = "";
                label.TextAlignment = TextAlignment.Center;
                label.Width = barWidth;
                Canvas.SetLeft(label, i * barWidth);
                Canvas.SetTop(label, chartHeight - bar.Height + 5);
                StatsCanvas.Children.Add(label);
            }
        }
        private void DrawLabels(ObservableCollection<int> monthlyStats, int barWidth, int chartHeight)
        {
            for (int i = 0; i < monthlyStats.Count; i++)
            {
                TextBlock label = new TextBlock();
                string[] labels = { "1.", "2.", "3.", "4.", "5.", "6.", "7.", "8.", "9.", "10.", "11.", "12." };
                if (i >= 0 && i < labels.Length)
                {
                    label.Text = labels[i];
                }

                label.TextAlignment = TextAlignment.Center;
                label.Width = barWidth;
                Canvas.SetLeft(label, i * barWidth);
                Canvas.SetTop(label, chartHeight + 25);
                StatsCanvas.Children.Add(label);
            }
        }
        private void DrawAxes(int chartHeight, int chartWidth)
        {
            Line xAxis = new Line();
            xAxis.X1 = 0;
            xAxis.X2 = chartWidth;
            xAxis.Y1 = chartHeight + 20;
            xAxis.Y2 = chartHeight + 20;
            xAxis.Stroke = Brushes.Black;
            xAxis.StrokeThickness = 1;
            StatsCanvas.Children.Add(xAxis);

            Line yAxis = new Line();
            yAxis.X1 = 0;
            yAxis.X2 = 0;
            yAxis.Y1 = 20;
            yAxis.Y2 = chartHeight + 20;
            yAxis.Stroke = Brushes.Black;
            yAxis.StrokeThickness = 1;
            StatsCanvas.Children.Add(yAxis);
        }
    }
}
