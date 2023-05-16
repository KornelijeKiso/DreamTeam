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
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticWindow.xaml
    /// </summary>
    public partial class TourRequestStatisticsWindow : Window
    {
        public TourRequestStatisticsWindow()
        {
            InitializeComponent();
            //DataContext = new 
        }
        public TourRequestStatisticsWindow(Guest2VM guest2)
        {
            InitializeComponent();
            DataContext = new TourRequestStatisticsVM(guest2);
        }
    }
}
