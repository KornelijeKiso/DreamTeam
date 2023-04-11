﻿using ProjectTourism.WPF.ViewModel;
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

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for PostponeRequestWindow.xaml
    /// </summary>
    public partial class PostponeRequestWindow : Window
    {
        public ReservationVM Reservation { get; set; }
        public PostponeRequestWindow(ReservationVM reservation)
        {
            InitializeComponent();
            DataContext = this;
            Reservation = reservation;
        }
        public void AcceptClick(object sender, RoutedEventArgs e)
        {

        }
        public void RejectClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
