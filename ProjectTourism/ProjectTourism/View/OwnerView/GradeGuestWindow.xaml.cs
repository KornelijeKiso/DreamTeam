﻿using ProjectTourism.Controller;
using ProjectTourism.Model;
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

namespace ProjectTourism.View.OwnerView
{
    /// <summary>
    /// Interaction logic for GradeGuestWindow.xaml
    /// </summary>
    public partial class GradeGuestWindow : Window
    {
        public Guest1GradeCotroller Guest1GradeCotroller { get; set; }
        public ReservationController ReservationController { get; set; }  
        public List<Guest1Grade> Guest1Grades { get; set; }
        public Guest1Grade GuestGrade { get; set; }
        public bool Graded;
        public GradeGuestWindow(int reservationId)
        {
            InitializeComponent();
            DataContext = this;
            ReservationController= new ReservationController();
            Guest1GradeCotroller = new Guest1GradeCotroller();
            Guest1Grades = Guest1GradeCotroller.GetAll();
            GuestGrade = new Guest1Grade();
            GuestGrade.ReservationId = reservationId;
            
        }

        private void GradeClick(object sender, RoutedEventArgs e)
        {
            GradeCleanness();
            GradeCommunication();
            GradeFollowingTheRules();
            foreach(var category in Guest1Grade.CategoryNames)
            {
                if (GuestGrade.Grades[category] == 0)
                {
                    MessageBox.Show("You have to grade each of listed categories.");
                    return;
                }
            }
            Guest1GradeCotroller.Add(GuestGrade);
            Graded = true;
            Close();
        }

        private void GradeFollowingTheRules()
        {
            foreach (RadioButton radioButton in FollowingTheRules.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    GuestGrade.Grades["Following the rules"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }

        private void GradeCommunication()
        {
            foreach (RadioButton radioButton in Communication.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    GuestGrade.Grades["Communication"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }

        private void GradeCleanness()
        {
            foreach (RadioButton radioButton in Cleanness.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    GuestGrade.Grades["Cleanness"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }
    }
}
