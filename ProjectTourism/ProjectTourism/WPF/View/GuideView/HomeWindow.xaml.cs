using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
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
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class HomeWindow : UserControl, INotifyPropertyChanged
    {
        public GuideVM Guide { get; set; }
        public string UpcomingTourPicture { get; set; }
        public HomeWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
            UpcomingTourPicture = "";
            TourAppointmentVM UpcomingTourApp = Guide.FindGuidesUpcomingTourApp();
            if(UpcomingTourApp != null && UpcomingTourApp.Tour.Pictures != null)
            {
                UpcomingTourPicture = UpcomingTourApp.Tour.Pictures[0];
                UpcomingLabelName.Content = UpcomingTourApp.Tour.Name;
            }
            else
                HideElements(new List<UIElement> { UpcomingLabel, ImageBorder, UpcomingImage, UpcomingLabelName });
        }
        private void HideElements(List<UIElement> elements)
        {
            elements.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        private void AddNewTourButton_Click(object sender, RoutedEventArgs e)
        {
            HideElements(new List<UIElement> { HomeLabel, WelcomeLabel, UpcomingLabel, UpcomingLabelName, AddNewTourButton, AllToursButton, ImageBorder, UpcomingImage });
            ContentArea.Content = new CreateTourWindow(Guide);
        }
        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            HideElements(new List<UIElement> { HomeLabel, WelcomeLabel, UpcomingLabel, UpcomingLabelName, AddNewTourButton, AllToursButton, ImageBorder, UpcomingImage });
            ContentArea.Content = new ViewAllToursWindow(Guide.Username);
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
