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

namespace ProjectTourism.DTO
{
    public class Guest1DTO : INotifyPropertyChanged
    {
        private Guest1 _guest1;
        public ObservableCollection<AccommodationDTO> Accommodations { get; set; }
        public ObservableCollection<ReservationDTO> Reservations { get; set; }
        public ObservableCollection<ReservationDTO> MyReservations { get; set; }
        public ObservableCollection<ReservationDTO> GradableReservations { get; set; }
        bool SuperGuest { get; set; }
        int reservationCount { get; set; }

        public Guest1DTO(string username)
        {
            Guest1Service guest1Service = new Guest1Service();
            AccommodationService accommodationService = new AccommodationService();
            ReservationService reservationService = new ReservationService();

            _guest1 = guest1Service.GetOne(username);
            Forums = new ObservableCollection<ForumDTO>();
            SynchronizeForums();
            Accommodations = new ObservableCollection<AccommodationDTO>(accommodationService.GetAll().Select(r => new AccommodationDTO(r)).ToList().OrderByDescending(a => a.Owner.IsSuperHost).ToList());
            MyReservations = new ObservableCollection<ReservationDTO>();
            GradableReservations = new ObservableCollection<ReservationDTO>();
            Reservations = new ObservableCollection<ReservationDTO>(reservationService.GetAll().Select(r => new ReservationDTO(r)).Reverse().ToList());
            SuperGuest = false;

            foreach (ReservationDTO reservationDTO in Reservations)
            {
                if (IsReserved(reservationDTO))
                {
                    reservationDTO.Accommodation = new AccommodationDTO(Accommodations.ToList().Find(a => a.Id == reservationDTO.AccommodationId).GetAccommodation());
                    MyReservations.Add(reservationDTO);
                }
                else if (IsGradable(reservationDTO))
                {
                    reservationDTO.Accommodation = new AccommodationDTO(Accommodations.ToList().Find(a => a.Id == reservationDTO.AccommodationId).GetAccommodation());
                    GradableReservations.Add(reservationDTO);
                }
            }
            foreach (ReservationDTO reservationDTO in Reservations)
            {
                if (reservationDTO.EndDate < DateOnly.FromDateTime(DateTime.Now) && reservationDTO.EndDate > DateOnly.FromDateTime(DateTime.Now).AddYears(-1) && reservationDTO.Guest1.Username == _guest1.Username)
                {
                    reservationCount += 1;
                }
            }
            if (reservationCount > 9)
            {
                SuperGuest = true;
                if (Points == -1)
                {
                    Points = 5;
                    new Guest1Service().Update(_guest1);
                }
            }
            else
            {
                Points = -1;
                new Guest1Service().Update(_guest1);
            }

        }

        private void SynchronizeForums()
        {
            List<ForumDTO> forums = new ForumService().GetAll().Select(fo => new ForumDTO(fo)).ToList();//.Where(f => Accommodations.ToList().Find(a => a.LocationId == f.LocationId) != null).ToList().Select(fo => new ForumDTO(fo)).ToList();
            foreach (ForumDTO forum in forums)
            {
                forum.Location = new LocationDTO(new LocationService().GetOne(forum.LocationId));
                forum.Comments = new ObservableCollection<CommentOnForumDTO>(new CommentOnForumService().GetAllByForum(forum.Id).Select(c => new CommentOnForumDTO(c)));
                foreach (CommentOnForumDTO comment in forum.Comments)
                {
                    comment.Forum = forum;
                }
            }
            foreach (var forum in forums)
            {
                if (!Forums.Contains(forum))
                {
                    Forums.Add(forum);
                }
            }
        }
        public void CancelReservation(ReservationDTO reservationDTO)
        {
            ReservationService reservationService = new ReservationService();
            CanceledReservationService canceledReservationService = new CanceledReservationService();
            MyReservations.Remove(reservationDTO);
            reservationService.Cancel(reservationDTO.GetReservation());
            canceledReservationService.Add(reservationDTO.GetReservation());
            var accommodation = new AccommodationService().GetOne(reservationDTO.AccommodationId);
            new NotificationService().Add(new Notification("Reservation cancellation",
                                                            FirstName + " " + LastName + " (" + Username + ")" + " canceled his reservation at " + accommodation.Name + " scheduled for " + reservationDTO.StartDate + ". It is now removed from your reservations.",
                                                            accommodation.OwnerUsername));
        }
        private void SendRequest(ReservationDTO reservationDTO)
        {
            PostponeRequestDTO postponeRequestDTO = new PostponeRequestDTO(new PostponeRequest());
            postponeRequestDTO.ReservationId = reservationDTO.Id;
            postponeRequestDTO.NewStartDate = reservationDTO.StartDate;
            postponeRequestDTO.NewEndDate = reservationDTO.EndDate;
            PostponeRequestService postponeRequestService = new PostponeRequestService();
            postponeRequestService.Add(postponeRequestDTO.GetPostponeRequest());
        }
        public bool ProcessReservation(ReservationDTO reservationDTO)
        {
            ReservationService reservationService = new ReservationService();
            if (reservationService.IsPossible(reservationDTO.GetReservation()))
            {
                BookAccommodation(reservationDTO);
                var accommodation = new AccommodationService().GetOne(reservationDTO.AccommodationId);
                new NotificationService().Add(new Notification("New reservation",
                                                                FirstName + " " + LastName + " (" + Username + ")" + " booked " + accommodation.Name + " from " + reservationDTO.StartDate + " to " + reservationDTO.EndDate + ". You can see it in your reservations.",
                                                                accommodation.OwnerUsername));
                return true;
            }
            else
            {
                FindFirstAvailableAccommodation(reservationDTO);
                return false;
            }
        }


        private void BookAccommodation(ReservationDTO reservationDTO)
        {
            ReservationService reservationService = new ReservationService();
            reservationService.Add(reservationDTO.GetReservation());
            if (Points > 0)
            {
                Points--;
                new Guest1Service().Update(_guest1);
            }
        }
        
        private void FindFirstAvailableAccommodation(ReservationDTO reservationDTO)
        {
            ReservationService reservationService = new ReservationService();
            while (!reservationService.IsPossible(reservationDTO.GetReservation()))
            {
                reservationDTO.StartDate = reservationDTO.StartDate.AddDays(1);
                reservationDTO.EndDate = reservationDTO.EndDate.AddDays(1);
            }
        }
        public bool ProcessRequest(ReservationDTO reservationDTO)
        {
            ReservationService reservationService = new ReservationService();
            if (reservationService.IsPossible(reservationDTO.GetReservation()))
            {
                SendRequest(reservationDTO);
                var accommodation = new AccommodationService().GetOne(reservationDTO.AccommodationId);
                new NotificationService().Add(new Notification("Reservation postpone request",
                                                                FirstName + " " + LastName + " (" + Username + ")" + " made a request to postpone his reservation scheduled for " + reservationDTO.StartDate + " at " + accommodation.Name + ". You can see it in your reservations.",
                                                                accommodation.OwnerUsername));
                return true;
            }
            else
            {
                FindFirstAvailableAccommodation(reservationDTO);
                return false;
            }
        }
        private bool IsGradable(ReservationDTO reservationVM)
        {
            return _guest1.Username == reservationVM.Guest1Username
                                    && reservationVM.EndDate < DateOnly.FromDateTime(DateTime.Now)
                                    && reservationVM.EndDate.AddDays(5) > DateOnly.FromDateTime(DateTime.Now);
        }
        private bool IsReserved(ReservationDTO reservationVM)
        {
            return _guest1.Username == reservationVM.Guest1Username
                                    && reservationVM.StartDate > DateOnly.FromDateTime(DateTime.Now);
        }
        public Guest1DTO(Guest1 guest1)
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
        public bool isSuperGuest
        {
            get => SuperGuest;
        }
        public int Points
        {
            get => _guest1.Points;
            set
            {
                if (value != _guest1.Points)
                {
                    _guest1.Points = value;
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
        public double AverageGrade
        {
            get => CalculateAverageGrade();
        }
        private double CalculateAverageGrade()
        {
            try
            {
                return Reservations.Where(reservation => reservation.Graded && reservation.Guest1Username == _guest1.Username).Average(reservation => reservation.Guest1Grade.AverageGrade);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
