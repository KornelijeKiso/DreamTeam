using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.OwnerView;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using ProjectTourism.WPF.ViewModel.OwnerViewModel;

namespace ProjectTourism.WPF.View.OwnerView
{
    public partial class YourAccommodationsMenuItem : UserControl
    {
        public YourAccommodationsMenuItem(string username)
        {
            InitializeComponent();
            DataContext = new YourAccommodationsMenuItemVM(username);
        }
        private void IntegerUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
            if (MaxUpDown.Value == 0)
            {
                MaxUpDown.Value = 1;
                e.Handled = true;
            }
            if (MinUpDown.Value == 0)
            {
                MinUpDown.Value = 1;
                e.Handled = true;
            }
        }
        public void ValidateNumberInput(object sender, RoutedEventArgs e)
        {
            var integerUpDown = (IntegerUpDown)sender;
            int? value = integerUpDown.Value;
            if (!value.HasValue)
            {
                integerUpDown.Value = 1;
            }
        }

    }
}
