using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProjectTourism.DTO
{
    public class OwnerDTO:INotifyPropertyChanged
    {
        private Owner _owner;
        public Timer timer;
        public OwnerDTO(Owner owner)
        {
            _owner = owner;
            Accommodations = new ObservableCollection<AccommodationDTO>(_owner.Accommodations.Select(r => new AccommodationDTO(r)).ToList());
            Reservations = new ObservableCollection<ReservationDTO>(_owner.Reservations.Select(r => new ReservationDTO(r)).Reverse().ToList());
        }
        public OwnerDTO(string username)
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
        public void DismissNotification(NotificationDTO notification)
        {
            Notifications.Remove(notification);
            new NotificationService().Dismiss(notification.GetNotification());
        }
        public void DismissAllNotification()
        {
            Notifications.Clear();
            new NotificationService().DismissAll(Username);
        }
        public void Synchronize(string username)
        {
            _owner = new OwnerService().GetOne(username);
            _owner.Accommodations = new AccommodationService().GetAllByOwner(_owner.Username);
            _owner.Reservations = new List<Reservation>();
            foreach (Accommodation accommodation in _owner.Accommodations)
            {
                accommodation.Renovations = new RenovationService().GetAllByAccommodation(accommodation.Id);
                accommodation.Location = new LocationService().GetOne(accommodation.LocationId);
                accommodation.Reservations = SynchronizeReservations(accommodation);
                AddReservationsToOwner(accommodation);
            }
            Accommodations = new ObservableCollection<AccommodationDTO>(_owner.Accommodations.Select(r => new AccommodationDTO(r)).ToList());
            Reservations = new ObservableCollection<ReservationDTO>(_owner.Reservations.Select(r => new ReservationDTO(r)).Reverse().ToList());
            SetPopularAndUnpopularDestination();
            LoadNotifications();
            SynchronizeForums();
        }
        private void SynchronizeForums()
        {
            List<ForumDTO> forums = new ForumService().GetAll().Where(f => Accommodations.ToList().Find(a => a.LocationId == f.LocationId) != null).ToList().Select(fo=>new ForumDTO(fo)).ToList();
            foreach(ForumDTO forum in forums)
            {
                forum.Location = new LocationDTO(new LocationService().GetOne(forum.LocationId));
                forum.Comments = new ObservableCollection<CommentOnForumDTO>(new CommentOnForumService().GetAllByForum(forum.Id).Select(c => new CommentOnForumDTO(c)));
                foreach(CommentOnForumDTO comment in forum.Comments)
                {
                    comment.Forum = forum;
                }
            }
            Forums = new ObservableCollection<ForumDTO>(forums);
        }
        public void SetPopularAndUnpopularDestination()
        {
            Dictionary<int, DestinationDTO> locations = new Dictionary<int, DestinationDTO>();
            foreach (var accommodation in Accommodations)
            {
                if (!locations.ContainsKey(accommodation.LocationId))
                    locations.Add(accommodation.LocationId, new DestinationDTO(accommodation.Location));
                locations[accommodation.LocationId].Accommodations.Add(accommodation);
                locations[accommodation.LocationId].Reservations.AddRange(accommodation.Reservations.ToList().Where(r => r.StartDate < DateOnly.FromDateTime(DateTime.Now) && r.EndDate > DateOnly.FromDateTime(DateTime.Now).AddYears(-1)));
            }
            PopularDestination = locations.Values.ToList().OrderByDescending(a => a.Occupancy).FirstOrDefault();
            UnpopularDestination = locations.Values.ToList().OrderByDescending(a => a.Occupancy).LastOrDefault();
        }
        private void AddReservationsToOwner(Accommodation accommodation)
        {
            if (accommodation.Reservations != null && accommodation.Reservations.Any())
            {
                foreach (Reservation reservation in accommodation.Reservations)
                    _owner.Reservations.Add(reservation);
            }
        }

        private void LoadNotifications()
        {
            Notifications = new ObservableCollection<NotificationDTO>(new NotificationService().GetAllByUser(_owner.Username).Select(r => new NotificationDTO(r)).Reverse().ToList());
            HasNewNotifications = Notifications.ToList().Any(n => n.New);
            NoNotifications = !Notifications.ToList().Any();
        }

        private List<Reservation> SynchronizeReservations(Accommodation accommodation)
        {
            List<Reservation> reservations = new ReservationService().GetAllByAccommodation(accommodation.Id);
            foreach (Reservation reservation in reservations)
            {
                reservation.PostponeRequest = new PostponeRequestService().GetOneByReservation(reservation.Id);
                reservation.Guest1 = new Guest1Service().GetOne(reservation.Guest1Username);
                reservation.Accommodation = accommodation;
                reservation.AccommodationGrade = new AccommodationGradeService().GetOneByReservation(reservation.Id);
                if (reservation.AccommodationGrade != null)
                    reservation.AccommodationGrade.RenovationRecommendation = new RenovationRecommendationService().GetOneByAccommodationGrade(reservation.AccommodationGrade.Id);
                reservation.Guest1Grade = new Guest1GradeService().GetOneByReservation(reservation.Id);
            }
            return reservations;
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
                if (value != _HasNewNotifications)
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
            get => AverageGrade > 4.5 && NumberOfReviews > 2;
        }
        public int NumberOfReviews
        {
            get => _owner.Reservations.Count(res => res.AccommodationGrade != null);
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
            get => _owner.Accommodations.Where(a => a.Renovations != null).Sum(a => a.Renovations.Count());
        }
        public double AverageGrade
        {
            get => CalculateAverageGrade();
        }
        public DestinationDTO PopularDestination { get; set; }
        public DestinationDTO UnpopularDestination { get; set; }

        private ObservableCollection<AccommodationDTO> _Accommodations;
        public ObservableCollection<AccommodationDTO> Accommodations
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
        private ObservableCollection<ForumDTO> _Forums;
        public ObservableCollection<ForumDTO> Forums
        {
            get => _Forums;
            set
            {
                if (value != _Forums)
                {
                    _Forums = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<NotificationDTO> _Notifications;
        public ObservableCollection<NotificationDTO> Notifications
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
        private ObservableCollection<ReservationDTO> _Reservations;
        public ObservableCollection<ReservationDTO> Reservations
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
            HasNewNotifications = false;
            List<Notification> notifications = new List<Notification>();
            foreach (var n in Notifications)
            {
                notifications.Add(n.GetNotification());
            }
            new NotificationService().Seen(notifications);
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
