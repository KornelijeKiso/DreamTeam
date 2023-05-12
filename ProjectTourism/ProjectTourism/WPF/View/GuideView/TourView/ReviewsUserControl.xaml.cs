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
using ProjectTourism.Localization;
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

            MessageBoxResult result = MessageBox.Show(GetLocalizedErrorMessage("ReportReviewQuestion") + SelectedTicket.Guest2Username, GetLocalizedErrorMessage("ReportReview"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(GetLocalizedErrorMessage("ReviewReported") + SelectedTicket.Guest2Username + GetLocalizedErrorMessage("ReviewHasBeenReported"));
                Guide.ReportTicketGrade(SelectedTicket);
                Update();
            }
        }
        void ShowLocalizedErrorMessage(string resourceKey)
        {
            string errorMessage = GetLocalizedErrorMessage(resourceKey);
            MessageBox.Show(errorMessage);
        }

        string GetLocalizedErrorMessage(string resourceKey)
        {
            TextBlock Templabel = new TextBlock();
            LocExtension locExtension = new LocExtension(resourceKey);
            BindingOperations.SetBinding(Templabel, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
            return Templabel.Text;
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
