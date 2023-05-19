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
using System.Windows.Shapes;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

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

        public NotificationsWindow(Guest2VM guest2)
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
