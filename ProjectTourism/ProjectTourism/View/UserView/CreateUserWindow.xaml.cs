using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.View.Guest1View;
using ProjectTourism.View.GuideView;
using ProjectTourism.View.OwnerView;
using ProjectTourism.View.Guest2View;
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
    /// Interaction logic for CreateUserView.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public User User;
        public UserController Controller;
        public CreateUserWindow()
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

        private void CreateUserClick(object sender, RoutedEventArgs e)
        {
            bool error = false;
            string username = txtUsername.Text;
            if (Controller.UsernameAlreadyInUse(username))
            {
                MessageBox.Show("Username already in use.");
                error = true;
            }
            string password = txtPassword.Password;
            string passwordAgain = txtPasswordAgain.Password;
            if (!password.Equals(passwordAgain))
            {
                MessageBox.Show("Error in passwords.");
                error = true;
            }
            User.Username = username;
            User.Password = password;
            switch (ComboType.SelectedIndex)
            {
                case 0: { User.Type = USERTYPE.OWNER; break; }
                case 1: { User.Type = USERTYPE.GUEST1; break; }
                case 2: { User.Type = USERTYPE.GUIDE; break; }
                case 3: { User.Type = USERTYPE.GUEST2; break; }
            }
            if (!error)
            {
                if (User.Type == USERTYPE.OWNER)
                {
                    CreateOwnerWindow CreateOwnerWindow = new CreateOwnerWindow(User);
                    CreateOwnerWindow.ShowDialog();
                    Close();
                }
                else if (User.Type == USERTYPE.GUIDE)
                {
                    CreateGuideWindow createGuideWindow = new CreateGuideWindow(User);
                    createGuideWindow.ShowDialog();
                    Close();
                }
                else if (User.Type == USERTYPE.GUEST1)
                {
                    CreateGuest1Window createGuest1Window = new CreateGuest1Window(User);
                    createGuest1Window.ShowDialog();
                    Close();
                }
                else if (User.Type == USERTYPE.GUEST2)
                {
                    CreateGuest2Window createGuest2Window = new CreateGuest2Window(User);
                    createGuest2Window.ShowDialog();
                    Close();
                }
            }
        }
    }
}
