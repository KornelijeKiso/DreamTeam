using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
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
        public OwnerVM Owner { get; set; }
        public Guest1GradeVM GuestGrade { get; set; }
        public ReservationVM Reservation { get; set; }
        public GradeGuestWindow(ReservationVM reservation, OwnerVM owner)
        {
            InitializeComponent();
            DataContext = this;
            Owner = owner;
            GuestGrade = new Guest1GradeVM();
            Reservation = reservation;
            GuestGrade.ReservationId = reservation.Id;
            GuestGrade.Reservation = reservation;
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
            Owner.GradeAGuest(GuestGrade);
            MessageBox.Show("You have successfully graded your guest.");
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
