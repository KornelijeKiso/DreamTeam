using System.Windows.Controls;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.WPF.View.GuideView.TourView;
using System.Windows.Input;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.ViewModel.GuideViewModels
{
    public class MainGuideWindowVM: ViewBase
    {
        public ContentControl ContentArea { get; set; } = new ContentControl();
        public string Username { get; set; }
        public GuideDTO Guide { get; set; }

        public MainGuideWindowVM(GuideDTO guide)
        {
            Guide = guide;
            ContentArea.Content = new HomeUserControl(Guide.Username);
        }
        private void HomeLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new HomeUserControl(Guide.Username);
        }
        private void AllAppointmentsLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new AllAppointmentsUserControl(Guide.Username);
        }
        private void ProfileLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new ProfileUserControl(Guide.Username);
        }
        private void RequestsLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new RequestsUserControl(Guide.Username);
        }
        private void LiveTourMonitorLink_RequestNavigate(object parameter)
        {
            ContentArea.Content = new TodaysToursUserControl(Guide.Username);
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
