using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Services;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class NotificationsVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public NotificationDTO SelectedNotification { get; set; }
        public NotificationService NotificationService { get; set; }
        public int NotificationType { get; set; } 
        public TourService TourService { get; set; }
        public TourDTO NewTour { get; set; }
        public TicketDTO Ticket { get; set; }

        public NotificationsVM() { }
        
        public NotificationsVM(Guest2DTO guest2) 
        {
            Guest2 = guest2;

            NotificationService = new NotificationService();
            TourService = new TourService();
        }
        public void SeenNotification()
        {
            List<Notification> notifications = NotificationService.GetAllByUser(Guest2.Username);
            NotificationService.Seen(notifications);
            Guest2.HasNewNotifications = false;
            Guest2.NumberOfNotifications = 0;
        }
        public void DismissNotification()
        {
            NotificationService.Dismiss(SelectedNotification.GetNotification());
            Guest2.Notifications.Remove(SelectedNotification);
        }
        
        private void GetNotificationType()
        {
            if (SelectedNotification.Title.StartsWith("New Tour")) NotificationType = 0;
            if (SelectedNotification.Title.StartsWith("Ticket")) NotificationType = 1;
        }
        public void DetailsDisplayClick()
        {
            GetNotificationType();
            switch (NotificationType)
            {
                // New Tour case
                case 0:
                    {   
                        NewTour = new TourDTO(TourService.GetOne(GetNewTourId(SelectedNotification.Title)));
                        CreateTicketWindow createTicketWidnow = new CreateTicketWindow(Guest2, NewTour);
                        createTicketWidnow.ShowDialog();
                        break;
                    }
                // Tour Attendance case
                case 1:
                    {
                        Ticket = Guest2.Tickets.First(t => t.Id == GetTicketId(SelectedNotification.Title));
                        Guest2AttendanceWindow guest2AttendanceWindow = new Guest2AttendanceWindow(Ticket, Guest2);
                        guest2AttendanceWindow.ShowDialog();
                        break;
                    }
                // error case
                default:
                    {
                        
                        break;
                    }
            }
        }
        private int GetNewTourId(string text)
        {
            int from = text.IndexOf(':') + 1;
            int to = text.Length - 1;
            string idStr = text.Substring(from, to - from);
            return int.Parse(idStr);
        }
        private int GetTicketId(string text)
        {
            int from = text.IndexOf(':') + 1;
            int to = text.Length - 1;
            string idStr = text.Substring(from, to - from);
            return int.Parse(idStr);
        }
    }
}
