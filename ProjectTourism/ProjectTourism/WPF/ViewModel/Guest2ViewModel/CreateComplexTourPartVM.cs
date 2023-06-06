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
        public List<string> LanguageList { get; set; }
        public ComplexTourDTO ComplexTour { get; set; }
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
        public CreateComplexTourPartVM(ComplexTourDTO complexTour)
        {
            ComplexTour = complexTour;
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };
            SetNewTourRequestPart();

            StartDateChangedCommand = new RelayCommand(StartDateChanged);
            EndDateChangedCommand = new RelayCommand(EndDateChanged);
        }
        public CreateComplexTourPartVM(TourRequestDTO complexTourPart)
        {
            NewTourRequestPart = complexTourPart;
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };
            //SetNewTourRequestPart();

            StartDateChangedCommand = new RelayCommand(StartDateChanged);
            EndDateChangedCommand = new RelayCommand(EndDateChanged);
        }

        private void SetNewTourRequestPart()
        {
            StartDate = null;
            EndDate = null;

            NewTourRequestPart = new TourRequestDTO(new TourRequest());
            //NewTourRequestPart.Guest2Username = Guest2.Username;
            NewTourRequestPart.CreationDateTime = DateTime.Now;
            NewTourRequestPart.State = REQUESTSTATE.PENDING;

            // TO DO -> clear text boxes
            NewTourRequestPart.Location = new LocationDTO(new Location("", ""));
            NewTourRequestPart.Language = "";
            NewTourRequestPart.NumberOfGuests = 0;
            NewTourRequestPart.Description = "";
            NewTourRequestPart.StartDate = new DateOnly();
            NewTourRequestPart.EndDate = new DateOnly();
            // TO DO -> fix location validation
            //NewTourRequestPart.Location.Country = "neka drzava";
            //NewTourRequestPart.Location.City = "neki grad";
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
            if (NewTourRequestPart.IsValid)
            {
                if (NewTourRequestPart.StartDate > NewTourRequestPart.EndDate)
                    MessageBox.Show("Invalid start and end date!");
                else
                {
                    //Guest2.CreateComplexTourRequestPart(NewTourRequestPart);
                    ComplexTour.AddTourPart(NewTourRequestPart.GetTourRequest());
                    //TourRequests.Add(NewTourRequestPart);

                    if (ComplexTour.TourRequestString == "")
                        ComplexTour.TourRequestString = NewTourRequestPart.Id.ToString();
                    else
                        ComplexTour.TourRequestString += "," + NewTourRequestPart.Id.ToString();

                    //SetNewTourRequestPart();

                    //ClearTextBoxs();
                    // TO DO -> clear text boxes
                }
            }
            else
                MessageBox.Show("Tour Request can't be made because the data were not entered correctly.");
        }

        // START DATE SELECTION CHANGED
        public ICommand StartDateChangedCommand { get; set; }
        private void StartDateChanged(object obj)
        {
            NewTourRequestPart.StartDate = DateOnly.FromDateTime((DateTime)StartDate);
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
                MessageBox.Show("Please select start date first!");
            }
            else
            {
                if (StartDate > EndDate)
                {
                    MessageBox.Show("Invalid Start and End Date! \nEnd Date must be after Start Date!");
                    EndDate = null;
                }
                else if (EndDate != null)
                    NewTourRequestPart.EndDate = DateOnly.FromDateTime((DateTime)EndDate);
            }
        }
    }
}
