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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Repositories;
using System.Runtime.CompilerServices;

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TrackToursWindow.xaml
    /// </summary>
    public partial class ViewAllAppointmentsWindow : UserControl, INotifyPropertyChanged, IObserver
    {
        public ObservableCollection<TourAppointmentVM> Appointments { get; set; }
        public TourAppointmentVM SelectedAppointment { get; set; }
        public GuideService GuideService { get; set; }
        public TourAppointmentService TourAppointmentService { get; set; }
        public string GuideUsername { get; set; }
        public CanceledTourAppointmentsService CanceledTourAppointmentsService { get; set; }

        public ViewAllAppointmentsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            GuideUsername = username;
            DataContext = this;
            SetServices();
            Appointments = new ObservableCollection<TourAppointmentVM>(GuideService.GetGuidesAppointments(username));
            Update();
        }
        public void SetServices()
        {
            GuideService = new GuideService(new GuideRepository());
            TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            CanceledTourAppointmentsService = new CanceledTourAppointmentsService(new CanceledTourAppointmentsRepository());
        }
        public void Update()
        {
            Appointments.Clear();
            foreach(var app in GuideService.GetGuidesAppointments(GuideUsername))
            {
                Appointments.Add(app);
            }
        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                if(SelectedAppointment.TourDateTime > DateTime.Now.AddHours(48)) {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this appointment?", "Delete appointment", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        CanceledTourAppointmentsService.Add(SelectedAppointment);
                        TourAppointmentService.Delete(SelectedAppointment.Id);
                        Appointments.Remove(SelectedAppointment);
                        Update();
                    }
                }
                else
                    MessageBox.Show("The tour can not be canceled because the cancelation time is at least 48 hours before start.");
            }
            else
            {
                
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
