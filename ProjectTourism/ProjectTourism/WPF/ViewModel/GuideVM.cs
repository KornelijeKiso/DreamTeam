using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        }
        public GuideVM(string username)
        {
            Synchronize(username);
            Tours = new ObservableCollection<TourVM>(_guide.Tours.Select(r => new TourVM(r)).ToList());
            TourAppointments = new ObservableCollection<TourAppointmentVM>(_guide.TourAppointments.Select(r => new TourAppointmentVM(r)).ToList());
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

            _guide = guideService.GetOne(username);
            foreach(var tour in tourService.GetAll())
            {
                if (tour.GuideUsername.Equals(username))
                {
                    tour.StopsList = tourService.LoadStops(tour);
                    tour.Guide = _guide;
                    tour.Location = locationService.GetOne(tour.LocationId);
                    foreach(var tourApp in tourAppointmentService.GetAllByTour(tour.Id))
                    {
                        tourApp.Tour = tour;
                        foreach(var ticket in ticketService.GetByAppointment(tourApp.Id))
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
        public string NextStop(int nextStopIndex, TourAppointment tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            TourService tourService = new TourService(new TourRepository());
            
            tourApp.CurrentTourStop = tourApp.Tour.StopsList[nextStopIndex];
            tourApp.State = TOURSTATE.STARTED;
            tourAppointmentService.Update(tourApp);
            return tourService.GetNextStop(tourApp.Tour, nextStopIndex);
        }
        public string FinishTheTour(TourAppointmentVM tourApp)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());

            tourApp.CurrentTourStop = tourApp.Tour.StopsList.Last();
            tourApp.State = TOURSTATE.FINISHED;
            tourApp.IsFinished = true;
            tourAppointmentService.Update(tourApp.GetTourAppointment());
            FinishTour(tourApp);
            return tourApp.Tour.StopsList.Last();
        }
        public void FinishTour(TourAppointmentVM tourApp)
        {
            GuideService guideService = new GuideService(new GuideRepository());
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());

            tourApp.IsNotFinished = false;
            tourApp.IsFinished = true;
            tourApp.State = TOURSTATE.FINISHED;
            _guide.HasTourStarted = false;
            guideService.Update(_guide);
            tourAppointmentService.Update(tourApp.GetTourAppointment());
        }

        public void CheckTicket(TicketVM ticket)
        {
            ticket.HasGuideChecked= true;
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
