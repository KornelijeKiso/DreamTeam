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
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class LiveToursTrackingWindow : UserControl, INotifyPropertyChanged
    {
        public GuideVM Guide { get; set; }
        public TourAppointmentVM SelectedTourAppointment { get; set; }
        public LiveToursTrackingWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourAppointment != null)
            {
                TourStopsWindow tourStopsWindow = new TourStopsWindow(SelectedTourAppointment);
                HideTodaysToursContent();
                ContentArea.Content = new TourStopsWindow(SelectedTourAppointment);
                SelectedTourAppointment = tourStopsWindow.TourAppointment;
            }
            else
                MessageBox.Show("You must choose a route which you want to start.");
        }
        private void HideTodaysToursContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { StartTourButton, TodaysToursLabel, DataGridTourAppointments };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        public void Update() { }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
