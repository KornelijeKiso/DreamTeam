using ProjectTourism.Utilities;
using System.Collections.ObjectModel;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class VouchersVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public ObservableCollection<VoucherDTO> UsedVouchers { get; set; }
        public ObservableCollection<VoucherDTO> UnusedVouchers { get; set; }
        public ObservableCollection<VoucherDTO> ExpiredVouchers { get; set; }

        public VouchersVM() { }
        public VouchersVM(Guest2DTO guest2)
        {
            Guest2 = guest2;
            UsedVouchers = SetUsedVouchers();
            UnusedVouchers = SetUnusedVouchers();
            ExpiredVouchers = SetExpiredVouchers();
        }

        private ObservableCollection<VoucherDTO> SetUsedVouchers()
        {
            ObservableCollection<VoucherDTO> used = new ObservableCollection<VoucherDTO>();
            foreach (var voucher in Guest2.Vouchers)
            {
                if (voucher.Status == STATUS.USED)
                    used.Add(voucher);
            }
            return used;
        }

        private ObservableCollection<VoucherDTO> SetUnusedVouchers()
        {
            ObservableCollection<VoucherDTO> unused = new ObservableCollection<VoucherDTO>();
            foreach (var voucher in Guest2.Vouchers)
            {
                if (voucher.Status == STATUS.VALID)
                    unused.Add(voucher);
            }
            return unused;
        }

        private ObservableCollection<VoucherDTO> SetExpiredVouchers()
        {
            ObservableCollection<VoucherDTO> expired = new ObservableCollection<VoucherDTO>();
            foreach (var voucher in Guest2.Vouchers)
            {
                if (voucher.Status == STATUS.INVALID)
                    expired.Add(voucher);
            }
            return expired;
        }


    }
}
