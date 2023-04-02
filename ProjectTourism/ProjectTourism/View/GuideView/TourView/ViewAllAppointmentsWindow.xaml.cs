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

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TrackToursWindow.xaml
    /// </summary>
    public partial class ViewAllAppointmentsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public ObservableCollection<TourAppointment> Appointments { get; set; }
        public TourAppointment SelectedAppointment { get; set; }
        public GuideController GuideController { get; set; }
        public TourAppointmentController TourAppointmentController { get; set; }
        public string GuideUsername { get; set; }
        public CanceledTourAppointmentsDAO CanceledTourAppointmentsDAO { get; set; }

        public ViewAllAppointmentsWindow(string username)
        {
            InitializeComponent();
            GuideUsername = username;
            DataContext = this;
            GuideController = new GuideController();
            Appointments = new ObservableCollection<TourAppointment>(GuideController.GetGuidesAppointments(username));
            TourAppointmentController = new TourAppointmentController();
            CanceledTourAppointmentsDAO = new CanceledTourAppointmentsDAO();
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
                        CanceledTourAppointmentsDAO.Add(SelectedAppointment);
                        TourAppointmentController.Delete(SelectedAppointment.Id);
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
            foreach(var appointment in GuideController.GetGuidesAppointments(GuideUsername))
            {
                Appointments.Add(appointment);
            }
        }
    }
}
