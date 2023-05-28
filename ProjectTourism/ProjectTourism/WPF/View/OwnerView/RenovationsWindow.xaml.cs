using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.OwnerViewModel;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RenovationsWindow.xaml
    /// </summary>
    public partial class RenovationsWindow : Window
    {
        public RenovationsWindow(AccommodationDTO accommodation)
        {
            InitializeComponent();
            DataContext = new RenovationsWindowVM(accommodation);
        }
        
        public void ValidateNumberInput(object sender, RoutedEventArgs e)
        {
            var integerUpDown = (IntegerUpDown)sender;
            int? value = integerUpDown.Value;
            if (!value.HasValue) integerUpDown.Value = 1;
        }
        private void IntegerValidation(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
            if (DurationTextBox.Value == 0){ DurationTextBox.Value = 1; e.Handled = true;}
        }
    }
}
