using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TourStatistics.xaml
    /// </summary>
    public partial class TourStatisticsWindow : UserControl
    {
        public GuideVM Guide { get; set; }
        public List<TourVM> SortedToursList { get; set; }
        public TourVM SelectedTour { get; set; }
        public ObservableCollection<TourVM> SortedTours { get; set; }
        public ObservableCollection<double> TourPercentage { get; set; }
        public int SelectedYear { get; set; }
        public List<int> Years { get; set; }
        public TourStatisticsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;

            SetModels(username);
            SetVisits();
            SetComboBox();
            SortedTours = new ObservableCollection<TourVM>(SortedToursList);
            TourPercentage = new ObservableCollection<double>();
            StatsLabels.Visibility = Visibility.Collapsed;
        }
        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateTicketPercentage(SelectedTour);
            StatsLabels.Visibility = Visibility.Visible;
        }
        private void SetModels(string username)
        {
            Guide = new GuideVM(username);
            SortedTours = new ObservableCollection<TourVM>();
            TourPercentage = new ObservableCollection<double>();
            SortedToursList = new List<TourVM>(Guide.Tours);
            Years = new List<int>(GetYears());
        }
        private void SetVisits()
        {
            SortedToursList.ForEach(tour => { tour.Visits = tour.TourAppointments.Sum(tourApp => tourApp.Tickets.Sum(ticket => ticket.NumberOfGuests)); });
            SortedToursList.Sort((t1, t2) => t2.Visits.CompareTo(t1.Visits));
        }
        private void SetComboBox()
        {
            comboboxYears.ItemsSource = Years;
            comboboxYears.SelectedIndex = Years.IndexOf(DateTime.Now.Year);
        }
        private void YearsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedYear = (int)comboboxYears.SelectedItem;

            SortedToursList = new List<TourVM>(Guide.Tours.Where(tour => tour.TourAppointments.Any(tourApp => tourApp.TourDateTime.Year == SelectedYear)));
            SetVisits();
            Update();
        }
        private List<int> GetYears()
        {
            return SortedToursList.SelectMany(tour => tour.TourAppointments).Select(tourApp => tourApp.TourDateTime.Year).Distinct().OrderByDescending(year => year).ToList();
        }
        private void CalculateTicketPercentage(TourVM SelectedTour)
        {
            int tickets = SelectedTour.TourAppointments.Sum(tourApp => tourApp.Tickets.Count);
            int vouchers = SelectedTour.TourAppointments.Sum(tourApp => tourApp.Tickets.Count(ticket => ticket.HasVoucher));

            TourPercentage.Clear();

            double TicketPercentage = tickets != 0 ? (double)(tickets - vouchers) / tickets * 100 : 0;
            double VoucherPercentage = tickets != 0 ? (double)vouchers / tickets * 100 : 0;

            TourPercentage.Add(Math.Round(TicketPercentage, 2));
            TourPercentage.Add(Math.Round(VoucherPercentage, 2));
        }
        private void Update()
        {
            SortedTours.Clear();
            foreach (var tour in SortedToursList)
            {
                SortedTours.Add(tour);
            }
        }
    }
}
