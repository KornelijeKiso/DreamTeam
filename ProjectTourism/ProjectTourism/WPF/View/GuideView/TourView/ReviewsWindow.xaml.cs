using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : UserControl, INotifyPropertyChanged
    {
        public TicketGradeVM SelectedTicketGrade { get; set; }
        public ObservableCollection<TicketVM> Tickets { get; set; }
        public ReviewsWindow(TourAppointmentVM tourApp)
        {
            InitializeComponent();
            DataContext = this;

            Tickets = new ObservableCollection<TicketVM>(tourApp.Tickets);
        }

        private void BadReviewButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
