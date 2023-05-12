using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class NotificationFileHandler
    {
        private Serializer<Notification> Serializer;
        private readonly string Filename = "../../../References/notifications.csv";
        private List<Notification> Notifications;

        public NotificationFileHandler()
        {
            Serializer = new Serializer<Notification>();
        }

        public List<Notification> Load()
        {
            Notifications = Serializer.fromCSV(Filename);
            return Notifications;
        }

        public void Save(List<Notification> accommodations)
        {
            Serializer.toCSV(Filename, accommodations);
        }
    }
}
