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

        public CreateComplexTourPartUserControl(TourRequestDTO part)
        {
            InitializeComponent();
            SetUpDatePicker();
            DataContext = new CreateComplexTourPartVM(part);
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
