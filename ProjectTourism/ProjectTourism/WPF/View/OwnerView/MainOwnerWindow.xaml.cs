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

            InitializeDays();
            InitializeTypes();

            InitializeControllers();
            InitializeNewEntities();

            SetOwner(username);

            LoadDataGrids(username);

            Subscribe();
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

        private void InitializeTypes()
        {
            Types = new List<string>() { "Apartment", "House", "Hut" };
            ComboType.SelectedItem = Types[0];
        }
        private void InitializeDays()
        {
            Deadlines = new List<String>() { "1 day", "3 days", "7 days", "14 days", "1 month", "3 months", "6 months" };
            Days = new Dictionary<string, int>();
            foreach (var item in Deadlines)
            {
                Days.Add(item, InDays(item));
            }
            ComboDeadline.SelectedItem = Deadlines[0];
        }

        private int InDays(string deadline)
        {
            int days = int.Parse(deadline.Split(' ')[0]);
            string multiply_unit = deadline.Split(" ")[1];
            int multiplyer = 1;
            if(multiply_unit.Equals("month") || multiply_unit.Equals("months"))
            {
                multiplyer = 30;
            }
            return days * multiplyer;
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
        private void HandleTypeCombobox()
        {
            if ((string)ComboType.SelectedItem == "Apartment")
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.APARTMENT;
            }
            else if ((string)ComboType.SelectedItem == "House")
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.HOUSE;
            }
            else
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.HUT;
            }
        }
        public void RegisterAccommodationClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.IsValid && NewLocation.IsValid) 
            {
                RegisterNewAccommodation();
            }
            else
            {
                MessageBox.Show("Not all fields are filled correctly.");
            }
        }

        private void RegisterNewAccommodation()
        {
            HandleTypeCombobox();
            LocationVM location = new LocationVM(NewLocation.City, NewLocation.Country);
            location.Id = LocationService.AddAndReturnId(location);
            NewLocation.Id = location.Id;
            NewAccommodation.SetLocation(location);
            NewAccommodation.CancellationDeadline = Days[(string)ComboDeadline.SelectedValue];

            AccommodationService.Add(NewAccommodation);
            AccommodationVM accommodation = new AccommodationVM(NewAccommodation);
            Owner.Accommodations.Add(accommodation);
            Accommodations.Add(accommodation);
            Reset();
        }

        private void Reset()
        {
            NewAccommodation.Reset();
            NewLocation.Reset();
            ComboDeadline.SelectedItem = Deadlines[0];
            ComboType.SelectedItem = Types[0];
        }

        public void Update()
        {
            Accommodations = new ObservableCollection<AccommodationVM>(Owner.Accommodations);
        }

        public void IncreaseMaxNumberOfGuestsClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.MaxNumberOfGuests < 15)
            {
                NewAccommodation.MaxNumberOfGuests++;
                if(NewAccommodation.MaxNumberOfGuests==15) IncDecButtons[0] = false;
                IncDecButtons[1] = true;
            }
            else
            {
                IncDecButtons[0] = false;
            }
        }
        public void DecreaseMaxNumberOfGuestsClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.MaxNumberOfGuests > 1)
            {
                NewAccommodation.MaxNumberOfGuests--;
                if (NewAccommodation.MaxNumberOfGuests == 1) IncDecButtons[1] = false;
                IncDecButtons[0] = true;
            }
            else
            {
                IncDecButtons[1] = false;
            }
        }
        public void IncreaseMinDaysForReservationClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.MinDaysForReservation < 7)
            {
                NewAccommodation.MinDaysForReservation++;
                if (NewAccommodation.MinDaysForReservation == 7) IncDecButtons[2] = false;
                IncDecButtons[3] = true;
            }
            else
            {
                IncDecButtons[2] = false;
            }
        }
        public void DecreaseMinDaysForReservationClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.MinDaysForReservation > 1)
            {
                NewAccommodation.MinDaysForReservation--;
                if (NewAccommodation.MinDaysForReservation == 1) IncDecButtons[3] = false;
                IncDecButtons[2] = true;
            }
            else
            {
                IncDecButtons[3] = false;
            }
        }
        public void GradeGuestClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            GradeGuestWindow gradeGuestWindow = new GradeGuestWindow(SelectedReservation, Owner);
            gradeGuestWindow.ShowDialog();
            SelectedReservation.Graded = gradeGuestWindow.Graded;
            if (gradeGuestWindow.Graded)
            {
                Owner = gradeGuestWindow.Owner;
                button.IsEnabled = false;
                SelectedReservation.VisibleReview = SelectedReservation.AccommodationGraded;
            }
            Update();
        }

        public void SeeReviewClick(object sender, RoutedEventArgs e)
        {
            Guest1ReviewWindow guestsReviewWindow = new Guest1ReviewWindow(SelectedReservation.AccommodationGrade);
            guestsReviewWindow.ShowDialog();
        }
        
    }
}
