using System;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

namespace ProjectTourism.WPF.View.Guest2View.UserControls
{
    /// <summary>
    /// Interaction logic for CreateComplexTourPartUserControl.xaml
    /// </summary>
    public partial class CreateComplexTourPartUserControl : UserControl
    {
        public CreateComplexTourPartUserControl()
        {
            InitializeComponent();
            SetUpDatePicker();
        }

        public CreateComplexTourPartUserControl(Guest2DTO guest2, ComplexTourDTO complexTour)
        {
            InitializeComponent();
            SetUpDatePicker();
            DataContext = new CreateComplexTourPartVM(guest2, complexTour);
        }
        private void SetUpDatePicker()
        {
            StartDatePicker.DisplayDate = DateTime.Now;
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));

            EndDatePicker.DisplayDate = DateTime.Now;
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));
        }
    }
}
