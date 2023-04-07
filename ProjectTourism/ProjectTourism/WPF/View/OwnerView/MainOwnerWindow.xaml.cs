using ProjectTourism.Services;
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.View.OwnerView;

namespace ProjectTourism.View.OwnerView
{
    /// <summary>
    /// Interaction logic for MainOwnerWindow.xaml
    /// </summary>
    public partial class MainOwnerWindow : Window, INotifyPropertyChanged, IObserver
    {
        public OwnerVM Owner { get; set; }
        public ObservableCollection<AccommodationVM> Accommodations { get; set; }
        public ObservableCollection<ReservationVM> Reservations { get; set; }
        public ReservationVM SelectedReservation { get; set; }
        public AccommodationVM SelectedAccommodation { get; set; }
        public OwnerService OwnerService { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public AccommodationVM NewAccommodation { get; set; }
        public LocationVM NewLocation { get; set; }
        public LocationService LocationService { get; set; }
        public List<string> Deadlines { get; set; } 
        public List<string> Types { get; set; }
        public Dictionary<string, int> Days { get; set; }
        public ObservableCollection<bool> IncDecButtons { get; set; }
        public List<Guest1GradeVM> Grades { get; set; }
        public Guest1GradeService Guest1GradeService { get; set; }

        public MainOwnerWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            IncDecButtons = new ObservableCollection<bool> { true, false, true, false };

            InitializeControllers();
            InitializeNewEntities();
            Content.Content = new YourAccommodationsMenuItem(username);
            AccommodationsItem.Background = Brushes.LightGreen;
            SetOwner(username);

            LoadDataGrids(username);

            Subscribe();
            if (Owner.IsSuperHost)
            {
                SuperHostIcon.Visibility = Visibility.Visible;
                SuperHostLabel.Visibility = Visibility.Visible;
            }
            else
            {
                SuperHostIcon.Visibility = Visibility.Collapsed;
                SuperHostLabel.Visibility = Visibility.Collapsed;
            }
        }
        public void SwitchToMyAccommodations(object sender, EventArgs e)
        {
            Content.Content = new YourAccommodationsMenuItem(Owner.Username);
            AccommodationsItem.Background = Brushes.LightGreen;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
        }
        public void SwitchToReservations(object sender, EventArgs e)
        {
            Content.Content = new ReservationsMenuItem(Owner.Username);
            AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.LightGreen;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
        }
        private void Subscribe()
        {
            AccommodationService.Subscribe(this);
            LocationService.Subscribe(this);
        }

        private void LoadDataGrids(string username)
        {
            Accommodations = new ObservableCollection<AccommodationVM>(Owner.Accommodations);
            Reservations = SortReservations();
            Grades = new List<Guest1GradeVM>();
            foreach(ReservationVM reservation in Reservations)
            {
                Grades.Add(reservation.Guest1Grade);
            }
        }

        private void SetOwner(string username)
        {
            Owner = OwnerService.GetOne(username);
            NewAccommodation.Owner = Owner;
            NewAccommodation.OwnerUsername = username;
        }

        private void InitializeNewEntities()
        {
            NewAccommodation = new AccommodationVM(new Accommodation());
            NewLocation = new LocationVM(new Location());
        }

        private void InitializeControllers()
        {
            LocationService = new LocationService(new LocationRepository());
            OwnerService = new OwnerService(new OwnerRepository());
            AccommodationService = new AccommodationService(new AccommodationRepository());
            Guest1GradeService = new Guest1GradeService(new Guest1GradeRepository());
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<ReservationVM> SortReservations()
        {
            List<ReservationVM> sortedReservations = Owner.Reservations;
            sortedReservations.Sort((x, y) => y.EndDate.CompareTo(x.EndDate));
            return new ObservableCollection<ReservationVM>(sortedReservations);
        }

        public void AreAllGuestsGraded(object sender, EventArgs e)
        {
            foreach (var reservation in Reservations)
            {
                if (reservation.IsAbleToGrade() && !reservation.Graded)
                {
                    MessageBox.Show("There are guests who are waiting to be graded.");
                    break;
                }
            }
        }

        public void Update()
        {
            Accommodations = new ObservableCollection<AccommodationVM>(Owner.Accommodations);
        }

        
        
    }
}
