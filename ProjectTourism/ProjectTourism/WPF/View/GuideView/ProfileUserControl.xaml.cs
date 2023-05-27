using System.Windows;
using System.Windows.Controls;
using ProjectTourism.DTO;
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
    }
}
