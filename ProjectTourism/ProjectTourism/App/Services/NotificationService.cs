using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
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
        public void DismissAll()
        {
            NotificationRepo.DeleteAll();
        }
        public Notification GetOne(int id)
        {
            return NotificationRepo.GetOne(id);
        }
        public List<Notification> GetAll()
        {
            return NotificationRepo.GetAll();
        }
        public List<Notification> GetAllByOwner(string username)
        {
            return NotificationRepo.GetAllByOwner(username);
        }
        public void Seen()
        {
            NotificationRepo.Seen();
        }
    }
}
