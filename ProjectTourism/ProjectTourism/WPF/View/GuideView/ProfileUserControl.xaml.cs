using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class ProfileUserControl : UserControl
    {
        public GuideVM Guide { get; set; }
        private List<UIElement> profileUIElements;
        public ProfileUserControl(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void SignOutLink_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Close();
        }
        private void toggleSwitch_Checked(object sender, RoutedEventArgs e) { }
        private void toggleSwitch_Unchecked(object sender, RoutedEventArgs e) { }
    }
}
