using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;

namespace ProjectTourism.WPF.ViewModel
{
    public class GuideVM : INotifyPropertyChanged
    {
        private Guide _guide;
        public GuideVM(Guide guide)
        {
            _guide = guide;
            Tours = new ObservableCollection<TourVM>(_guide.Tours.Select(r => new TourVM(r)).ToList());
            TourAppointments = new ObservableCollection<TourAppointmentVM>(_guide.TourAppointments.Select(r => new TourAppointmentVM(r)).ToList());
            TodaysAppointments = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.TourDateTime.Date.Equals(DateTime.Now.Date)));
        }
        public GuideVM(string username)
        {
            Synchronize(username);
            Tours = new ObservableCollection<TourVM>(_guide.Tours.Select(r => new TourVM(r)).ToList());
            TourAppointments = new ObservableCollection<TourAppointmentVM>(_guide.TourAppointments.Select(r => new TourAppointmentVM(r)).ToList());
            TodaysAppointments = new ObservableCollection<TourAppointmentVM>(TourAppointments.Where(t => t.TourDateTime.Date.Equals(DateTime.Now.Date)));
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
            foreach (var tour in tourService.GetAll())
            {
                if (tour.GuideUsername.Equals(username))
                {
                    tour.StopsList = tourService.LoadStops(tour);
                    tour.Guide = _guide;
                    tour.Location = locationService.GetOne(tour.LocationId);
                    SynchronizeTourAppointments(tourAppointmentService, ticketService, ticketGradeService, guest2Service, voucherService, tour);
                    if (!_guide.Tours.Contains(tour))
                    {
                        _guide.Tours.Add(tour);
                    }
                    foreach(var app in tour.TourAppointments)
                    {
                        if (!_guide.TourAppointments.Contains(app))
                            _guide.TourAppointments.Add(app);
                    }
                }
            }
        }

        public void DismissRequest(RequestVM request)
        {
            RequestService requestService = new RequestService();
            request.State = REQUESTSTATE.DISMISSED;
            requestService.Update(request.GetRequest());
        }
        public void AcceptRequest(RequestVM request)
        {
            RequestService requestService = new RequestService();
            request.State = REQUESTSTATE.ACCEPTED;
            requestService.Update(request.GetRequest());
        }
        public void Add(Guide Guide)
        {
            GuideService guideService = new GuideService();
            guideService.Add(Guide);
        }
        private static void SynchronizeTourAppointments(TourAppointmentService tourAppointmentService, TicketService ticketService, TicketGradeService ticketGradeService, Guest2Service guest2Service, VoucherService voucherService, Tour tour)
        {
            foreach (var tourApp in tourAppointmentService.GetAllByTour(tour.Id))
            {
                tourApp.Tour = tour;
                tourApp.TicketGrades = new List<TicketGrade>();
                tourApp.Tickets = new List<Ticket>();
                SynchronizeTickets(ticketService, ticketGradeService, guest2Service, voucherService, tourApp);
                tour.TourAppointments.Add(tourApp);
            }
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
            _guide.HasTourStarted = true;
            guideService.Update(_guide);
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
            GuideService guideService = new GuideService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.CurrentTourStop = tourApp.Tour.StopsList.Last();
            tourApp.State = TOURSTATE.FINISHED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
            guideService.Update(_guide);
        }
        public string FinishTourAndReturnStop(TourAppointmentVM tourApp)
        {
            GuideService guideService = new GuideService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.CurrentTourStop = tourApp.Tour.StopsList.Last();
            tourApp.State = TOURSTATE.FINISHED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
            _guide.HasTourStarted = false;
            guideService.Update(_guide);
            return tourApp.Tour.StopsList.Last();
        }
        public void CheckTicket(TicketVM ticket)
        {
            ticket.HasGuideChecked = true;
            TicketService ticketService = new TicketService();
            ticketService.Update(ticket.GetTicket());
        }

        public void EmergencyStop(TourAppointmentVM tourApp)
        {
            GuideService guideService = new GuideService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();

            tourApp.State = TOURSTATE.STOPPED;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
            _guide.HasTourStarted = false;
            guideService.Update(_guide);
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

        public ObservableCollection<RequestVM> GetAllRequests()
        {
            RequestService requestService = new RequestService();
            LocationService locationService = new LocationService();
            ObservableCollection<RequestVM> requests = new ObservableCollection<RequestVM>();

            foreach(var request in requestService.GetAll())
            {
                request.Location = locationService.GetOne(request.LocationId);
                requests.Add(new RequestVM(request));
            }
            return requests;
        }
        public ObservableCollection<TourVM> Tours { get; set; }
        public ObservableCollection<TourAppointmentVM> TourAppointments { get; set; }
        public ObservableCollection<TourAppointmentVM> TodaysAppointments { get; set; }
        public ObservableCollection<RequestVM> Requests { get => GetAllRequests(); }
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
        public bool HasTourStarted
        {
            get => _guide.HasTourStarted;
            set
            {
                if (_guide.HasTourStarted != value)
                {
                    _guide.HasTourStarted = value;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
