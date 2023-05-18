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
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.OwnerViewModel;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for StatsByMonthsWindow.xaml
    /// </summary>
    public partial class StatsByMonthsWindow : Window
    {
        public StatsByMonthsWindow(AccommodationStatisticsDTO yearStats, AccommodationDTO accommodation)
        {
            InitializeComponent();
            DataContext = new StatsByMonthsWindowVM(yearStats, accommodation);
        }
        public void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
