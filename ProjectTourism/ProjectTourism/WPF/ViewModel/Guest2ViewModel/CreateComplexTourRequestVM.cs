using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.Utilities;


namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class CreateComplexTourRequestVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public List<string> LanguageList { get; set; }
        public ComplexTourDTO ComplexTour { get; set; }
        private TourRequestDTO _NewTourRequestPart;
        public TourRequestDTO NewTourRequestPart { get; set; }
        //{
        //    get => _NewTourRequestPart;
        //    set
        //    {
        //        if (value != _NewTourRequestPart)
        //        {
        //            _NewTourRequestPart = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        //private ObservableCollection<TourRequestDTO> _TourRequests;
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }
        //{
        //    get => _TourRequests;
        //    set
        //    {
        //        if (value != _TourRequests)
        //        {
        //            _TourRequests = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        public TourRequestDTO SelectedTourRequest { get; set; }

        private object _Content;
        public object Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }

        public CreateComplexTourRequestVM() { }
        public CreateComplexTourRequestVM(Guest2DTO guest2) 
        {
            Guest2 = guest2; 
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };
            
            SetNewTourRequestPart();
            ComplexTour = new ComplexTourDTO(new ComplexTour(Guest2.Username, ""));

            TourRequests = new ObservableCollection<TourRequestDTO>();
            //SetUpDatePicker();

            //
            ComplexTourRequestsCommand = new RelayCommand(ReturnToComplexTourRequests);
        }

        private void SetNewTourRequestPart()
        {
            NewTourRequestPart = new TourRequestDTO(new TourRequest());
            NewTourRequestPart.Guest2Username = Guest2.Username;
            NewTourRequestPart.CreationDateTime = DateTime.Now;
            NewTourRequestPart.Location = new LocationDTO(new Location("", ""));

            // TO DO -> fix location validation
            //NewTourRequestPart.Location.Country = "neka drzava";
            //NewTourRequestPart.Location.City = "neki grad";
        }

        //private void SetUpDatePicker()
        //{
        //    StartDatePicker.DisplayDate = DateTime.Now;
        //    NewTourRequestPart.StartDate = DateOnly.FromDateTime(new DateTime(1, 1, 1));
        //    StartDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));

        //    EndDatePicker.DisplayDate = DateTime.Now;
        //    NewTourRequestPart.EndDate = DateOnly.FromDateTime(new DateTime(1, 1, 1));
        //    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));
        //}

        ////////////////// COMMANDS ////////////////// 
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
            if (NewTourRequestPart.IsValid &&
               (NewTourRequestPart.StartDate != DateOnly.FromDateTime(new DateTime(1, 1, 1))) &&
               (NewTourRequestPart.EndDate != DateOnly.FromDateTime(new DateTime(1, 1, 1))))
            {
                if (NewTourRequestPart.StartDate > NewTourRequestPart.EndDate)
                    MessageBox.Show("Invalid start and end date!");
                else
                {
                    TourRequests.Add(NewTourRequestPart);
                    Guest2.CreateComplexTourRequestPart(NewTourRequestPart);

                    if (ComplexTour.TourRequestString == "")
                        ComplexTour.TourRequestString = NewTourRequestPart.Id.ToString();
                    else
                        ComplexTour.TourRequestString += "," + NewTourRequestPart.Id.ToString();

                    SetNewTourRequestPart();

                    //ClearTextBoxs();
                    // TO DO -> clear text boxes
                    // TO DO -> set up datePickers 
                }
            }
            else
                MessageBox.Show("Tour Request can't be made because the data were not entered correctly.");
        }
        //private void ClearTextBoxs()
        //{
        //    // TO DO
        //    //CountryText.Clear();
        //    //CityText.Clear();
        //    //LanguageComboBox.SelectedIndex//Clear();
        //    LanguageText.Text = String.Empty;
        //    NumberOfGuestsText.Text = String.Empty;
        //    DescriptionText.Text = String.Empty;

        //    StartDatePicker.SelectedDate = null;
        //    StartDatePicker.Text = String.Empty;
        //    StartDatePicker.DisplayDate = DateTime.Now;
        //    NewTourRequestPart.StartDate = DateOnly.FromDateTime(new DateTime(1, 1, 1));
        //    StartDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));

        //    EndDatePicker.SelectedDate = null;
        //    EndDatePicker.Text = String.Empty;
        //    EndDatePicker.DisplayDate = DateTime.Now;
        //    NewTourRequestPart.EndDate = DateOnly.FromDateTime(new DateTime(1, 1, 1));
        //    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));
        //}

        private ICommand _MakeComplexTourCommand;
        public ICommand MakeComplexTourCommand
        {
            get
            {
                return _MakeComplexTourCommand ?? (_MakeComplexTourCommand = new CommandHandler(() => MakeComplexTour_Click(), () => true));
            }
        }
        public void MakeComplexTour_Click()
        {
            // TO DO -> save created Complex Tour
            if (ComplexTour.IsValid && TourRequests.Count != 0)
            {
                Guest2.CreateComplexTour(ComplexTour);
                //MessageBox.Show("Complex Tour Request created! ");
                //Content = new ComplexToursVM(Guest2);
            }
            else
            {
                MessageBox.Show("Complex Tour Request can't be made because the data were not entered correctly.");
            }
        }

        public ICommand ComplexTourRequestsCommand { get; set; }
        private void ReturnToComplexTourRequests(object obj)
        {
            Content = new ComplexToursVM(Guest2);
        }
    }
}
