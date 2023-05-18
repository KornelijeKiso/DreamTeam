using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Services;
using ProjectTourism.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class NewTourNotificationVM
    {
        public Guest2VM Guest2 { get; set; }
        public NotificationDTO Notification { get; set; }
        public TourDTO NewTour { get; set; }
        public List<Notification> NotificationList { get; set; }
        public NotificationService NotificationService { get; set; }
        
        public NewTourNotificationVM() { }
        // TO DO -> 
        //public NewTourNotificationVM(Guest2VM guest2, NotificationDTO Notification){} 
        public NewTourNotificationVM(Guest2VM guest2) 
        {
            Guest2 = guest2;
            NotificationService = new NotificationService();
            
            NotificationList = NotificationService.GetAllByOwner(Guest2.Username);
            Notification = new NotificationDTO(NotificationList[0]);
        }

        public void SeenNotification()
        {
            NotificationDTO notificationDTO = Notification;
            NotificationService.Seen(Notification.Id);
        }
        public void DismissNotification()
        {
            NotificationService.Dismiss(Notification.GetNotification());
            //Guest2.Notifications.Remove(Notification);
        }
    }
}
