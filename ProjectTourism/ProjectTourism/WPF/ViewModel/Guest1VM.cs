using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
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
using ProjectTourism.Domain.Model;

namespace ProjectTourism.WPF.ViewModel
{
    public class Guest1VM : INotifyPropertyChanged
    {
        private Guest1 _guest1;
        public ObservableCollection<OwnerVM> OwnerVMs { get; set; }
        public ObservableCollection<AccommodationVM> Accommodations { get; set; }
        public ObservableCollection<ReservationVM> Reservations { get; set; }
        public ObservableCollection<ReservationVM> MyReservations { get; set; }
        public ObservableCollection<ReservationVM> GradableReservations { get; set; }

        public Guest1VM(string username)
        {
            Guest1Service guest1Service = new Guest1Service();
            AccommodationService accommodationService = new AccommodationService();
            ReservationService reservationService = new ReservationService();

            _guest1 = guest1Service.GetOne(username);
            Accommodations = new ObservableCollection<AccommodationVM>(accommodationService.GetAll().Select(r => new AccommodationVM(r)).ToList().OrderByDescending(a => a.Owner.IsSuperHost).ToList());
            MyReservations = new ObservableCollection<ReservationVM>();
            GradableReservations = new ObservableCollection<ReservationVM>();
            Reservations = new ObservableCollection<ReservationVM>(reservationService.GetAll().Select(r => new ReservationVM(r)).Reverse().ToList());
            
            foreach (ReservationVM reservationVM in Reservations)
            {
                if (IsReserved(reservationVM))
                {
                    reservationVM.Accommodation = new AccommodationVM(Accommodations.ToList().Find(a => a.Id == reservationVM.AccommodationId).GetAccommodation());
                    MyReservations.Add(reservationVM);
                }
                else if (IsGradable(reservationVM))
                {
                    reservationVM.Accommodation = new AccommodationVM(Accommodations.ToList().Find(a => a.Id == reservationVM.AccommodationId).GetAccommodation());
                    GradableReservations.Add(reservationVM);
                }
            }
        }

        private bool IsGradable(ReservationVM reservationVM)
        {
            return _guest1.Username == reservationVM.Guest1Username
                                    && reservationVM.EndDate < DateOnly.FromDateTime(DateTime.Now)
                                    && reservationVM.EndDate.AddDays(5) > DateOnly.FromDateTime(DateTime.Now);
        }

        private bool IsReserved(ReservationVM reservationVM)
        {
            return _guest1.Username == reservationVM.Guest1Username 
                                    && reservationVM.StartDate > DateOnly.FromDateTime(DateTime.Now);
        }
        public void GradeAccommodation(AccommodationGradeVM grade)
        {
            AccommodationGradeService accommodationGradeService = new AccommodationGradeService();
            RenovationRecommendationService renovationRecommendationService = new RenovationRecommendationService();
            var reservation = Reservations.ToList().Find(r => r.Id == grade.ReservationId);
            if (grade.RenovationRecommendation != null)
            {
                grade.RenovationRecommendation.AccommodationGradeId = accommodationGradeService.Add(grade.GetAccommodationGrade());
                renovationRecommendationService.Add(grade.RenovationRecommendation.GetRenovationRecommendation());
            }
            else
            {
                accommodationGradeService.Add(grade.GetAccommodationGrade());
            }
        }
        public void CancelReservation(ReservationVM reservationVM)
        {
            ReservationService reservationService = new ReservationService();
            CanceledReservationService canceledReservationService = new CanceledReservationService();
            MyReservations.Remove(reservationVM);
            reservationService.Cancel(reservationVM.GetReservation());
            canceledReservationService.Add(reservationVM.GetReservation());
        }
        public bool ProcessReservation(ReservationVM reservationVM)
        {
            ReservationService reservationService = new ReservationService();
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
        public bool ProcessRequest(ReservationVM reservationVM)
        {
            ReservationService reservationService = new ReservationService();
            if (reservationService.IsPossible(reservationVM.GetReservation()))
            {
                SendRequest(reservationVM);
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
            ReservationService reservationService = new ReservationService();
            reservationService.Add(reservationVM.GetReservation());
        }
        private void SendRequest(ReservationVM reservationVM)
        {
            PostponeRequestVM postponeRequestVM = new PostponeRequestVM(new PostponeRequest());
            postponeRequestVM.ReservationId = reservationVM.Id;
            postponeRequestVM.NewStartDate = reservationVM.StartDate;
            postponeRequestVM.NewEndDate = reservationVM.EndDate;
            PostponeRequestService postponeRequestService = new PostponeRequestService();
            postponeRequestService.Add(postponeRequestVM.GetPostponeRequest());
        }
        private void FindFirstAvailableAccommodation(ReservationVM reservationVM)
        {
            ReservationService reservationService = new ReservationService();
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
