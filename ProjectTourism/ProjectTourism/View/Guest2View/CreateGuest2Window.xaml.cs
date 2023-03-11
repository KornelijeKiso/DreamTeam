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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.View.Guest2View
{
    /// <summary>
    /// Interaction logic for CreateGuest2Window.xaml
    /// </summary>
    public partial class CreateGuest2Window : Window, INotifyPropertyChanged, IObserver
    {
        public Guest2 Guest2 { get; set; }
        public User User { get; set; }
        public Guest2Controller Guest2Controller { get; set; }
        public UserController UserController { get; set; }

        public CreateGuest2Window(User user)
        {
            InitializeComponent();
            DataContext = this;
            Guest2Controller = new Guest2Controller();
            UserController = new UserController();
            Guest2 = new Guest2();
            User = user;
            Guest2.Username = user.Username;
            Guest2Controller.Subscribe(this);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (Guest2.FirstName != null && Guest2.LastName != null)
            {
                UserController.Add(User);
                Guest2Controller.Add(Guest2);
                Close();
            }
            else
            {
                MessageBox.Show("You have not entered the data correctly.");
            }

        }
    }
}
