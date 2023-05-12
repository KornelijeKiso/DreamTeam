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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.Guest2View.Menu
{
    /// <summary>
    /// Interaction logic for G2TourRequest.xaml
    /// </summary>
    public partial class G2TourRequest : UserControl
    {
        public G2TourRequest()
        {
            InitializeComponent();
            //DataContext = new TourRequestsVM(guest2);
        }
        public G2TourRequest(Guest2VM guest2)
        {
            InitializeComponent();
            DataContext = new TourRequestsVM(guest2);
        }
    }
}
