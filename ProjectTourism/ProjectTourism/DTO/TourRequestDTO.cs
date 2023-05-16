using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.DTO
{
    public class TourRequestDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        private TourRequest _tourRequest;
        public TourRequestDTO(TourRequest tourRequest)
        {
            _tourRequest = tourRequest;
        }
        public TourRequestDTO(TourRequestDTO tourRequest)
        {
            _tourRequest = new TourRequest(tourRequest.GetTourRequest());
        }
        public TourRequestDTO() { }

        public TourRequest GetTourRequest()
        {
            return _tourRequest;
        }
        public int StatForYear(int year)
        {
            int tourRequestCounter = 0;
            foreach (var tourRequest in GetAll())
            {
                if (tourRequest.CreationDateTime.Year == year)
                    tourRequestCounter++;
            }
            return tourRequestCounter;
        }

        public int StatForYearLanguageFiltered(int year, string language)
        {
            int tourRequestCounter = 0;
            foreach (var tourRequest in GetAll())
            {
                if (tourRequest.CreationDateTime.Year == year && tourRequest.Language.Equals(language))
                    tourRequestCounter++;
            }
            return tourRequestCounter;
        }
        public int StatForYearLocationFiltered(int year, string location)
        {
            int tourRequestCounter = 0;
            foreach (var tourRequest in GetAll())
            {
                if (tourRequest.CreationDateTime.Year == year && (tourRequest.Location.City.ToString() + ", " + tourRequest.Location.Country.ToString()).Equals(location))
                    tourRequestCounter++;
            }
            return tourRequestCounter;
        }
        public int StatForYearLanguageAndLocationFiltered(int year, string language, string location)
        {
            int tourRequestCounter = 0;
            foreach (var tourRequest in GetAll())
            {
                if (tourRequest.CreationDateTime.Year == year && tourRequest.Language.Equals(language) && (tourRequest.Location.City.ToString() + ", " + tourRequest.Location.Country.ToString()).Equals(location))
                    tourRequestCounter++;
            }
            return tourRequestCounter;
        }
        public List<int> MonthlyStats(int year)
        {
            List<int> months = Enumerable.Repeat(0, 12).ToList();
            foreach (var tourRequest in GetAllByYear(year))
            {
                months[tourRequest.CreationDateTime.Month - 1]++;
            }
            return months;
        }

        public List<int> MonthlyStatsForLanguage(int year, string language)
        {
            List<int> months = Enumerable.Repeat(0, 12).ToList();
            foreach (var tourRequest in GetAllByYear(year))
            {
                if (tourRequest.Language.Equals(language))
                    months[tourRequest.CreationDateTime.Month - 1]++;
            }
            return months;
        }

        public List<int> MonthlyStatsForLocation(int year, string location)
        {
            List<int> months = Enumerable.Repeat(0, 12).ToList();
            foreach (var tourRequest in GetAllByYear(year))
            {
                if ((tourRequest.Location.City.ToString() + ", " + tourRequest.Location.Country.ToString()).Equals(location))
                    months[tourRequest.CreationDateTime.Month - 1]++;
            }
            return months;
        }

        public List<int> MonthlyStatsForLanguageAndLocation(int year, string language, string location)
        {
            List<int> months = Enumerable.Repeat(0, 12).ToList();
            foreach (var tourRequest in GetAllByYear(year))
            {
                if (tourRequest.Language.Equals(language) && (tourRequest.Location.City.ToString() + ", " + tourRequest.Location.Country.ToString()).Equals(location))
                    months[tourRequest.CreationDateTime.Month - 1]++;
            }
            return months;
        }
        public ObservableCollection<string> GetAllLanguages()
        {
            ObservableCollection<string> languages = new ObservableCollection<string>();
            foreach (var tourRequest in GetAll())
            {
                if (!languages.Contains(tourRequest.Language))
                    languages.Add(tourRequest.Language);
            }
            return languages;
        }
        public ObservableCollection<string> GetAllLocations()
        {
            ObservableCollection<string> locations = new ObservableCollection<string>();
            foreach (var tourRequest in GetAll())
            {
                if (!locations.Contains(tourRequest.Location.City + ", " + tourRequest.Location.Country))
                    locations.Add(tourRequest.Location.City + ", " + tourRequest.Location.Country);
            }
            return locations;
        }
        public List<string> TourRequestLanguagesInLastYear()
        {
            TourRequestService tourRequestService = new TourRequestService();
            List<string> languages = new List<string>();

            foreach (var tourRequest in tourRequestService.GetAll())
            {
                if (tourRequest.CreationDateTime >= DateTime.UtcNow.AddYears(-1))
                    languages.Add(tourRequest.Language);
            }
            return languages;
        }
        public List<string> TourRequestLocationsInLastYear()
        {
            TourRequestService tourRequestService = new TourRequestService();
            List<string> locations = new List<string>();

            foreach (var tourRequest in tourRequestService.GetAll())
            {
                if (tourRequest.CreationDateTime >= DateTime.UtcNow.AddYears(-1))
                    locations.Add(tourRequest.Location.City + ", " + tourRequest.Location.Country);
            }
            return locations;
        }
        public ObservableCollection<int> Years()
        {
            ObservableCollection<TourRequestDTO> TourRequests = new ObservableCollection<TourRequestDTO>();
            TourRequests = GetAll();
            List<int> years = new List<int>();
            TourRequests.ToList().ForEach(tourRequest => { if (!years.Contains(tourRequest.CreationDateTime.Year)) years.Add(tourRequest.CreationDateTime.Year); });
            years.Sort();
            years.Reverse();

            ObservableCollection<int> YearsList = new ObservableCollection<int>(years);
            return YearsList;
        }
        public ObservableCollection<TourRequestDTO> GetAll()
        {
            TourRequestService tourRequestService = new TourRequestService();
            ObservableCollection<TourRequestDTO> tourRequests = new ObservableCollection<TourRequestDTO>();
            foreach (var tourRequest in tourRequestService.GetAll())
            {
                tourRequests.Add(new TourRequestDTO(tourRequest));
            }
            return tourRequests;
        }
        public ObservableCollection<TourRequestDTO> GetAllByYear(int year)
        {
            TourRequestService tourRequestService = new TourRequestService();
            ObservableCollection<TourRequestDTO> tourRequests = new ObservableCollection<TourRequestDTO>();
            foreach (var tourRequest in tourRequestService.GetAll())
            {
                if (tourRequest.CreationDateTime.Year == year)
                    tourRequests.Add(new TourRequestDTO(tourRequest));
            }
            return tourRequests;
        }
        public int Id
        {
            get => _tourRequest.Id;
            set
            {
                if (value != _tourRequest.Id)
                {
                    _tourRequest.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public LocationDTO Location
        {
            get => new LocationDTO(_tourRequest.Location);
            set
            {
                if (value.GetLocation() != _tourRequest.Location)
                {
                    _tourRequest.Location = value.GetLocation();
                    OnPropertyChanged();
                }
            }
        }
        public int LocationId
        {
            get => _tourRequest.LocationId;
            set
            {
                if (value != _tourRequest.LocationId)
                {
                    _tourRequest.LocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => _tourRequest.Description;
            set
            {
                if (value != _tourRequest.Description)
                {
                    _tourRequest.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Language
        {
            get => _tourRequest.Language;
            set
            {
                if (value != _tourRequest.Language)
                {
                    _tourRequest.Language = value;
                    OnPropertyChanged();
                }
            }
        }
        public int NumberOfGuests
        {
            get => _tourRequest.NumberOfGuests;
            set
            {
                if (value != _tourRequest.NumberOfGuests)
                {
                    _tourRequest.NumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly StartDate
        {
            get => _tourRequest.StartDate;
            set
            {
                if (value != _tourRequest.StartDate)
                {
                    _tourRequest.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly EndDate
        {
            get => _tourRequest.EndDate;
            set
            {
                if (value != _tourRequest.EndDate)
                {
                    _tourRequest.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Guest2Username
        {
            get => _tourRequest.Guest2Username;
            set
            {
                if (value != _tourRequest.Guest2Username)
                {
                    _tourRequest.Guest2Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public REQUESTSTATE State
        {
            get => _tourRequest.State;
            set
            {
                if (value != _tourRequest.State)
                {
                    _tourRequest.State = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime CreationDateTime
        {
            get => _tourRequest.CreationDateTime;
            set
            {
                if (value != _tourRequest.CreationDateTime)
                {
                    _tourRequest.CreationDateTime = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Description is required!";
                }
                else if (columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                        return "Language is required!";
                }
                else if (columnName == "NumberOfGuests")
                {
                    if (string.IsNullOrEmpty(NumberOfGuests.ToString()))
                        return "Number Of Guests is required!";
                }
                else if (columnName == "StartDate")
                {
                    if (string.IsNullOrEmpty(StartDate.ToString()))
                        return "Start Date is required!";
                }
                else if (columnName == "EndDate")
                {
                    if (string.IsNullOrEmpty(EndDate.ToString()))
                        return "End Date is required!";
                }
                else if (columnName == "Location.Country")
                {
                    if (string.IsNullOrEmpty(Location.Country))
                        return "Country is required!";
                }
                else if (columnName == "Location.City")
                {
                    if (string.IsNullOrEmpty(Location.City))
                        return "City is required!";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Description", "Language", "NumberOfGuests", "StartDate", "EndDate", "Location.Country", "Location.City" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
