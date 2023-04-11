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
using ProjectTourism.Model;
using ProjectTourism.View.GuideView.TourView;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : UserControl
    {
        public ProfileWindow()
        {
            InitializeComponent();
            DataContext= this;
        }
        private void TourStatistics_Click(object sender, RoutedEventArgs e)
        {
           ContentArea.Content = new TourStatisticsWindow();
        }
    }
}
