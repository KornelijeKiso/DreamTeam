using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class RequestsWindow : UserControl
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
            Requests = Guide.Requests;
            RequestList = new List<RequestVM>(Requests);
            UpdatedList= new List<RequestVM>();
            SetStartSearchedValues();
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
            Update();
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
            Update();
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
            Update();
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
            Update();
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
            Update();
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
            Update();
        }

        private void InitializeComponents()
        {
            Requests.Clear();
            foreach (var req in Guide.Requests)
                Requests.Add(req);
            RequestList = new List<RequestVM>(Requests);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Dismiss_Click(object sender, RoutedEventArgs e)
        {
            Guide.DismissRequest(SelectedRequest);
            Requests.Remove(SelectedRequest);
        }
        private void Update()
        {
            Requests.Clear();
            foreach (var request in RequestList)
            {
                Requests.Add(request);
            }
        }
    }
}
