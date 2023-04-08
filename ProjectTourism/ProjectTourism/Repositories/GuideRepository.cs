using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Repositories
{
    public class GuideRepository : IGuideRepository
    {
        public GuideFileHandler FileHandler { get; set; }
        public List<Guide> Guides { get; set; }
        public GuideRepository()
        {
            FileHandler = new GuideFileHandler();
            Guides = FileHandler.Load();
            Synchronize();
        }
        public void Synchronize()
        {

        }
        public void Add(Guide guide)
        {
            Guides.Add(guide);
            FileHandler.Save(Guides);
        }

        public void Delete(Guide guide)
        {
            Guides.Remove(guide);
            FileHandler.Save(Guides);
        }

        public List<Guide> GetAll()
        {
            return Guides;
        }

        public Guide GetOne(string username)
        {
            foreach(var guide in Guides)
            {
                if (guide.Username.Equals(username))
                    return guide;
            }
            return null;
        }

        public void UpdateHasTourStarted(string username, bool hasTourStarted)
        {
            Guide Guide = GetOne(username);
            Guide.HasTourStarted = hasTourStarted;
            FileHandler.Save(Guides);
        //    NotifyObservers();
        }

        public void Update(Guide guide)
        {
            throw new NotImplementedException();
        }
        private static bool AppointmentAdditionIsValid(string username, TourAppointment tourAppointment)
        {
            return tourAppointment.Tour.GuideUsername.Equals(username) && tourAppointment.TourDateTime.Date.Equals(DateTime.Now.Date);
        }
        public List<TourAppointment> GetGuidesCurrentAppointments(string username)
        {
            List<TourAppointment> appointments = new List<TourAppointment>();
            TourAppointmentDAO tourAppDAO = new TourAppointmentDAO();
            foreach (var tourAppointment in tourAppDAO.GetAll())
            {
                if (AppointmentAdditionIsValid(username, tourAppointment))
                {
                    appointments.Add(tourAppointment);
                }
            }
            return appointments;
        }
    }
}
