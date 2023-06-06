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
        public Guest2DTO Guest2 { get; set; }
        public TourRequestStatisticsUserControl()
        {
            InitializeComponent();
            //DataContext = new TourRequestStatisticsVM(guest2);
        }
        public TourRequestStatisticsUserControl(Guest2DTO guest2)
        {
            InitializeComponent();
            Guest2 = guest2;
            this.TourRequestStatisticsVM = new TourRequestStatisticsVM(guest2);
            DataContext = this.TourRequestStatisticsVM;
            FilterYear(this.TourRequestStatisticsVM);
            
        }

        private void ClearPieChart(PieChart pieChart)
        {
            foreach (PieSeries series in pieChart.Series)
            {
                series.Values.Clear();
            }
        }

        private void FilterYear(TourRequestStatisticsVM tourRequestStatisticsVM)
        {
            if ((tourRequestStatisticsVM != null) && (tourRequestStatisticsVM.SelectedYear != null))
            {
                tourRequestStatisticsVM.CalculateYearlyStatsFiltered(tourRequestStatisticsVM.AllTourRequests, tourRequestStatisticsVM.SelectedYear);
                ClearPieChart(RequestsPieChart);
                tourRequestStatisticsVM.DisplayYearlyStat();
            }
        }
        private void FilterYear(object sender, SelectionChangedEventArgs e)
        {
            // TO DO -> fix this
            //TourRequestStatisticsVM TourRequestStatisticsVM = new TourRequestStatisticsVM(Guest2);
            TourRequestStatisticsVM.CalculateYearlyStatsFiltered(TourRequestStatisticsVM.AllTourRequests, TourRequestStatisticsVM.SelectedYear);
            ClearPieChart(RequestsPieChart);
            //this.TourRequestStatisticsVM.DisplayYearlyStat();
        }
    }
}
