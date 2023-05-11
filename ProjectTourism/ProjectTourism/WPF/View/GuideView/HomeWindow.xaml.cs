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
        private App app;
        public GuideVM Guide { get; set; }
        public string UpcomingTourPicture { get; set; }
        public string Username { get; set; }
        public HomeWindow(string username)
        {
            Username = username;
            app = (App)Application.Current;

            InitializeComponent();
            DataContext = this;

            Guide = new GuideVM(username);
            SetLanguage();

            UpcomingTourPicture = "";
            SetUpcomingTour();
        }

        private void SetLanguage()
        {
            Guide = new GuideVM(Username);
            if (Guide.Localization == "ENG")
            {
                app.ChangeLanguage("en-US");
            }
            else
            {
                app.ChangeLanguage("sr-Latn-RS");
            }
        }

        private void SetUpcomingTour()
        {
            TourAppointmentVM UpcomingTourApp = Guide.FindGuidesUpcomingTourApp();
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
            ContentArea.Content = new CreateTourWindow(Guide);
        }
        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            HideElements(new List<UIElement> { HomeLabel, WelcomeLabel, UpcomingLabel, UpcomingLabelName, AddNewTourButton, AllToursButton, ImageBorder, UpcomingImage });
            ContentArea.Content = new ViewAllToursWindow(Guide.Username);
        }
        private void Localization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = LocalizationComboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                string selectedLanguage = selectedItem.Content as string;

                if (selectedLanguage != null)
                {
                    switch (selectedLanguage)
                    {
                        case "ENG":
                            {
                                app.ChangeLanguage("en-US");
                                Guide.ChangeLocalization();
                                LocalizationComboBox.Text = "ENG";
                                break;
                            }
                        case "SRB":
                            {
                                app.ChangeLanguage("sr-Latn-RS");
                                Guide.ChangeLocalization();
                                LocalizationComboBox.Text = "SRB";
                                break;
                            }
                        default:
                            app.ChangeLanguage("en-US");
                            break;
                    }
                }
                SetLanguage();
            }
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
