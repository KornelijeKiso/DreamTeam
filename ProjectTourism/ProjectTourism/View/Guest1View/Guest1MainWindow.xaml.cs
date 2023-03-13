﻿using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProjectTourism.View.Guest1View
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainWindow : Window
    {
        public Guest1 Guest1 { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Guest1Controller Guest1Controller { get; set; }
        public AccommodationController AccommodationController { get; set; }


        public Guest1MainWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1Controller = new Guest1Controller();
            Guest1 = Guest1Controller.GetOne(username);
            AccommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(AccommodationController.GetAll());
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}