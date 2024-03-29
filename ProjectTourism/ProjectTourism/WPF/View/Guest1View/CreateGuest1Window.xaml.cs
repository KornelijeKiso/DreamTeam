﻿using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
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
using ProjectTourism.DTO;

namespace ProjectTourism.View.Guest1View
{
    /// <summary>
    /// Interaction logic for CreateGuest1Window.xaml
    /// </summary>
    public partial class CreateGuest1Window : Window, INotifyPropertyChanged
    {
        public Guest1VM Guest1VM { get; set; }
        public UserDTO UserVM { get; set; }
        public Guest1Service Guest1Service { get; set; }
        public UserService UserService { get; set; }

        public CreateGuest1Window(UserDTO userVM)
        {
            InitializeComponent();
            DataContext = this;
            Guest1VM = new Guest1VM(new Guest1(userVM.GetUser()));
            UserVM = userVM;
            Guest1Service = new Guest1Service();
            UserService = new UserService();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreateGuestClick(object sender, RoutedEventArgs e)
        {
            if (Guest1VM.GetGuest1().Email != null &&
                Guest1VM.GetGuest1().FirstName != null &&
                Guest1VM.GetGuest1().LastName != null
                )
            {
                UserService.Add(UserVM);
                Guest1Service.Add(Guest1VM.GetGuest1());
                Close();
            }
            else
            {
                MessageBox.Show("Niste dobro uneli podatke.");
            }

        }
        public void Update()
        {
        
        }
    }
}
