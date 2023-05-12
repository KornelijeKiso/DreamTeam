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
    /// Interaction logic for G2Tickets.xaml
    /// </summary>
    public partial class G2Tickets : UserControl
    {
        public G2Tickets()
        {
            InitializeComponent();
            //DataContext = new TicketsVM(guest2);
        }
        public G2Tickets(Guest2VM guest2)
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
