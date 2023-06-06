﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectTourism.DTO;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.View.Guest2View.TicketView;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class ComplexToursVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public ObservableCollection<ComplexTourDTO> YourComplexTours { get; set; }
        public ComplexTourDTO SelectedComplexTour { get; set; }
        private object _Content;
        public object Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }
        public ComplexToursVM() { }
        public ComplexToursVM(Guest2DTO guest2) 
        {
            Guest2 = guest2;
            YourComplexTours = Guest2.ComplexTours;

            //
            CreateComplexTourRequestCommand = new RelayCommand(CreateComplexTourRequest);
        }



        public ICommand CreateComplexTourRequestCommand { get; set; }
        public void CreateComplexTourRequest(object obj)
        {
            Content = new CreateComplexTourRequestVM(Guest2);
        }
    }
}
