using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for LiveToursTrackingWindow.xaml
    /// </summary>
    public partial class LiveToursTrackingWindow : UserControl, INotifyPropertyChanged, IObserver
    {
        public GuideVM Guide { get; set; }
        public ObservableCollection<TourAppointmentVM> TourAppointments { get; set; }
        public TourAppointmentVM SelectedTourAppointment { get; set; }
        public GuideService GuideService { get; set; }
        public TourAppointmentService TourAppointmentService { get; set; }
        public LiveToursTrackingWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            GuideService = new GuideService(new GuideRepository());
            TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            Guide = GuideService.GetOne(username);
            TourAppointments = new ObservableCollection<TourAppointmentVM>(GuideService.GetGuidesCurrentAppointments(username));
            Update();
        }
        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourAppointment != null)
            {
                TourStopsWindow tourStopsWindow = new TourStopsWindow(SelectedTourAppointment);
                HideTodaysToursContent();
                ContentArea.Content = new TourStopsWindow(SelectedTourAppointment);
                SelectedTourAppointment = tourStopsWindow.TourAppointment;
                Update();
            }
            else
            {
                MessageBox.Show("You must choose a route which you want to start.");
            }
        }
        private void HideTodaysToursContent()
        {
            StartTourButton.Visibility = Visibility.Hidden;
            TodaysToursLabel.Visibility = Visibility.Hidden;
            DataGridTourAppointments.Visibility = Visibility.Hidden;
        }
        public void Update()
        {
            TourAppointments.Clear();
            foreach (var item in GuideService.GetGuidesCurrentAppointments(Guide.Username))
            {
                TourAppointments.Add(item);
                if (item.State == TOURSTATE.FINISHED)
                    item.IsFinished = true;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
