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
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Repositories;

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TrackToursWindow.xaml
    /// </summary>
    public partial class ViewAllAppointmentsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public ObservableCollection<TourAppointmentVM> Appointments { get; set; }
        public TourAppointmentVM SelectedAppointment { get; set; }
        public GuideService GuideService { get; set; }
        public TourAppointmentService TourAppointmentService { get; set; }
        public string GuideUsername { get; set; }
        public CanceledTourAppointmentsRepository CanceledTourAppointmentsRepo { get; set; }

        public ViewAllAppointmentsWindow(string username)
        {
            InitializeComponent();
            GuideUsername = username;
            DataContext = this;
            GuideService = new GuideService(new GuideRepository());
            List<TourAppointmentVM> TourApps = new List<TourAppointmentVM>();
            foreach (var tourApp in GuideService.GetGuidesCurrentAppointments(username))
            {
                TourApps.Add(new TourAppointmentVM(tourApp));
            }
            Appointments = new ObservableCollection<TourAppointmentVM>(TourApps);
            TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            CanceledTourAppointmentsRepo = new CanceledTourAppointmentsRepository();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {

        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                if(SelectedAppointment.TourDateTime > DateTime.Now.AddHours(48)) {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this appointment?", "Delete appointment", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        CanceledTourAppointmentsRepo.Add(SelectedAppointment.GetTourAppointment());
                        TourAppointmentService.Delete(SelectedAppointment);
                        Appointments.Remove(SelectedAppointment);
                        UpdateAppointments();
                    }
                }
                else
                    MessageBox.Show("The tour can not be canceled because the cancelation time is at least 48 hours before start.");
            }
            else
            {
                
            }
        }
        private void UpdateAppointments()
        {
            Appointments.Clear();
            foreach (var tourApp in GuideService.GetGuidesAppointments(GuideUsername))
            {
                Appointments.Add(new TourAppointmentVM(tourApp));
            }
        }
    }
}
