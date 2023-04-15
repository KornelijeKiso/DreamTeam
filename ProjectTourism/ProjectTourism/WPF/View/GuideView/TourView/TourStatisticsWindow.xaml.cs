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
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TourStatistics.xaml
    /// </summary>
    public partial class TourStatisticsWindow : UserControl
    {
        public GuideVM Guide { get; set; }
        public List<TourVM> SortedTours { get; set; }
        public TourStatisticsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);

            SortedTours = new List<TourVM>(Guide.Tours);

            foreach (var tour in SortedTours)
            {
                int totalVisits = 0;
                foreach (var tourApp in tour.TourAppointments)
                {
                    foreach (var ticket in tourApp.Tickets)
                    {
                        totalVisits += ticket.NumberOfGuests;
                    }
                }
                tour.Visits = totalVisits;
            }
            SortedTours.Sort((t1, t2) => t2.Visits.CompareTo(t1.Visits));
        }
    }
}
