using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Services;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.View.OwnerView;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class RenovationsWindowVM:INotifyPropertyChanged
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
        private string _SelectedFreeAppointment;
        public string SelectedFreeAppointment
        {
            get => _SelectedFreeAppointment;
            set
            {
                if (value != _SelectedFreeAppointment)
                {
                    _SelectedFreeAppointment = value;
                    OnPropertyChanged();
                }
            }
        }
        public AccommodationDTO Accommodation { get; set; }
        public RenovationDTO SelectedRenovation { get; set; }
        public RenovationDTO NewRenovation { get; set; }
        public RenovationAppointmentDTO RenovationAppointment { get; set; }
        public bool Help { get; set; }
        public RenovationsWindowVM(AccommodationDTO accommodation, bool help)
        {
            Accommodation = accommodation;
            NewRenovation = new RenovationDTO();
            RenovationAppointment = new RenovationAppointmentDTO();
            RenovationAppointment.AccommodationId = Accommodation.Id;
            NewRenovation.AccommodationId = Accommodation.Id;
            popupVisible = false;
            popupOpacity = 1.0;
            Help = help;
        }
        public RenovationsWindowVM()
        {
        }
        public void ScheduleRenovationClick(object parameter)
        {
            if (SelectedFreeAppointment.ToString() == "") System.Windows.MessageBox.Show("You have to select appointment for your renovation.");
            else
            {
                ScheduleNewRenovation(NewRenovation);
                ShowPopupMessage("You have successfully scheduled new renovation.\n You can see it in Previously scheduled renovations down below.");
                NewRenovation.Reset();
                SelectedFreeAppointment = "";
            }
        }
        public void ScheduleNewRenovation(RenovationDTO renovation)
        {
            RenovationService renovationService = new RenovationService();
            Renovation ren = new Renovation(renovation.GetRenovation());
            ren.Id = renovationService.ScheduleAndReturnId(new Renovation(ren));
            Accommodation.Renovations.Insert(0, new RenovationDTO(ren));
            Accommodation.NoRenovations = false;
        }
        public void SelectFreeAppointmentClick(object parameter)
        {
            if (string.IsNullOrEmpty(RenovationAppointment.Duration.ToString()))
            {
                System.Windows.MessageBox.Show("Enter a duration.");
                return;
            }
            SelectFreeAppointmentForRenovatonWindow selectFreeAppointmentForRenovatonWindow = new SelectFreeAppointmentForRenovatonWindow(RenovationAppointment);
            selectFreeAppointmentForRenovatonWindow.ShowDialog();
            if (selectFreeAppointmentForRenovatonWindow.SelectedRenovation != null)
            {
                SelectedFreeAppointment = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.StartDate.ToString() + " - " + selectFreeAppointmentForRenovatonWindow.SelectedRenovation.EndDate.ToString();
                NewRenovation.StartDate = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.StartDate;
                NewRenovation.EndDate = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.EndDate;
            }
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
        public void DeclineClick(object sparameter)
        {
            SelectedFreeAppointment = "";
        }

        public void CancelRenovationClick(object parameter)
        {
            CancelRenovation(SelectedRenovation);
            ShowPopupMessage("You have successfully canceled renovation.\n It is now removed from your scheduled renovations.");
        }
        public void CancelRenovation(RenovationDTO renovation)
        {
            Accommodation.Renovations.Remove(renovation);
            RenovationService renovationService = new RenovationService();
            renovationService.Cancel(renovation.GetRenovation());
            Accommodation.NoRenovations = Accommodation.Renovations.Count == 0;
        }
        public ICommand SelectFreeAppointmentClickCommand
        {
            get => new RelayCommand(SelectFreeAppointmentClick);
        }
        
        public ICommand ScheduleRenovationClickCommand
        {
            get => new RelayCommand(ScheduleRenovationClick);
        }
        public ICommand CancelRenovationClickCommand
        {
            get => new RelayCommand(CancelRenovationClick);
        }
        public ICommand DeclineClickCommand
        {
            get => new RelayCommand(DeclineClick);
        }
    }
}
