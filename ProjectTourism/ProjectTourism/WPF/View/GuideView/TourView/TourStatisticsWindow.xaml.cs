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
        public ObservableCollection<TourVM> SortedTours { get; set; }
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
        }
        private void YearsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedYear = (int)comboboxYears.SelectedItem;

            SortedToursList = new List<TourVM>(Guide.Tours.Where(tour => tour.TourAppointments.Any(tourApp => tourApp.TourDateTime.Year == SelectedYear)));
            SetVisits();
            Update();
        }
        private void SetComboBox()
        {
            comboboxYears.ItemsSource = Years;
            comboboxYears.SelectedIndex = Years.IndexOf(DateTime.Now.Year);
        }
        private void SetModels(string username)
        {
            Guide = new GuideVM(username);
            SortedTours = new ObservableCollection<TourVM>();
            SortedToursList = new List<TourVM>(Guide.Tours);
            Years = new List<int>(GetYears());
        }
        private void SetVisits()
        {
            SortedToursList.ForEach(tour =>{ tour.Visits = tour.TourAppointments.Sum(tourApp => tourApp.Tickets.Sum(ticket => ticket.NumberOfGuests));});
            SortedToursList.Sort((t1, t2) => t2.Visits.CompareTo(t1.Visits));
        }
        private List<int> GetYears()
        {
            return SortedToursList.SelectMany(tour => tour.TourAppointments).Select(tourApp => tourApp.TourDateTime.Year).Distinct().OrderByDescending(year => year).ToList();
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
