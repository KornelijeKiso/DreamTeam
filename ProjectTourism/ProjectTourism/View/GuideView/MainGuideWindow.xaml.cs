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
using ProjectTourism.View.GuideView.RouteView;
using ProjectTourism.View.RouteView;

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
            DataContext= this;
            GuideController = new GuideController();
            UserController = new UserController();
            Guide = GuideController.GetOne(username);
            User = UserController.GetOne(username);
            LocationDAO= new LocationDAO();
            NewLocation = new Location();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            //TODO
            throw new NotImplementedException();
        }

        private void AddNewRouteButton_Click(object sender, RoutedEventArgs e)
        {
            CreateRouteWindow createGuideWindow = new CreateRouteWindow(Guide);
            createGuideWindow.ShowDialog();
        }

        private void RoutesButton_Click(object sender, RoutedEventArgs e)
        {
            ViewAllRoutesWindow viewAllRoutesWindow = new ViewAllRoutesWindow(Guide.Username);
            viewAllRoutesWindow.ShowDialog();
        }

        private void LiveRouteMonitorButton_Click(object sender, RoutedEventArgs e)
        {
            LiveRoutesTrackingWindow liveRoutesTrackingWindow = new LiveRoutesTrackingWindow(Guide.Username);
            liveRoutesTrackingWindow.ShowDialog();
        }
    }
}
