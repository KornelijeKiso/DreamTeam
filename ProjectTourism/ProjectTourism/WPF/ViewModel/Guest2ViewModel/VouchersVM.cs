using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Utilities;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using System.Collections.ObjectModel;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class VouchersVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public ObservableCollection<VoucherVM> UsedVouchers { get; set; }
        public ObservableCollection<VoucherVM> UnusedVouchers { get; set; }
        public ObservableCollection<VoucherVM> ExpiredVouchers { get; set; }

        public VouchersVM() { }
        public VouchersVM(Guest2VM guest2)
        {
            Guest2 = guest2;
            UsedVouchers = SetUsedVouchers();
            UnusedVouchers = SetUnusedVouchers();
            ExpiredVouchers = SetExpiredVouchers();
        }

        private ObservableCollection<VoucherVM> SetUsedVouchers()
        {
            ObservableCollection<VoucherVM> used = new ObservableCollection<VoucherVM>();
            foreach (var voucher in Guest2.Vouchers)
            {
                if (voucher.Status == STATUS.USED)
                    used.Add(voucher);
            }
            return used;
        }

        private ObservableCollection<VoucherVM> SetUnusedVouchers()
        {
            ObservableCollection<VoucherVM> unused = new ObservableCollection<VoucherVM>();
            foreach (var voucher in Guest2.Vouchers)
            {
                if (voucher.Status == STATUS.VALID)
                    unused.Add(voucher);
            }
            return unused;
        }

        private ObservableCollection<VoucherVM> SetExpiredVouchers()
        {
            ObservableCollection<VoucherVM> expired = new ObservableCollection<VoucherVM>();
            foreach (var voucher in Guest2.Vouchers)
            {
                if (voucher.Status == STATUS.INVALID)
                    expired.Add(voucher);
            }
            return expired;
        }


    }
}
