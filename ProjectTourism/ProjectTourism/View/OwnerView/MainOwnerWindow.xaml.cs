using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public MainOwnerWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            OwnerController = new OwnerController();
            Owner = OwnerController.GetOne(username);
            NewAccommodation = new Accommodation();
            Accommodations = new ObservableCollection<Accommodation>(OwnerController.GetOwnersAccommodations(username));
            AccommodationController= new AccommodationController();
            Reservations = SortReservations();
            NewAccommodation.Owner = Owner;
            NewAccommodation.OwnerUsername= username;
            LocationDAO= new LocationDAO();
            NewLocation = new Location();
            AccommodationController.Subscribe(this);
            LocationDAO.Subscribe(this);
            
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
        public void RegisterAccommodationClick(object sender, RoutedEventArgs e)
        {
            if(ComboType.SelectedIndex== 0)
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.APARTMENT;
            }
            else if(ComboType.SelectedIndex== 1)
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.HOUSE;
            }
            else
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.HUT;
            }
            NewLocation.Id = LocationDAO.AddAndReturnId(NewLocation);
            NewAccommodation.LocationId = NewLocation.Id;
            NewAccommodation.Location = NewLocation;
            AccommodationController.Add(NewAccommodation);
            Accommodations.Add(NewAccommodation);
        }

        public void Update()
        {
            Accommodations = new ObservableCollection<Accommodation>(OwnerController.GetOwnersAccommodations(Owner.Username));
            foreach(var reservation in Reservations)
            {
                reservation.IsGraded();
            }
        }
        public void SelectedReservationChanged(object sender, SelectionChangedEventArgs e)
        {
            gradeButton.IsEnabled = false;
            if(SelectedReservation == null)
            {
                gradeButton.IsEnabled = false;
            }
            else if(SelectedReservation.IsGraded())
            {
                gradeButton.IsEnabled = false;
            }
            else if(SelectedReservation.IsAbleToGrade())
            {
                gradeButton.IsEnabled = true;
            }
        }

        public void GradeGuestClick(object sender, RoutedEventArgs e)
        {
            GradeGuestWindow gradeGuestWindow = new GradeGuestWindow(SelectedReservation.Id);
            gradeGuestWindow.ShowDialog();
            gradeButton.IsEnabled=false;
            Update();
        }
    }
}
