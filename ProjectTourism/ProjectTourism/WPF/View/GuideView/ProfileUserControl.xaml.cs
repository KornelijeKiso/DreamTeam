using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectTourism.DTO;
using ProjectTourism.Localization;
using ProjectTourism.View.GuideView;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class ProfileUserControl : UserControl
    {
        public GuideDTO Guide { get; set; }
        public ProfileUserControl(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideDTO(username);
        }
        private void SignOutLink_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Close();
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            Guide.ChangeTheme();
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Close();
            MainGuideWindow mainGuideWindow = new MainGuideWindow(Guide.Username);
            mainGuideWindow.Show();
        }

        string GetLocalizedErrorMessage(string resourceKey)
        {
            TextBlock Templabel = new TextBlock();
            LocExtension locExtension = new LocExtension(resourceKey);
            BindingOperations.SetBinding(Templabel, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
            return Templabel.Text;
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(GetLocalizedErrorMessage("QuitQuestion"), GetLocalizedErrorMessage("Quit"), MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(GetLocalizedErrorMessage("QuitSuccessful"));
                Guide.Quit();
            }
        }
    }
}
