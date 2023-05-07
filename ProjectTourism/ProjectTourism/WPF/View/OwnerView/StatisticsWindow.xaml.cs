using LiveCharts.Wpf;
using LiveCharts;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
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
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public AccommodationVM Accommodation { get; set; }
        public AccommodationStatisticsVM SelectedYear { get; set; }

        public StatisticsWindow(AccommodationVM accommodation)
        {
            Accommodation = accommodation;
            InitializeComponent();
            DataContext = this;
            myPieChart.Series.Add(new PieSeries { Title = "Reserved", Stroke=Brushes.Black, Fill = Brushes.Orange, StrokeThickness = 2, Values = new ChartValues<double> { Accommodation.BestYear.Occupancy } });
            myPieChart.Series.Add(new PieSeries { Title = "Free", Stroke = Brushes.Black, Fill = Brushes.White, StrokeThickness = 2, Values = new ChartValues<double> { 100 - Accommodation.BestYear.Occupancy } });
        }

        public void StatsByMonthsClick(object sender, RoutedEventArgs e)
        {
            StatsByMonthsWindow statsByMonthsWindow = new StatsByMonthsWindow(SelectedYear, Accommodation);
            statsByMonthsWindow.Show();
        }
    }
}
