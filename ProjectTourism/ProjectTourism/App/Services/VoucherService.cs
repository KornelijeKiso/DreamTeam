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

        public VoucherVM GetOne(int id)
        {
            return new VoucherVM(VoucherRepository.GetOne(id));
        }

        public List<VoucherVM> GetAll()
        {
            List<VoucherVM> vouchers = new List<VoucherVM>();
            foreach (var voucher in VoucherRepository.GetAll())
            {
                vouchers.Add(new VoucherVM(voucher));
            }
            return vouchers;
        }
        
        public void Add(VoucherVM voucher)
        {
            VoucherRepository.Add(voucher.GetVoucher());
        }

        public void Delete(VoucherVM voucher)
        {
            VoucherRepository.Delete(voucher.GetVoucher());
        }

        public void Update(VoucherVM voucher)
        {
            VoucherRepository.Update(voucher.GetVoucher());
        }

        public List<VoucherVM> GetByGuest2(string guest2username)
        {
            List <VoucherVM> guest2vouchers = new List<VoucherVM> ();
            foreach (var voucher in VoucherRepository.GetByGuest2(guest2username))
            {
                guest2vouchers.Add(new VoucherVM(voucher));
            }
            return guest2vouchers;
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
