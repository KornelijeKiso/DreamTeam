using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Repositories;
using System.Runtime.CompilerServices;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class ViewAllAppointmentsWindow : UserControl, INotifyPropertyChanged
    {
        public TourAppointmentVM SelectedAppointment { get; set; }
        public GuideVM Guide { get; set; }

        public ViewAllAppointmentsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
                CancelChosenAppointment();
            else
                MessageBox.Show("You must choose an appointment which you would like to cancel!");
        }
        private void CancelChosenAppointment()
        {
            if (SelectedAppointment.TourDateTime > DateTime.Now.AddHours(48))
            {
                string name = new string(SelectedAppointment.Tour.Name);
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this appointment?", "Delete appointment", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    Guide.CancelAppointment(SelectedAppointment);
                MessageBox.Show(name + " has been succesfully deleted!");
            }
            else
                MessageBox.Show("The tour can not be canceled because the cancelation time is at least 48 hours before start.");
        }
        public void Update() { }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
