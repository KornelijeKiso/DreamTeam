using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Services;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class RenovationsWindowVM:ViewBase
    {
        public TextBox SelectedFreeAppointment { get; set; } = new TextBox();
        public Grid popupContainer { get;set; } = new Grid();
        public TextBlock popupText { get; set; } = new TextBlock();
        public AccommodationDTO Accommodation { get; set; }
        public RenovationDTO SelectedRenovation { get; set; }
        public RenovationDTO NewRenovation { get; set; }
        public RenovationAppointmentDTO RenovationAppointment { get; set; }
        public RenovationsWindowVM(AccommodationDTO accommodation)
        {
            Accommodation = accommodation;
            NewRenovation = new RenovationDTO();
            RenovationAppointment = new RenovationAppointmentDTO();
            RenovationAppointment.AccommodationId = Accommodation.Id;
            NewRenovation.AccommodationId = Accommodation.Id;
        }
        public RenovationsWindowVM()
        {
        }
        public void ScheduleRenovationClick(object parameter)
        {
            if (SelectedFreeAppointment.Text.ToString() == "") System.Windows.MessageBox.Show("You have to select appointment for your renovation.");
            else
            {
                ScheduleNewRenovation(NewRenovation);
                ShowPopupMessage("You have successfully scheduled new renovation.\n You can see it in Previously scheduled renovations down below.");
                NewRenovation.Reset();
                SelectedFreeAppointment.Text = "";
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
                SelectedFreeAppointment.Text = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.StartDate.ToString() + " - " + selectFreeAppointmentForRenovatonWindow.SelectedRenovation.EndDate.ToString();
                NewRenovation.StartDate = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.StartDate;
                NewRenovation.EndDate = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.EndDate;
            }
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
        public void DeclineClick(object sparameter)
        {
            SelectedFreeAppointment.Text = "";
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
