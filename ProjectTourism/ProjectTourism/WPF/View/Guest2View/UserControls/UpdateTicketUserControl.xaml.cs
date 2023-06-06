using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

namespace ProjectTourism.WPF.View.Guest2View.UserControls
{
    /// <summary>
    /// Interaction logic for UpdateTicketUserControl.xaml
    /// </summary>
    public partial class UpdateTicketUserControl : UserControl
    {
        public UpdateTicketUserControl()
        {
            InitializeComponent();
        }

        public UpdateTicketUserControl(Guest2DTO guest2, TicketDTO ticket)
        {
            InitializeComponent();
            DataContext = new UpdateTicketVM(guest2, ticket);
        }
    }
}
