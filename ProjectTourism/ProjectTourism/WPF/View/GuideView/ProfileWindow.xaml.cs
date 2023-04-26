﻿using System;
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
    public partial class ProfileWindow : UserControl
    {
        public GuideVM Guide { get; set; }
        private List<UIElement> profileUIElements;
        public ProfileWindow(string username)
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
        private void TourStatistics_Click(object sender, RoutedEventArgs e)
        {
            HideProfilesContent();
            ContentArea.Content = new TourStatisticsWindow(Guide.Username);
        }
        private void HideProfilesContent()
        {
            profileUIElements = new List<UIElement>
            {
                NameSurnameLabel, usernamelabel, EmailLabel, PhoneNumberLabel, LanguagesLabel,
                BioLabel, MaxGuestsLabel, DarkThemeLabel, textbox1, textbox2, textbox3, textbox4,
                textbox5, toggleSwitch, rectangle, LinkSignOut, TourStatsLink
            };
            profileUIElements.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        private void toggleSwitch_Checked(object sender, RoutedEventArgs e) { }
        private void toggleSwitch_Unchecked(object sender, RoutedEventArgs e) { }
    }
}