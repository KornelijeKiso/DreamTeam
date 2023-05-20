using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public NotificationFileHandler FileHandler { get; set; }
        public List<Notification> Notifications { get; set; }
        public NotificationRepository()
        {
            FileHandler = new NotificationFileHandler();
            Notifications = FileHandler.Load();
        }
        public int GenerateId()
        {
            if (Notifications == null || Notifications.Count==0) return 0;
            else return Notifications.Last().Id + 1;
        }
        public void Add(Notification notification)
        {
            notification.Id = GenerateId();
            Notifications.Add(notification);
            FileHandler.Save(Notifications);
        }

        public void Delete(Notification notification)
        {
            Notifications.Remove(GetOne(notification.Id));
            FileHandler.Save(Notifications);
        }
        public void DeleteAll(string username)
        {
            Notifications.RemoveAll(n=>n.Username.Equals(username));
            FileHandler.Save(Notifications);
        }

        public List<Notification> GetAll()
        {
            return Notifications;
        }

        public List<Notification> GetAllByUser(string username)
        {
            return Notifications.Where(n=>n.Username.Equals(username)).ToList();
        }

        public Notification GetOne(int id)
        {
            return Notifications.Find(a => a.Id == id);
        }
        public void Seen(List<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                notification.New = false;
            }
            FileHandler.Save(Notifications);
        }
    }
}
