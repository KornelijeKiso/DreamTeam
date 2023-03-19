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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.View.TourView;

namespace ProjectTourism.View.GuideView
{
    /// <summary>
    /// Interaction logic for MainGuideWindow.xaml
    /// </summary>
    public partial class MainGuideWindow : Window, INotifyPropertyChanged, IObserver
    {
        public User User { get; set; }
        public Guide Guide { get; set; }
        public UserController UserController { get; set; }
        public GuideController GuideController { get; set; }
        public Location NewLocation { get; set; }
        public LocationDAO LocationDAO { get; set; }
        public MainGuideWindow(string username)
        {
            InitializeComponent();
            DataContext = this;

            MakeControllers();
            GetModels(username);
            LocationDAO = new LocationDAO();
            NewLocation = new Location();
        }

        private void GetModels(string username)
        {
            Guide = GuideController.GetOne(username);
            User = UserController.GetOne(username);
        }

        private void MakeControllers()
        {
            GuideController = new GuideController();
            UserController = new UserController();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
