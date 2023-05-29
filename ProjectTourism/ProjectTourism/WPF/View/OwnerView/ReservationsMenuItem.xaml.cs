using ProjectTourism.WPF.ViewModel.OwnerViewModel;
using System.Windows.Controls;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for ReservationsMenuItem.xaml
    /// </summary>
    public partial class ReservationsMenuItem : UserControl
    {
        public ReservationsMenuItem(string username)
        {
            InitializeComponent();
            DataContext = new ReservationsMenuItemVM(username);
        }
    }
}
