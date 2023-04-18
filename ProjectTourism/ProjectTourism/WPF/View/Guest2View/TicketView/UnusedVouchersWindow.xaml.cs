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
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for UnusedVouchersWindow.xaml
    /// </summary>
    public partial class UnusedVouchersWindow : Window
    {
        public VouchersVM VouchersVM { get; set; }
        public VoucherVM SelectedVoucherVM { get; set; }
        public VoucherVM UsedVoucherVM { get; set; }
        public TicketVM TicketVM { get; set; }
        public bool IsUsed { get; set; }

        public UnusedVouchersWindow(TicketVM ticketVM)
        {
            InitializeComponent();
            DataContext = this;
            VouchersVM = new VouchersVM();
            TicketVM = ticketVM;
            IsUsed = false;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UseVoucherClick(object sender, RoutedEventArgs e)
        {
            if (SelectedVoucherVM != null)
            {
                UsedVoucherVM = new VoucherVM(SelectedVoucherVM, TicketVM);
                IsUsed = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select the voucher!");
            }
        }

    }
}
