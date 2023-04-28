using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class RequestStatisticsUserControl : UserControl
    {
        public struct Stat
        {
            public int Year { get; set; }
            public int StatsForThatYear { get; set; }
        }
        public RequestVM Request { get; set; }
        public List<int> nizovi { get; set; } 
        public ObservableCollection<Stat> Stats { get; set; }
        public ObservableCollection<int> MonthlyStats { get; set; }
        public Stat SelectedStat { get; set; }

        public RequestStatisticsUserControl()
        {
            InitializeComponent();
            DataContext = this;

            Request = new RequestVM();
            Stats = new ObservableCollection<Stat>();
            CalculateYearStats();
        }

        private void CalculateYearStats()
        {
            foreach (var year in Request.Years())
            {
                Stat stat = new Stat();
                stat.Year = year;
                stat.StatsForThatYear = Request.StatForYear(year);
                Stats.Add(stat);
            }
        }
        private void DataGridStats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MonthlyStats = new ObservableCollection<int>();
            foreach (var stat in Request.MonthlyStats(SelectedStat.Year))
            {
                MonthlyStats.Add(stat);
            }
            DrawBlockChart(MonthlyStats);
        }
        private void DrawBlockChart(ObservableCollection<int> monthlyStats)
        {
            AgeGropsCanvas.Children.Clear();

            int chartWidth = 200;
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
                AgeGropsCanvas.Children.Add(bar);

                TextBlock label = new TextBlock();
                label.Text = monthlyStats[i].ToString();
                label.TextAlignment = TextAlignment.Center;
                label.Width = barWidth;
                Canvas.SetLeft(label, i * barWidth);
                Canvas.SetTop(label, chartHeight - bar.Height + 5);
                AgeGropsCanvas.Children.Add(label);
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
                AgeGropsCanvas.Children.Add(label);
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
            AgeGropsCanvas.Children.Add(xAxis);

            Line yAxis = new Line();
            yAxis.X1 = 0;
            yAxis.X2 = 0;
            yAxis.Y1 = 20;
            yAxis.Y2 = chartHeight + 20;
            yAxis.Stroke = Brushes.Black;
            yAxis.StrokeThickness = 1;
            AgeGropsCanvas.Children.Add(yAxis);
        }
    }
}
