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
using System.Windows.Shapes;
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.View.GuideView.RouteView
{
    /// <summary>
    /// Interaction logic for RouteStopsWindow.xaml
    /// </summary>
    public partial class RouteStopsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public RouteController RouteController { get; set; }
        public Route Route { get; set; }
        public RouteStopsWindow(int id)
        {
            InitializeComponent();
            DataContext = this;
            RouteController = new RouteController();
            Route = RouteController.GetOne(id);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            //TODO
            throw new NotImplementedException();
        }
        public int pom = 0;
        private void StopPassedButton_Click(object sender, RoutedEventArgs e)
        {
            if(Route.StopsList.Count-1 == pom)
            {
                pom = 0;
            }
            else
            {
                StopTextBox.Text = RouteController.GetNextStop(Route, pom);
                pom++;
                if(pom == Route.StopsList.Count-1)
                {
                    StopPassedButton.IsEnabled = false;
                }
            }
        }
        private void EmergencyStopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}