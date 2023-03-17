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

namespace ProjectTourism.View.OwnerView
{
    /// <summary>
    /// Interaction logic for MainOwnerWindow.xaml
    /// </summary>
    public partial class MainOwnerWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Owner Owner { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation SelectedReservation { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public OwnerController OwnerController { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public Accommodation NewAccommodation { get; set; }
        public Location NewLocation { get; set; }
        public LocationDAO LocationDAO { get; set; }
        public List<string> Deadlines { get; set; } 
        public Dictionary<string, int> Days { get; set; }
        public MainOwnerWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            InitializeDays();
            ComboDeadline.SelectedItem = Deadlines[0];

            LocationDAO = new LocationDAO();
            OwnerController = new OwnerController();
            AccommodationController = new AccommodationController();

            NewAccommodation = new Accommodation();
            NewLocation = new Location();

            Owner = OwnerController.GetOne(username);
            NewAccommodation.Owner = Owner;
            NewAccommodation.OwnerUsername = username;

            Accommodations = new ObservableCollection<Accommodation>(OwnerController.GetOwnersAccommodations(username));
            Reservations = SortReservations();

            AccommodationController.Subscribe(this);
            LocationDAO.Subscribe(this);
            SetButtons();
        }

        private void InitializeDays()
        {
            Deadlines = new List<String>() { "1 day", "3 days", "7 days", "14 days", "1 month", "3 months", "6 months" };
            Days = new Dictionary<string, int>();
            foreach (var item in Deadlines)
            {
                Days.Add(item, InDays(item));
            }
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

        private ObservableCollection<Reservation> SortReservations()
        {
            List<Reservation> sortedReservations = OwnerController.GetOwnersReservations(Owner.Username);
            sortedReservations.Sort((x, y) => y.StartDate.CompareTo(x.StartDate));
            return new ObservableCollection<Reservation>(sortedReservations);
        }

        private void SetButtons()
        {
            foreach(var reservation in Reservations)
            {
                if (reservation.IsGraded())
                {
                    reservation.CanBeGraded = false;
                }
                else if (reservation.IsAbleToGrade())
                {
                    reservation.CanBeGraded = true;
                }
                else
                {
                    reservation.CanBeGraded = false;
                }
            }
        }
        public void AreAllGuestsGraded(object sender, EventArgs e)
        {
            foreach (var reservation in Reservations)
            {
                if (reservation.IsAbleToGrade() && !reservation.IsGraded())
                {
                    MessageBox.Show("There are guests who are waiting to be graded.");
                    break;
                }
            }
        }
        private void HandleTypeCombobox()
        {
            if (ComboType.SelectedIndex == 0)
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.APARTMENT;
            }
            else if (ComboType.SelectedIndex == 1)
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
            HandleTypeCombobox();
            Location location = new Location(NewLocation.City, NewLocation.Country);
            location.Id = LocationDAO.AddAndReturnId(location);
            NewLocation.Id = location.Id;
            NewAccommodation.SetLocation(location);
            NewAccommodation.CancellationDeadline = Days[(string)ComboDeadline.SelectedValue];

            Accommodation accommodation= new Accommodation(NewAccommodation);

            AccommodationController.Add(accommodation);
            Accommodations.Add(accommodation);
            NewAccommodation.Reset();
            NewLocation.Reset();
        }

        public void Update()
        {
            Accommodations = new ObservableCollection<Accommodation>(OwnerController.GetOwnersAccommodations(Owner.Username));
            foreach(var reservation in Reservations)
            {
                reservation.IsGraded();
            }
        }

        public void IncreaseMaxNumberOfGuestsClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.MaxNumberOfGuests < 15)
            {
                NewAccommodation.MaxNumberOfGuests++;
            }
        }
        public void DecreaseMaxNumberOfGuestsClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.MaxNumberOfGuests > 1)
            {
                NewAccommodation.MaxNumberOfGuests--;
            }
        }
        public void IncreaseMinDaysForReservationClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.MinDaysForReservation < 7)
            {
                NewAccommodation.MinDaysForReservation++;
            }
        }
        public void DecreaseMinDaysForReservationClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.MinDaysForReservation > 1)
            {
                NewAccommodation.MinDaysForReservation--;
            }
        }
        public void EventSetter_OnHandler(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @SelectedAccommodation.PictureURLs, UseShellExecute = true });
        }
        public void GradeGuestClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (SelectedReservation == null)
            {
                button.IsEnabled = false;
            }
            else if (SelectedReservation.IsGraded())
            {
                button.IsEnabled = false;
            }
            else if (SelectedReservation.IsAbleToGrade())
            {
                button.IsEnabled = true;
                GradeGuestWindow gradeGuestWindow = new GradeGuestWindow(SelectedReservation.Id);
                gradeGuestWindow.ShowDialog();
                if (gradeGuestWindow.Graded)
                {
                    button.IsEnabled = false;
                }
                
            }
            Update();
        }
        
    }
}
