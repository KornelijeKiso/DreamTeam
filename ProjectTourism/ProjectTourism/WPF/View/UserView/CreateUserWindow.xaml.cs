using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.View.Guest1View;
using ProjectTourism.View.GuideView;
using ProjectTourism.View.OwnerView;
using ProjectTourism.View.Guest2View;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Domain.IRepositories;
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

namespace ProjectTourism.View.UserView
{
    /// <summary>
    /// Interaction logic for CreateUserView.xaml
    /// </summary>
    public partial class CreateUserWindow : Window, INotifyPropertyChanged//, IObserver
    {
        public UserVM UserVM;
        public UserService Service;
        public CreateUserWindow()
        {
            InitializeComponent();
            UserVM = new UserVM(new User());
            Service = new UserService(new UserRepository());
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
            if (Service.UsernameAlreadyInUse(username))
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
            UserVM.Username = username;
            UserVM.Password = password;
            switch (ComboType.SelectedIndex)
            {
                case 0: { UserVM.Type = USERTYPE.OWNER; break; }
                case 1: { UserVM.Type = USERTYPE.GUEST1; break; }
                case 2: { UserVM.Type = USERTYPE.GUIDE; break; }
                case 3: { UserVM.Type = USERTYPE.GUEST2; break; }
            }
            if (!error)
            {
                if (UserVM.Type == USERTYPE.OWNER)
                {
                    CreateOwnerWindow CreateOwnerWindow = new CreateOwnerWindow(UserVM);
                    CreateOwnerWindow.ShowDialog();
                    Close();
                }
                else if (UserVM.Type == USERTYPE.GUIDE)
                {
                    CreateGuideWindow createGuideWindow = new CreateGuideWindow(UserVM);
                    createGuideWindow.ShowDialog();
                    Close();
                }
                else if (UserVM.Type == USERTYPE.GUEST1)
                {
                    CreateGuest1Window createGuest1Window = new CreateGuest1Window(UserVM);
                    createGuest1Window.ShowDialog();
                    Close();
                }
                else if (UserVM.Type == USERTYPE.GUEST2)
                {
                    CreateGuest2Window createGuest2Window = new CreateGuest2Window(UserVM);
                    createGuest2Window.ShowDialog();
                    Close();
                }
            }
        }
    }
}
