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

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for GradeAccommodatonWindow.xaml
    /// </summary>
    public partial class GradeAccommodatonWindow : Window
    {
        public Guest1VM Guest1VM { get; set; }
        public AccommodationGradeVM AccommodationGradeVM { get; set; }
        public ReservationVM ReservationVM { get; set; }
        public GradeAccommodatonWindow(ReservationVM reservationVM, string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1VM = new Guest1VM(username);
            AccommodationGradeVM = new AccommodationGradeVM(new AccommodationGrade());
            ReservationVM = reservationVM;
            AccommodationGradeVM.ReservationId = reservationVM.Id;
            AccommodationGradeVM.Reservation = reservationVM;
        }

        private void GradeClick(object sender, RoutedEventArgs e)
        {
            GradePriceAndQuality();
            GradeComfort();
            GradeLocation();
            GradeHospitality();
            GradeCleanness();


            foreach (var category in AccommodationGradeVM.CategoryNames)
            {
                if (AccommodationGradeVM.Grades[category] == 0)
                {
                    MessageBox.Show("You have to grade each of listed categories.");
                    return;
                }
            }
            Guest1VM.GradeAccommodation(AccommodationGradeVM);
            MessageBox.Show("You've successfully graded this accommodation");
            Close();
        }

        private void GradePriceAndQuality()
        {
            foreach (RadioButton radioButton in PriceAndQualityRatio.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    AccommodationGradeVM.Grades["Price and quality ratio"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }

        private void GradeComfort()
        {
            foreach (RadioButton radioButton in Comfort.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    AccommodationGradeVM.Grades["Comfort"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }
        private void GradeLocation()
        {
            foreach (RadioButton radioButton in Location.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    AccommodationGradeVM.Grades["Location"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }
        private void GradeHospitality()
        {
            foreach (RadioButton radioButton in Hospitality.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    AccommodationGradeVM.Grades["Hospitality"] = Convert.ToInt32(radioButton.Content);
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
                    AccommodationGradeVM.Grades["Cleanness"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }

    }
}
