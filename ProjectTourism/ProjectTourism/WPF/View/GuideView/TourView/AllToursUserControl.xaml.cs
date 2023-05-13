using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.WPF.View.GuideView;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class AllToursUserControl : UserControl, INotifyPropertyChanged
    {
        public GuideVM Guide { get; set; }
        public TourVM SelectedTour { get; set; }
        public AllToursUserControl(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void TourStatistics_Click(object sender, RoutedEventArgs e)
        {
            HideProfilesContent();
            ContentArea.Content = new TourStatisticsUserControl(Guide.Username);
        }
        private void HideProfilesContent()
        {
            List<UIElement> profileUIElements = new List<UIElement> { DataGridTours,AllToursLabel, StatsPanel };
            profileUIElements.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SelectedTour.ArePicturesEmpty)
            {
                ContentArea.Content = new ImageViewerUserControl(SelectedTour);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
