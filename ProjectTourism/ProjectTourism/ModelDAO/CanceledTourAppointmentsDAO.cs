using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.ModelDAO
{
    public class CanceledTourAppointmentsDAO
    {
        public List<IObserver> Observers;
        public CanceledTourAppointmentsFileHandler FileHandler { get; set; }
        public List<TourAppointment> CanceledAppointments { get; set; }
        public CanceledTourAppointmentsDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new CanceledTourAppointmentsFileHandler();
            CanceledAppointments = FileHandler.Load();
        }

        public void Add(TourAppointment appointment)
        {
            CanceledAppointments.Add(appointment);
            FileHandler.Save(CanceledAppointments);
        }
        public List<TourAppointment> GetAll()
        {
            return CanceledAppointments;
        }
        public void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update();
            }
        }
    }
}
