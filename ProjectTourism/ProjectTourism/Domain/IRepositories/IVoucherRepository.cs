using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IVoucherRepository
    {
        Voucher GetOne(int voucherId);
        List<Voucher> GetAllByTourAppointment(int tourAppointmentId);
        List<Voucher> GetAll();
        void Add(Voucher voucher);
        void Delete(Voucher voucher);
        void Update(Voucher voucher);
        List<Voucher> GetAllByGuest2(string guest2username);
    }
}
