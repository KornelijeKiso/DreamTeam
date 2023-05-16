using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public string Average { get; set; }
        public YourProfileMenuItem(string username)
        {
            InitializeComponent();
            DataContext = this;
            Owner = new OwnerVM(username);
            Average = Owner.AverageGrade.ToString("0.0");
        }
        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
    }
}
