using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
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

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for PostponeRequestWindow.xaml
    /// </summary>
    public partial class PostponeRequestWindow : Window
    {
        public ReservationDTO Reservation { get; set; }
        public PostponeRequestWindow(ReservationDTO reservation)
        {
            InitializeComponent();
            DataContext = this;
            Reservation = reservation;
        }
        public void AcceptClick(object sender, RoutedEventArgs e)
        {
            Reservation.PostponeRequest.Accepted = true;
            Reservation.StartDate = Reservation.PostponeRequest.NewStartDate;
            Reservation.EndDate = Reservation.PostponeRequest.NewEndDate;
            Reservation.PostponeRequest.Update();
            Reservation.Update();
            Reservation.RequestedPostpone = false;
            Close();
        }

        public void RejectClick(object sender, RoutedEventArgs e)
        {
            RejectingPostponeMessageWindow rejectingPostponeMessageWindow = new RejectingPostponeMessageWindow();
            rejectingPostponeMessageWindow.ShowDialog();
            if (rejectingPostponeMessageWindow.Submited)
            {

                Reservation.PostponeRequest.Rejected = true;
                Reservation.PostponeRequest.AdditionalComment = rejectingPostponeMessageWindow.Message;
                Reservation.PostponeRequest.Update();
                Reservation.RequestedPostpone = false;
                Close();
            }
        }
    }
}
