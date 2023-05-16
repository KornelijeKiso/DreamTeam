using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Utilities;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class TourRequestsVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public TourRequestVM SelectedTourRequest { get; set; }
        public ObservableCollection<TourRequestVM> PendingRequests { get; set; }
        public ObservableCollection<TourRequestVM> AcceptedRequests { get; set; }
        public ObservableCollection<TourRequestVM> ExpiredRequests { get; set; }
        public ObservableCollection<TourRequestVM> OthersRequests { get; set; }

        public TourRequestsVM() { }
        public TourRequestsVM(Guest2VM guest2)
        {
            Guest2 = guest2;
            PendingRequests = SetPendingRequests();
            AcceptedRequests = SetAcceptedRequests();
            ExpiredRequests = SetExpiredRequests();
            OthersRequests = SetOthersRequests();
        }

        private ObservableCollection<TourRequestVM> SetPendingRequests()
        {
            ObservableCollection<TourRequestVM> pending = new ObservableCollection<TourRequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.PENDING))
                    pending.Add(request);
            }
            return pending;
        }

        private ObservableCollection<TourRequestVM> SetAcceptedRequests()
        {
            ObservableCollection<TourRequestVM> accepted = new ObservableCollection<TourRequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.ACCEPTED))
                    accepted.Add(request);
            }
            return accepted;
        }

        private ObservableCollection<TourRequestVM> SetExpiredRequests()
        {
            ObservableCollection<TourRequestVM> expired = new ObservableCollection<TourRequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.EXPIRED))
                    expired.Add(request);
            }
            return expired;
        }

        private ObservableCollection<TourRequestVM> SetOthersRequests()
        {
            ObservableCollection<TourRequestVM> others = new ObservableCollection<TourRequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if (!request.Guest2Username.Equals(Guest2.Username))
                    others.Add(request);
            }
            return others;
        }

        private ICommand _TourRequestCommand;
        public ICommand TourRequestCommand
        {
            get
            {
                return _TourRequestCommand ?? (_TourRequestCommand = new CommandHandler(() => TourRequest_Click(), () => true));
            }
        }
        public void TourRequest_Click()
        {
            CreateTourRequestWindow tourRequestWindow = new CreateTourRequestWindow(Guest2);
            tourRequestWindow.ShowDialog();
            PendingRequests = SetPendingRequests(); // updating pending list
            // TO DO -> new PENDING request that we just created isn't visible when the tourRequestWindow is closed,
            //          only when we enter the RequestTour MenuItem again
            //          FIX THIS
        }

        private ICommand _TourRequestStatisticsCommand;
        public ICommand TourRequestStatisticsCommand
        {
            get
            {
                return _TourRequestStatisticsCommand ?? (_TourRequestStatisticsCommand = new CommandHandler(() => TourRequestStatisticsClick(), () => true));
            }
        }
        public void TourRequestStatisticsClick()
        {
            TourRequestStatisticsWindow tourRequestStatisticsWindow = new TourRequestStatisticsWindow(Guest2);
            tourRequestStatisticsWindow.ShowDialog();
        }
    }
}
