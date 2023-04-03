using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
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

namespace ProjectTourism.View.Guest1View
{
    /// <summary>
    /// Interaction logic for CreateGuest1Window.xaml
    /// </summary>
    public partial class CreateGuest1Window : Window, INotifyPropertyChanged, IObserver
    {
        public Guest1 Guest1 { get; set; }
        public User User { get; set; }
        public Guest1Controller Guest1Controller { get; set; }
        public UserController UserController { get; set; }

        public CreateGuest1Window(User user)
        {
            InitializeComponent();
            DataContext = this;
            Guest1 = new Guest1();
            User = user;
            Guest1.Username = User.Username;
            Guest1Controller = new Guest1Controller();
            UserController = new UserController();
            Guest1Controller.Subscribe(this);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreateGuestClick(object sender, RoutedEventArgs e)
        {
            if (Guest1.Email != null && Guest1.FirstName != null && Guest1.LastName != null)
            {
                UserController.Add(User);
                Guest1Controller.Add(Guest1);
                Close();
            }
            else
            {
                MessageBox.Show("Niste dobro uneli podatke.");
            }

        }
        public void Update()
        {

        }
    }
}
