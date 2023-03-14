using ProjectTourism.Controller;
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
            GuestGrade.Grades["Communication"] = int.Parse(Communication.Text);
            GuestGrade.Grades["Following the rules"] = int.Parse(FollowingTheRules.Text);
            GuestGrade.Grades["Cleanness"] = int.Parse(Cleanness.Text);
            GuestGrade.Comment = Comment.Text;
            Guest1GradeCotroller.Add(GuestGrade);
            Graded = true;
            Close();
        }
    }
}
