using System.Windows;
using System.Windows.Controls;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.Guest2View.Menu
{
    /// <summary>
    /// Interaction logic for G2Home.xaml
    /// </summary>
    public partial class G2Home : UserControl
    {
        public G2Home()
        {
            InitializeComponent();
            //DataContext = new HomeVM(guest2);
        }
        public G2Home(Guest2DTO guest2)
        {
            InitializeComponent();
            DataContext = new HomeVM(guest2);
        }

        private void ItemsShown(object sender, SelectionChangedEventArgs e)
        {
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    {
                        BuyTicket.Visibility = Visibility.Visible;
                        BuyTicket.IsEnabled = true;
                        break;
                    }
                default:
                    {
                        BuyTicket.Visibility = Visibility.Collapsed;
                        BuyTicket.IsEnabled = false;
                        break;
                    }
            }
        }
    }
}
