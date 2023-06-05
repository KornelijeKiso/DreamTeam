using ProjectTourism.Services;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.View.OwnerView;
using ProjectTourism.WPF.ViewModel.OwnerViewModel;
using ProjectTourism.DTO;

namespace ProjectTourism.View.OwnerView
{
    public partial class MainOwnerWindow : Window
    {
        public string Username { get; set; }
        public MainOwnerWindow(string username)
        {
            InitializeComponent();
            Username = username;
            DataContext = new MainOwnerWindowVM(username);
        }
        public MainOwnerWindow(string username, int i)
        {
            InitializeComponent();
            Username = username;
            DataContext = new MainOwnerWindowVM(username, i);
        }
        public void AreAllGuestsGraded(object sender, RoutedEventArgs e)
        {
            foreach (var reservation in new OwnerDTO(Username).Reservations)
            {
                if (reservation.IsAbleToGrade() && !reservation.Graded)
                {
                    MessageBox.Show("There are guests who are waiting to be graded.");
                    break;
                }
            }
        }
    }
}
