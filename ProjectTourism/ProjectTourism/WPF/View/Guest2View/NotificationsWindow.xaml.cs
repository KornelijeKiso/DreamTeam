using System.Windows;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for NewTourNotificationWindow.xaml
    /// </summary>
    public partial class NotificationsWindow : Window
    {
        public NotificationsVM NotificationsVM { get; set; }
        public NotificationsWindow()
        {
            InitializeComponent();
            //DataContext = new NewTourNotificationVM();
        }

        public NotificationsWindow(Guest2DTO guest2)
        {
            InitializeComponent();
            this.NotificationsVM = new NotificationsVM(guest2);
            DataContext = this.NotificationsVM;
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.NotificationsVM.SeenNotification();
            Close();
        }

        private void DismissNotificationClick(object sender, RoutedEventArgs e)
        {
            this.NotificationsVM.DismissNotification();
        }

        private void DetailsDisplayClick(object sender, RoutedEventArgs e)
        {
            this.NotificationsVM.DetailsDisplayClick();
        }
    }
}
