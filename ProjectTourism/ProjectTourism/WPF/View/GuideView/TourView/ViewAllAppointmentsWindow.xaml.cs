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
using ProjectTourism.WPF.View.GuideView.TourView;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class ViewAllAppointmentsWindow : UserControl, INotifyPropertyChanged
    {
        public TourAppointmentVM SelectedAppointment { get; set; }
        public GuideVM Guide { get; set; }
        public ObservableCollection<TourAppointmentVM> FinishedApps { get; set; }
        public ObservableCollection<TourAppointmentVM> CanceledApps { get; set; }
        public ObservableCollection<TourAppointmentVM> ReadyApps { get; set; }
        public ObservableCollection<TourAppointmentVM> StoppedApps { get; set; }
        public ObservableCollection<TourAppointmentVM> SortedFinishedApps { get; set; }
        public ObservableCollection<TourAppointmentVM> SortedReadyApps { get; set; }
        public ObservableCollection<TourAppointmentVM> SortedStoppedApps { get; set; }
        public ViewAllAppointmentsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
            InitializeApps();
            Update(Guide);
            SortByDate();
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                if (SelectedAppointment.State == TOURSTATE.FINISHED || SelectedAppointment.State == TOURSTATE.STOPPED)
                    ShowReviewWindow();
                else
                    MessageBox.Show("The selected appointment was not finished yet!");
            }
            else
                MessageBox.Show("You must choose an appointment which reviews you would like to see.");
        }
        private void ShowReviewWindow()
        {
            if (SelectedAppointment.TicketGrades.Count > 0)
            {
                HideAllAppsContent();
                ContentArea.Content = new ReviewsWindow(SelectedAppointment);
            }
            else
                MessageBox.Show("There are no reviews for this appointment!");
        }

        public void HideAllAppsContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { grid };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        private void SortByDate()
        {
            SortedFinishedApps = new ObservableCollection<TourAppointmentVM>(FinishedApps.OrderByDescending(a => a.TourDateTime));
            SortedReadyApps = new ObservableCollection<TourAppointmentVM>(ReadyApps.OrderByDescending(a => a.TourDateTime));
            SortedStoppedApps = new ObservableCollection<TourAppointmentVM>(StoppedApps.OrderByDescending(a => a.TourDateTime));
        }
        private void InitializeApps()
        {
            FinishedApps = new ObservableCollection<TourAppointmentVM>();
            CanceledApps = new ObservableCollection<TourAppointmentVM>();
            ReadyApps = new ObservableCollection<TourAppointmentVM>();
            StoppedApps = new ObservableCollection<TourAppointmentVM>();
        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            string name = new string(SelectedAppointment.Tour.Name);
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this appointment?", "Delete appointment", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Guide.CancelAppointment(SelectedAppointment);
                MessageBox.Show(name + " has been succesfully deleted!");
                CanceledApps.Add(SelectedAppointment);
                SortedReadyApps.Remove(SelectedAppointment);
            }
        }
        private void Update(GuideVM guide)
        {
            foreach (var tourApp in guide.TourAppointments)
            {
                if (tourApp.State == TOURSTATE.FINISHED)
                    FinishedApps.Add(tourApp);
                else if (tourApp.State == TOURSTATE.READY)
                    ReadyApps.Add(tourApp);
                else if (tourApp.State == TOURSTATE.STOPPED)
                    StoppedApps.Add(tourApp);
                else if (tourApp.State == TOURSTATE.CANCELED)
                    CanceledApps.Add(tourApp);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
