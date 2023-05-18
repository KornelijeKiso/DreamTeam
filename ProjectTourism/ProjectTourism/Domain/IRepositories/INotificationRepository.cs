using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface INotificationRepository
    {
        Notification GetOne(int id);
        List<Notification> GetAll();
        void Add(Notification notification);
        void Delete(Notification notification);
        void DeleteAll(string username);
        List<Notification> GetAllByOwner(string ownerUsername);
        void Seen();
        void Seen(int id);
    }
}
