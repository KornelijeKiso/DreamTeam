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
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RenovationsWindow.xaml
    /// </summary>
    public partial class RenovationsWindow : Window, INotifyPropertyChanged
    {
        public AccommodationVM Accommodation { get; set; }
        public RenovationVM SelectedRenovation { get; set; }
        public RenovationVM NewRenovation { get; set; }
        public RenovationsWindow(AccommodationVM accommodation)
        {
            Accommodation = accommodation;
            NewRenovation = new RenovationVM();
            InitializeComponent();
            DataContext = this;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ScheduleRenovationClick(object sender, RoutedEventArgs e)
        {
            Accommodation.ScheduleNewRenovation(NewRenovation);
        }
        public void CancelRenovationClick(object sender, RoutedEventArgs e)
        {
            Accommodation.CancelRenovation(SelectedRenovation);
        }
    }
}
