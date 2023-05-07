using LiveCharts.Wpf;
using LiveCharts;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StatsByMonthsWindow.xaml
    /// </summary>
    public partial class StatsByMonthsWindow : Window
    {
        public AccommodationStatisticsVM YearlyStats { get; set; }
        public AccommodationVM Accommodation { get; set; }
        public StatsByMonthsWindow(AccommodationStatisticsVM yearStats, AccommodationVM accommodation)
        {
            YearlyStats = yearStats;
            Accommodation = accommodation;
            InitializeComponent();
            DataContext = this;
            myPieChart.Series.Add(new PieSeries { Title = "Reserved", Stroke = Brushes.Black, Fill = Brushes.Orange, StrokeThickness = 2, Values = new ChartValues<double> { YearlyStats.BestMonth.Occupancy } });
            myPieChart.Series.Add(new PieSeries { Title = "Free", Stroke = Brushes.Black, Fill = Brushes.White, StrokeThickness = 2, Values = new ChartValues<double> { 100 - YearlyStats.BestMonth.Occupancy } });
        }
        public void CancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
