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
using ProjectTourism.Utilities;


namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class ComplexTourRequestDetailsVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public ComplexTourDTO ComplexTour { get; set; }
        public DateTime ValidUntil { get; set; }
        public struct Parts
        {
            public string Header { get; set; }
            public TourRequestDTO Part { get; set; }
            public bool isAccepted 
            { 
                get => (Part.State == REQUESTSTATE.ACCEPTED); 
            }

            public Parts(string header, TourRequestDTO part)
            {
                this.Header = header;
                this.Part = part;
            }
        }
        public ObservableCollection<Parts> PartsCollection { get; set; }
        public Parts SelectedPart { get; set; }

        private object _Content;
        public object Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }
        public ComplexTourRequestDetailsVM() { }
        public ComplexTourRequestDetailsVM(Guest2DTO guest2, ComplexTourDTO complexTour) 
        {
            Guest2 = guest2;
            ComplexTour = complexTour;

            SetAttributes();

            // Commands
            CreateTicketCommand = new RelayCommand(CreateTicket);
            ContentCommand = new RelayCommand(ReturnToComplexTours);
        }

        private void SetAttributes()
        {
            ValidUntil = ComplexTour.TourRequests[0].CreationDateTime.AddDays(2);
            PartsCollection = new ObservableCollection<Parts>();

            int i = 1;
            foreach (var item in ComplexTour.TourRequests)
            {
                Parts parts = new Parts("Part " + i.ToString(), item);
                i++;
                PartsCollection.Add(parts);
            }
        }

        
        public ICommand CreateTicketCommand { get; set; }
        public void CreateTicket(object obj)
        {
            // TO DO 
        }
        public ICommand ContentCommand { get; set; }
        public void ReturnToComplexTours(object obj)
        {
            Content = new ComplexToursVM(Guest2);
        }
    }
}
