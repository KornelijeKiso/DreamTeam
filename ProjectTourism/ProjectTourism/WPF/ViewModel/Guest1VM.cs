using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
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
    public class Guest1VM : INotifyPropertyChanged
    {
        private Guest1 _guest1;
        public ObservableCollection<OwnerVM> OwnerVMs { get; set; }
        public ObservableCollection<AccommodationVM> Accommodations { get; set; }
        public ObservableCollection<ReservationVM> Reservations { get; set; }
        public ObservableCollection<ReservationVM> MyReservations { get; set; }

        public Guest1VM(string username)
        {
            Guest1Service guest1Service = new Guest1Service(new Guest1Repository());
            _guest1 = guest1Service.GetOne(username);
            AccommodationService accommodationService = new AccommodationService(new AccommodationRepository());
            Accommodations = new ObservableCollection<AccommodationVM>(accommodationService.GetAll().Select(r => new AccommodationVM(r)).ToList().OrderByDescending(a => a.Owner.IsSuperHost).ToList());
            MyReservations = new ObservableCollection<ReservationVM>();
            ReservationService reservationService = new ReservationService(new ReservationRepository());
            Reservations = new ObservableCollection<ReservationVM>(reservationService.GetAll().Select(r => new ReservationVM(r)).Reverse().ToList());
            foreach (ReservationVM reservationVM in Reservations)
            {
                if (_guest1.Username == reservationVM.Guest1Username)
                {
                    reservationVM.Accommodation = new AccommodationVM(Accommodations.ToList().Find(a => a.Id == reservationVM.AccommodationId).GetAccommodation());
                    MyReservations.Add(reservationVM);
                }
            }
        }
        public void CancelReservation(ReservationVM reservationVM)
        {
            ReservationService reservationService = new ReservationService(new ReservationRepository());
            CanceledReservationService canceledReservationService = new CanceledReservationService(new CanceledReservationRepository());
            MyReservations.Remove(reservationVM);
            reservationService.Cancel(reservationVM.GetReservation());
            canceledReservationService.Add(reservationVM.GetReservation());
        }
        public void PrepareReservation(out ReservationVM reservationVM, out AccommodationVM accommodationVM)
        {
            reservationVM = new ReservationVM(new Reservation());
            ReservationService reservationService = new ReservationService(new ReservationRepository());
            AccommodationService accommodationService = new AccommodationService(new AccommodationRepository());
            accommodationVM = new AccommodationVM(accommodationService.GetOne(reservationVM.AccommodationId));
        }

        public bool ProcessReservation(ReservationVM reservationVM)
        {
            ReservationService reservationService = new ReservationService(new ReservationRepository());
            if (reservationService.IsPossible(reservationVM.GetReservation()))
            {
                BookAccommodation(reservationVM);
                return true;
            }
            else
            {
                FindFirstAvailableAccommodation(reservationVM);
                return false;
            }
        }
        private void BookAccommodation(ReservationVM reservationVM)
        {
            ReservationService reservationService = new ReservationService(new ReservationRepository());
            reservationService.Add(reservationVM.GetReservation());
        }
        private void FindFirstAvailableAccommodation(ReservationVM reservationVM)
        {
            ReservationService reservationService = new ReservationService(new ReservationRepository());
            while (!reservationService.IsPossible(reservationVM.GetReservation()))
            {
                reservationVM.StartDate = reservationVM.StartDate.AddDays(1);
                reservationVM.EndDate = reservationVM.EndDate.AddDays(1);
            }
        }
        public Guest1VM(Guest1 guest1)
        {
            _guest1 = guest1;
        }
        public Guest1 GetGuest1()
        {
            return _guest1;
        }
        public string Username
        {
            get => _guest1.Username;
            set
            {
                if (value != _guest1.Username)
                {
                    _guest1.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => _guest1.Password;
            set
            {
                if (value != _guest1.Password)
                {
                    _guest1.Password = value;
                    OnPropertyChanged();
                }
            }
        }
        public USERTYPE Type
        {
            get => _guest1.Type;
            set
            {
                if (value != _guest1.Type)
                {
                    _guest1.Type = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => _guest1.FirstName;
            set
            {
                if (value != _guest1.FirstName)
                {
                    _guest1.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => _guest1.LastName;
            set
            {
                if (value != _guest1.LastName)
                {
                    _guest1.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Birthday
        {
            get => _guest1.Birthday;
            set
            {
                if (value != _guest1.Birthday)
                {
                    _guest1.Birthday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => _guest1.Email;
            set
            {
                if (value != _guest1.Email)
                {
                    _guest1.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PhoneNumber
        {
            get => _guest1.PhoneNumber;
            set
            {
                if (value != _guest1.PhoneNumber)
                {
                    _guest1.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public double AverageGrade
        {
            get => _guest1.AverageGrade;
            set
            {
                if (value != _guest1.AverageGrade)
                {
                    _guest1.AverageGrade = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
