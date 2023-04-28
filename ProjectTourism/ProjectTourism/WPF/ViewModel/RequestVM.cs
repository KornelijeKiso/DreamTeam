using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Services;

namespace ProjectTourism.WPF.ViewModel
{
    public enum REQUESTSTATE { PENDING, ACCEPTED, DISMISSED }
    public class RequestVM : INotifyPropertyChanged
    {
        private Request _request;
        public RequestVM(Request request)
        {
            _request = request;
        }
        public RequestVM(RequestVM request)
        {
            _request = new Request(request.GetRequest());
        }
        public RequestVM() { }
        
        public Request GetRequest()
        {
            return _request;
        }
        public ObservableCollection<RequestVM> GetAll()
        {
            RequestService requestService = new RequestService();
            ObservableCollection<RequestVM> requests = new ObservableCollection<RequestVM>();
            foreach(var req in requestService.GetAll())
            {
                requests.Add(new RequestVM(req));
            }
            return requests;
        }
        public ObservableCollection<RequestVM> GetAllByYear(int year)
        {
            RequestService requestService = new RequestService();
            ObservableCollection<RequestVM> requests = new ObservableCollection<RequestVM>();
            foreach (var req in requestService.GetAll())
            {
                if(req.CreationDateTime.Year == year)
                    requests.Add(new RequestVM(req));
            }
            return requests;
        }
        public ObservableCollection<int> Years()
        {
            ObservableCollection<RequestVM> Requests = new ObservableCollection<RequestVM>();
            Requests = GetAll();
            List<int> years = new List<int>();
            Requests.ToList().ForEach(request => { if (!years.Contains(request.CreationDateTime.Year)) years.Add(request.CreationDateTime.Year); });
            years.Sort();
            years.Reverse();

            ObservableCollection<int> YearsList = new ObservableCollection<int>(years);
            return YearsList;
        }
        public int StatForYear(int year)
        {
            int ReqCounter = 0;
            foreach(var request in GetAll())
            {
                if (request.CreationDateTime.Year == year)
                    ReqCounter++;
            }
            return ReqCounter;
        }
        public List<int> MonthlyStats(int year)
        {
            List<int> months = Enumerable.Repeat(0, 12).ToList();
            foreach (var request in GetAllByYear(year) )
            {
                months[request.CreationDateTime.Month-1]++;
            }
            return months;
        }
        public int Id
        {
            get => _request.Id;
            set
            {
                if (value != _request.Id)
                {
                    _request.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public LocationVM Location
        {
            get => new LocationVM(_request.Location);
            set
            {
                if (value.GetLocation() != _request.Location)
                {
                    _request.Location = value.GetLocation();
                    OnPropertyChanged();
                }
            }
        }
        public int LocationId
        {
            get => _request.LocationId;
            set
            {
                if (value != _request.LocationId)
                {
                    _request.LocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => _request.Description;
            set
            {
                if (value != _request.Description)
                {
                    _request.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Language
        {
            get => _request.Language;
            set
            {
                if (value != _request.Language)
                {
                    _request.Language = value;
                    OnPropertyChanged();
                }
            }
        }
        public int NumberOfGuests
        {
            get => _request.NumberOfGuests;
            set
            {
                if (value != _request.NumberOfGuests)
                {
                    _request.NumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly StartDate
        {
            get => _request.StartDate;
            set
            {
                if (value != _request.StartDate)
                {
                    _request.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly EndDate
        {
            get => _request.EndDate;
            set
            {
                if (value != _request.EndDate)
                {
                    _request.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Guest2Username
        {
            get => _request.Guest2Username;
            set
            {
                if(value != _request.Guest2Username)
                {
                    _request.Guest2Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public REQUESTSTATE State
        {
            get => _request.State;
            set
            {
                if (value != _request.State)
                {
                    _request.State = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime CreationDateTime
        {
            get => _request.CreationDateTime;
            set
            {
                if (value != _request.CreationDateTime)
                {
                    _request.CreationDateTime = value;
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
