using System;
using System.Collections.Generic;
using System.Windows;
using ProjectTourism.Model;
using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Utilities;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class CreateTourRequestVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public List<string> LanguageList { get; set; }
        public TourRequestDTO TourRequest { get; set; }
        private LocationDTO _NewLocation;
        public LocationDTO NewLocation
        {
            get => _NewLocation;
            set
            {
                if (value != _NewLocation)
                {
                    _NewLocation = value;
                    OnPropertyChanged();
                }
            }
        }
        // TO DO -> blackout dates in EndDatePicker when StartDate is selected
        //private CalendarDateRange _EndBlackoutDates;
        //public CalendarDateRange EndBlackoutDates
        //{
        //    get => _EndBlackoutDates;
        //    set
        //    {
        //        if (value != _EndBlackoutDates)
        //        {
        //            _EndBlackoutDates = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        private DateTime? _StartDate;
        public DateTime? StartDate
        {
            get => _StartDate;
            set
            {
                if (value != _StartDate)
                {
                    _StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime? _EndDate;
        public DateTime? EndDate
        {
            get => _EndDate;
            set
            {
                if (value != _EndDate)
                {
                    _EndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private object _Content;
        public object Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }


        public CreateTourRequestVM() { }
        public CreateTourRequestVM(Guest2DTO guest2) 
        {
            Guest2 = guest2;
            SetTourRequest();
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };

            // Commands
            ContentCommand = new RelayCommand(ReturnToTourRequests);
            StartDateChangedCommand = new RelayCommand(StartDateChanged);
            EndDateChangedCommand = new RelayCommand(EndDateChanged);
        }

        private void SetTourRequest()
        {
            StartDate = null;
            StartDateValidationVisible = true;
            EndDate = null;
            EndDateValidationVisible = true;
            NewLocation = new LocationDTO(new Location());
            TourRequest = new TourRequestDTO(new TourRequest());
            TourRequest.State = REQUESTSTATE.PENDING;
            TourRequest.Guest2Username = Guest2.Username;
            TourRequest.CreationDateTime = DateTime.Now;
            TourRequest.Location = new LocationDTO(new Location());
        }


        private ICommand _CreateTourRequestCommand;
        public ICommand CreateTourRequestCommand
        {
            get
            {
                return _CreateTourRequestCommand ?? (_CreateTourRequestCommand = new CommandHandler(() => CreateTourRequest(), () => true));
            }
        }
        private void CreateTourRequest()
        {
            if (TourRequest.IsValid && NewLocation.IsValid
                && StartDate != null && EndDate != null)
            {
                if (TourRequest.StartDate > TourRequest.EndDate)
                    MessageBox.Show("Invalid start and end date!");
                else
                {
                    Guest2.CreateTourRequest(TourRequest, NewLocation);
                    MessageBox.Show("Tour Request successfully created!");
                    Content = new TourRequestsVM(Guest2);
                }
            }
            else
                MessageBox.Show("Tour Request can't be made because the data were not entered correctly.");
        }

        // START DATE SELECTION CHANGED
        public ICommand StartDateChangedCommand { get; set; }
        private void StartDateChanged(object obj)
        {
            TourRequest.StartDate = DateOnly.FromDateTime((DateTime)StartDate);
            StartDateValidationVisible = false;
            DateTime startDate = (TourRequest.StartDate.ToDateTime(TimeOnly.MinValue));
            //EndBlackoutDates = new CalendarDateRange(new DateTime(1, 1, 1), startDate);
            // TO DO -> blackout dates in EndDatePicker when StartDate is selected
        }
        // END DATE SELECTION CHANGED
        public ICommand EndDateChangedCommand { get; set; }
        private void EndDateChanged(object obj)
        {
            if (StartDate == null)
            {
                EndDate = null;
                EndDateValidationVisible = true;
                MessageBox.Show("Please select start date first!");
            }
            else
            {
                if (StartDate > EndDate)
                {
                    MessageBox.Show("Invalid Start and End Date! \nEnd Date must be after Start Date!");
                    EndDateValidationVisible = true;
                    EndDate = null;
                }    
                else if (EndDate != null)
                {
                    TourRequest.EndDate = DateOnly.FromDateTime((DateTime)EndDate);
                    EndDateValidationVisible = false;
                }
            }
        }
        public ICommand ContentCommand { get; set; }
        private void ReturnToTourRequests(Object obj)
        {
            Content = new TourRequestsVM(Guest2);
        }

        //
        private bool _StartDateValidationVisible;
        public bool StartDateValidationVisible
        {
            get => _StartDateValidationVisible;
            set
            {
                if (value != _StartDateValidationVisible)
                {
                    _StartDateValidationVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _EndDateValidationVisible;
        public bool EndDateValidationVisible
        {
            get => _EndDateValidationVisible;
            set
            {
                if (value != _EndDateValidationVisible)
                {
                    _EndDateValidationVisible = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
