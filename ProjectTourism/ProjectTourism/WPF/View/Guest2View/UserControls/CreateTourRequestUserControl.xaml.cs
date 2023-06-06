using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.Guest2View.UserControls
{
    /// <summary>
    /// Interaction logic for CreateTourRequestUserControl.xaml
    /// </summary>
    public partial class CreateTourRequestUserControl : UserControl
    {
        public CreateTourRequestVM createTourRequestVM { get; set; }
        public CreateTourRequestUserControl()
        {
            InitializeComponent();
            //DataContext = new CreateTourRequestVM();
        }

        public CreateTourRequestUserControl(Guest2DTO guest2)
        {
            InitializeComponent();
            this.createTourRequestVM = new CreateTourRequestVM();
            SetUpDatePicker(createTourRequestVM.TourRequest);
            DataContext = this.createTourRequestVM;
        }

        private void SetUpDatePicker(TourRequestDTO TourRequest)
        {
            StartDatePicker.DisplayDate = DateTime.Now;
            TourRequest.StartDate = DateOnly.FromDateTime(new DateTime(1, 1, 1));
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));

            EndDatePicker.DisplayDate = DateTime.Now;
            TourRequest.EndDate = DateOnly.FromDateTime(new DateTime(1, 1, 1));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(2)));
        }
        
        // TO DO -> fix
        private void StartDateChanged(object sender, SelectionChangedEventArgs e)
        {
            createTourRequestVM.TourRequest.StartDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
            DateTime startDate = (createTourRequestVM.TourRequest.StartDate.ToDateTime(TimeOnly.MinValue));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), startDate));
        }
        private void EndDateChanged(object sender, SelectionChangedEventArgs e)
        {
            createTourRequestVM.TourRequest.EndDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }
    }
}
