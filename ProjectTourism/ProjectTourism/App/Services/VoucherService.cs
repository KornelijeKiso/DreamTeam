using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Repositories.IRepositories;

namespace ProjectTourism.Services
{
    public class VoucherService
    {
        private IVoucherRepository VoucherRepository;
        public VoucherService()
        {
            VoucherRepository = Injector.Injector.CreateInstance<IVoucherRepository>();
        }
        public Voucher GetOne(int id)
        {
            return VoucherRepository.GetOne(id);
        }
        public Voucher GetOneByTicket(int ticketId)
        {
            return VoucherRepository.GetOneByTicket(ticketId);
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
        public List<Voucher> GetAllByGuest2(string guest2username)
        {
            return VoucherRepository.GetAllByGuest2(guest2username);
        }
    }
}
