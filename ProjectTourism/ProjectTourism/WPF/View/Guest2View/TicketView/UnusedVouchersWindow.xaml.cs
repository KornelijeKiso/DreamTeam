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

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for UnusedVouchersWindow.xaml
    /// </summary>
    public partial class UnusedVouchersWindow : Window
    {
        public UnusedVouchersWindow()
        {
            InitializeComponent();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UseVoucherClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
