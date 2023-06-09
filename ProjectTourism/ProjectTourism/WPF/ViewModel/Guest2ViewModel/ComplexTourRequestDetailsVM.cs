using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProjectTourism.DTO;
using ProjectTourism.Utilities;


namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class ComplexTourRequestDetailsVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public ComplexTourDTO ComplexTour { get; set; }
        public DateTime ValidUntil { get; set; }
        public TourRequestDTO SelectedPart { get; set; }

        private bool _isAccepted;
        public bool isAccepted
        {
            get => _isAccepted; 
            set 
            {
                if (value != _isAccepted)
                {
                    _isAccepted = value;
                    OnPropertyChanged();
                }
            }
        }



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

            ValidUntil = ComplexTour.TourRequests[0].CreationDateTime.AddDays(2);

            // Commands
            ContentCommand = new RelayCommand(ReturnToComplexTours);
        }



        public ICommand ContentCommand { get; set; }
        public void ReturnToComplexTours(object obj)
        {
            Content = new ComplexToursVM(Guest2);
        }
    }
}
