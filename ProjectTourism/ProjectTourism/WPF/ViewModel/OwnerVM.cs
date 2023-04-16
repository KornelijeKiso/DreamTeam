using ProjectTourism.Model;
using ProjectTourism.Observer;
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

namespace ProjectTourism.WPF.ViewModel
{
    public class OwnerVM : INotifyPropertyChanged
    {
        private Owner _owner;
        public OwnerVM(Owner owner)
        {
            _owner = owner;
            Accommodations = new ObservableCollection<AccommodationVM>(_owner.Accommodations.Select(r => new AccommodationVM(r)).ToList());
            Reservations = new ObservableCollection<ReservationVM>(_owner.Reservations.Select(r => new ReservationVM(r)).Reverse().ToList());
        }

        public OwnerVM(string username)
        {
            Synchronize(username);
            Accommodations = new ObservableCollection<AccommodationVM>(_owner.Accommodations.Select(r => new AccommodationVM(r)).ToList());
            Reservations = new ObservableCollection<ReservationVM>(_owner.Reservations.Select(r => new ReservationVM(r)).Reverse().ToList());
        }
        private void Synchronize(string username)
        {
            OwnerService ownerService = new OwnerService(new OwnerRepository());
            AccommodationService accommodationService = new AccommodationService(new AccommodationRepository());
            LocationService locationService = new LocationService(new LocationRepository());

            _owner = ownerService.GetOne(username);
            _owner.Accommodations = accommodationService.GetAllByOwner(_owner.Username);
            _owner.Reservations = new List<Reservation>();
            foreach (Accommodation accommodation in _owner.Accommodations)
            {
                accommodation.Location = locationService.GetOne(accommodation.LocationId);
                accommodation.Reservations = SynchronizeReservations(accommodation);
                _owner.Reservations.AddRange(accommodation.Reservations);
            }
        }
        private List<Reservation> SynchronizeReservations(Accommodation accommodation)
        {
            ReservationService reservationService = new ReservationService(new ReservationRepository());
            Guest1GradeService guest1GradeService = new Guest1GradeService(new Guest1GradeRepository());
            Guest1Service guest1Service = new Guest1Service(new Guest1Repository());
            AccommodationGradeService accommodationGradeService = new AccommodationGradeService(new AccommodationGradeRepository());
            PostponeRequestService postponeRequestService = new PostponeRequestService(new PostponeRequestRepository());

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
            AccommodationService accommodationService = new AccommodationService(new AccommodationRepository());
            LocationService locationService = new LocationService(new LocationRepository());
            Location location = new Location(newLocation.City, newLocation.Country);
            location.Id = locationService.AddAndReturnId(location);
            newLocation.Id = location.Id;
            newAccommodation.SetLocation(location);

            accommodationService.Add(newAccommodation.GetAccommodation());
            AccommodationVM accommodation = new AccommodationVM(newAccommodation);
            accommodation.Location = new LocationVM(locationService.GetOne(location.Id));
            Accommodations.Add(accommodation);
            _owner.Accommodations.Add(accommodation.GetAccommodation());
        }

        public void GradeAGuest(Guest1GradeVM grade)
        {
            Guest1GradeService guest1GradeService = new Guest1GradeService(new Guest1GradeRepository());
            foreach( var reservation in Reservations)
            {
                if (reservation.Id == grade.ReservationId)
                {
                    reservation.Guest1Grade = grade;
                    reservation.Graded = true;
                    guest1GradeService.Add(grade.GetGuest1Grade());
                    return;
                }
            }
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
        public double AverageGrade
        {
            get => CalculateAverageGrade();
        }
        public ObservableCollection<AccommodationVM> Accommodations { get; set; }
        public ObservableCollection<ReservationVM> Reservations{ get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

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
