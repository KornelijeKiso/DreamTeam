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

namespace ProjectTourism.View.OwnerView
{
    /// <summary>
    /// Interaction logic for CreateOwnerWindow.xaml
    /// </summary>
    public partial class CreateOwnerWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Owner Owner { get; set; }
        public User User { get; set; }
        public OwnerController OwnerController { get; set; }
        public UserController UserController { get; set; }

        public CreateOwnerWindow(User user)
        {
            InitializeComponent();
            DataContext= this;
            Owner = new Owner();
            User = user;
            Owner.Username= User.Username;
            OwnerController = new OwnerController();
            UserController = new UserController();
            OwnerController.Subscribe(this);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreateClick(object sender, RoutedEventArgs e)
        {
            if(Owner.Email!=null && Owner.FirstName!=null && Owner.LastName != null)
            {
                UserController.Add(User);
                OwnerController.Add(Owner);
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
