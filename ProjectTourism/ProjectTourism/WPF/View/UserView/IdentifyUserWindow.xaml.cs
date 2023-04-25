using ProjectTourism.Model;
using ProjectTourism.View.GuideView;
using ProjectTourism.View.OwnerView;
using ProjectTourism.View.Guest1View;
using ProjectTourism.WPF.View.Guest2View;
using ProjectTourism.WPF.ViewModel;
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
        public UserVM UserVM { get; set; }
        public CurrentUserVM CurrentUserVM { get; set; }
        public IdentifyUserWindow()
        {
            InitializeComponent();
        }
        private void LogInClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
                MessageBox.Show("Write your username and password.");
            else
            {
                UserVM = new UserVM(txtUsername.Text);
                if (UserVM.GetUser() == null)
                {
                    MessageBox.Show("User with that username doesn't exist.");
                }
                else
                {
                    if (UserVM != null && UserVM.Password.Equals(txtPassword.Password))
                    {
                        switch (UserVM.Type)
                        {
                            case USERTYPE.OWNER:
                                {
                                    MainOwnerWindow mainOwnerWindow = new MainOwnerWindow(UserVM.Username);
                                    mainOwnerWindow.ShowDialog();
                                    Clear();
                                    break;
                                }
                            case USERTYPE.GUIDE:
                                {
                                    MainGuideWindow mainGuideWindow = new MainGuideWindow(UserVM.Username);
                                    mainGuideWindow.ShowDialog();
                                    Clear();
                                    break;
                                }
                            case USERTYPE.GUEST1:
                                {
                                    Guest1MainWindow guest1mainWindow = new Guest1MainWindow(UserVM.Username);
                                    guest1mainWindow.ShowDialog();
                                    Clear();
                                    break;
                                }
                            case USERTYPE.GUEST2:
                                {
                                    CurrentUserVM = new CurrentUserVM(UserVM);
                                    Guest2MainWindow mainGuest2Window = new Guest2MainWindow();
                                    mainGuest2Window.ShowDialog();
                                    CurrentUserVM.LogoutCurrentUser();
                                    Clear();
                                    break;
                                }
                        }
                    }
                    else
                        MessageBox.Show("Incorrect password.");
                }
            }
        }
        private void CreateUserClick(object sender, RoutedEventArgs e)
        {
            CreateUserWindow CreateUser = new CreateUserWindow();
            CreateUser.ShowDialog();
        }
        private void Clear()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Password = string.Empty;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
