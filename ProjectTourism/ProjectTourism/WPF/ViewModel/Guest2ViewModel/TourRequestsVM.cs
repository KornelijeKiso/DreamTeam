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
        public CurrentUserService CurrentUserService { get; set; }
        public UserVM CurrentUser { get; set; }
        public Guest2VM Guest2 { get; set; }
        public RequestVM SelectedTourRequest { get; set; }
        public ObservableCollection<RequestVM> WaitingRequests { get; set; }
        public ObservableCollection<RequestVM> AcceptedRequests { get; set; }
        public ObservableCollection<RequestVM> ExpiredRequests { get; set; }
        public ObservableCollection<RequestVM> OthersRequests { get; set; }

        public TourRequestsVM()
        {
            SetGuest2();
            WaitingRequests = SetWaitingRequests();
            AcceptedRequests = SetAcceptedRequests();
            ExpiredRequests = SetExpiredRequests();
            OthersRequests = SetOthersRequests();
        }

        private void SetGuest2()
        {
            CurrentUserService = new CurrentUserService();
            CurrentUser = new UserVM(CurrentUserService.GetUser());
            Guest2 = new Guest2VM(CurrentUser.Username);
        }

        private ObservableCollection<RequestVM> SetWaitingRequests()
        {
            ObservableCollection<RequestVM> waiting = new ObservableCollection<RequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.PENDING))
                    waiting.Add(request);
            }
            return waiting;
        }

        private ObservableCollection<RequestVM> SetAcceptedRequests()
        {
            ObservableCollection<RequestVM> accepted = new ObservableCollection<RequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.ACCEPTED))
                    accepted.Add(request);
            }
            return accepted;
        }

        private ObservableCollection<RequestVM> SetExpiredRequests()
        {
            ObservableCollection<RequestVM> expired = new ObservableCollection<RequestVM>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.DISMISSED))
                    expired.Add(request);
            }
            return expired;
        }

        private ObservableCollection<RequestVM> SetOthersRequests()
        {
            ObservableCollection<RequestVM> others = new ObservableCollection<RequestVM>();
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
                return _TourRequestCommand ?? (_TourRequestCommand = new CommandHandler(() => TourRequest_Click(), () => CanRequest));
            }
        }
        public bool CanRequest
        {
            get
            {
                return (Guest2 != null);
            }
        }
        public void TourRequest_Click()
        {
            CreateTourRequestWindow tourRequestWindow = new CreateTourRequestWindow(Guest2);
            tourRequestWindow.ShowDialog();
        }
    }
}
