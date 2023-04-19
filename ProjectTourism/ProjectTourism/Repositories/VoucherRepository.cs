using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        public VoucherFileHandler FileHandler { get; set; }
        public List<Voucher> Vouchers { get; set; }
        public VoucherRepository()
        {
            FileHandler = new VoucherFileHandler();
            Vouchers = FileHandler.Load();
            CheckIfValid();
        }

        private void CheckIfValid()
        {
            foreach (var voucher in Vouchers)
            {
                if (voucher.Status == STATUS.VALID && (DateTime.Compare(voucher.ValidDue, DateTime.Now) < 0))
                {
                    voucher.Status = STATUS.INVALID;
                }
            }
            FileHandler.Save(Vouchers);
        }
        private int GenerateId()
        {
            int id = 0;
            if (Vouchers == null)
            {
                id = 0;
            }
            else
            {
                foreach (var voucher in Vouchers)
                {
                    id = voucher.Id + 1;
                }
            }
            return id;
        }

        public void Add(Voucher addedVoucher)
        {
            addedVoucher.Id = GenerateId();
            Vouchers.Add(addedVoucher);
            FileHandler.Save(Vouchers);
        }

        public void Delete(Voucher voucher)
        {
            Vouchers.Remove(voucher);
            FileHandler.Save(Vouchers);
        }

        public void Update(Voucher voucher)
        {
            foreach (var existingVoucher in Vouchers)
            {
                if (existingVoucher.Id == voucher.Id)
                {
                    existingVoucher.Status = voucher.Status;
                    existingVoucher.TicketId = voucher.TicketId;
                    existingVoucher.UsedOnDate = voucher.UsedOnDate;
                }
            }
            FileHandler.Save(Vouchers);
        }

        public Voucher GetOne(int Id)
        {
            foreach (var voucher in Vouchers)
            {
                if (voucher.Id == Id) return voucher;
            }
            return null;
        }

        public Voucher GetOneByTicket(int ticketId)
        {
            foreach (var voucher in Vouchers)
            {
                if (voucher.TicketId == ticketId) return voucher;
            }
            return null;
        }
        public List<Voucher> GetAll()
        {
            return Vouchers;
        }

        public List<Voucher> GetAllByGuest2(string guest2username)
        {
            List<Voucher> guest2Vouchers = new List<Voucher>();

            foreach (var voucher in Vouchers)
            {
                if (voucher.Guest2Username.Equals(guest2username))
                {
                    guest2Vouchers.Add(voucher);
                }
            }
            return guest2Vouchers;
        }
    }
}
