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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.View.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView
{
    /// <summary>
    /// Interaction logic for MainGuideWindow.xaml
    /// </summary>
    public partial class MainGuideWindow : Window, INotifyPropertyChanged, IObserver
    {
        public GuideVM Guide { get; set; }
        public GuideService GuideService { get; set; }
        public LocationVM NewLocation { get; set; }
        public LocationService LocationService { get; set; }
        public MainGuideWindow(string username)
        {
            InitializeComponent();
            DataContext = this;

            SetServices();
            Guide = GuideService.GetOne(username);
            NewLocation = new LocationVM(new Location());
        }

        private void SetServices()
        {
            LocationService = new LocationService(new LocationRepository());
            GuideService = new GuideService(new GuideRepository());
        }

        
        public void Update()
        {

        }

        private void AddNewTourButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourWindow createTourWindow = new CreateTourWindow(Guide);
            createTourWindow.ShowDialog();
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllToursWindow viewAllToursWindow = new ViewAllToursWindow(Guide.Username);
            viewAllToursWindow.ShowDialog();
        }

        private void LiveTourMonitorButton_Click(object sender, RoutedEventArgs e)
        {
            LiveToursTrackingWindow liveToursTrackingWindow = new LiveToursTrackingWindow(Guide.Username);
            liveToursTrackingWindow.ShowDialog();
        }

        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllToursWindow viewAllToursWindow = new ViewAllToursWindow(Guide.Username);
            viewAllToursWindow.ShowDialog();
        }
        private void AllAppointmentsLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Window viewAllAppointmentsWindow = new ViewAllAppointmentsWindow(Guide.Username);
            viewAllAppointmentsWindow.Show();
            e.Handled = true;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
