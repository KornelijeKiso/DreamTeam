using System.Windows;
using System.Windows.Controls;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.Guest2View.Menu
{
    /// <summary>
    /// Interaction logic for G2Tickets.xaml
    /// </summary>
    public partial class G2Tickets : UserControl
    {
        public G2Tickets()
        {
            InitializeComponent();
            //DataContext = new TicketsVM(guest2);
        }
        public G2Tickets(Guest2DTO guest2)
        {
            InitializeComponent();
            DataContext = new TicketsVM(guest2);
        }

        private void ItemsShown(object sender, SelectionChangedEventArgs e)
        {
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    {
                        UpdateTicket.Visibility = Visibility.Visible;
                        UpdateTicket.IsEnabled = true;

                        ReturnTicket.Visibility = Visibility.Visible;
                        ReturnTicket.IsEnabled = true;

                        GradeTicketButton.Visibility = Visibility.Collapsed;
                        GradeTicketButton.IsEnabled = false;

                        break;
                    }
                case 1:
                    {
                        UpdateTicket.Visibility = Visibility.Collapsed;
                        UpdateTicket.IsEnabled = false;

                        ReturnTicket.Visibility = Visibility.Collapsed;
                        ReturnTicket.IsEnabled = false;

                        GradeTicketButton.Visibility = Visibility.Visible;
                        GradeTicketButton.IsEnabled = true;

                        break;
                    }
                default:
                    {
                        UpdateTicket.Visibility = Visibility.Collapsed;
                        UpdateTicket.IsEnabled = false;

                        ReturnTicket.Visibility = Visibility.Collapsed;
                        ReturnTicket.IsEnabled = false;

                        GradeTicketButton.Visibility = Visibility.Collapsed;
                        GradeTicketButton.IsEnabled = false;

                        break;
                    }
            }
        }
    }
}
