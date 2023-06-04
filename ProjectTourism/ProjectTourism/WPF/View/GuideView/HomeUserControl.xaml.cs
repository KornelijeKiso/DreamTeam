using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.View.TourView;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class HomeUserControl : UserControl, INotifyPropertyChanged
    {
        private App app;
        public GuideDTO Guide { get; set; }
        public string UpcomingTourPicture { get; set; }
        public string Username { get; set; }
        public HomeUserControl(string username)
        {
            Username = username;
            app = (App)Application.Current;

            InitializeComponent();
            DataContext = this;

            Guide = new GuideDTO(username);
            SetLanguage();

            UpcomingTourPicture = "";
            SetUpcomingTour();
        }

        private void SetLanguage()
        {
            Guide = new GuideDTO(Username);
            if (Guide.Localization == "ENG")
            {
                LocalizationButton.Content = "Serbian";
                app.ChangeLanguage("en-US");
            }
            else
            {
                LocalizationButton.Content = "English";
                app.ChangeLanguage("sr-Latn-RS");
            }
        }
        private void SetUpcomingTour()
        {
            TourAppointmentDTO UpcomingTourApp = Guide.FindGuidesUpcomingTourApp();
            if (UpcomingTourApp != null && UpcomingTourApp.Tour.Pictures != null && !UpcomingTourApp.Tour.ArePicturesNull())
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
            ContentArea.Content = new CreateTourUserControl(Guide);
        }
        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            HideElements(new List<UIElement> { HomeLabel, WelcomeLabel, UpcomingLabel, UpcomingLabelName, AddNewTourButton, AllToursButton, ImageBorder, UpcomingImage });
            ContentArea.Content = new AllToursUserControl(Guide.Username);
        }

        private void LocalizationButton_Click(object sender, RoutedEventArgs e)
        {
            Guide.ChangeLocalization();
            SetLanguage();
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
