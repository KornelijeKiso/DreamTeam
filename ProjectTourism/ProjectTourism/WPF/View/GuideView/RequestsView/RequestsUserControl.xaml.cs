using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.View.TourView;
using ProjectTourism.WPF.View.GuideView.RequestsView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class RequestsUserControl : UserControl, INotifyPropertyChanged
    {
        public TourRequestDTO SelectedTourRequest { get; set; }
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }
        public List<TourRequestDTO> TourRequestList { get; set; }
        public List<TourRequestDTO> UpdatedList { get; set; }
        public GuideDTO Guide { get; set; }
        public string SearchedLocation { get; set; }
        public string SearchedNumberOfGuests { get; set; }
        public string SearchedLanguage { get; set; }
        public DateTime SearchedStartDate { get; set; }
        public DateTime SearchedEndDate { get; set; }


        public RequestsUserControl(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideDTO(username);
            SetRequests();
            TourRequestList = new List<TourRequestDTO>(TourRequests);
            UpdatedList= new List<TourRequestDTO>();
            SetStartSearchedValues();
        }
        private void RequestStatisticsLink_Click(object sender, RoutedEventArgs e)
        {
            HideRequestsContent();
            ContentArea.Content = new RequestStatisticsUserControl(Guide.Username);
        }
        private void TourSuggestionLink_Click(object sender, RoutedEventArgs e)
        {
            HideRequestsContent();
            ContentArea.Content = new TourSuggestionUserControl(Guide);
        }
        private void ComplexToursLink_Click(object sender, RoutedEventArgs e)
        {
            HideRequestsContent();
            ContentArea.Content = new ComplexTourRequestsUserControl(Guide.Username);
        }
        public void SetRequests()
        {
            TourRequests = new ObservableCollection<TourRequestDTO>();
            foreach (var request in Guide.TourRequests)
                if (request.State == REQUESTSTATE.PENDING)
                    TourRequests.Add(request);
        }
        private void SetStartSearchedValues()
        {
            SearchedLocation = "";
            SearchedNumberOfGuests = "";
            SearchedLanguage = "";
            SearchedStartDate = DateTime.Today;
            SearchedEndDate = DateTime.Today;
        }
        private void SetUpdatedList()
        {
            UpdatedList.Clear();
            UpdatedList.AddRange(TourRequestList);
        }
        private void FilterByLocation()
        {
            TourRequestList.Clear();
            foreach (var tourRequest in Guide.TourRequests)
            {
                if (tourRequest.Location.City.ToLower().Contains(SearchedLocation.ToLower()) || tourRequest.Location.Country.ToLower().Contains(SearchedLocation.ToLower()))
                    TourRequestList.Add(tourRequest);
            }
            UpdateRequests();
            SetUpdatedList();
        }
        private void FilterByNumberOfGuests()
        {
            if (SearchedLocation.Equals(""))
                SetUpdatedList();

            TourRequestList.Clear();
            foreach (var tourRequest in UpdatedList)
            {
                if (tourRequest.NumberOfGuests >= int.Parse(SearchedNumberOfGuests))
                    TourRequestList.Add(tourRequest);
            }
            UpdateRequests();
            SetUpdatedList();
        }
        private void FilterByLanguage()
        {
            if(SearchedLocation.Equals("") && SearchedNumberOfGuests.Equals(""))
                SetUpdatedList();
            TourRequestList.Clear();
            foreach (var tourRequest in UpdatedList)
            {
                if (tourRequest.Language.ToLower().Contains(SearchedLanguage.ToLower()))
                    TourRequestList.Add(tourRequest);
            }
            UpdateRequests();
            SetUpdatedList();
        }
        private void FilterByStartDate()
        {
            if (SearchedLocation.Equals("") && SearchedNumberOfGuests.Equals("") && SearchedLanguage.Equals(""))
                SetUpdatedList();
            TourRequestList.Clear();
            foreach (var tourRequest in UpdatedList)
            {
                if (SearchedStartDate <= DateTime.Parse(tourRequest.StartDate.ToShortDateString()))
                    TourRequestList.Add(tourRequest);
            }
            UpdateRequests();
            SetUpdatedList();
        }

        private void FilterByEndDate()
        {
            if (SearchedLocation.Equals("") && SearchedNumberOfGuests.Equals("") && SearchedLanguage.Equals("") && SearchedStartDate.Date == DateTime.Today.Date)
                SetUpdatedList();
            TourRequestList.Clear();
            foreach (var tourRequest in UpdatedList)
            {
                if (SearchedEndDate >= DateTime.Parse(tourRequest.EndDate.ToShortDateString()))
                    TourRequestList.Add(tourRequest);
            }
            UpdateRequests();
            SetUpdatedList();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponents();
            if (!SearchedLocation.Equals(""))
                FilterByLocation();
            if (!SearchedNumberOfGuests.Equals(""))
                FilterByNumberOfGuests();
            if (!SearchedLanguage.Equals(""))
                FilterByLanguage();
            if (SearchedStartDate.Date != DateTime.Today.Date)
                FilterByStartDate();
            if (SearchedEndDate.Date != DateTime.Today.Date)
                FilterByEndDate();
            if (SearchedLocation.Equals("") && SearchedNumberOfGuests.Equals("") && SearchedLanguage.Equals("") && SearchedStartDate.Date == DateTime.Today.Date && SearchedEndDate.Date == DateTime.Today.Date)
            {
                TourRequestList.Clear();
                TourRequestList.AddRange(Guide.TourRequests);
            }
            UpdateRequests();
        }
        private void InitializeComponents()
        {
            TourRequests.Clear();
            foreach (var tourRequest in Guide.TourRequests)
                TourRequests.Add(tourRequest);
            TourRequestList = new List<TourRequestDTO>(TourRequests);
        }
        public void HideRequestsContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { RequestsLabel, DataGridRow, rectangle, searchGrid, SearchButton, StatsLink, StatsImage, TourSuggestionImage, TourSuggestionLink, ComplexToursImage, ComplexToursLink };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            HideRequestsContent();
            AcceptedRequestUserControl acceptedRequestUserControl = new AcceptedRequestUserControl(Guide, SelectedTourRequest);
            ContentArea.Content = acceptedRequestUserControl;
            if (acceptedRequestUserControl.TourRequest.State == REQUESTSTATE.ACCEPTED)
                TourRequestList.Remove(SelectedTourRequest);
            UpdateRequests();
        }
        private void UpdateRequests()
        {
            TourRequests.Clear();
            foreach (var tourRequest in TourRequestList)
            {
                TourRequests.Add(tourRequest);
            }
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
