using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectTourism.DTO;
using ProjectTourism.Localization;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class ReviewsUserControl : UserControl, INotifyPropertyChanged
    {
        public TicketDTO SelectedTicket { get; set; }
        public ObservableCollection<TicketDTO> Tickets { get; set; }
        public List<TicketDTO> TicketsList { get; set; }
        public GuideDTO Guide { get; set; }
        public ReviewsUserControl(TourAppointmentDTO tourApp)
        {
            InitializeComponent();
            DataContext = this;

            Tickets = new ObservableCollection<TicketDTO>(tourApp.Tickets);
            TicketsList = new List<TicketDTO>(tourApp.Tickets);
            Guide = new GuideDTO(tourApp.Tour.GuideUsername);
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
