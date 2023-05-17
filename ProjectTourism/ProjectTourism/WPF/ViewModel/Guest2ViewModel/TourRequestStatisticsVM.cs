using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using LiveCharts;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class TourRequestStatisticsVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public List<TourRequestVM> AllTourRequests { get; set; }
        public List<int> Years { get; set; }
        public int SelectedYear { get; set; }
        public List<string> Languages { get; set; }
        public SeriesCollection LanguageSeries { get; set; }
        public Func<double, string> LanguageFormatter { get; set; }
        public SeriesCollection LocationSeries { get; set; }
        public Func<double, string> LocationFormatter { get; set; }
        public List<string> Locations { get; set; }

        public double LanguageStat { get; set; }


        private int _Accepted;
        public int Accepted
        {
            get => _Accepted;
            set
            {
                if (value != _Accepted)
                {
                    _Accepted = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _Expired;
        public int Expired
        {
            get => _Expired;
            set
            {
                if (value != _Expired)
                {
                    _Expired = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _Pending;
        public int Pending
        {
            get => _Pending;
            set
            {
                if (value != _Pending)
                {
                    _Pending = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _NumberOfGuestsStat;
        public double NumberOfGuestsStat
        {
            get => _NumberOfGuestsStat;
            set
            {
                if (value != _NumberOfGuestsStat)
                {
                    _NumberOfGuestsStat = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public TourRequestStatisticsVM() { }
        public TourRequestStatisticsVM(Guest2VM guest2)
        {
            Guest2 = guest2;
            SetAttributes();
        }

        private void SetAttributes()
        {
            AllTourRequests = GetAllRequests(Guest2.TourRequests);
            
            Years = GetYears(AllTourRequests);
            Languages = GetLanguages(AllTourRequests);
            Locations = GetLocations(AllTourRequests);
            
            NumberOfGuestsStat = CalculateAverageNumberOfGuests(AllTourRequests);
            CalculateYearlyStats(AllTourRequests);

            LanguageFormatter = value => value.ToString("N");
            LocationFormatter = value => value.ToString("N");
            DisplayLanguageStat();
            DisplayLocationStat();
        }

        private List<TourRequestVM> GetAllRequests(ObservableCollection<TourRequestVM> Guest2Requests)
        {
            List<TourRequestVM> allRequests = new List<TourRequestVM>();
            foreach (var request in Guest2Requests)
            {
                if (request.Guest2Username.Equals(Guest2.Username))
                    allRequests.Add(request);
            }
            return allRequests;
        }
        
        private List<int> GetYears(List<TourRequestVM> allRequests)
        {
            List<int> years = new List<int>();
            foreach (var request in allRequests)
            {
                if (!years.Contains(request.CreationDateTime.Year))
                    years.Add(request.CreationDateTime.Year);
            }
            return years;
        }
        private List<string> GetLanguages(List<TourRequestVM> requests)
        {
            List<string> languages = new List<string>();
            foreach (var request in requests)
            {
                if (!languages.Contains(request.Language))
                {
                    languages.Add(request.Language);
                }
            }
            return languages;
        }

        private List<string> GetLocations(List<TourRequestVM> requests)
        {
            List<string> locations = new List<string>();
            foreach (var request in requests)
            {
                string location = request.Location.Country + "\n " + request.Location.City;
                if (!locations.Contains(location))
                {
                    locations.Add(location);
                }
            }
            return locations;
        }


        // YEARLY STATISTICS
        private double CalculateAverageNumberOfGuests(List<TourRequestVM> tourRequests)
        {
            if (tourRequests.Count == 0) return 0;
            double stat = 0;
            int sum = 0;
            foreach (var request in tourRequests)
            {
                sum += request.NumberOfGuests;
            }
            stat =  sum /(double) tourRequests.Count;
            return stat;
        }
        private void CalculateYearlyStats(List<TourRequestVM> allRequests)
        {
            Pending = allRequests.Where(request => request.State == REQUESTSTATE.PENDING).Count();
            Accepted = allRequests.Where(request => request.State == REQUESTSTATE.ACCEPTED).Count();
            Expired = allRequests.Where(request => request.State == REQUESTSTATE.EXPIRED).Count();
        }
        public void CalculateYearlyStatsFiltered(List<TourRequestVM> allRequests, int year)
        {
            List<TourRequestVM> filterYear = allRequests.Where(request => request.CreationDateTime.Year == year).ToList();
            CalculateYearlyStats(filterYear);
            NumberOfGuestsStat = CalculateAverageNumberOfGuests(filterYear);
        }

        
        // LANGUAGE STATISTICS
        private void DisplayLanguageStat()
        {
            LanguageSeries = new SeriesCollection();
            foreach (var year in Years)
            {
                LanguageSeries.Add(new ColumnSeries
                {
                    Title = year.ToString(),
                    Values = GenerateLanguageChartValue(AllTourRequests, year)
                }) ;

            }
        }
        private ChartValues<double> GenerateLanguageChartValue(List<TourRequestVM> requests, int year)
        {
            ChartValues<double> chartValue = new ChartValues<double>();
            double value;
            foreach (var language in Languages)
            {
                value = requests.Where(request => request.CreationDateTime.Year == year 
                                               && request.Language.Equals(language)).Count();
                chartValue.Add(value);
            }        
            return chartValue;
        }


        // LOCATION STATISTICS
        private void DisplayLocationStat()
        {
            LocationSeries = new SeriesCollection();
            foreach (var year in Years)
            {
                LocationSeries.Add(new ColumnSeries
                {
                    Title = year.ToString(),
                    Values = GenerateLocationChartValue(AllTourRequests, year)
                });
            }
        }
        private ChartValues<double> GenerateLocationChartValue(List<TourRequestVM> requests, int year)
        {
            ChartValues<double> chartValue = new ChartValues<double>();
            double value;
            foreach (var location in Locations)
            {
                string[] locationString = location.Split('\n');
                string country = locationString[0].Trim();
                string city = locationString[1].Trim();

                value = requests.Where(request => request.CreationDateTime.Year == year 
                                               && request.Location.Country.Equals(country)
                                               && request.Location.City.Equals(city)).Count();
                chartValue.Add(value);
            }
            return chartValue;
        }
    }
}
