using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Services;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class NotificationsVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public NotificationDTO SelectedNotification { get; set; }
        public NotificationService NotificationService { get; set; }
        public TourService TourService { get; set; }
        public TourDTO NewTour { get; set; }
        public TourVM SelectedTour { get; set; } // TO DO -> remove
        public NotificationsVM() { }
        
        public NotificationsVM(Guest2VM guest2) 
        {
            Guest2 = guest2;

            NotificationService = new NotificationService();
            TourService = new TourService();    
        }

        private int GetNewTourId(string text)
        {
            int from = text.IndexOf(':') + 1;
            int to = text.Length - 1;
            string idStr = text.Substring(from, to - from);
            return int.Parse(idStr);
        }

        public void SeenNotification()
        {
            List<Notification> notifications = NotificationService.GetAllByOwner(Guest2.Username);
            NotificationService.Seen(notifications);
            Guest2.HasNewNotifications = false;
            Guest2.NumberOfNotifications = 0;
        }
        public void DismissNotification()
        {
            NotificationService.Dismiss(SelectedNotification.GetNotification());
            Guest2.Notifications.Remove(SelectedNotification);
        }

        // TO DO -> replace SelectedTour(TourVM) with NewTour(TourDTO) in CreateTicketWindow
        
        //public ICommand DetailsDisplayCommand
        //{
        //    get => new RelayCommand(DetailsDisplayClick);
        //}
        //public void DetailsDisplayClick(object parameter)
        //{
        //    NewTour = new TourDTO(TourService.GetOne(GetNewTourId(SelectedNotification.Title)));
        //    SelectedTour = new TourVM(TourService.GetOne(GetNewTourId(SelectedNotification.Title)));
        //    CreateTicketWindow createTicketWidnow = new CreateTicketWindow(Guest2, SelectedTour);
        //    createTicketWidnow.ShowDialog();
        //}
        public void DetailsDisplayClick()
        {
            NewTour = new TourDTO(TourService.GetOne(GetNewTourId(SelectedNotification.Title)));
            SelectedTour = new TourVM(TourService.GetOne(GetNewTourId(SelectedNotification.Title)));
            CreateTicketWindow createTicketWidnow = new CreateTicketWindow(Guest2, SelectedTour);
            createTicketWidnow.ShowDialog();
        }
    }
}
