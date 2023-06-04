﻿using ProjectTourism.Domain.Model;
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
