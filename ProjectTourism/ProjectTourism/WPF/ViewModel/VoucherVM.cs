using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class VoucherVM : INotifyPropertyChanged
    {
        private Voucher _voucher;

        public VoucherVM(Voucher voucher)
        {
            _voucher = voucher;
        }

        public Voucher GetVoucher()
        {
            return _voucher;
        }



        public int Id 
        { 
            get => _voucher.Id;
            set
            {
                if (value != _voucher.Id)
                {
                    _voucher.Id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Guest2Username
        {
            get => _voucher.Guest2Username;
            set
            {
                if (value != _voucher.Guest2Username)
                {
                    _voucher.Guest2Username = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guest2 Guest2
        {
            get => _voucher.Guest2;
            set
            {
                if (value != _voucher.Guest2)
                {
                    _voucher.Guest2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ValidFrom
        {
            get => _voucher.ValidFrom;
            set
            {
                if (value != _voucher.ValidFrom)
                {
                    _voucher.ValidFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ValidDue
        {
            get => _voucher.ValidDue;
            set
            {
                if (value != _voucher.ValidDue)
                {
                    _voucher.ValidDue = value;
                    OnPropertyChanged();
                }
            }
        }

        public STATUS Status
        {
            get => _voucher.Status;
            set
            {
                if (value != _voucher.Status)
                {
                    _voucher.Status = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _voucher.Description;
            set
            {
                if (value != _voucher.Description)
                {
                    _voucher.Description = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
