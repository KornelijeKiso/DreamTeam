using System.Windows.Controls;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.Guest2View.Menu
{
    /// <summary>
    /// Interaction logic for G2Vouchers.xaml
    /// </summary>
    public partial class G2Vouchers : UserControl
    {
        public G2Vouchers()
        {
            InitializeComponent();
            //DataContext = new VouchersVM(guest2);
        }
        public G2Vouchers(Guest2DTO guest2)
        {
            InitializeComponent();
            DataContext = new VouchersVM(guest2);
        }
    }
}
