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
        public RenovationAppointmentVM RenovationAppointment {get; set;}
        public RenovationsWindow(AccommodationVM accommodation)
        {
            Accommodation = accommodation;
            NewRenovation = new RenovationVM();
            RenovationAppointment = new RenovationAppointmentVM();
            RenovationAppointment.AccommodationId = Accommodation.Id;
            NewRenovation.AccommodationId = Accommodation.Id;
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
            if (SelectedFreeAppointment.Text.ToString() == "") MessageBox.Show("You have to select appointment for your renovation.");
            else
            {
                Accommodation.ScheduleNewRenovation(NewRenovation);
                NewRenovation.Reset();
                SelectedFreeAppointment.Text = "";
            }
        }
        public void SelectFreeAppointmentClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DurationTextBox.Text.ToString()))
            {
                MessageBox.Show("Enter a duration.");
                return;
            }
            SelectFreeAppointmentForRenovatonWindow selectFreeAppointmentForRenovatonWindow = new SelectFreeAppointmentForRenovatonWindow(RenovationAppointment);
            selectFreeAppointmentForRenovatonWindow.ShowDialog();
            if (selectFreeAppointmentForRenovatonWindow.SelectedRenovation != null)
            {
                SelectedFreeAppointment.Text = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.StartDate.ToString() + " - " + selectFreeAppointmentForRenovatonWindow.SelectedRenovation.EndDate.ToString();
                NewRenovation.StartDate = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.StartDate;
                NewRenovation.EndDate = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.EndDate;
            }
        }
        public void DeclineClick(object sender, RoutedEventArgs e)
        {
            SelectedFreeAppointment.Text = "";
        }
        private void IntegerValidation(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
            else
            {
                int res;
                if (int.TryParse(DurationTextBox.Value.ToString(), out res))
                {
                    if(res == 0)
                    {
                        e.Handled = true;
                        DurationTextBox.Value = 1;
                    }
                }
            }
        }

        public void CancelRenovationClick(object sender, RoutedEventArgs e)
        {
            Accommodation.CancelRenovation(SelectedRenovation);
        }
    }
}
