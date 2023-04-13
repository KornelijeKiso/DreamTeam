using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class VoucherService
    {
        public List<IObserver> Observers;
        private IVoucherRepository VoucherRepository;

        public VoucherService(IVoucherRepository ivr)
        {
            Observers = new List<IObserver>();
            VoucherRepository = ivr;
        }

        public Voucher GetOne(int id)
        {
            return VoucherRepository.GetOne(id);
        }

        public List<Voucher> GetAll()
        {
            return VoucherRepository.GetAll();
        }
        
        public void Add(Voucher voucher)
        {
            VoucherRepository.Add(voucher);
        }

        public void Delete(Voucher voucher)
        {
            VoucherRepository.Delete(voucher);
        }

        public void Update(Voucher voucher)
        {
            VoucherRepository.Update(voucher);
        }

        public List<Voucher> GetByGuest2(string guest2username)
        {
            return VoucherRepository.GetAllByGuest2(guest2username);
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
