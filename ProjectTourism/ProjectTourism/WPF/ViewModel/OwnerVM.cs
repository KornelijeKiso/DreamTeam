using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProjectTourism.WPF.ViewModel
{
    public class OwnerVM : INotifyPropertyChanged
    {
        private Owner _owner;
        public Timer timer;
        public OwnerVM(Owner owner)
        {
            _owner = owner;
            Accommodations = new ObservableCollection<AccommodationVM>(_owner.Accommodations.Select(r => new AccommodationVM(r)).ToList());
            Reservations = new ObservableCollection<ReservationVM>(_owner.Reservations.Select(r => new ReservationVM(r)).Reverse().ToList());
            SetDestinations();

        }
        public void SetDestinations()
        {
            Dictionary<int, DestinationVM> locations = new Dictionary<int, DestinationVM>();
            foreach(var accommodation in Accommodations)
            {
                if (!locations.ContainsKey(accommodation.LocationId))
                {
                    locations.Add(accommodation.LocationId, new DestinationVM(accommodation.Location));
                }
                locations[accommodation.LocationId].Accommodations.Add(accommodation);
                locations[accommodation.LocationId].Reservations.AddRange(accommodation.Reservations.ToList().Where(r=>r.StartDate<DateOnly.FromDateTime(DateTime.Now) && r.EndDate>DateOnly.FromDateTime(DateTime.Now).AddYears(-1)));
            }
            PopularDestination = locations.Values.ToList().OrderByDescending(a => a.Occupancy).FirstOrDefault();
            UnpopularDestination = locations.Values.ToList().OrderByDescending(a => a.Occupancy).LastOrDefault();
        }
        public DestinationVM PopularDestination { get; set; }
        public DestinationVM UnpopularDestination { get; set; }
        public OwnerVM(string username)
        {
            Synchronize(username);
            timer = new Timer(5000);
            SetTimer();
        }

        public void SetTimer()
        {
            timer.Elapsed += TimerElapsed;
            timer.AutoReset = true;
            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Synchronize(Username);
        }
        public void DismissNotification(NotificationVM notification)
        {
            Notifications.Remove(notification);
            new NotificationService().Dismiss(notification.GetNotification());
        }
        public void DismissAllNotification()
        {
            Notifications.Clear();
            new NotificationService().DismissAll();
        }
        private void Synchronize(string username)
        {
            OwnerService ownerService = new OwnerService();
            AccommodationService accommodationService = new AccommodationService();
            LocationService locationService = new LocationService();
            RenovationService renovationService = new RenovationService();

            _owner = ownerService.GetOne(username);
            _owner.Accommodations = accommodationService.GetAllByOwner(_owner.Username);
            _owner.Reservations = new List<Reservation>();
            foreach (Accommodation accommodation in _owner.Accommodations)
            {
                accommodation.Renovations = renovationService.GetAllByAccommodation(accommodation.Id);
                accommodation.Location = locationService.GetOne(accommodation.LocationId);
                accommodation.Reservations = SynchronizeReservations(accommodation);
                if (accommodation.Reservations != null && accommodation.Reservations.Any())
                {
                    foreach (Reservation reservation in accommodation.Reservations)
                    {
                        _owner.Reservations.Add(reservation);
                    }
                }
            }
            Accommodations = new ObservableCollection<AccommodationVM>(_owner.Accommodations.Select(r => new AccommodationVM(r)).ToList());
            Reservations = new ObservableCollection<ReservationVM>(_owner.Reservations.Select(r => new ReservationVM(r)).ToList());
            SetDestinations();
            Notifications = new ObservableCollection<NotificationVM>(new NotificationService().GetAllByOwner(_owner.Username).Select(r => new NotificationVM(r)).Reverse().ToList());
            HasNewNotifications = Notifications.ToList().Any(n=>n.New);
            NoNotifications = !Notifications.ToList().Any();
        }
        private List<Reservation> SynchronizeReservations(Accommodation accommodation)
        {
            ReservationService reservationService = new ReservationService();
            Guest1GradeService guest1GradeService = new Guest1GradeService();
            Guest1Service guest1Service = new Guest1Service();
            AccommodationGradeService accommodationGradeService = new AccommodationGradeService();
            PostponeRequestService postponeRequestService = new PostponeRequestService();

            List<Reservation> reservations = reservationService.GetAllByAccommodation(accommodation.Id);
            foreach (Reservation reservation in reservations)
            {
                reservation.PostponeRequest = postponeRequestService.GetOneByReservation(reservation.Id);
                reservation.Guest1 = guest1Service.GetOne(reservation.Guest1Username);
                reservation.Accommodation = accommodation;
                reservation.AccommodationGrade = accommodationGradeService.GetOneByReservation(reservation.Id);
                reservation.Guest1Grade = guest1GradeService.GetOneByReservation(reservation.Id);
            }
            return reservations;
        }
        public void AddAccommodation(AccommodationVM newAccommodation, LocationVM newLocation)
        {
            AccommodationService accommodationService = new AccommodationService();
            LocationService locationService = new LocationService();
            Location location = new Location(newLocation.City, newLocation.Country);
            int id = locationService.AddAndReturnId(location);
            Accommodation accommodationToAdd = new Accommodation(newAccommodation.GetAccommodation());
            accommodationToAdd.LocationId = id;
            accommodationService.Add(accommodationToAdd);
            Synchronize(Username);
        }
        public void GradeAGuest(Guest1GradeVM grade)
        {
            Guest1GradeService guest1GradeService = new Guest1GradeService();
            var reservation = Reservations.ToList().Find(r=>r.Id == grade.ReservationId);
            reservation.Guest1Grade = grade;
            reservation.Graded = true;
            reservation.CanBeGraded = false;
            reservation.VisibleReview = reservation.AccommodationGraded;
            guest1GradeService.Add(grade.GetGuest1Grade());
        }
        public Owner GetOwner()
        {
            return _owner;
        }
        public string Username
        {
            get => _owner.Username;
            set
            {
                if (value != _owner.Username)
                {
                    _owner.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _HasNewNotifications;
        public bool HasNewNotifications
        {
            get => _HasNewNotifications;
            set
            {
                if(value!=_HasNewNotifications)
                {
                    _HasNewNotifications = value;
                    OnPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get => _owner.FirstName;
            set
            {
                if (value != _owner.FirstName)
                {
                    _owner.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => _owner.LastName;
            set
            {
                if (value != _owner.LastName)
                {
                    _owner.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Birthday
        {
            get => _owner.Birthday;
            set
            {
                if (value != _owner.Birthday)
                {
                    _owner.Birthday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => _owner.Email;
            set
            {
                if (value != _owner.Email)
                {
                    _owner.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PhoneNumber
        {
            get => _owner.PhoneNumber;
            set
            {
                if (value != _owner.PhoneNumber)
                {
                    _owner.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsSuperHost
        {
            get => AverageGrade>4.5 && NumberOfReviews>2;
        }
        public int NumberOfReviews
        {
            get => _owner.Reservations.Count(res=>res.AccommodationGrade!=null);
        }
        public int NumberOfReservations
        {
            get => _owner.Reservations.Count();
        }
        public int NumberOfAccommodations
        {
            get => _owner.Accommodations.Count();
        }
        public int NumberOfRenovations
        {
            get=> _owner.Accommodations.Where(a=> a.Renovations!=null).Sum(a=> a.Renovations.Count());
        }
        public double AverageGrade
        {
            get => CalculateAverageGrade();
        }
        private ObservableCollection<AccommodationVM> _Accommodations;
        public ObservableCollection<AccommodationVM> Accommodations
        { 
            get => _Accommodations;
            set 
            {
                if (value != _Accommodations)
                {
                    _Accommodations = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<NotificationVM> _Notifications;
        public ObservableCollection<NotificationVM> Notifications
        {
            get => _Notifications;
            set
            {
                if (value != _Notifications)
                {
                    _Notifications = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<ReservationVM> _Reservations;
        public ObservableCollection<ReservationVM> Reservations
        {
            get => _Reservations;
            set
            {
                if (value != _Reservations)
                {
                    _Reservations = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void SeenNotifications()
        {
            foreach (var n in Notifications) n.New = false;
            new NotificationService().Seen();
        }
        private bool _NoNotifications;
        public bool NoNotifications
        {
            get
            {
                if (Notifications == null) return _NoNotifications = true;
                else return NoNotifications = Notifications.Count == 0;
            }
            set
            {
                if (value != _NoNotifications)
                {
                    _NoNotifications = value;
                    OnPropertyChanged();
                }
            }
        }
        private double CalculateAverageGrade()
        {
            try
            {
                return Reservations.Where(reservation => reservation.AccommodationGraded && reservation.EndDate.AddYears(1) > DateOnly.FromDateTime(DateTime.Now))
                                   .Average(reservation => reservation.AccommodationGrade.AverageGrade);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
