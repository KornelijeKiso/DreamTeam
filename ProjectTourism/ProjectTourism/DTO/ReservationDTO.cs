﻿using ProjectTourism.Model;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class ReservationDTO : INotifyPropertyChanged
    {
        private Reservation _reservation;
        public ReservationDTO(Reservation reservation)
        {
            _reservation = reservation;
        }
        public Reservation GetReservation()
        {
            return _reservation;
        }
        public void Update()
        {
            ReservationService reservationService = new ReservationService();
            reservationService.Update(this.GetReservation());
        }

        
        public int Id
        {
            get => _reservation.Id;
            set
            {
                if (value != _reservation.Id)
                {
                    _reservation.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly StartDate
        {
            get => _reservation.StartDate;
            set
            {
                if (value != _reservation.StartDate)
                {
                    _reservation.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly EndDate
        {
            get => _reservation.EndDate;
            set
            {
                if (value != _reservation.EndDate)
                {
                    _reservation.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly GradingDeadline
        {
            get => _reservation.EndDate.AddDays(5);
        }
        public AccommodationDTO Accommodation
        {
            get => new AccommodationDTO(_reservation.Accommodation);
            set
            {
                if (value.GetAccommodation() != _reservation.Accommodation)
                {
                    _reservation.Accommodation = value.GetAccommodation();
                    OnPropertyChanged();
                }
            }
        }
        public int AccommodationId
        {
            get => _reservation.AccommodationId;
            set
            {
                if (value != _reservation.AccommodationId)
                {
                    _reservation.AccommodationId = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _AccommodationGraded;
        public bool AccommodationGraded
        {
            get => _AccommodationGraded = _reservation.AccommodationGrade != null;
            set
            {
                if (value != _AccommodationGraded)
                {
                    _AccommodationGraded = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Guest1Username
        {
            get => _reservation.Guest1Username;
            set
            {
                if (value != _reservation.Guest1Username)
                {
                    _reservation.Guest1Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guest1DTO Guest1

        {
            get => new Guest1DTO(_reservation.Guest1);
            set
            {
                if (value.GetGuest1() != _reservation.Guest1)
                {
                    _reservation.Guest1 = value.GetGuest1();
                    OnPropertyChanged();
                }
            }
        }
        public PostponeRequestDTO PostponeRequest
        {
            get => new PostponeRequestDTO(_reservation.PostponeRequest);
        }
        private bool _RequestedPostpone;
        public bool RequestedPostpone
        {
            get => _RequestedPostpone = IsPostponeRequested();
            set
            {
                if (value != _RequestedPostpone)
                {
                    _RequestedPostpone = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool IsPostponeRequested()
        {
            if (_reservation.PostponeRequest != null)
            {
                return !_reservation.PostponeRequest.Accepted && !_reservation.PostponeRequest.Rejected && StartDate > DateOnly.FromDateTime(DateTime.Now);
            }
            else return false;
        }
        private bool _Graded;
        public bool Graded
        {
            get => _Graded = _reservation.Guest1Grade != null;
            set
            {
                if (value != _Graded)
                {
                    _Graded = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Conflict
        {
            get => PostponeConflictMessage.Equals("Requested appointment is in conflict with other reservations for this accommodation. You have options to accept or reject this postpone request.");
        }
        public bool NotConflict
        {
            get => PostponeConflictMessage.Equals("Requested appointment is not in conflict with other reservations for this accommodation. You have options to accept or reject this postpone request.");
        }
        private bool _CanBeGraded;
        public bool CanBeGraded
        {
            get => _CanBeGraded = !Graded && IsAbleToGrade();
            set
            {
                if (value != _CanBeGraded)
                {
                    _CanBeGraded = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _VisibleReview;
        public bool VisibleReview
        {
            get => _VisibleReview = (Graded || EndDate <= DateOnly.FromDateTime(DateTime.Now).AddDays(-5)) && AccommodationGraded;
            set
            {
                if (value != _VisibleReview)
                {
                    _VisibleReview = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PostponeConflictMessage
        {
            get => GetPostponeConflictMessage();
        }
        public string GradingDeadlineMessage
        {
            get => GenerateGradingDeadlineMessage();
        }
        public AccommodationGradeDTO AccommodationGrade
        {
            get => new AccommodationGradeDTO(_reservation.AccommodationGrade);
            set
            {
                if (value.GetAccommodationGrade() != _reservation.AccommodationGrade)
                {
                    _reservation.AccommodationGrade = value.GetAccommodationGrade();
                    OnPropertyChanged();
                }
            }
        }
        public Guest1GradeDTO Guest1Grade
        {
            get => new Guest1GradeDTO(_reservation.Guest1Grade);
            set
            {
                if (value.GetGuest1Grade() != _reservation.Guest1Grade)
                {
                    _reservation.Guest1Grade = value.GetGuest1Grade();
                    OnPropertyChanged();
                }
            }
        }
        public string GetPostponeConflictMessage()
        {
            Reservation reservation = new Reservation(_reservation);
            ReservationService reservationService = new ReservationService();
            reservation.StartDate = PostponeRequest.NewStartDate;
            reservation.EndDate = PostponeRequest.NewEndDate;
            if (reservationService.IsPossible(reservation))
            {
                return "Requested appointment is not in conflict with other reservations for this accommodation. You have options to accept or reject this postpone request.";
            }
            else
            {
                return "Requested appointment is in conflict with other reservations for this accommodation. You have options to accept or reject this postpone request.";
            }
        }
        public string GenerateGradingDeadlineMessage()
        {
            if (Graded)
            {
                return "Already graded.";
            }
            if (IsAbleToGrade())
            {
                return GradingDeadline.ToString();
            }
            else
            {
                if (Graded)
                {
                    return "Already graded.";
                }
                else if (DateOnly.FromDateTime(DateTime.Now) > EndDate)
                {
                    return "Expired.";
                }
                else
                {
                    return "Visit not ended yet.";
                }
            }
        }
        public bool IsAbleToGrade()
        {
            return DateOnly.FromDateTime(DateTime.Now) > EndDate && DateOnly.FromDateTime(DateTime.Now) < GradingDeadline;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
