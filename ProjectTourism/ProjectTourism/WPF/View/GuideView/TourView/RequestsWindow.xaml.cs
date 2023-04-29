using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.Model;
using ProjectTourism.View.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class RequestsWindow : UserControl, INotifyPropertyChanged
    {
        public RequestVM SelectedRequest { get; set; }
        public ObservableCollection<RequestVM> Requests { get; set; }
        public List<RequestVM> RequestList { get; set; }
        public List<RequestVM> UpdatedList { get; set; }
        public GuideVM Guide { get; set; }
        public string SearchedLocation { get; set; }
        public string SearchedNumberOfGuests { get; set; }
        public string SearchedLanguage { get; set; }
        public DateTime SearchedStartDate { get; set; }
        public DateTime SearchedEndDate { get; set; }


        public RequestsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
            SetRequests();
            RequestList = new List<RequestVM>(Requests);
            UpdatedList= new List<RequestVM>();
            SetStartSearchedValues();
        }
        private void RequestStatisticslink_Click(object sender, RoutedEventArgs e)
        {
            HideRequestsContent();
            ContentArea.Content = new RequestStatisticsUserControl();
            e.Handled = true;
        }
        public void SetRequests()
        {
            Requests = new ObservableCollection<RequestVM>();
            foreach (var request in Guide.Requests)
                if (request.State == REQUESTSTATE.PENDING)
                    Requests.Add(request);
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
            UpdatedList.AddRange(RequestList);
        }
        private void FilterByLocation()
        {
            RequestList.Clear();
            foreach (var request in Guide.Requests)
            {
                if (request.Location.City.ToLower().Contains(SearchedLocation.ToLower()) || request.Location.Country.ToLower().Contains(SearchedLocation.ToLower()))
                    RequestList.Add(request);
            }
            UpdateRequests();
            SetUpdatedList();
        }
        private void FilterByNumberOfGuests()
        {
            if (SearchedLocation.Equals(""))
                SetUpdatedList();

            RequestList.Clear();
            foreach (var request in UpdatedList)
            {
                if (request.NumberOfGuests >= int.Parse(SearchedNumberOfGuests))
                    RequestList.Add(request);
            }
            UpdateRequests();
            SetUpdatedList();
        }
        private void FilterByLanguage()
        {
            if(SearchedLocation.Equals("") && SearchedNumberOfGuests.Equals(""))
                SetUpdatedList();
            RequestList.Clear();
            foreach (var request in UpdatedList)
            {
                if (request.Language.ToLower().Contains(SearchedLanguage.ToLower()))
                    RequestList.Add(request);
            }
            UpdateRequests();
            SetUpdatedList();
        }
        private void FilterByStartDate()
        {
            if (SearchedLocation.Equals("") && SearchedNumberOfGuests.Equals("") && SearchedLanguage.Equals(""))
                SetUpdatedList();
            RequestList.Clear();
            foreach (var request in UpdatedList)
            {
                if (SearchedStartDate <= DateTime.Parse(request.StartDate.ToShortDateString()))
                    RequestList.Add(request);
            }
            UpdateRequests();
            SetUpdatedList();
        }

        private void FilterByEndDate()
        {
            if (SearchedLocation.Equals("") && SearchedNumberOfGuests.Equals("") && SearchedLanguage.Equals("") && SearchedStartDate.Date == DateTime.Today.Date)
                SetUpdatedList();
            RequestList.Clear();
            foreach (var request in UpdatedList)
            {
                if (SearchedEndDate >= DateTime.Parse(request.EndDate.ToShortDateString()))
                    RequestList.Add(request);
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
                RequestList.Clear();
                RequestList.AddRange(Guide.Requests);
            }
            UpdateRequests();
        }
        private void InitializeComponents()
        {
            Requests.Clear();
            foreach (var req in Guide.Requests)
                Requests.Add(req);
            RequestList = new List<RequestVM>(Requests);
        }
        public void HideRequestsContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { RequestsLabel, DataGridRow, rectangle, searchGrid, SearchButton, StatsLink, StatsImage };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            HideRequestsContent();
            AcceptedRequestUserControl acceptedRequestUserControl = new AcceptedRequestUserControl(Guide, SelectedRequest);
            ContentArea.Content = acceptedRequestUserControl;
            if (acceptedRequestUserControl.Request.State == REQUESTSTATE.ACCEPTED)
                RequestList.Remove(SelectedRequest);
            UpdateRequests();
        }
        private void Dismiss_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to dismiss this request?", "Dismiss request", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Guide.DismissRequest(SelectedRequest);
                Requests.Remove(SelectedRequest);
            }
        }
        private void UpdateRequests()
        {
            Requests.Clear();
            foreach (var request in RequestList)
            {
                Requests.Add(request);
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
