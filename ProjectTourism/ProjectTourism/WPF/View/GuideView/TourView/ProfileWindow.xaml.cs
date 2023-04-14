using System;
using System.Collections.Generic;
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
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : UserControl
    {
        public GuideVM Guide { get; set; }
        public ProfileWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void TourStatistics_Click(object sender, RoutedEventArgs e)
        {
            HideProfilesContent();
            ContentArea.Content = new TourStatisticsWindow();
        }
        private void HideProfilesContent()
        {
            NameSurnameLabel.Visibility = Visibility.Collapsed;
            usernamelabel.Visibility = Visibility.Collapsed;
            EmailLabel.Visibility = Visibility.Collapsed;
            PhoneNumberLabel.Visibility = Visibility.Collapsed;
            LanguagesLabel.Visibility = Visibility.Collapsed;
            BioLabel.Visibility = Visibility.Collapsed;
            MaxGuestsLabel.Visibility = Visibility.Collapsed;
            DarkThemeLabel.Visibility = Visibility.Collapsed;
            textbox1.Visibility = Visibility.Collapsed;
            textbox2.Visibility = Visibility.Collapsed;
            textbox3.Visibility = Visibility.Collapsed;
            textbox4.Visibility = Visibility.Collapsed;
            textbox5.Visibility = Visibility.Collapsed;
            toggleSwitch.Visibility = Visibility.Collapsed;
            rectangle.Visibility = Visibility.Collapsed;
            ContentArea.Visibility = Visibility.Collapsed;
            LinkSignOut.Visibility = Visibility.Collapsed;
        }
        private void toggleSwitch_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void toggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
