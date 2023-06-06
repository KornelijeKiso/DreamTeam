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
            SetUpDatePicker();
            //DataContext = new CreateTourRequestVM();
        }

        public CreateTourRequestUserControl(Guest2DTO guest2)
        {
            InitializeComponent();
            SetUpDatePicker();
            this.createTourRequestVM = new CreateTourRequestVM();
            DataContext = this.createTourRequestVM;
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
