using ProjectTourism.DTO;
using ProjectTourism.View.OwnerView;
using ProjectTourism.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ProjectTourism.Utilities;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class ReservationsMenuItemVM:INotifyPropertyChanged
    {
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
        public OwnerDTO Owner { get; set; }
        public ReservationDTO SelectedReservation { get; set; }
        public ReservationsMenuItemVM(string username)
        {
            Owner = new OwnerDTO(username);
            popupVisible = false;
            popupOpacity = 1.0;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void GradeGuestClick(object parameter)
        {
            Owner.timer.Stop();
            GradeGuestWindow gradeGuestWindow = new GradeGuestWindow(SelectedReservation, Owner);
            gradeGuestWindow.ShowDialog();
            if (SelectedReservation.Graded) ShowPopupMessage("You have successfully graded your guest.\n You are now able to see guests review if guest left it.");
            Owner.SetTimer();
        }
        public void SeeReviewClick(object parameter)
        {
            Owner.timer.Stop();
            Guest1ReviewWindow guestsReviewWindow = new Guest1ReviewWindow(SelectedReservation);
            guestsReviewWindow.ShowDialog();
            Owner.SetTimer();
        }

        private void PostponeRequestClick(object parameter)
        {
            Owner.timer.Stop();
            PostponeRequestWindow postponeRequestWindow = new PostponeRequestWindow(SelectedReservation, Owner.HelpOn);
            postponeRequestWindow.ShowDialog();
            if (SelectedReservation != null)
            {
                if (SelectedReservation.PostponeRequest.Accepted) ShowPopupMessage("You have successfully accepted postpone request.\n Reservation is postponed for the appointment requested by guest.");
                if (SelectedReservation.PostponeRequest.Rejected) ShowPopupMessage("You have successfully rejected postpone request.\n Your guest will be informed about this action.");
            }
            Owner.SetTimer();
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

        public ICommand PostponeRequestClickCommand
        {
            get => new RelayCommand(PostponeRequestClick);
        }
        public ICommand SeeReviewClickCommand
        {
            get => new RelayCommand(SeeReviewClick);
        }
        public ICommand GradeGuestClickCommand
        {
            get => new RelayCommand(GradeGuestClick);
        }
    }
}
