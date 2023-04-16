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
        public List<TourAppointmentVM> TourApps { get; set; }
        public TourAppointmentVM SelectedTourApp { get; set; }
        public ObservableCollection<TourAppointmentVM> TourAppsObs { get; set; }
        public ObservableCollection<double> TourAppPercentage { get; set; }
        public int SelectedYear { get; set; }
        public List<int> Years { get; set; }
        public TourStatisticsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;

            SetModels(username);
            SetVisits();
            SetComboBox();
            
            StatsLabels.Visibility = Visibility.Collapsed;
        }
        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            FieldSet.Header = SelectedTourApp.Tour.Name;
            CalculateTicketPercentage(SelectedTourApp);
            StatsLabels.Visibility = Visibility.Visible;
        }
        private void SetModels(string username)
        {
            Guide = new GuideVM(username);
            TourAppsObs = new ObservableCollection<TourAppointmentVM>();
            TourAppPercentage = new ObservableCollection<double>();
            TourApps = new List<TourAppointmentVM>(Guide.TourAppointments);
            TourAppsObs = new ObservableCollection<TourAppointmentVM>(TourApps);
            Years = new List<int>(GetYears());
        }

        private void SetVisits()
        {
            foreach (var tourApp in TourApps)
    {
                tourApp.Visits = tourApp.Tickets.Count;
            }
            TourApps.Sort((t1, t2) => t2.Visits.CompareTo(t1.Visits));
        }
        private void SetComboBox()
        {
            comboboxYears.ItemsSource = Years;
            comboboxYears.SelectedIndex = Years.IndexOf(DateTime.Now.Year);
        }
        private void YearsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedYear = (int)comboboxYears.SelectedItem;
         
            TourApps.Clear();   foreach (var tourApp in Guide.TourAppointments)
            {
                if (tourApp.TourDateTime.Year == SelectedYear)
                    TourApps.Add(tourApp);
            }
            SetVisits();
            Update();
        }
        private List<int> GetYears()
        {
            List<int> years = new List<int>();


            foreach (var tourApp in TourApps)
            {
                int year = tourApp.TourDateTime.Year;
                if (!years.Contains(year))
                    years.Add(year);
            }
            years.Sort();
            years.Reverse();

            return years;
        }

        private void CalculateTicketPercentage(TourAppointmentVM SelectedTourApp)
        {
            int tickets = 0;
            int vouchers = 0;


            tickets += SelectedTourApp.Tickets.Count;
            vouchers += SelectedTourApp.Tickets.Count(ticket => ticket.HasVoucher);
            

            TourAppPercentage.Clear();

            double TicketPercentage = tickets != 0 ? (double)(tickets - vouchers) / tickets * 100 : 0;
            double VoucherPercentage = tickets != 0 ? (double)vouchers / tickets * 100 : 0;

            TourAppPercentage.Add(Math.Round(TicketPercentage, 2));
            TourAppPercentage.Add(Math.Round(VoucherPercentage, 2));
        }

        private void Update()
        {
            TourAppsObs.Clear();
            foreach (var tourApp in TourApps)
            {
                TourAppsObs.Add(tourApp);
            }
        }
    }
}
