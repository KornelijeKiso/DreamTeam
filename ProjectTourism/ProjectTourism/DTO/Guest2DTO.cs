using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Services;

namespace ProjectTourism.DTO
{
    public class Guest2DTO : INotifyPropertyChanged, IDataErrorInfo
    {
        private Guest2 _guest2 { get; set; }
        public Guest2 GetGuest2()
        {
            return _guest2;
        }
        public Guest2DTO(Guest2 guest2)
        {
            _guest2 = guest2;
            //SelectedTour = null;//new TourDTO(new Tour());
            Tours = new ObservableCollection<TourDTO>();
            Notifications = new ObservableCollection<NotificationDTO>();
            Tickets = new ObservableCollection<TicketDTO>(_guest2.Tickets.Select(r => new TicketDTO(r)).ToList());
            Vouchers = new ObservableCollection<VoucherDTO>(_guest2.Vouchers.Select(r => new VoucherDTO(r)).ToList());
            TourRequests = new ObservableCollection<TourRequestDTO>(_guest2.TourRequests.Select(r => new TourRequestDTO(r)).ToList());
            ComplexTours = new ObservableCollection<ComplexTourDTO>(_guest2.ComplexTours.Select(r => new ComplexTourDTO(r)).ToList());
        }
        public Guest2DTO(string username)
        {
            Synchronize(username);
            //SelectedTour = null;//new TicketDTO(new Ticket());
            Tickets = new ObservableCollection<TicketDTO>(_guest2.Tickets.Select(r => new TicketDTO(r)).ToList());
            Vouchers = new ObservableCollection<VoucherDTO>(_guest2.Vouchers.Select(r => new VoucherDTO(r)).ToList());
            TourRequests = new ObservableCollection<TourRequestDTO>(_guest2.TourRequests.Select(r => new TourRequestDTO(r)).ToList());
            ComplexTours = new ObservableCollection<ComplexTourDTO>(_guest2.ComplexTours.Select(r => new ComplexTourDTO(r)).ToList());
        }
        
       
        public void Synchronize(string username)
        {
            Guest2Service guest2Service = new Guest2Service();
            _guest2 = guest2Service.GetOne(username);

            TourService tourService = new TourService();
            Tours = new ObservableCollection<TourDTO>(tourService.GetAll().Select(r => new TourDTO(r)).ToList());
            SynchronizeTours(Tours);
            SynchronizeNotifications(_guest2);
            SynchronizeTicketsList(_guest2);
            SynchronizeVouchersList(_guest2);
            SynchronizeTourRequestsList(_guest2);
            SynchronizeComplexToursList(_guest2);
        }
       
        private void SynchronizeVouchersList(Guest2 _guest2)
        {
            TicketService ticketService = new TicketService();
            VoucherService voucherService = new VoucherService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            TourService tourService = new TourService();
            TicketGradeService ticketGradeService = new TicketGradeService();
            LocationService locationService = new LocationService();
            GuideService guideService = new GuideService();

            _guest2.Vouchers = new List<Voucher>();
            foreach (var voucher in voucherService.GetAllByGuest2(_guest2.Username))
            {
                voucher.Guest2 = _guest2;
                if (voucher.TicketId == -1)
                {
                    voucher.Ticket = null;
                    _guest2.Vouchers.Add(voucher);
                }
                else
                {
                    voucher.Ticket = ticketService.GetOne(voucher.TicketId);
                    voucher.Ticket.Guest2 = _guest2;

                    voucher.Ticket.TourAppointment = tourAppointmentService.GetOne(voucher.Ticket.TourAppointmentId);
                    voucher.Ticket.TourAppointment.Tickets = new List<Ticket>();
                    voucher.Ticket.TourAppointment.TicketGrades = new List<TicketGrade>();
                    voucher.Ticket.TourAppointment.Tour = tourService.GetOne(voucher.Ticket.TourAppointment.TourId);
                    voucher.Ticket.TourAppointment.Tour.Location = locationService.GetOne(voucher.Ticket.TourAppointment.Tour.LocationId);
                    voucher.Ticket.TourAppointment.Tour.Guide = guideService.GetOne(voucher.Ticket.TourAppointment.Tour.GuideUsername);
                    voucher.Ticket.TourAppointment.Tour.TourAppointments = tourAppointmentService.GetAllByTour(voucher.Ticket.TourAppointment.TourId);
                    voucher.Ticket.TourAppointment.Tour.StopsList = tourService.LoadStops(voucher.Ticket.TourAppointment.Tour);

                    voucher.Ticket.TicketGrade = ticketGradeService.GetOneByTicket(voucher.Ticket.Id);

                    _guest2.Vouchers.Add(voucher);
                }
            }

        }
        private void SynchronizeTicketsList(Guest2 _guest2)
        {
            TicketService ticketService = new TicketService();
            VoucherService voucherService = new VoucherService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            TourService tourService = new TourService();
            TicketGradeService ticketGradeService = new TicketGradeService();
            LocationService locationService = new LocationService();
            GuideService guideService = new GuideService();

            _guest2.Tickets = new List<Ticket>();
            foreach (var ticket in ticketService.GetByGuest2(_guest2.Username))
            {
                ticket.Guest2 = _guest2;
                ticket.HasVoucher = voucherService.GetOneByTicket(ticket.Id) != null;
                ticket.TourAppointment = tourAppointmentService.GetOne(ticket.TourAppointmentId);
                ticket.TourAppointment.Tickets = new List<Ticket>();
                ticket.TourAppointment.TicketGrades = new List<TicketGrade>();
                ticket.TourAppointment.Tour = tourService.GetOne(ticket.TourAppointment.TourId);
                ticket.TourAppointment.Tour.Location = locationService.GetOne(ticket.TourAppointment.Tour.LocationId);
                ticket.TourAppointment.Tour.Guide = guideService.GetOne(ticket.TourAppointment.Tour.GuideUsername);
                ticket.TourAppointment.Tour.TourAppointments = tourAppointmentService.GetAllByTour(ticket.TourAppointment.TourId);
                ticket.TourAppointment.Tour.StopsList = tourService.LoadStops(ticket.TourAppointment.Tour);
                ticket.TicketGrade = ticketGradeService.GetOneByTicket(ticket.Id);
                _guest2.Tickets.Add(ticket);
            }
        }

        private void SynchronizeTourRequestsList(Guest2 _guest2)
        {
            TourRequestService requestService = new TourRequestService();
            LocationService locationService = new LocationService();

            _guest2.TourRequests = new List<TourRequest>();
            foreach (var request in requestService.GetAll())
            {
                request.Location = locationService.GetOne(request.LocationId);
                _guest2.TourRequests.Add(request);
            }
        }
        private void SynchronizeComplexToursList(Guest2 guest2)
        {
            ComplexTourService complexTourService = new ComplexTourService();
            ComplexTourRequestPartService complexTourRequestPartService = new ComplexTourRequestPartService();
            LocationService locationService = new LocationService();
            guest2.ComplexTours = new List<ComplexTour>();

            foreach (var complexTour in complexTourService.GetAllByGuest(guest2))
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
                guest2.ComplexTours.Add(complexTour);
            }
        }

        private void SynchronizeTours(ObservableCollection<TourDTO> Tours)
        {
            TourService tourService = new TourService();
            GuideService guideService = new GuideService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            LocationService locationService = new LocationService();
            TicketService ticketService = new TicketService();

            foreach (var tour in Tours)
            {
                tour.Location = new LocationDTO(locationService.GetOne(tour.LocationId));
                tour.Guide = new GuideDTO(guideService.GetOne(tour.GuideUsername));
                tour.TourAppointments = new ObservableCollection<TourAppointmentDTO>();
                foreach (var tourAppointment in tourAppointmentService.GetAllByTour(tour.Id))
                {
                    tour.TourAppointments.Add(new TourAppointmentDTO(tourAppointment));
                }
                tour.StopsList = tourService.LoadStops(tour.GetTour());


                foreach (var tourAppointment in tour.TourAppointments)
                {
                    tourAppointment.Tickets = new ObservableCollection<TicketDTO>(ticketService.GetByAppointment(tourAppointment.Id).Select(r => new TicketDTO(r)).ToList());
                    tourAppointment.Tour = new TourDTO(tourService.GetOne(tourAppointment.TourId));
                }
            }
        }
        private void SynchronizeNotifications(Guest2 _guest2)
        {
            Notifications = new ObservableCollection<NotificationDTO>(new NotificationService().GetAllByUser(_guest2.Username).Select(r => new NotificationDTO(r)).Reverse().ToList());
            HasNewNotifications = Notifications.ToList().Any(n => n.New);
            NumberOfNotifications = Notifications.Where(r => r.New == true).Count();
        }
        public void GradeATicket(TicketGradeDTO ticketGrade)
        {
            TicketGradeService ticketGradeService = new TicketGradeService();
            foreach (var ticket in Tickets)
            {
                if (ticket.Id == ticketGrade.Id)
                {
                    ticket.TicketGrade = ticketGrade;
                    ticketGradeService.Add(ticketGrade.GetTicketGrade());
                    return;
                }
            }
        }

        public void CreateTourRequest(TourRequestDTO tourRequest)
        {
            TourRequestService requestService = new TourRequestService();
            LocationService locationService = new LocationService();
            tourRequest.LocationId = locationService.AddAndReturnId(tourRequest.Location.GetLocation());
            requestService.Add(tourRequest.GetTourRequest());
            TourRequests.Add(tourRequest);
        }

        public void CreateComplexTourRequestPart(TourRequestDTO tourRequest)
        {
            ComplexTourRequestPartService requestService = new ComplexTourRequestPartService();
            LocationService locationService = new LocationService();
            tourRequest.LocationId = locationService.AddAndReturnId(tourRequest.Location.GetLocation());
            requestService.Add(tourRequest.GetTourRequest());
        }

        public void CreateComplexTour(ComplexTourDTO complexTour)
        {
            ComplexTourService complexTourService = new ComplexTourService();
            complexTourService.Add(complexTour.GetComplexTour());
            ComplexTours.Add(complexTour);
        }

        public string Username
        {
            get => _guest2.Username;
            set
            {
                if (value != _guest2.Username)
                {
                    _guest2.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => _guest2.Password;
            set
            {
                if (value != _guest2.Password)
                {
                    _guest2.Password = value;
                    OnPropertyChanged();
                }
            }
        }
        public USERTYPE Type
        {
            get => _guest2.Type;
            set
            {
                if (value != _guest2.Type)
                {
                    _guest2.Type = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => _guest2.FirstName;
            set
            {
                if (value != _guest2.FirstName)
                {
                    _guest2.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => _guest2.LastName;
            set
            {
                if (value != _guest2.LastName)
                {
                    _guest2.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Birthday
        {
            get => _guest2.Birthday;
            set
            {
                if (value != _guest2.Birthday)
                {
                    _guest2.Birthday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => _guest2.Email;
            set
            {
                if (value != _guest2.Email)
                {
                    _guest2.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PhoneNumber
        {
            get => _guest2.PhoneNumber;
            set
            {
                if (value != _guest2.PhoneNumber)
                {
                    _guest2.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<TourDTO> Tours { get; set; }
        public ObservableCollection<TicketDTO> Tickets { get; set; }
        public ObservableCollection<VoucherDTO> Vouchers { get; set; }
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }
        public ObservableCollection<ComplexTourDTO> ComplexTours { get; set; }

        private ObservableCollection<NotificationDTO> _Notifications;
        public ObservableCollection<NotificationDTO> Notifications
        {
            get => _Notifications;
            set
            {
                if (value != _Notifications)
                {
                    _Notifications = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _HasNewNotifications;
        public bool HasNewNotifications
        {
            get => _HasNewNotifications;
            set
            {
                if (value != _HasNewNotifications)
                {
                    _HasNewNotifications = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _NumberOfNotifications;
        public int NumberOfNotifications
        {
            get => _NumberOfNotifications;
            set
            {
                if (value != _NumberOfNotifications)
                {
                    _NumberOfNotifications = value;
                    OnPropertyChanged();
                }
            }
        }

        private TourDTO _SelectedTour;
        public TourDTO? SelectedTour
        {
            get => _SelectedTour;
            set
            {
                if (value != _SelectedTour)
                {
                    _SelectedTour = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // validation
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "Username")
                {
                    if (string.IsNullOrEmpty(Username))
                        return "Username is required!";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Username" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
    }
}
