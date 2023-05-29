using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public OwnerDTO Owner { get; set; }
        public Guest1GradeDTO GuestGrade { get; set; }
        public ReservationDTO Reservation { get; set; }
        public GradeGuestWindow(ReservationDTO reservation, OwnerDTO owner)
        {
            InitializeComponent();
            DataContext = this;
            Owner = owner;
            GuestGrade = new Guest1GradeDTO();
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
            Guest1GradeService guest1GradeService = new Guest1GradeService();
            var reservation = Owner.Reservations.ToList().Find(r => r.Id == GuestGrade.ReservationId);
            reservation.Guest1Grade = GuestGrade;
            reservation.Graded = true;
            reservation.CanBeGraded = false;
            reservation.VisibleReview = reservation.AccommodationGraded;
            guest1GradeService.Add(GuestGrade.GetGuest1Grade());
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
