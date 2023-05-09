using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.GuideView;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class ViewAllToursWindow : UserControl, INotifyPropertyChanged
    {
        public GuideVM Guide { get; set; }
        public TourVM SelectedTour { get; set; }
        public ViewAllToursWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void TourStatistics_Click(object sender, RoutedEventArgs e)
        {
            HideProfilesContent();
            ContentArea.Content = new TourStatisticsWindow(Guide.Username);
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
