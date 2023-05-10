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
    /// Interaction logic for G2Vouchers.xaml
    /// </summary>
    public partial class G2Vouchers : UserControl
    {
        public G2Vouchers(Guest2VM guest2)
        {
            InitializeComponent();
            DataContext = new VouchersVM(guest2);
        }
    }
}
