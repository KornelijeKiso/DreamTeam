using ProjectTourism.Model;
using ProjectTourism.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Xceed.Wpf.Toolkit;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.Services;
using ProjectTourism.Utilities;
using System.ComponentModel;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class YourAccommodationsMenuItemVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _popupText;
        public string popupText
        {
            get => _popupText;
            set
            {
                if (value != _popupText)
                {
                    _popupText = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _popupVisible;
        public bool popupVisible
        {
            get => _popupVisible;
            set
            {
                if (value != _popupVisible)
                {
                    _popupVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _popupOpacity;
        public double popupOpacity
        {
            get => _popupOpacity;
            set
            {
                if (value != _popupOpacity)
                {
                    _popupOpacity = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _SelectedType;
        public string SelectedType
        {
            get => _SelectedType;
            set
            {
                if (value != _SelectedType)
                {
                    _SelectedType = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _SelectedDeadline;
        public string SelectedDeadline
        {
            get => _SelectedDeadline;
            set
            {
                if (value != _SelectedDeadline)
                {
                    _SelectedDeadline = value;
                    OnPropertyChanged();
                }
            }
        }
        public OwnerDTO Owner { get; set; }
        public AccommodationDTO SelectedAccommodation { get; set; }
        public AccommodationDTO NewAccommodation { get; set; }
        public LocationDTO NewLocation { get; set; }
        public List<string> Deadlines { get; set; }
        public List<string> Types { get; set; }
        public Dictionary<string, int> Days { get; set; }

        public YourAccommodationsMenuItemVM(string username)
        {
            InitializeDays();
            InitializeTypes();
            InitializeNewEntities();
            SetOwner(username);
            popupOpacity = 1.0;
            popupVisible = false;
        }
        public YourAccommodationsMenuItemVM() { }
        private void SetOwner(string username)
        {
            Owner = new OwnerDTO(username);
            NewAccommodation.Owner = Owner;
            NewAccommodation.OwnerUsername = username;
        }

        private void InitializeNewEntities()
        {
            NewAccommodation = new AccommodationDTO(new Accommodation());
            NewLocation = new LocationDTO(new ProjectTourism.Model.Location());
        }

        private void InitializeTypes()
        {
            Types = new List<string>() { "Apartment", "House", "Hut" };
            SelectedType= Types[0];
        }
        private void InitializeDays()
        {
            Deadlines = new List<String>() { "1 day", "3 days", "7 days", "14 days", "1 month", "3 months", "6 months" };
            Days = new Dictionary<string, int>();
            foreach (var item in Deadlines)
            {
                Days.Add(item, InDays(item));
            }
            SelectedDeadline = Deadlines[0];
        }

        private int InDays(string deadline)
        {
            int days = int.Parse(deadline.Split(' ')[0]);
            string multiply_unit = deadline.Split(" ")[1];
            int multiplyer = 1;
            if (multiply_unit.Equals("month") || multiply_unit.Equals("months"))
            {
                multiplyer = 30;
            }
            return days * multiplyer;
        }

        private void HandleTypeCombobox()
        {
            if (SelectedType.ToString().Equals("Apartment"))
                NewAccommodation.Type = ACCOMMODATIONTYPE.APARTMENT;
            else if (SelectedType.ToString().Equals("House"))
                NewAccommodation.Type = ACCOMMODATIONTYPE.HOUSE;
            else
                NewAccommodation.Type = ACCOMMODATIONTYPE.HUT;
        }
        public void RegisterAccommodationClick(object parameter)
        {
            if (NewAccommodation.IsValid && NewLocation.IsValid)
                RegisterNewAccommodation();
            else
                System.Windows.MessageBox.Show("Not all fields are filled correctly.");
        }

        public void StatisticsClick(object parameter)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(SelectedAccommodation);
            statisticsWindow.ShowDialog();
        }
        public void RenovateClick(object parameter)
        {
            RenovationsWindow renovationsWindow = new RenovationsWindow(SelectedAccommodation);
            renovationsWindow.ShowDialog();
        }
        private void RegisterNewAccommodation()
        {
            HandleTypeCombobox();
            NewAccommodation.CancellationDeadline = Days[(string)SelectedDeadline];
            AddAccommodation(NewAccommodation, NewLocation);
            ShowPopupMessage("You have successfully registered new accommodation.\n You can see it in Your accommodations down below.");
            Reset();
        }
        public void AddAccommodation(AccommodationDTO newAccommodation, LocationDTO newLocation)
        {
            AccommodationService accommodationService = new AccommodationService();
            LocationService locationService = new LocationService();
            Model.Location location = new Model.Location(newLocation.City, newLocation.Country);
            int id = locationService.AddAndReturnId(location);
            Accommodation accommodationToAdd = new Accommodation(newAccommodation.GetAccommodation());
            accommodationToAdd.LocationId = id;
            accommodationService.Add(accommodationToAdd);
            Owner.Synchronize(Owner.Username);
        }
        private async void ShowPopupMessage(string message)
        {
            popupText = message;
            popupVisible = true;
            await Task.Delay(4000);
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(9);
                popupOpacity += -0.05;
            }
            popupVisible = false;
            popupOpacity = 1.0;
        }
        private void Reset()
        {
            NewAccommodation.Reset();
            NewLocation.Reset();
            SelectedDeadline = Deadlines[0];
            SelectedType = Types[0];
        }
        public ICommand RegisterAccommodationClickCommand
        {
            get => new RelayCommand(RegisterAccommodationClick);
        }
        public ICommand StatisticsClickCommand
        {
            get => new RelayCommand(StatisticsClick);
        }
        public ICommand RenovateClickCommand
        {
            get => new RelayCommand(RenovateClick);
        }
    }
}
