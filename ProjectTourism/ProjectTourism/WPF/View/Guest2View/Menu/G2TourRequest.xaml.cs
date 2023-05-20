using System.Windows.Controls;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.DTO;

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
        public G2TourRequest(Guest2DTO guest2)
        {
            InitializeComponent();
            DataContext = new TourRequestsVM(guest2);
        }
    }
}
