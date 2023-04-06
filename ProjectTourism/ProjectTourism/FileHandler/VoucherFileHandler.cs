using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class VoucherFileHandler
    {
        private Serializer<Voucher> Serializer;
        private readonly string FileName = "../../../References/vouchers.csv";
        private List<Voucher> Vouchers;
        public VoucherFileHandler()
        {
            Serializer = new Serializer<Voucher>();
            Vouchers = Serializer.fromCSV(FileName);
        }

        public List<Voucher> Load()
        {
            Vouchers = Serializer.fromCSV(FileName);
            return Vouchers;
        }

        public void Save(List<Voucher> vouchers)
        {
            Serializer.toCSV(FileName, vouchers);
        }
    }
}
