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
using ProjectTourism.Observer;
using ProjectTourism.View.Guest2View.TicketView;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.Services;
using ProjectTourism.Repositories;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for Guest2MainWindow.xaml
    /// </summary>
    public partial class Guest2MainWindow : Window//, INotifyPropertyChanged//, IObserver
    {
        public Guest2VM Guest2 { get; set; }
        //public Guest2Service Guest2Service { get; set; }
        
        public NavigationVM NavigationVM { get; set; }
        public Guest2MainWindow(string username)
        {
            InitializeComponent();
            //Guest2Service = new Guest2Service(new Guest2Repository());
            Guest2 = new Guest2VM(username);
            NavigationVM = new NavigationVM();
            NavigationVM.SetGuest2(Guest2);
            DataContext = NavigationVM;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
    }
}
