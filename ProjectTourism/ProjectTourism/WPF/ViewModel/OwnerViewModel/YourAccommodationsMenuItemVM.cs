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

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class YourAccommodationsMenuItemVM:ViewBase
    {
        public TextBlock popupText { get; } = new TextBlock();
        public Grid popupContainer { get; } = new Grid();
        public ComboBox ComboType { get; set; } = new ComboBox();
        public ComboBox ComboDeadline { get; set; } = new ComboBox();
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
            popupContainer.Visibility = Visibility.Collapsed;
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
            ComboType.SelectedValue= Types[0];
        }
        private void InitializeDays()
        {
            Deadlines = new List<String>() { "1 day", "3 days", "7 days", "14 days", "1 month", "3 months", "6 months" };
            Days = new Dictionary<string, int>();
            foreach (var item in Deadlines)
            {
                Days.Add(item, InDays(item));
            }
            ComboDeadline.SelectedValue = Deadlines[0];
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
            if (ComboType.SelectedValue.ToString() == "Apartment")
                NewAccommodation.Type = ACCOMMODATIONTYPE.APARTMENT;
            else if (ComboType.SelectedValue.ToString() == "House")
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
            NewAccommodation.CancellationDeadline = Days[(string)ComboDeadline.SelectedValue];
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
            popupText.Text = message;
            popupContainer.Visibility = Visibility.Visible;
            await Task.Delay(4000);
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(9);
                popupContainer.Opacity += -0.05;
            }
            popupContainer.Visibility = Visibility.Collapsed;
            popupContainer.Opacity = 1.0;
        }
        private void Reset()
        {
            NewAccommodation.Reset();
            NewLocation.Reset();
            ComboDeadline.SelectedValue = Deadlines[0];
            ComboType.SelectedValue = Types[0];
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
