﻿using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class ReservationVM:INotifyPropertyChanged
    {
        private Reservation _reservation;
        public ReservationVM(Reservation reservation)
        {
            _reservation = reservation;
        }
        public Reservation GetReservation()
        {
            return _reservation;
        }
        public void Update()
        {
            ReservationService reservationService = new ReservationService(new ReservationRepository());
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
            get => _reservation.GradingDeadline;
            set
            {
                if (value != _reservation.GradingDeadline)
                {
                    _reservation.GradingDeadline = value;
                    OnPropertyChanged();
                }
            }
        }
        public AccommodationVM Accommodation
        {
            get => new AccommodationVM(_reservation.Accommodation);
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
        public bool AccommodationGraded
        {
            get => _reservation.AccommodationGraded;
            set
            {
                if (value != _reservation.AccommodationGraded)
                {
                    _reservation.AccommodationGraded = value;
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
        public Guest1VM Guest1

        {
            get => new Guest1VM(_reservation.Guest1);
            set
            {
                if (value.GetGuest1() != _reservation.Guest1)
                {
                    _reservation.Guest1 = value.GetGuest1();
                    OnPropertyChanged();
                }
            }
        }
        public PostponeRequestVM PostponeRequest
        {
            get => new PostponeRequestVM(_reservation.PostponeRequest);
        }
        private bool _RequestedPostpone;
        public bool RequestedPostpone
        {
            get => _RequestedPostpone = IsPostponeRequested();
            set
            {
                if(value!=_RequestedPostpone)
                {
                    _RequestedPostpone = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool IsPostponeRequested()
        {
            if (PostponeRequest.GetPostponeRequest() != null)
            {
                return !PostponeRequest.Accepted && !PostponeRequest.Rejected && StartDate>DateOnly.FromDateTime(DateTime.Now);
            }
            else return false;
        }
        public bool Graded
        {
            get => _reservation.Graded;
            set
            {
                if (value != _reservation.Graded)
                {
                    _reservation.Graded = value;
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
        public bool CanBeGraded
        {
            get => !Graded && IsAbleToGrade();
        }
        public bool VisibleReview
        {
            get => _reservation.VisibleReview;
            set
            {
                if (value != _reservation.VisibleReview)
                {
                    _reservation.VisibleReview = value;
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
            set
            {
                if (value != _reservation.GradingDeadlineMessage)
                {
                    _reservation.GradingDeadlineMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        public AccommodationGradeVM AccommodationGrade
        {
            get => new AccommodationGradeVM(_reservation.AccommodationGrade);
            set
            {
                if (value.GetAccommodationGrade() != _reservation.AccommodationGrade)
                {
                    _reservation.AccommodationGrade = value.GetAccommodationGrade();
                    OnPropertyChanged();
                }
            }
        }
        public Guest1GradeVM Guest1Grade
        {
            get => new Guest1GradeVM(_reservation.Guest1Grade);
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
            ReservationService reservationService = new ReservationService(new ReservationRepository());
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
                else if(DateOnly.FromDateTime(DateTime.Now) > EndDate)
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
