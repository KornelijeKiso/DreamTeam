using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class CreateComplexTourPartVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public ComplexTourDTO ComplexTour { get; set; }
        public List<string> LanguageList { get; set; }
        private TourRequestDTO _NewTourRequestPart;
        public TourRequestDTO NewTourRequestPart
        {
            get => _NewTourRequestPart;
            set
            {
                if (value != _NewTourRequestPart)
                {
                    _NewTourRequestPart = value;
                    OnPropertyChanged();
                }
            }
        }
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
        public CreateComplexTourPartVM() { }
        public CreateComplexTourPartVM(Guest2DTO guest2, ComplexTourDTO complexTour)
        {
            Guest2 = guest2;
            ComplexTour = complexTour;
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };
            SetAttributes();

            // Commands
            StartDateChangedCommand = new RelayCommand(StartDateChanged);
            EndDateChangedCommand = new RelayCommand(EndDateChanged);
        }

        private void SetAttributes()
        {
            NewTourRequestPart = new TourRequestDTO(new TourRequest());
            NewTourRequestPart.Guest2Username = Guest2.Username;
            NewTourRequestPart.CreationDateTime = DateTime.Now;
            NewLocation = new LocationDTO(new Location());
            StartDateValidationVisible = true;
            EndDateValidationVisible = true;
        }

        // Commands
        private ICommand _AddTourRequestCommand;
        public ICommand AddTourRequestCommand
        {
            get
            {
                return _AddTourRequestCommand ?? (_AddTourRequestCommand = new CommandHandler(() => AddTourRequestPartClick(), () => true));
            }
        }
        public void AddTourRequestPartClick()
        {
            if (NewTourRequestPart.IsValid && NewLocation.IsValid)
            {
                if (NewTourRequestPart.StartDate > NewTourRequestPart.EndDate)
                    MessageBox.Show("Invalid start and end date!");
                else
                {
                    ComplexTour.CreateComplexTourRequestPart(ComplexTour, NewTourRequestPart, NewLocation);
                    Reset();
                }
            }
            else
                MessageBox.Show("Tour Request can't be made because the data were not entered correctly.");
        }
        private void Reset()
        {
            StartDate = new DateTime(NewTourRequestPart.EndDate.Year, NewTourRequestPart.EndDate.Month, NewTourRequestPart.EndDate.Day);
            EndDate = null;
            SetAttributes();
            NewTourRequestPart.Reset();
            NewLocation.Reset();
        }

        // START DATE SELECTION CHANGED
        public ICommand StartDateChangedCommand { get; set; }

        private void StartDateChanged(object obj)
        {
            NewTourRequestPart.StartDate = DateOnly.FromDateTime((DateTime)StartDate);
            StartDateValidationVisible = false;
            DateTime startDate = (NewTourRequestPart.StartDate.ToDateTime(TimeOnly.MinValue));
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
                    EndDate = null;
                    EndDateValidationVisible = true;
                }
                else if (EndDate != null)
                {
                    NewTourRequestPart.EndDate = DateOnly.FromDateTime((DateTime)EndDate);
                    EndDateValidationVisible = false;
                }
                
            }
        }

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
