using System.Windows.Controls;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.WPF.View.GuideView.TourView;
using System.Windows.Input;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.ViewModel.GuideViewModels
{
    public class MainGuideWindowVM: ViewBase
    {
        public ContentControl ContentArea { get; set; } = new ContentControl();
        public string Username { get; set; }

        public MainGuideWindowVM(string username)
        {
            Username = username;
            ContentArea.Content = new HomeUserControl(username);
        }
        private void HomeLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new HomeUserControl(Username);
        }
        private void AllAppointmentsLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new AllAppointmentsUserControl(Username);
        }
        private void ProfileLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new ProfileUserControl(Username);
        }
        private void RequestsLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new RequestsUserControl(Username);
        }
        private void LiveTourMonitorLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new TodaysToursUserControl(Username);
        }

        public ICommand HomeLink_RequestNavigateCommand
        {
            get => new RelayCommand(HomeLink_RequestNavigate);
        }
        public ICommand AllAppointmentsLink_RequestNavigateCommand
        {
            get => new RelayCommand(AllAppointmentsLink_RequestNavigate);
        }
        public ICommand ProfileLink_RequestNavigateCommand
        {
            get => new RelayCommand(ProfileLink_RequestNavigate);
        }
        public ICommand RequestsLink_RequestNavigateCommand
        {
            get => new RelayCommand(RequestsLink_RequestNavigate);
        }
        public ICommand LiveTourMonitorLink_RequestNavigateCommand
        {
            get => new RelayCommand(LiveTourMonitorLink_RequestNavigate);
        }
    }
}
