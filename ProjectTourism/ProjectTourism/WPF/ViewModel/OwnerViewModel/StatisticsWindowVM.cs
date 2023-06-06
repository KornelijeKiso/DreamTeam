using LiveCharts.Wpf;
using LiveCharts;
using ProjectTourism.DTO;
using ProjectTourism.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectTourism.Utilities;
using System.Windows.Input;
using System.ComponentModel;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class StatisticsWindowVM:INotifyPropertyChanged
    {
        public PieChart myPieChart { get; set; } = new PieChart();
        public AccommodationDTO Accommodation { get; set; }
        public AccommodationStatisticsDTO SelectedYear { get; set; }
        private bool _help;
        public bool Help
        {
            get { return _help; }
            set
            {
                if (_help != value)
                {
                    _help = value;
                    OnPropertyChanged(nameof(Help));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public StatisticsWindowVM() { }
        public StatisticsWindowVM(AccommodationDTO accommodation, bool help)
        {
            Accommodation = accommodation;
            myPieChart.Series.Add(new PieSeries { Title = "Reserved", Stroke = Brushes.Black, Fill = Brushes.Orange, StrokeThickness = 2, Values = new ChartValues<double> { Accommodation.BestYear.Occupancy } });
            myPieChart.Series.Add(new PieSeries { Title = "Free", Stroke = Brushes.Black, Fill = Brushes.White, StrokeThickness = 2, Values = new ChartValues<double> { 100 - Accommodation.BestYear.Occupancy } });
            Help = help;
        }

        public void StatsByMonthsClick(object parameter)
        {
            StatsByMonthsWindow statsByMonthsWindow = new StatsByMonthsWindow(SelectedYear, Accommodation, Help);
            statsByMonthsWindow.Show();
        }
        public ICommand StatsByMonthsClickCommand
        {
            get => new RelayCommand(StatsByMonthsClick);
        }
    }
}
