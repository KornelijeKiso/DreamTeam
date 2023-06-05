using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts.Wpf;
using LiveCharts;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.DTO;


namespace ProjectTourism.WPF.View.Guest2View.UserControls
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticsUserControl.xaml
    /// </summary>
    public partial class TourRequestStatisticsUserControl : UserControl
    {
        public TourRequestStatisticsVM TourRequestStatisticsVM { get; set; }
        public TourRequestStatisticsUserControl()
        {
            InitializeComponent();
            //DataContext = new TourRequestStatisticsVM(guest2);
        }
        public TourRequestStatisticsUserControl(Guest2DTO guest2)
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
            // TO DO -> fix this
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
            // TO DO -> fix this
            TourRequestStatisticsVM.CalculateYearlyStatsFiltered(TourRequestStatisticsVM.AllTourRequests, TourRequestStatisticsVM.SelectedYear);
            ClearPieChart(RequestsPieChart);
            DisplayPieChartData();
        }
    }
}
