using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.Repositories;
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
        public VoucherVM(VoucherVM voucher, TicketVM ticket)
        {
            _voucher = voucher.GetVoucher();
            _voucher.TicketId = ticket.Id;
            _voucher.Ticket = ticket.GetTicket();
            _voucher.Status = STATUS.USED;
            _voucher.UsedOnDate = DateTime.Now;
            VoucherService voucherService = new VoucherService();
            voucherService.Update(_voucher);
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

        public TicketVM Ticket
        {
            get => new TicketVM(_voucher.Ticket);
            set
            {
                if (value.GetTicket() != _voucher.Ticket)
                {
                    _voucher.Ticket = value.GetTicket();
                    OnPropertyChanged();
                }
            }
        }

        public int TicketId
        {
            get => _voucher.TicketId;
            set
            {
                if (value != _voucher.TicketId)
                {
                    _voucher.TicketId = value;
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

        public Guest2VM Guest2
        {
            get => new Guest2VM(_voucher.Guest2);
            set
            {
                if (value.GetGuest2() != _voucher.Guest2)
                {
                    _voucher.Guest2 = value.GetGuest2();
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

        public DateTime UsedOnDate
        {
            get => _voucher.UsedOnDate;
            set
            {
                if (value != _voucher.UsedOnDate)
                {
                    _voucher.UsedOnDate = value;
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
