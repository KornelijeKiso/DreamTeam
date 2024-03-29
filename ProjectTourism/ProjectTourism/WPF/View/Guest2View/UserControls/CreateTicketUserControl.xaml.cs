﻿using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

namespace ProjectTourism.WPF.View.Guest2View.UserControls
{
    /// <summary>
    /// Interaction logic for CreateTicketUserControl.xaml
    /// </summary>
    public partial class CreateTicketUserControl : UserControl
    {
        public CreateTicketVM createTicketVM { get; set; }
        public CreateTicketUserControl()
        {
            InitializeComponent();
            //DataContext = new CreateTicketVM()
        }

        public CreateTicketUserControl(Guest2DTO guest2, TourDTO tour)
        {
            InitializeComponent();
            this.createTicketVM = new CreateTicketVM(guest2, tour);
            DataContext = this.createTicketVM;
        }
    }
}
