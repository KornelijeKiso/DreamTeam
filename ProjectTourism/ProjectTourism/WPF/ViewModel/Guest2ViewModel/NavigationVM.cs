using System.Windows.Input;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.View.Guest2View;
using ProjectTourism.DTO;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public Guest2DTO Guest2 { get; set; }

        public ICommand HomeCommand { get; set; }
        public ICommand TicketsCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand TourRequestsCommand { get; set; }
        public ICommand ComplexToursCommand { get; set; }
        public ICommand NotificationsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM(Guest2);
        private void Tickets(object obj) => CurrentView = new TicketsVM(Guest2);
        private void Vouchers(object obj) => CurrentView = new VouchersVM(Guest2);
        private void Profile(object obj) => CurrentView = new ProfileVM(Guest2);
        private void TourRequests(object obj) => CurrentView = new TourRequestsVM(Guest2);
        private void ComplexTour(object obj) => CurrentView = new ComplexToursVM(Guest2);

        private void Notifications(object obj)
        {
            NotificationsWindow notificationWindow = new NotificationsWindow(Guest2);
            notificationWindow.ShowDialog();
            Guest2.HasNewNotifications = false;
            Guest2.NumberOfNotifications = 0;
        }
        public void DisplayNotifications()
        {
            NotificationsWindow notificationWindow = new NotificationsWindow(Guest2);
            notificationWindow.ShowDialog();
            Guest2.HasNewNotifications = false;
            Guest2.NumberOfNotifications = 0;
        }
        public NavigationVM(string username)
        {
            Guest2 = new Guest2DTO(username);

            HomeCommand = new RelayCommand(Home);
            TicketsCommand = new RelayCommand(Tickets);
            VouchersCommand = new RelayCommand(Vouchers);
            ProfileCommand = new RelayCommand(Profile);
            TourRequestsCommand = new RelayCommand(TourRequests);
            ComplexToursCommand = new RelayCommand(ComplexTour);
            NotificationsCommand = new RelayCommand(Notifications);

            // Startup Page
            CurrentView = new HomeVM(Guest2);

            // DEMO
            DemoOn = false;
            DemoCommand = new RelayCommand(StartDemo);
            StopDemoCommand = new RelayCommand(StopDemo);
            popupVisible = false;
            popupOpacity = 1.0;
        }

        // DEMO
        public StartGuest2DemoVM StartGuest2Demo { get; set; }
        private bool _DemoOn;
        public bool DemoOn
        {
            get => _DemoOn;
            set
            {
                _DemoOn = value;
                OnPropertyChanged();
            }
        }
        public ICommand DemoCommand { get; set; }
        private void StartDemo(object obj)
        {
            this.DemoOn = true;
            this.StartGuest2Demo = new StartGuest2DemoVM(Guest2, this);
        }
        public ICommand StopDemoCommand { get; set; }
        private void StopDemo(object obj)
        {
            DemoOn = false;
            this.StartGuest2Demo.StopCommand.Execute(null);
            ShowPopupMessage("DEMO STOPPED!", 3000);
        }
        // POP UP
        private string _popupText;
        public string popupText
        {
            get => _popupText;
            set
            {
                if (value != _popupText)
                {
                    _popupText = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _popupVisible;
        public bool popupVisible
        {
            get => _popupVisible;
            set
            {
                if (value != _popupVisible)
                {
                    _popupVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _popupOpacity;
        public double popupOpacity
        {
            get => _popupOpacity;
            set
            {
                if (value != _popupOpacity)
                {
                    _popupOpacity = value;
                    OnPropertyChanged();
                }
            }
        }
        public async void ShowPopupMessage(string message, int time)
        {
            popupText = message;
            popupVisible = true;
            await Task.Delay(time);
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(9);
                popupOpacity += -0.05;
            }
            popupVisible = false;
            popupOpacity = 1.0;
        }
    }
}
