using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using ProjectTourism.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Utilities;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class CreateTourRequestVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public List<string> LanguageList { get; set; }
        public TourRequestDTO TourRequest { get; set; }
        private object _TourRequestsContent;
        public object TourRequestsContent
        {
            get { return _TourRequestsContent; }
            set { _TourRequestsContent = value; OnPropertyChanged(); }
        }


        public CreateTourRequestVM() { }
        public CreateTourRequestVM(Guest2DTO guest2) 
        {
            Guest2 = guest2;
            SetTourRequest();
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };

            // Return to TourRequest
            TourRequestsCommand = new RelayCommand(ReturnToTourRequests);
        }

        private void SetTourRequest()
        {
            TourRequest = new TourRequestDTO(new TourRequest());
            TourRequest.Guest2Username = Guest2.Username;
            TourRequest.CreationDateTime = DateTime.Now;
            TourRequest.Location = new LocationDTO(new Location());
        }


        private ICommand _CreateTourRequestCommand;
        public ICommand CreateTourRequestCommand
        {
            get
            {
                return _CreateTourRequestCommand ?? (_CreateTourRequestCommand = new CommandHandler(() => CreateTourRequest(), () => true));
            }
        }
        private void CreateTourRequest()
        {
            if (TourRequest.IsValid &&
               (TourRequest.StartDate != DateOnly.FromDateTime(new DateTime(1, 1, 1))) &&
               (TourRequest.EndDate != DateOnly.FromDateTime(new DateTime(1, 1, 1))))
            {
                if (TourRequest.StartDate > TourRequest.EndDate)
                    MessageBox.Show("Invalid start and end date!");
                else
                {
                    Guest2.CreateTourRequest(TourRequest);
                    // TO DO
                    //Close();
                }
            }
            else
                MessageBox.Show("Tour Request can't be made because the data were not entered correctly.");
        }

        public ICommand TourRequestsCommand { get; set; }
        private void ReturnToTourRequests(Object obj)
        {
            TourRequestsContent = new TourRequestsVM(Guest2);
        }
    }
}
