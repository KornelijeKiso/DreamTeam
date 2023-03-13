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

namespace ProjectTourism.View.GuideView.RouteView
{
    /// <summary>
    /// Interaction logic for LiveRoutesTrackingWindow.xaml
    /// </summary>
    public partial class LiveRoutesTrackingWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Guide Guide { get; set; }
        public ObservableCollection<Route> Routes { get; set; }
        public Route SelectedRoute { get; set; }
        public GuideController GuideController { get; set; }
        public RouteController RouteController { get; set; }
        public LiveRoutesTrackingWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            GuideController = new GuideController();
            RouteController = new RouteController();
            Guide = GuideController.GetOne(username);
            Routes = new ObservableCollection<Route>(GuideController.GetGuidesRoutesCurrent(username));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Update()
        {
            throw new NotImplementedException();
        }

        private void StartRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRoute != null)
            {
                RouteStopsWindow routeStopsWindow = new RouteStopsWindow(SelectedRoute.Id);
                SelectedRoute.State = ROUTESTATE.STARTED;
                routeStopsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must choose a route which you want to start.");
            }
        }
    }
}
