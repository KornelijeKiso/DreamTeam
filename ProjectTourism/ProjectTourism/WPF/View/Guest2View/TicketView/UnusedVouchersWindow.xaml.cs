using System.Windows;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for UnusedVouchersWindow.xaml
    /// </summary>
    public partial class UnusedVouchersWindow : Window
    {
        public VouchersVM VouchersVM { get; set; }
        public VoucherDTO SelectedVoucherDTO { get; set; }
        public VoucherDTO UsedVoucherDTO { get; set; }
        public TicketDTO TicketDTO { get; set; }
        public bool IsUsed { get; set; }

        public UnusedVouchersWindow(Guest2DTO guest2, TicketDTO ticketDTO)
        {
            InitializeComponent();
            DataContext = this;
            VouchersVM = new VouchersVM(new Guest2DTO(guest2.GetGuest2()));
            TicketDTO = ticketDTO;
            IsUsed = false;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UseVoucherClick(object sender, RoutedEventArgs e)
        {
            if (SelectedVoucherDTO != null)
            {
                UsedVoucherDTO = new VoucherDTO(SelectedVoucherDTO, TicketDTO);
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
