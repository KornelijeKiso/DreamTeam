using ProjectTourism.WPF.ViewModel;
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

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for YourProfileMenuItem.xaml
    /// </summary>
    public partial class YourProfileMenuItem : UserControl
    {
        public OwnerVM Owner { get; set; }
        public YourProfileMenuItem(OwnerVM owner)
        {
            InitializeComponent();
            DataContext = this;
            Owner = owner;
            if (Owner.IsSuperHost)
            {
                SuperHostIcon.Visibility = Visibility.Visible;
                SuperHostLabel.Visibility = Visibility.Visible;
            }
            else
            {
                SuperHostIcon.Visibility = Visibility.Collapsed;
                SuperHostLabel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
