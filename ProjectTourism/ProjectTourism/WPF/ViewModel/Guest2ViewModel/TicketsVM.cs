using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjectTourism.Utilities;
using ProjectTourism.Repositories;
using ProjectTourism.Services;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class TicketsVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public Guest2Service Guest2Service { get; set; }
        public TicketVM SelectedTicket { get; set; }
        public ObservableCollection<TicketVM> Tickets { get; set; }


        public void SetGuest2(Guest2VM guest2)
        {
            Guest2 = guest2;
            Tickets = new ObservableCollection<TicketVM>(Guest2Service.GetTickets(Guest2.GetGuest2()).Select(r => new TicketVM(r)).ToList());
        }

        public TicketsVM()
        {
            // Guest2 null here :(
            Guest2Service = new Guest2Service(new Guest2Repository());
            if (Guest2 != null)
                Tickets = new ObservableCollection<TicketVM>(Guest2Service.GetTickets(Guest2.GetGuest2()).Select(r => new TicketVM(r)).ToList());
        }




    }
}
