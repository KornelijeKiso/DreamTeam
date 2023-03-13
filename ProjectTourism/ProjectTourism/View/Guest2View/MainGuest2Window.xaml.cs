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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.View.Guest2View
{
    /// <summary>
    /// Interaction logic for MainGuest2Window.xaml
    /// </summary>
    public partial class MainGuest2Window : Window, INotifyPropertyChanged, IObserver
    {
        public User User { get; set; }
        public UserController UserController { get; set; }
        public Guest2 Guest { get; set; }
        public Guest2Controller GuestController { get; set; }
        public RouteController RouteController { get; set; }
        public Route? SelectedRoute { get; set; }
        public ObservableCollection<Route> Routes { get; set; }
        //public GuideController GuideController { get; set; }
        

        public MainGuest2Window(string username)
        {
            InitializeComponent();
            DataContext = this;
            UserController = new UserController();
            User = UserController.GetOne(username);
            GuestController = new Guest2Controller();
            Guest = GuestController.GetOne(username);
            //GuideController = new GuideController();
            RouteController = new RouteController();
            Routes = new ObservableCollection<Route>(RouteController.GetAll());
            
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

    }
}
