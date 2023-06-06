using LiveCharts.Wpf;
using LiveCharts;
using ProjectTourism.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectTourism.Utilities;
using System.Windows.Input;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.PDF.OwnerPDFs;
using System.Windows;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class StatsByMonthsWindowVM:ViewBase
    {
        public PieChart myPieChart { get; set; } = new PieChart();
        public AccommodationStatisticsDTO YearlyStats { get; set; }
        public AccommodationDTO Accommodation { get; set; }
        public StatsByMonthsWindowVM(AccommodationStatisticsDTO yearStats, AccommodationDTO accommodation)
        {
            YearlyStats = yearStats;
            Accommodation = accommodation;
            myPieChart.Series.Add(new PieSeries { Title = "Reserved", Stroke = Brushes.Black, Fill = Brushes.Orange, StrokeThickness = 2, Values = new ChartValues<double> { YearlyStats.BestMonth.Occupancy } });
            myPieChart.Series.Add(new PieSeries { Title = "Free", Stroke = Brushes.Black, Fill = Brushes.White, StrokeThickness = 2, Values = new ChartValues<double> { 100 - YearlyStats.BestMonth.Occupancy } });
        }

        private void PDF(object parameter)
        {
            PDFgenerator generatePDFDocumentVM = new PDFgenerator(YearlyStats, Accommodation);
            //MessageBox.Show("Your Report is generated in folder ../../PDF/OwnerPDFs\n\n" +
            //    "under the name: statistics_report" + Accommodation.Id.ToString() + ".pdf");
        }

        public ICommand PDFCommand
        {
            get => new RelayCommand(PDF);
        }
    }
}
