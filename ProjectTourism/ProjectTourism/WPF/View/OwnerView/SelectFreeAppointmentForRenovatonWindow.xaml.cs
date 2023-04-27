using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for SelectFreeAppointmentForRenovaton.xaml
    /// </summary>
    public partial class SelectFreeAppointmentForRenovatonWindow : Window
    {
        public RenovationAppointmentVM RenovationAppointment { get; set; }
        public RenovationVM SelectedRenovation { get; set; }
        public ObservableCollection<RenovationVM> OfferedAppointments { get; set; }
        public bool NoAppointments { get; set; }
        public SelectFreeAppointmentForRenovatonWindow(RenovationAppointmentVM renovationAppointment)
        {
            InitializeComponent();
            DataContext = this;
            RenovationAppointment= renovationAppointment;
            OfferedAppointments = RenovationAppointment.OfferedAppointments();
            NoAppointments = OfferedAppointments.Count == 0;
        }
        public void SelectClick(object sender, RoutedEventArgs e)
        {
            if (SelectedRenovation == null) MessageBox.Show("You have to choose one of the offered appointments.");
            else
            {
                Close();
            }
        }
        public void CancelClick(object sender, RoutedEventArgs e)
        {
            SelectedRenovation = null;
            Close();
        }
    }
}
