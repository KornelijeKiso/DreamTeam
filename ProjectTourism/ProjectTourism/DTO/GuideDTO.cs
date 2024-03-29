﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ProjectTourism.Model;
using ProjectTourism.Services;
using System.Timers;
using ProjectTourism.Domain.Model;
using System.Windows;

namespace ProjectTourism.DTO
{
    public class GuideDTO : INotifyPropertyChanged
    {
        private Guide _guide;
        public Timer Timer;
        public Guide GetGuide()
        {
            return _guide;
        }
        public GuideDTO(Guide guide)
        {
            _guide = guide;
            Tours = new ObservableCollection<TourDTO>(_guide.Tours.Select(r => new TourDTO(r)).ToList());
            TourAppointments = new ObservableCollection<TourAppointmentDTO>(_guide.TourAppointments.Select(r => new TourAppointmentDTO(r)).ToList());
            TodaysAppointments = new ObservableCollection<TourAppointmentDTO>(TourAppointments.Where(t => t.TourDateTime.Date.Equals(DateTime.Now.Date)).OrderBy(t => t.TourDateTime));
            ComplexTours = new ObservableCollection<ComplexTourDTO>(_guide.ComplexTours.Select(r => new ComplexTourDTO(r)).ToList());
        }
        public GuideDTO(string username)
        {
            Synchronize(username);
            Timer = new Timer(5000);
            SetTimer();
        }

        public void SetTimer()
        {
            Timer.Elapsed += TimerElapsed;
            Timer.AutoReset = true;
            Timer.Start();
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Synchronize(Username);
        }
        private void Synchronize(string username)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            TourService tourService = new TourService();
            TicketService ticketService = new TicketService();
            TicketGradeService ticketGradeService = new TicketGradeService();
            LocationService locationService = new LocationService();
            GuideService guideService = new GuideService();
            Guest2Service guest2Service = new Guest2Service();
            VoucherService voucherService = new VoucherService();

            _guide = guideService.GetOne(username);
            _guide.Tours = tourService.GetAll().Where(t => t.GuideUsername.Equals(username)).ToList();
            foreach (var tour in _guide.Tours)
            {
                tour.StopsList = tourService.LoadStops(tour);
                tour.Guide = _guide;
                tour.Location = locationService.GetOne(tour.LocationId);
                tour.TourAppointments = SynchronizeTourAppointments(tourAppointmentService, ticketService, ticketGradeService, guest2Service, voucherService, tour);

                foreach (var app in tour.TourAppointments)
                {
                    if (!_guide.TourAppointments.Contains(app))
                    {
                        tourAppointmentService.Update(app);
                        _guide.TourAppointments.Add(app);
                    }
                }
            }
            SynchronizeComplexToursList(_guide);
            Tours = new ObservableCollection<TourDTO>(_guide.Tours.Select(r => new TourDTO(r)).ToList());
            TourAppointments = new ObservableCollection<TourAppointmentDTO>(_guide.TourAppointments.Select(r => new TourAppointmentDTO(r)).ToList());
            TodaysAppointments = new ObservableCollection<TourAppointmentDTO>(TourAppointments.Where(t => t.TourDateTime.Date.Equals(DateTime.Now.Date)).OrderBy(t => t.TourDateTime));
            ComplexTours = new ObservableCollection<ComplexTourDTO>(_guide.ComplexTours.Select(r => new ComplexTourDTO(r)).ToList());
            SortByDate();
        }
        private void SynchronizeComplexToursList(Guide guide)
        {
            ComplexTourService complexTourService = new ComplexTourService(); 
            ComplexTourRequestPartService complexTourRequestPartService = new ComplexTourRequestPartService();
            LocationService locationService = new LocationService();
            guide.ComplexTours = new List<ComplexTour>();
            
            foreach (var complexTour in complexTourService.GetAll())
            {
                complexTour.TourRequests = new List<TourRequest>();
                string[] tourRequestIDs = complexTour.TourRequestString.Split(',');
                foreach (string tourRequestId in tourRequestIDs)
                {
                    tourRequestId.Trim();
                    if (tourRequestId != "")
                    {
                        TourRequest complexTourPart = complexTourRequestPartService.GetOne(int.Parse(tourRequestId));
                        complexTourPart.Location = locationService.GetOne(complexTourPart.LocationId);
                        complexTour.TourRequests.Add(complexTourRequestPartService.GetOne(int.Parse(tourRequestId)));
                    }
                }
                guide.ComplexTours.Add(complexTour);
            }
        }
        public void ReportTicketGrade(TicketDTO ticket)
        {
            TicketGradeService ticketGradeService = new TicketGradeService();
            ticket.TicketGrade.IsNotReported = false;
            ticketGradeService.Update(ticket.TicketGrade.GetTicketGrade());
        }
        public void CancelAppointment(TourAppointmentDTO tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            tourApp.State = TOURSTATE.CANCELED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
        }
        public string NextStop(TourAppointment tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            GuideService guideService = new GuideService();

            string currentStop = tourApp.CurrentTourStop;
            int stopIndex = CalculateStopIndex(tourApp, currentStop);
            MoveCurrentStop(tourApp, stopIndex);
            tourApp.State = TOURSTATE.STARTED;
            tourAppointmentService.Update(tourApp);
            return tourApp.CurrentTourStop;
        }
        private static void MoveCurrentStop(TourAppointment tourApp, int stopIndex)
        {
            if (stopIndex == tourApp.Tour.StopsList.Count)
                tourApp.CurrentTourStop = tourApp.Tour.StopsList.Last();
            else
                tourApp.CurrentTourStop = tourApp.Tour.StopsList[stopIndex + 1];
        }
        private static int CalculateStopIndex(TourAppointment tourApp, string currentStop)
        {
            int stopIndex = 0;
            foreach (var stop in tourApp.Tour.StopsList)
            {
                if (stop.Trim() == currentStop.Trim())
                    return stopIndex;
                stopIndex += 1;
            }
            return stopIndex;
        }
        public void EndTour(TourAppointmentDTO tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.CurrentTourStop = tourApp.Tour.Finish;
            tourApp.State = TOURSTATE.FINISHED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
        }
        public string FinishTourAndReturnStop(TourAppointmentDTO tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.CurrentTourStop = tourApp.Tour.Finish;
            tourApp.State = TOURSTATE.FINISHED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
            return tourApp.Tour.Finish;
        }
        public void CheckTicket(TicketDTO ticket)
        {
            ticket.HasGuideChecked = true;
            TicketService ticketService = new TicketService();
            ticketService.Update(ticket.GetTicket());
            // Ticket notification
            NotificationService notificationService = new NotificationService();
            notificationService.Add(new Notification("Ticket (id:" + ticket.Id + ")",
                                                     "Guide " + ticket.TourAppointment.Tour.Guide.FirstName + " " + ticket.TourAppointment.Tour.Guide.LastName + " (" + ticket.TourAppointment.Tour.Guide.Username + ") has checked if you are present on " + ticket.TourAppointment.Tour.Name + ". Please confirm your attendance. ",
                                                     ticket.Guest2Username));
        }
        public void EmergencyStop(TourAppointmentDTO tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.State = TOURSTATE.STOPPED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
        }
        public bool CanGuideAcceptAppointment(DateTime dateTime)
        {
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.TourDateTime.Date == dateTime.Date && tourApp.TourDateTime.AddHours(tourApp.Tour.Duration) > dateTime)
                    return false;
            }
            return true;
        }
        public void AcceptTourRequest(TourRequestDTO tourRequest)
        {
            TourRequestService tourRequestService = new TourRequestService();
            tourRequest.State = REQUESTSTATE.ACCEPTED;
            tourRequestService.Update(tourRequest.GetTourRequest());
        }
        public void AddTour(TourDTO NewTour, LocationDTO NewLocation)
        {
            TourService tourService = new TourService();
            LocationService locationService = new LocationService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            Location location = new Location(NewLocation.City, NewLocation.Country);
            location.Id = locationService.AddAndReturnId(location);
            NewLocation.Id = location.Id;
            NewTour.LocationId = location.Id;
            NewTour.Location = new LocationDTO(location);

            Tours.Add(NewTour);
            _guide.Tours.Add(NewTour.GetTour());
            Tour tour = new Tour(NewTour.GetTour());
            tour.Id = tourService.AddAndReturnId(NewTour.GetTour());
            tourAppointmentService.MakeTourAppointments(tour);
        }

        public void NotifyGuests(TourDTO newTour)   // New Tour notification
        {
            NotificationService notificationService = new NotificationService();
            TourRequestService requestService = new TourRequestService();

            List<TourRequest> tourRequests = requestService.GetByNewTour(newTour.GetTour());
            foreach (TourRequest tourRequest in tourRequests)
            {
                notificationService.Add(new Notification("New Tour (id:" + newTour.Id + ")" ,
                                                         "Guide " + newTour.Guide.FirstName + " " + newTour.Guide.LastName + " (" + newTour.Guide.Username + ") has created a Tour similar to your unfulfilled request - check out new " + newTour.Name,
                                                         tourRequest.Guest2Username));
            }
        }

        public void ChangeLocalization()
        {
            GuideService guideService = new GuideService();
            guideService.UpdateLocalization(_guide);
        }
        public List<TourAppointmentDTO> FindGuidesReadyAppointments()
        {
            List<TourAppointmentDTO> readyTourApps = new List<TourAppointmentDTO>();
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.State == TOURSTATE.READY && tourApp.TourDateTime >= DateTime.Now)
                    readyTourApps.Add(tourApp);
            }
            return readyTourApps;
        }
        public TourAppointmentDTO FindGuidesUpcomingTourApp()
        {
            List<TourAppointmentDTO> ReadyAppointments = new List<TourAppointmentDTO>();
            ReadyAppointments = FindGuidesReadyAppointments();
            if (ReadyAppointments.Count == 0)
            {
                return null;
            }
            TourAppointmentDTO closestTourAppointment = new TourAppointmentDTO(ReadyAppointments[0].GetTourAppointment());
            TimeSpan closestDifference = closestTourAppointment.TourDateTime - DateTime.Today;
            if (closestDifference < TimeSpan.Zero)
            {
                closestDifference = closestDifference.Negate();
            }

            foreach (var tourApp in ReadyAppointments)
            {
                TimeSpan tourAppDifference = tourApp.TourDateTime - DateTime.Today;
                if (tourAppDifference < TimeSpan.Zero)
                {
                    tourAppDifference = tourAppDifference.Negate();
                }

                if (tourAppDifference < closestDifference)
                {
                    closestTourAppointment = new TourAppointmentDTO(tourApp.GetTourAppointment());
                    closestDifference = tourAppDifference;
                }
            }

            return closestTourAppointment;
        }

        public void Add(Guide Guide)
        {
            GuideService guideService = new GuideService();
            guideService.Add(Guide);
        }
        private List<TourAppointment> SynchronizeTourAppointments(TourAppointmentService tourAppointmentService, TicketService ticketService, TicketGradeService ticketGradeService, Guest2Service guest2Service, VoucherService voucherService, Tour tour)
        {
            List<TourAppointment> tourAppointments = new List<TourAppointment>();
            foreach (var tourApp in tourAppointmentService.GetAllByTour(tour.Id))
            {
                tourApp.Tour = tour;
                tourApp.TicketGrades = new List<TicketGrade>();
                tourApp.Tickets = new List<Ticket>();
                SynchronizeTickets(ticketService, ticketGradeService, guest2Service, voucherService, tourApp);
                tourAppointments.Add(tourApp);
            }
            return tourAppointments;
        }
        private static void SynchronizeTickets(TicketService ticketService, TicketGradeService ticketGradeService, Guest2Service guest2Service, VoucherService voucherService, TourAppointment tourApp)
        {
            foreach (var ticket in ticketService.GetByAppointment(tourApp.Id))
            {
                ticket.HasVoucher = voucherService.GetOneByTicket(ticket.Id) != null;
                ticket.Guest2 = guest2Service.GetOne(ticket.Guest2Username);
                ticket.TicketGrade = ticketGradeService.GetOneByTicket(ticket.Id);
                tourApp.Tickets.Add(ticket);
                if (ticket.TicketGrade != null)
                {
                    tourApp.TicketGrades.Add(ticket.TicketGrade);
                }
                ticket.TourAppointment = tourApp;
            }
            tourApp.TicketGrades.RemoveAll(t => t == null);
        }

        
        public ObservableCollection<TourRequestDTO> GetAllTourRequests()
        {
            TourRequestService tourRequestService = new TourRequestService();
            LocationService locationService = new LocationService();
            ObservableCollection<TourRequestDTO> tourRequests = new ObservableCollection<TourRequestDTO>();

            foreach (var tourRequest in tourRequestService.GetAll())
            {
                tourRequest.Location = locationService.GetOne(tourRequest.LocationId);
                tourRequests.Add(new TourRequestDTO(tourRequest));
            }
            return tourRequests;
        }

        public void Quit()
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            foreach (var tourApp in _guide.TourAppointments)
            {
                if (tourApp.State == TOURSTATE.READY || tourApp.State == TOURSTATE.STARTED)
                {
                    foreach (var ticket in tourApp.Tickets)
                    {
                        Voucher voucher = new Voucher(ticket.Guest2Username, 
                            "Congrats, you won a Voucher! Guide " + _guide.FirstName + " " + _guide.LastName + " (" + _guide.Username + ") quit! " +
                            "You have a ticket for his Tour - " + tourApp.Tour.Name + " on " + tourApp.TourDateTime.ToString() + "! " +
                            "As an apology for the inconvinience we offer this Voucher! Use it until it expires!");
                        VoucherService voucherService = new VoucherService();
                        voucherService.Add(voucher);
                    }
                    tourApp.State = TOURSTATE.CANCELED;
                    tourAppointmentService.Update(tourApp);
                }
            }
        }

        public void ChangeTheme()
        {
            GuideService guideService = new GuideService();

            if (_guide.DarkTheme)
                _guide.DarkTheme = false;
            else
                _guide.DarkTheme = true;
            guideService.Update(_guide);
        }

        private ObservableCollection<TourAppointmentDTO> _FinishedApps;
        public ObservableCollection<TourAppointmentDTO> FinishedApps
        {
            get => _FinishedApps;
            set
            {
                if (_FinishedApps != value)
                {
                    _FinishedApps = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<TourAppointmentDTO> _CanceledApps;
        public ObservableCollection<TourAppointmentDTO> CanceledApps
        {
            get => _CanceledApps;
            set
            {
                if (_CanceledApps != value)
                {
                    _CanceledApps = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<TourAppointmentDTO> _ReadyApps;
        public ObservableCollection<TourAppointmentDTO> ReadyApps
        {
            get => _ReadyApps;
            set
            {
                if (_ReadyApps != value)
                {
                    _ReadyApps = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<TourAppointmentDTO> _StoppedApps;
        public ObservableCollection<TourAppointmentDTO> StoppedApps
        {
            get => _StoppedApps;
            set
            {
                if (_StoppedApps != value)
                {
                    _StoppedApps = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TourAppointmentDTO> _ExpiredApps;
        public ObservableCollection<TourAppointmentDTO> ExpiredApps
        {
            get => _ExpiredApps;
            set
            {
                if (_ExpiredApps != value)
                {
                    _ExpiredApps = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool HasGuideStartedTour()
        {
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.State.Equals(TOURSTATE.STARTED))
                    return true;
            }
            return false;
        }
        public bool HasTourStarted
        {
            get => HasGuideStartedTour();
        }
        public bool IsTodaysAppsImageVisible
        {
            get => TourAppointments.Where(t => t.TourDateTime.Date.Equals(DateTime.Now.Date)).Count() == 0;
        }
        public ObservableCollection<string> GetAllLanguages()
        {
            ObservableCollection<string> languages = new ObservableCollection<string>();
            foreach (var tourApp in _guide.TourAppointments)
            {
                if (!languages.Contains(tourApp.Tour.Language))
                    languages.Add(tourApp.Tour.Language);
            }
            return languages;
        }

        private ObservableCollection<string> SuperGuideChecker()
        {
            ObservableCollection<string> SuperGuideLanguages = new ObservableCollection<string>();

            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            foreach (var language in GetAllLanguages())
            {
                int tourCount = 0;
                double totalRating = 0.0;

                foreach (var tourApp in _guide.TourAppointments)
                {
                    if (tourApp.TourDateTime >= oneYearAgo && tourApp.Tour.Language == language)
                    {
                        foreach (var ticket in tourApp.Tickets)
                        {
                            if (ticket.TicketGrade != null && GoodGrade(ticket.TicketGrade))
                            {
                                tourCount++;
                                totalRating += CalculateAverageScore(ticket.TicketGrade);
                            }
                        }
                    }
                }

                if (tourCount >= 3 && totalRating / tourCount > 9.0) //treba da stoji 20
                {
                    SuperGuideLanguages.Add(language);
                }
            }

            return SuperGuideLanguages;
        }

        private bool GoodGrade(TicketGrade grade)
        {
            foreach (var category in TicketGrade.CategoryNames)
            {
                if (grade.Grades[category] <= 9)
                {
                    return false;
                }
            }

            return true;
        }

        private double CalculateAverageScore(TicketGrade grade)
        {
            double totalScore = 0.0;
            int totalGrades = 0;

            foreach (var category in TicketGrade.CategoryNames)
            {
                totalScore += grade.Grades[category];
                totalGrades++;
            }

            return totalScore / totalGrades;
        }

        public bool IsSuperGuide
        {
            get => SuperGuideChecker().Count() != 0;
        }

        public ObservableCollection<String> SuperGuidesLanguages
        {
            get => SuperGuideChecker();
        }

        private void SortByDate()
        {
            FinishedApps = new ObservableCollection<TourAppointmentDTO>(TourAppointments.Where(t => t.State == TOURSTATE.FINISHED).OrderByDescending(a => a.TourDateTime));
            ReadyApps = new ObservableCollection<TourAppointmentDTO>(TourAppointments.Where(t => t.State == TOURSTATE.READY).OrderByDescending(a => a.TourDateTime));
            StoppedApps = new ObservableCollection<TourAppointmentDTO>(TourAppointments.Where(t => t.State == TOURSTATE.STOPPED).OrderByDescending(a => a.TourDateTime));
            CanceledApps = new ObservableCollection<TourAppointmentDTO>(TourAppointments.Where(t => t.State == TOURSTATE.CANCELED).OrderByDescending(a => a.TourDateTime));
            ExpiredApps = new ObservableCollection<TourAppointmentDTO>(TourAppointments.Where(t => t.State == TOURSTATE.EXPIRED).OrderByDescending(a => a.TourDateTime));
        }


        public ObservableCollection<ComplexTourDTO> _ComplexTours;
        public ObservableCollection<ComplexTourDTO> ComplexTours
        {
            get => _ComplexTours;
            set
            {
                if (_ComplexTours != value)
                {
                    _ComplexTours = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TourDTO> _Tours;
        public ObservableCollection<TourDTO> Tours
        {
            get => _Tours;
            set
            {
                if (_Tours != value)
                {
                    _Tours = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TourAppointmentDTO> _TourAppointments;
        public ObservableCollection<TourAppointmentDTO> TourAppointments
        {
            get => _TourAppointments;
            set
            {
                if (_TourAppointments != value)
                {
                    _TourAppointments = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TourAppointmentDTO> _TodaysAppointments;
        public ObservableCollection<TourAppointmentDTO> TodaysAppointments
        {
            get => _TodaysAppointments;
            set
            {
                if (_TodaysAppointments != value)
                {
                    _TodaysAppointments = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<TourRequestDTO> _TourRequests;
        public ObservableCollection<TourRequestDTO> TourRequests
        {
            get => GetAllTourRequests();
            set
            {
                if (_TourRequests != value)
                {
                    _TourRequests = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Username
        {
            get => _guide.Username;
            set
            {
                if (_guide.Username != value)
                {
                    _guide.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => _guide.Password;
            set
            {
                if (value != _guide.Password)
                {
                    _guide.Password = value;
                    OnPropertyChanged();
                }
            }
        }
        public USERTYPE Type
        {
            get => _guide.Type;
            set
            {
                if (value != _guide.Type)
                {
                    _guide.Type = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => _guide.FirstName;
            set
            {
                if (value != _guide.FirstName)
                {
                    _guide.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => _guide.LastName;
            set
            {
                if (value != _guide.LastName)
                {
                    _guide.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Birthday
        {
            get => _guide.Birthday;
            set
            {
                if (value != _guide.Birthday)
                {
                    _guide.Birthday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => _guide.Email;
            set
            {
                if (value != _guide.Email)
                {
                    _guide.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PhoneNumber
        {
            get => _guide.PhoneNumber;
            set
            {
                if (value != _guide.PhoneNumber)
                {
                    _guide.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Biography
        {
            get => _guide.Biography;
            set
            {
                if (_guide.Biography != value)
                {
                    _guide.Biography = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Language
        {
            get => _guide.Language;
            set
            {
                if (_guide.Language != value)
                {
                    _guide.Language = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Localization
        {
            get => _guide.Localization;
            set
            {
                if (_guide.Localization != value)
                {
                    _guide.Localization = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool DarkTheme
        {
            get => _guide.DarkTheme;
            set
            {
                if (_guide.DarkTheme != value)
                {
                    _guide.DarkTheme = value;
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
