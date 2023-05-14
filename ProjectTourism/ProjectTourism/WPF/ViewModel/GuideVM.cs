using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using ProjectTourism.Model;
using ProjectTourism.Services;

namespace ProjectTourism.WPF.ViewModel
{
    public class GuideVM : INotifyPropertyChanged
    {
        private Guide _guide;
        public Timer Timer;
        public GuideVM(Guide guide)
        {
            _guide = guide;
            Tours = new ObservableCollection<TourVM>(_guide.Tours.Select(r => new TourVM(r)).ToList());
            TourAppointments = new ObservableCollection<TourAppointmentVM>(_guide.TourAppointments.Select(r => new TourAppointmentVM(r)).ToList());
            TodaysAppointments = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.TourDateTime.Date.Equals(DateTime.Now.Date)).OrderBy(t => t.TourDateTime));
        }
        public GuideVM(string username)
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
                
                foreach(var app in tour.TourAppointments)
                {
                    if (!_guide.TourAppointments.Contains(app))
                    {
                        tourAppointmentService.Update(app);
                        _guide.TourAppointments.Add(app);
                    }
                }
            }
            Tours = new ObservableCollection<TourVM>(_guide.Tours.Select(r => new TourVM(r)).ToList());
            TourAppointments = new ObservableCollection<TourAppointmentVM>(_guide.TourAppointments.Select(r => new TourAppointmentVM(r)).ToList());
            TodaysAppointments = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.TourDateTime.Date.Equals(DateTime.Now.Date)).OrderBy(t => t.TourDateTime));
            SortByDate();
        }

        public void ChangeLocalization()
        {
            GuideService guideService = new GuideService();
            guideService.UpdateLocalization(_guide);
        }
        public List<TourAppointmentVM> FindGuidesReadyAppointments()
        {
            List<TourAppointmentVM> readyTourApps = new List<TourAppointmentVM>();
            foreach(var tourApp in TourAppointments)
            {
                if(tourApp.State == TOURSTATE.READY && tourApp.TourDateTime >= DateTime.Now)
                    readyTourApps.Add(tourApp);
            }
            return readyTourApps;
        }
        public TourAppointmentVM FindGuidesUpcomingTourApp()
        {
            List<TourAppointmentVM> ReadyAppointments = new List<TourAppointmentVM>();
            ReadyAppointments = FindGuidesReadyAppointments();
            if (ReadyAppointments.Count == 0)
            {
                return null;
            }
            TourAppointmentVM closestTourAppointment = new TourAppointmentVM(ReadyAppointments[0].GetTourAppointment());
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
                    closestTourAppointment = new TourAppointmentVM(tourApp.GetTourAppointment());
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
        public void AcceptTourRequest(TourRequestVM tourRequest)
        {
            TourRequestService tourRequestService = new TourRequestService();
            tourRequest.State = REQUESTSTATE.ACCEPTED;
            tourRequestService.Update(tourRequest.GetTourRequest());
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
        }
        public void ReportTicketGrade(TicketVM ticket)
        {
            TicketGradeService ticketGradeService = new TicketGradeService();
            ticket.TicketGrade.IsNotReported = false;
            ticketGradeService.Update(ticket.TicketGrade.GetTicketGrade());
        }
        public void CancelAppointment(TourAppointmentVM tourApp)
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
        public void EndTour(TourAppointmentVM tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.CurrentTourStop = tourApp.Tour.Finish;
            tourApp.State = TOURSTATE.FINISHED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
        }
        public string FinishTourAndReturnStop(TourAppointmentVM tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.CurrentTourStop = tourApp.Tour.Finish;
            tourApp.State = TOURSTATE.FINISHED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
            return tourApp.Tour.Finish;
        }
        public void CheckTicket(TicketVM ticket)
        {
            ticket.HasGuideChecked = true;
            TicketService ticketService = new TicketService();
            ticketService.Update(ticket.GetTicket());
        }
        public void EmergencyStop(TourAppointmentVM tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.State = TOURSTATE.STOPPED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
        }
        public void AddTour(TourVM NewTour, LocationVM NewLocation)
        {
            TourService tourService = new TourService();
            LocationService locationService = new LocationService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            Location location = new Location(NewLocation.City, NewLocation.Country);
            location.Id = locationService.AddAndReturnId(location);
            NewLocation.Id = location.Id;
            NewTour.LocationId = location.Id;
            NewTour.Location = new LocationVM(location);

            Tours.Add(NewTour);
            _guide.Tours.Add(NewTour.GetTour());
            Tour tour = new Tour(NewTour.GetTour());
            tour.Id = tourService.AddAndReturnId(NewTour.GetTour());
            tourAppointmentService.MakeTourAppointments(tour);
        }
        public Guide GetGuide()
        {
            return _guide;
        }
        public ObservableCollection<TourRequestVM> GetAllTourRequests()
        {
            TourRequestService tourRequestService = new TourRequestService();
            LocationService locationService = new LocationService();
            ObservableCollection<TourRequestVM> tourRequests = new ObservableCollection<TourRequestVM>();

            foreach (var tourRequest in tourRequestService.GetAll())
            {
                tourRequest.Location = locationService.GetOne(tourRequest.LocationId);
                tourRequests.Add(new TourRequestVM(tourRequest));
            }
            return tourRequests;
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

        private ObservableCollection<TourAppointmentVM> _FinishedApps;
        public ObservableCollection<TourAppointmentVM> FinishedApps
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
        private ObservableCollection<TourAppointmentVM> _CanceledApps;
        public ObservableCollection<TourAppointmentVM> CanceledApps
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
        private ObservableCollection<TourAppointmentVM> _ReadyApps;
        public ObservableCollection<TourAppointmentVM> ReadyApps
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
        private ObservableCollection<TourAppointmentVM> _StoppedApps;
        public ObservableCollection<TourAppointmentVM> StoppedApps
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

        private ObservableCollection<TourAppointmentVM> _ExpiredApps;
        public ObservableCollection<TourAppointmentVM> ExpiredApps
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
            foreach(var tourApp in TourAppointments)
            {
                if(tourApp.State.Equals(TOURSTATE.STARTED)) 
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
        private void SortByDate()
        {
            FinishedApps = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.State == TOURSTATE.FINISHED).OrderByDescending(a => a.TourDateTime));
            ReadyApps = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.State == TOURSTATE.READY).OrderByDescending(a => a.TourDateTime));
            StoppedApps = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.State == TOURSTATE.STOPPED).OrderByDescending(a => a.TourDateTime));
            CanceledApps = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.State == TOURSTATE.CANCELED).OrderByDescending(a => a.TourDateTime));
            ExpiredApps = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.State == TOURSTATE.EXPIRED).OrderByDescending(a => a.TourDateTime));
        }

        private ObservableCollection<TourVM> _Tours;
        public ObservableCollection<TourVM> Tours
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
        public ObservableCollection<TourAppointmentVM> _TourAppointments;
        public ObservableCollection<TourAppointmentVM> TourAppointments
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
        public ObservableCollection<TourAppointmentVM> _TodaysAppointments;
        public ObservableCollection<TourAppointmentVM> TodaysAppointments
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
        private ObservableCollection<TourRequestVM> _TourRequests;
        public ObservableCollection<TourRequestVM> TourRequests
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
        public bool? IsSuperGuide
        {
            get => _guide.IsSuperGuide;
            set
            {
                if (_guide.IsSuperGuide != value)
                {
                    _guide.IsSuperGuide = value;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
