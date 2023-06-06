using ProjectTourism.Utilities;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class TourRequestsVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public TourRequestDTO SelectedTourRequest { get; set; }
        public ObservableCollection<TourRequestDTO> PendingRequests { get; set; }
        public ObservableCollection<TourRequestDTO> AcceptedRequests { get; set; }
        public ObservableCollection<TourRequestDTO> ExpiredRequests { get; set; }
        public ObservableCollection<TourRequestDTO> OthersRequests { get; set; }
        
        private object _TourRequestsContent;
        public object TourRequestsContent
        {
            get { return _TourRequestsContent; }
            set { _TourRequestsContent = value; OnPropertyChanged(); }
        }
        public TourRequestsVM() { }
        public TourRequestsVM(Guest2DTO guest2)
        {
            Guest2 = guest2;
            PendingRequests = SetPendingRequests();
            AcceptedRequests = SetAcceptedRequests();
            ExpiredRequests = SetExpiredRequests();
            OthersRequests = SetOthersRequests();

            //DisplayStatisticsCommand
            TourRequestStatisticsCommand = new RelayCommand(TourRequestStatisticsClick);
            TourRequestCommand = new RelayCommand(CreateTourRequest);
        }

        private ObservableCollection<TourRequestDTO> SetPendingRequests()
        {
            ObservableCollection<TourRequestDTO> pending = new ObservableCollection<TourRequestDTO>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.PENDING))
                    pending.Add(request);
            }
            return pending;
        }

        private ObservableCollection<TourRequestDTO> SetAcceptedRequests()
        {
            ObservableCollection<TourRequestDTO> accepted = new ObservableCollection<TourRequestDTO>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.ACCEPTED))
                    accepted.Add(request);
            }
            return accepted;
        }

        private ObservableCollection<TourRequestDTO> SetExpiredRequests()
        {
            ObservableCollection<TourRequestDTO> expired = new ObservableCollection<TourRequestDTO>();
            foreach (var request in Guest2.TourRequests)
            {
                if ((request.Guest2Username.Equals(Guest2.Username)) && (request.State == REQUESTSTATE.EXPIRED))
                    expired.Add(request);
            }
            return expired;
        }

        private ObservableCollection<TourRequestDTO> SetOthersRequests()
        {
            ObservableCollection<TourRequestDTO> others = new ObservableCollection<TourRequestDTO>();
            foreach (var request in Guest2.TourRequests)
            {
                if (!request.Guest2Username.Equals(Guest2.Username))
                    others.Add(request);
            }
            return others;
        }

        public ICommand TourRequestCommand { get; set; }
        public void CreateTourRequest(object obj)
        {
            TourRequestsContent = new CreateTourRequestVM(Guest2);
            
            //PendingRequests = SetPendingRequests(); // updating pending list
            //// TO DO -> new PENDING request that we just created isn't visible when the tourRequestWindow is closed,
            ////          only when we enter the RequestTour MenuItem again
            ////          FIX THIS
        }

        public ICommand TourRequestStatisticsCommand { get; set; }
        public void TourRequestStatisticsClick(object obj)
        {
            TourRequestsContent = new TourRequestStatisticsVM(Guest2);
        }
    }
}
