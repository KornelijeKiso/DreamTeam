﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.View.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : UserControl, INotifyPropertyChanged, IObserver
    {
        public GuideVM Guide { get; set; }
        public HomeWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        public void Update()
        {

        }
        private void AddNewTourButton_Click(object sender, RoutedEventArgs e)
        {
            HideHomeContent();
            ContentArea.Content = new CreateTourWindow(Guide);
        }
        private void HideHomeContent()
        {
            HomeLabel.Visibility = Visibility.Hidden;
            WelcomeLabel.Visibility = Visibility.Hidden;
            UpcomingLabel.Visibility = Visibility.Hidden;
            AddNewTourButton.Visibility = Visibility.Hidden;
            AllToursButton.Visibility = Visibility.Hidden;
        }
        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            HideHomeContent();
            ContentArea.Content = new ViewAllToursWindow(Guide.Username);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}