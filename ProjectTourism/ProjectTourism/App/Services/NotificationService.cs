using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class NotificationService
    {
        private INotificationRepository NotificationRepo;
        public NotificationService()
        {
            NotificationRepo = Injector.Injector.CreateInstance<INotificationRepository>();
        }
        public void Add(Notification notification)
        {
            NotificationRepo.Add(notification);
        }
        public void Dismiss(Notification notification)
        {
            NotificationRepo.Delete(notification);
        }
        public void DismissAll(string username)
        {
            NotificationRepo.DeleteAll(username);
        }
        public Notification GetOne(int id)
        {
            return NotificationRepo.GetOne(id);
        }
        public List<Notification> GetAll()
        {
            return NotificationRepo.GetAll();
        }
        public List<Notification> GetAllByUser(string username)
        {
            return NotificationRepo.GetAllByUser(username);
        }
        public void Seen(List<Notification> notifications)
        {
            NotificationRepo.Seen(notifications);
        }
        public void NotifyAllOwnersAboutNewForum(Location location)
        {
            foreach(var owner in new OwnerService().GetAllOnLocation(location.Id))
            {
                Add(new Notification("New forum",
                "New forum has just been opened on " + location.City + ", " + location.Country+". You can see it in your forums.",
                owner.Username));
            }
        }
    }
}
