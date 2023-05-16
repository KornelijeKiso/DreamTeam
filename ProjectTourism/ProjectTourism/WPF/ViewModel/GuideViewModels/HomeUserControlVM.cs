using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.View.TourView;

namespace ProjectTourism.WPF.ViewModel.GuideViewModels
{
    public class HomeUserControlVM: ViewBase
    {
        public Label HomeLabel { get; set; } = new Label();
        public Label WelcomeLabel { get; set; } = new Label();
        public Label UpcomingLabel { get; set; } = new Label();
        public Label UpcomingLabelName { get; set; } = new Label();
        public Button AddNewTourButton { get; set; } = new Button();
        public Button AllToursButton { get; set; } = new Button();
        public Border ImageBorder { get; set; } = new Border();
        public Image UpcomingImage { get; set; } = new Image();
        public ComboBox LocalizationComboBox { get; set; } = new ComboBox();
        public ContentControl ContentArea { get; set; } = new ContentControl();


        private App app;
        public GuideDTO Guide { get; set; }
        public string UpcomingTourPicture { get; set; }
        public string Username { get; set; }
        public HomeUserControlVM(string username)
        {
            Username = username;
            app = (App)Application.Current;

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
                app.ChangeLanguage("en-US");
            }
            else
            {
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
        private void AddNewTourButton_Click(object parameter)
        {
            HideElements(new List<UIElement> { HomeLabel, WelcomeLabel, UpcomingLabel, UpcomingLabelName, AddNewTourButton, AllToursButton, ImageBorder, UpcomingImage });
            ContentArea.Content = new CreateTourUserControl(Guide);
        }
        private void AllToursButton_Click(object parameter)
        {
            HideElements(new List<UIElement> { HomeLabel, WelcomeLabel, UpcomingLabel, UpcomingLabelName, AddNewTourButton, AllToursButton, ImageBorder, UpcomingImage });
            ContentArea.Content = new AllToursUserControl(Guide.Username);
        }
        private void Localization_SelectionChanged(object parameter)
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
    }
}
