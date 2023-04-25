using ProjectTourism.Model;
using ProjectTourism.View.Guest1View;
using ProjectTourism.View.GuideView;
using ProjectTourism.View.Guest2View;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Domain.IRepositories;
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
    public partial class CreateUserWindow : Window, INotifyPropertyChanged
    {
        public UserVM User { get; set; }
        public CreateUserWindow()
        {
            InitializeComponent();
            DataContext = this;
            User = new UserVM(new User());
        }
        private void CreateUserClick(object sender, RoutedEventArgs e)
        {
            UserTypeComboBox();

            if (User.IsValid && IsValidUsername() && IsValidPassword())
            {
                if (User.Type == USERTYPE.OWNER)
                {
                    User.CreateOwner();
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
            else
                MessageBox.Show("You have not entered the data correctly.");
        }

        private bool IsValidUsername()
        {
            string username = txtUsername.Text;
            if (User.IsUsernameAlreadyInUse(username))
            {
                MessageBox.Show("Username already in use.");
                return false;
            }
            return true;
        }

        private bool IsValidPassword()
        {
            string password = txtPassword.Password;
            string passwordAgain = txtPasswordAgain.Password;
            if (!password.Equals(passwordAgain))
            {
                MessageBox.Show("Error in passwords.");
                return false;
            }
            User.Password = password;
            return true;
        }

        private void UserTypeComboBox()
        {
            switch (ComboType.SelectedIndex)
            {
                case 0: { User.Type = USERTYPE.OWNER; break; }
                case 1: { User.Type = USERTYPE.GUEST1; break; }
                case 2: { User.Type = USERTYPE.GUIDE; break; }
                case 3: { User.Type = USERTYPE.GUEST2; break; }
            }
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
