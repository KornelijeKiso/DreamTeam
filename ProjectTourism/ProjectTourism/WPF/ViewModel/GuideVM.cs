using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
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
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            TourService tourService = new TourService(new TourRepository());
            TicketService ticketService = new TicketService(new TicketRepository());
            TicketGradeService ticketGradeService = new TicketGradeService(new TicketGradeRepository());
            LocationService locationService = new LocationService(new LocationRepository());
            GuideService guideService = new GuideService(new GuideRepository());
            Guest2Service guest2Service = new Guest2Service(new Guest2Repository());
            VoucherService voucherService = new VoucherService(new VoucherRepository());

            _guide = guideService.GetOne(username);
            

            foreach (var tour in tourService.GetAll())
            {
                if (tour.GuideUsername.Equals(username))
                {
                    tour.StopsList = tourService.LoadStops(tour);
                    tour.Guide = _guide;
                    tour.Location = locationService.GetOne(tour.LocationId);
                    foreach (var tourApp in tourAppointmentService.GetAllByTour(tour.Id))
                    {
                        tourApp.Tour = tour;
                        tourApp.Vouchers.AddRange(voucherService.GetAllByTourAppointment(tourApp.Id));
                        foreach (var ticket in ticketService.GetByAppointment(tourApp.Id))
                        {
                            ticket.TourAppointment = tourApp;
                            ticket.Guest2 = guest2Service.GetOne(ticket.Guest2Username);
                            ticket.TicketGrade = ticketGradeService.GetOneByTicket(ticket.Id);
                            tourApp.Tickets.Add(ticket);
                        }
                        tour.TourAppointments.Add(tourApp);
                    }
                    _guide.Tours.Add(tour);
                    _guide.TourAppointments.AddRange(tour.TourAppointments);
                }
            }
        }
        public void CancelAppointment(TourAppointmentVM tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            CanceledTourAppointmentsService canceledTourAppointmentsService = new CanceledTourAppointmentsService(new CanceledTourAppointmentsRepository());
            TourAppointments.Remove(tourApp);
            tourAppointmentService.Delete(tourApp.Id);
            canceledTourAppointmentsService.Add(tourApp.GetTourAppointment());
        }
        public string NextStop(TourAppointment tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            TourService tourService = new TourService(new TourRepository());

            string currentStop = tourApp.CurrentTourStop;
            int nextStopIndex = tourApp.Tour.StopsList.FindIndex(stop => stop == currentStop) + 1;

            tourApp.CurrentTourStop = tourApp.Tour.StopsList[nextStopIndex];
            tourApp.State = TOURSTATE.STARTED;
            tourAppointmentService.Update(tourApp);
            return tourService.GetNextStop(tourApp.Tour, nextStopIndex-1);
        }
        public string FinishTourAndReturnStop(TourAppointmentVM tourApp)
        {
            GuideService guideService = new GuideService(new GuideRepository());
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());

            tourApp.CurrentTourStop = tourApp.Tour.StopsList.Last();
            tourApp.State = TOURSTATE.FINISHED;
            tourApp.IsFinished = true;
            tourApp.IsNotFinished = false;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
            guideService.Update(_guide);
            return tourApp.Tour.StopsList.Last();
        }
        public void CheckTicket(TicketVM ticket)
        {
            ticket.HasGuideChecked = true;
            TicketService ticketService = new TicketService(new TicketRepository());
            ticketService.Update(ticket.GetTicket());
        }

        public void EmergencyStop(TourAppointmentVM tourApp)
        {
            GuideService guideService = new GuideService(new GuideRepository());
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());

            tourApp.State = TOURSTATE.STOPPED;
            tourApp.IsNotFinished = false;
            tourApp.IsFinished = true;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
            _guide.HasTourStarted = false;
            guideService.Update(_guide);
        }

        public void AddTour(TourVM NewTour, LocationVM NewLocation)
        {
            TourService tourService = new TourService(new TourRepository());
            LocationService locationService = new LocationService(new LocationRepository());
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());


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
        public ObservableCollection<TourVM> Tours { get; set; }
        public ObservableCollection<TourAppointmentVM> TourAppointments { get; set; }
        public ObservableCollection<TourAppointmentVM> TodaysAppointments { get; set; }
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
