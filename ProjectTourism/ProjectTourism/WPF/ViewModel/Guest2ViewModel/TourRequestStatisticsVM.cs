using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Utilities;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class TourRequestStatisticsVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public List<TourRequestVM> AccepptedTourRequests { get; set; }
        public List<TourRequestVM> ExpiredTourRequests { get; set; }
        // TO DO -> get YEAR
        //          Language Stats
        //          Location Stats
        //          NumberOfGuest Stats


        public TourRequestStatisticsVM(Guest2VM guest2)
        {
            Guest2 = guest2;

            AccepptedTourRequests = SetAcceptedTourRequests();
            ExpiredTourRequests = SetExpiredTourRequests();
        }
        private List<TourRequestVM> SetAcceptedTourRequests()
        {
            List<TourRequestVM> accepted = new List<TourRequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username))
                    && (request.State == REQUESTSTATE.ACCEPTED))
                    accepted.Add(request);
            }
            return accepted;
        }
        private List<TourRequestVM> SetExpiredTourRequests()
        {
            List<TourRequestVM> expired = new List<TourRequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username))
                    && (request.State == REQUESTSTATE.EXPIRED))
                    expired.Add(request);
            }
            return expired;
        }


    }
}
