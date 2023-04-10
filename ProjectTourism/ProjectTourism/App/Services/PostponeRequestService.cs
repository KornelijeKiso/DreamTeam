using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class PostponeRequestService
    {
        public List<IObserver> Observers;
        private IPostponeRequestRepository PostponeRequestRepo;
        public PostponeRequestService(IPostponeRequestRepository iprr)
        {
            PostponeRequestRepo = iprr;
            Observers = new List<IObserver>();
        }
        public void Add(PostponeRequestVM postponeRequest)
        {
            PostponeRequestRepo.Add(postponeRequest.GetPostponeRequest());
        }
        public void Delete(PostponeRequestVM postponeRequest)
        {
            PostponeRequestRepo.Delete(postponeRequest.GetPostponeRequest());
        }
        public PostponeRequestVM GetOne(int id)
        {
            return new PostponeRequestVM(PostponeRequestRepo.GetOne(id));
        }
        public List<PostponeRequestVM> GetAll()
        {
            List<PostponeRequestVM> postponeRequests = new List<PostponeRequestVM>();
            foreach (var postponeRequest in PostponeRequestRepo.GetAll())
            {
                postponeRequests.Add(new PostponeRequestVM(postponeRequest));
            }
            return postponeRequests;
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
