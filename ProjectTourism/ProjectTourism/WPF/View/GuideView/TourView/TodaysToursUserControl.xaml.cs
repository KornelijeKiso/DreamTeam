using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.WPF.View.GuideView;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class TodaysToursUserControl : UserControl, INotifyPropertyChanged
    {
        public GuideDTO Guide { get; set; }
        public TourAppointmentDTO SelectedTourAppointment { get; set; }
        public TodaysToursUserControl(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideDTO(username);
        }
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SelectedTourAppointment.Tour.ArePicturesEmpty)
            {
                ContentArea.Content = new ImageViewerUserControl(SelectedTourAppointment.Tour);
            }
        }
        private void ManageButton_Click(object sender, RoutedEventArgs e)
        {
            TourStopsUserControl tourStopsWindow = new TourStopsUserControl(SelectedTourAppointment);
            HideTodaysToursContent();
            ContentArea.Content = tourStopsWindow;
            SelectedTourAppointment = tourStopsWindow.TourAppointment;
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            HideTodaysToursContent();
            ContentArea.Content = new ReviewsUserControl(SelectedTourAppointment);
        }
        private void HideTodaysToursContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { TodaysToursLabel, DataGridTourAppointments };
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
