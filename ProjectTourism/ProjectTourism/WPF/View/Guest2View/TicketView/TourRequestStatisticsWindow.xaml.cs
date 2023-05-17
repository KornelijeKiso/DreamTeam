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
using LiveCharts.Wpf;
using LiveCharts;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;


namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticWindow.xaml
    /// </summary>
    public partial class TourRequestStatisticsWindow : Window
    {
        public TourRequestStatisticsVM TourRequestStatisticsVM { get; set; }
        public TourRequestStatisticsWindow()
        {
            InitializeComponent();
            //DataContext = new TourRequestStatisticsVM(guest2);
        }
        public TourRequestStatisticsWindow(Guest2VM guest2)
        {
            InitializeComponent();
            this.TourRequestStatisticsVM = new TourRequestStatisticsVM(guest2);
            DataContext = this.TourRequestStatisticsVM;
            DisplayPieChartData();
        }

        private void ClearPieChart(PieChart pieChart)
        {
            foreach (PieSeries series in pieChart.Series)
            {
                series.Values.Clear();
            }
        }
        private void DisplayPieChartData()
        {
            RequestsPieChart.Series.Add(new PieSeries
            {
                Title = "Accepted",
                Stroke = Brushes.Black,
                Fill = Brushes.DarkSlateBlue,
                StrokeThickness = 2,
                Values = new ChartValues<double> { TourRequestStatisticsVM.Accepted }
            });
            RequestsPieChart.Series.Add(new PieSeries
            {
                Title = "Expired",
                Stroke = Brushes.Black,
                Fill = Brushes.DarkRed,
                StrokeThickness = 2,
                Values = new ChartValues<double> { TourRequestStatisticsVM.Expired }
            });
            RequestsPieChart.Series.Add(new PieSeries
            {
                Title = "Pending",
                Stroke = Brushes.Black,
                Fill = Brushes.White,
                StrokeThickness = 2,
                Values = new ChartValues<double> { TourRequestStatisticsVM.Pending }
            });
        }

        private void FilterYear(object sender, SelectionChangedEventArgs e)
        {
            TourRequestStatisticsVM.CalculateYearlyStatsFiltered(TourRequestStatisticsVM.AllTourRequests, TourRequestStatisticsVM.SelectedYear);
            ClearPieChart(RequestsPieChart);
            DisplayPieChartData();
        }
    }
}
