using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class TourStatisticsUserControl : UserControl
    {
        public GuideDTO Guide { get; set; }
        public List<TourAppointmentDTO> TourApps { get; set; }
        public TourAppointmentDTO SelectedTourApp { get; set; }
        public ObservableCollection<TourAppointmentDTO> TourAppsObs { get; set; }
        public ObservableCollection<double> TourAppPercentage { get; set; }
        public int SelectedYear { get; set; }
        public List<int> Years { get; set; }
        public ObservableCollection<int> AgeGroups { get; set; }
        public TourStatisticsUserControl(string username)
        {
            InitializeComponent();
            DataContext = this;

            SetModels(username);
            TourApps.Sort((t1, t2) => t2.Visits.CompareTo(t1.Visits));
            SetComboBox();

            StatsSadImage.Visibility = Visibility.Hidden;
            SadLabel.Visibility = Visibility.Hidden;

            HideInstructions();
            Update();
        }
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ImageViewerUserControl(SelectedTourApp.Tour);
        }
        private void DataGridTours_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            if (row != null)
            {
                UnhideStatsInstructions();
                var item = row.Item as TourAppointmentDTO;
                FieldSet.Header = item.Tour.Name;
                CalculateTicketPercentage(item);
                CalculateAgeStats(item);
                AgeGropsCanvas.Children.Clear();
                DrawTourAppPercentageStats(TourAppPercentage[0] / 100, TourAppPercentage[1] / 100, AgeGroups);
            }
        }
        private void UnhideStatsInstructions()
        {
            List<UIElement> elementsToHide = new List<UIElement> { Label1, StatsImage };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);

            List<UIElement> elementsToUnhide = new List<UIElement> { Label3, Label4, Label5, Rectangle1, Rectangle2 };
            elementsToUnhide.ForEach(element => element.Visibility = Visibility.Visible);
        }
        private void HideInstructions()
        {
            List<UIElement> elementsToHide = new List<UIElement> { Label3, Label4, Label5, Rectangle1, Rectangle2 };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }

        private void SetModels(string username)
        {
            Guide = new GuideDTO(username);
            TourAppsObs = new ObservableCollection<TourAppointmentDTO>();
            TourAppPercentage = new ObservableCollection<double>();
            TourApps = new List<TourAppointmentDTO>(Guide.TourAppointments);
            TourAppsObs = new ObservableCollection<TourAppointmentDTO>(TourApps);
            Years = new List<int>(GetYears());
            AgeGroups = new ObservableCollection<int>();
        }
        private void SetComboBox()
        {
            comboboxYears.ItemsSource = Years;
            comboboxYears.SelectedIndex = Years.IndexOf(DateTime.Now.Year);
        }
        private void YearsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedYear = (int)comboboxYears.SelectedItem;

            TourApps.Clear();
            Guide.TourAppointments.Where(tourApp => tourApp.TourDateTime.Year == SelectedYear && tourApp.State.Equals(TOURSTATE.FINISHED))
                                  .ToList().ForEach(tourApp => TourApps.Add(tourApp));
            TourApps.Sort((t1, t2) => t2.Visits.CompareTo(t1.Visits)); 
            Update();
        }
        private List<int> GetYears()
        {
            List<int> years = new List<int>();
            Update();
            TourAppsObs.ToList().ForEach(tourApp => { if (!years.Contains(tourApp.TourDateTime.Year)) years.Add(tourApp.TourDateTime.Year); });
            years.Sort();
            years.Reverse();

            return years;
        }
        private void CalculateTicketPercentage(TourAppointmentDTO SelectedTourApp)
        {
            int tickets = 0;
            int vouchers = 0;

            tickets += SelectedTourApp.Tickets.Count;
            vouchers += SelectedTourApp.Tickets.Count(ticket => ticket.HasVoucher);

            TourAppPercentage.Clear();
            TourAppPercentage.Add(Math.Round(tickets != 0 ? (double)(tickets - vouchers) / tickets * 100 : 0));
            TourAppPercentage.Add(Math.Round(tickets != 0 ? (double)vouchers / tickets * 100 : 0));
        }
        private void CalculateAgeStats(TourAppointmentDTO SelectedTourApp)
        {
            int year = DateTime.Now.Year;
            int group1 = 0, group2 = 0, group3 = 0;

            foreach (var ticket in SelectedTourApp.Tickets)
            {
                if (ticket.Guest2.Birthday.Year + 18 > year)
                    group1++;
                else if (ticket.Guest2.Birthday.Year + 18 <= year && ticket.Guest2.Birthday.Year + 50 > year)
                    group2++;
                else
                    group3++;
            }
            AgeGroups.Clear();
            AgeGroups.Add(group1);
            AgeGroups.Add(group2);
            AgeGroups.Add(group3);
        }
        private void Update()
        {
            TourAppsObs.Clear();
            foreach (var tourApp in TourApps)
            {
                if(tourApp.State == TOURSTATE.FINISHED)
                    TourAppsObs.Add(tourApp);
            }
        }
        private void DrawTourAppPercentageStats(double tickets, double vouchers, ObservableCollection<int> ageGroups)
        {
            PieChartCanvas.Children.Clear();

            if (tickets == 0 && vouchers == 0)
            {
                HideInstructions();
                StatsSadImage.Visibility = Visibility.Visible;
                SadLabel.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                StatsSadImage.Visibility = Visibility.Hidden;
                SadLabel.Visibility = Visibility.Hidden;
                DrawPieChart(tickets, vouchers);
                DrawBlockChart(ageGroups);
            }
        }
        private void DrawBlockChart(ObservableCollection<int> ageGroups)
        {
            AgeGropsCanvas.Children.Clear();

            int chartWidth = 100;
            int chartHeight = 120;
            int barWidth = chartWidth / ageGroups.Count;
            int maxValue = ageGroups.Max();
            double scale = (double)chartHeight / maxValue;

            DrawBars(ageGroups, barWidth, chartHeight, scale);
            DrawLabels(ageGroups, barWidth, chartHeight);
            DrawAxes(chartHeight, chartWidth);
        }
        private void DrawBars(ObservableCollection<int> ageGroups, int barWidth, int chartHeight, double scale)
        {
            for (int i = 0; i < ageGroups.Count; i++)
            {
                Rectangle bar = new Rectangle();
                bar.Width = barWidth - 2;
                bar.Height = ageGroups[i] * scale;
                bar.Fill = Brushes.Blue;
                bar.Stroke = Brushes.Black;
                bar.StrokeThickness = 1;
                Canvas.SetLeft(bar, i * barWidth + 1);
                Canvas.SetTop(bar, chartHeight - bar.Height + 20);
                AgeGropsCanvas.Children.Add(bar);

                // add a TextBlock for the age group value on top of the bar
                TextBlock label = new TextBlock();
                label.Text = ageGroups[i].ToString();
                label.TextAlignment = TextAlignment.Center;
                label.Width = barWidth;
                Canvas.SetLeft(label, i * barWidth);
                Canvas.SetTop(label, chartHeight - bar.Height + 5);
                AgeGropsCanvas.Children.Add(label);
            }
        }
        private void DrawLabels(ObservableCollection<int> ageGroups, int barWidth, int chartHeight)
        {
            for (int i = 0; i < ageGroups.Count; i++)
            {
                TextBlock label = new TextBlock();
                if (i == 0)
                    label.Text = "<18";
                else if (i == 1)
                    label.Text = "18-50";
                else if (i == 2)
                    label.Text = "50+";
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
        
        private void DrawPieChart(double tickets, double vouchers)
        {
            PieChartCanvas.Children.Clear();
            double[] values = { tickets, vouchers };

            double chartSize = 150;
            double centerX = chartSize / 2;
            double centerY = chartSize / 2;
            double radius = chartSize / 2;
            double startAngle = -90;

            for (int i = 0; i < values.Length; i++)
            {
                Polygon slice = DrawSlice(values[i], startAngle, centerX, centerY, radius);
                slice.Fill = i % 2 == 1 ? new SolidColorBrush(Color.FromRgb(0, 122, 204)) : new SolidColorBrush(Color.FromRgb(108, 206, 255));
                slice.Stroke = Brushes.Black;
                slice.StrokeThickness = 1;
                PieChartCanvas.Children.Add(slice);

                TextBlock label = CreateLabel(values[i], i, chartSize);
                double labelAngle = startAngle + values[i] * 180;
                SetLabelPosition(label, labelAngle, centerX, centerY, radius);
                PieChartCanvas.Children.Add(label);

                startAngle += values[i] * 360;
            }
        }
        private Polygon DrawSlice(double value, double startAngle, double centerX, double centerY, double radius)
        {
            double sweepAngle = value * 360;
            double endAngle = startAngle + sweepAngle;
            Point startPoint = new Point(centerX, centerY);
            Point endPoint = new Point(centerX + radius * Math.Cos(endAngle * Math.PI / 180), centerY + radius * Math.Sin(endAngle * Math.PI / 180));
            PointCollection points = new PointCollection();
            points.Add(startPoint);
            for (int j = (int)startAngle; j <= endAngle; j++)
            {
                double x = centerX + radius * Math.Cos(j * Math.PI / 180);
                double y = centerY + radius * Math.Sin(j * Math.PI / 180);
                points.Add(new Point(x, y));
            }
            points.Add(endPoint);
            points.Add(startPoint);
            return new Polygon() { Points = points };
        }
        private TextBlock CreateLabel(double value, int index, double chartSize)
        {
            TextBlock label = new TextBlock();
            label.Text = index == 0 ? $"{value:#0%}" : $"{value:#0%}";
            label.TextAlignment = TextAlignment.Center;
            label.Width = chartSize;
            return label;
        }
        private void SetLabelPosition(TextBlock label, double labelAngle, double centerX, double centerY, double radius)
        {
            double labelX = centerX + radius * 0.1 * Math.Cos(labelAngle * Math.PI / 180) - label.ActualWidth / 2;
            double labelY = centerY + radius * 0.8 * Math.Sin(labelAngle * Math.PI / 180) - label.ActualHeight / 2;
            Canvas.SetLeft(label, labelX);
            Canvas.SetTop(label, labelY);
        }
    }
}
