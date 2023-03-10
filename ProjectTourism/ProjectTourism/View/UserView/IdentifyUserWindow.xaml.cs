using ProjectTourism.Controller;
using ProjectTourism.Model;
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

namespace ProjectTourism.View.UserView
{
    /// <summary>
    /// Interaction logic for IdentifyUserView.xaml
    /// </summary>
    public partial class IdentifyUserWindow : Window
    {
        public User User;
        public UserController Controller;
        public IdentifyUserWindow()
        {
            InitializeComponent();
            User = new User();
            Controller = new UserController();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void LogInClick(object sender, RoutedEventArgs e)
        {
            User.Password = txtPassword.Password;
            User.Username = txtUsername.Text;
            User newUser = Controller.Identify(User);
            if (newUser != null)
            {
                switch (User.Type)
                {
                    case USERTYPE.OWNER: { MessageBox.Show("owner."); break; }
                    case USERTYPE.GUIDE: { MessageBox.Show("guide"); break; }
                    case USERTYPE.GUEST1: { MessageBox.Show("guest1"); break; }
                    case USERTYPE.GUEST2: { MessageBox.Show("guest2"); break; }
                }
            }
            else
            {
                MessageBox.Show("Incorrect username or password.");
            }
        }

        private void CreateUserClick(object sender, RoutedEventArgs e)
        {
            CreateUserWindow CreateUser = new CreateUserWindow();
            CreateUser.ShowDialog();
        }
    }
}
