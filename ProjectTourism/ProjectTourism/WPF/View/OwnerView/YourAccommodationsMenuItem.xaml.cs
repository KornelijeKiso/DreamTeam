using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.OwnerView;
using ProjectTourism.WPF.ViewModel;
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

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for YourAccommodationsMenuItem.xaml
    /// </summary>
    public partial class YourAccommodationsMenuItem : UserControl,INotifyPropertyChanged
    {
        public OwnerVM Owner { get; set; }
        public AccommodationVM SelectedAccommodation { get; set; }
        public AccommodationVM NewAccommodation { get; set; }
        public LocationVM NewLocation { get; set; }
        public List<string> Deadlines { get; set; }
        public List<string> Types { get; set; }
        public Dictionary<string, int> Days { get; set; }
        public ObservableCollection<bool> IncDecButtons { get; set; }

        public YourAccommodationsMenuItem(string username)
        {
            InitializeComponent();
            DataContext = this;
            IncDecButtons = new ObservableCollection<bool> { true, false, true, false };

            InitializeDays();
            InitializeTypes();

            InitializeNewEntities();

            SetOwner(username);

        }

        private void SetOwner(string username)
        {
            Owner = new OwnerVM(username);
            NewAccommodation.Owner = Owner;
            NewAccommodation.OwnerUsername = username;
        }

        private void InitializeNewEntities()
        {
            NewAccommodation = new AccommodationVM(new Accommodation());
            NewLocation = new LocationVM(new Location());
        }

        private void InitializeTypes()
        {
            Types = new List<string>() { "Apartment", "House", "Hut" };
            ComboType.SelectedItem = Types[0];
        }
        private void InitializeDays()
        {
            Deadlines = new List<String>() { "1 day", "3 days", "7 days", "14 days", "1 month", "3 months", "6 months" };
            Days = new Dictionary<string, int>();
            foreach (var item in Deadlines)
            {
                Days.Add(item, InDays(item));
            }
            ComboDeadline.SelectedItem = Deadlines[0];
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HandleTypeCombobox()
        {
            if ((string)ComboType.SelectedItem == "Apartment")
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.APARTMENT;
            }
            else if ((string)ComboType.SelectedItem == "House")
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.HOUSE;
            }
            else
            {
                NewAccommodation.Type = ACCOMMODATIONTYPE.HUT;
            }
        }
        public void RegisterAccommodationClick(object sender, RoutedEventArgs e)
        {
            if (NewAccommodation.IsValid && NewLocation.IsValid)
            {
                RegisterNewAccommodation();
            }
            else
            {
                MessageBox.Show("Not all fields are filled correctly.");
            }
        }

        public void RenovateClick(object sender, RoutedEventArgs e)
        {
            RenovationsWindow renovationsWindow = new RenovationsWindow(SelectedAccommodation);
            renovationsWindow.ShowDialog();
        }
        private void RegisterNewAccommodation()
        {
            HandleTypeCombobox();
            NewAccommodation.CancellationDeadline = Days[(string)ComboDeadline.SelectedValue];
            Owner.AddAccommodation(NewAccommodation, NewLocation);
            Reset();
        }

        private void Reset()
        {
            NewAccommodation.Reset();
            NewLocation.Reset();
            ComboDeadline.SelectedItem = Deadlines[0];
            ComboType.SelectedItem = Types[0];
        }
        public void IncreaseMaxNumberOfGuestsClick(object sender, RoutedEventArgs e)
        {
                NewAccommodation.MaxNumberOfGuests++;
                if (NewAccommodation.MaxNumberOfGuests == 15) IncDecButtons[0] = false;
                IncDecButtons[1] = true;
        }
        public void DecreaseMaxNumberOfGuestsClick(object sender, RoutedEventArgs e)
        {
                NewAccommodation.MaxNumberOfGuests--;
                if (NewAccommodation.MaxNumberOfGuests == 1) IncDecButtons[1] = false;
                IncDecButtons[0] = true;
        }
        public void IncreaseMinDaysForReservationClick(object sender, RoutedEventArgs e)
        {
                NewAccommodation.MinDaysForReservation++;
                if (NewAccommodation.MinDaysForReservation == 7) IncDecButtons[2] = false;
                IncDecButtons[3] = true;
        }
        public void DecreaseMinDaysForReservationClick(object sender, RoutedEventArgs e)
        {
                NewAccommodation.MinDaysForReservation--;
                if (NewAccommodation.MinDaysForReservation == 1) IncDecButtons[3] = false;
                IncDecButtons[2] = true;
        }
        
    }
}
