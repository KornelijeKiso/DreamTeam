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
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.View.TourView;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView
{
    /// <summary>
    /// Interaction logic for MainGuideWindow.xaml
    /// </summary>
    public partial class MainGuideWindow : Window
    {
        public string Username { get; set; }
        public MainGuideWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Username = username;
            ContentArea.Content = new HomeWindow(username);
        }
        private void HomeLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ContentArea.Content = new HomeWindow(Username);
            e.Handled = true;
        }
        private void AllAppointmentsLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ContentArea.Content = new ViewAllAppointmentsWindow(Username);
            e.Handled = true;
        }

        private void ProfileLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ContentArea.Content = new ProfileWindow(Username);
            e.Handled = true;
        }

        private void RequestsLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ContentArea.Content = new RequestsWindow();
            e.Handled = true;
        }

        private void LiveTourMonitorLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ContentArea.Content = new LiveToursTrackingWindow(Username);
            e.Handled = true;
        }

    }
}
