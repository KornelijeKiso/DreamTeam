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
    public partial class ReviewsUserControl : UserControl, INotifyPropertyChanged
    {
        public TicketVM SelectedTicket { get; set; }
        public ObservableCollection<TicketVM> Tickets { get; set; }
        public List<TicketVM> TicketsList { get; set; }
        public GuideVM Guide { get; set; }
        public ReviewsUserControl(TourAppointmentVM tourApp)
        {
            InitializeComponent();
            DataContext = this;

            Tickets = new ObservableCollection<TicketVM>(tourApp.Tickets);
            TicketsList = new List<TicketVM>(tourApp.Tickets);
            Guide = new GuideVM(tourApp.Tour.GuideUsername);
            Update();
        }
        private void BadReviewButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SelectedTicket.Guest2Username + "'s ticket has been reported!");
            Guide.ReportTicketGrade(SelectedTicket);
            Update();
        }
        public void Update()
        {
            Tickets.Clear();
            foreach(var ticket in TicketsList)
            {
                Tickets.Add(ticket);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
