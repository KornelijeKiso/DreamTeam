using ProjectTourism.WPF.ViewModel.OwnerViewModel;
using System.Windows;
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
        private void toti_Loaded(object sender, RoutedEventArgs e)
        {
            ToolTip toolTip = ((Button)sender).ToolTip as ToolTip;
            ReservationsMenuItemVM viewModel = DataContext as ReservationsMenuItemVM;
            if (toolTip != null && viewModel != null) toolTip.Visibility = viewModel.Owner.HelpOn ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
