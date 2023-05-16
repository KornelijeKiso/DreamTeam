using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel;

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
    }
}
