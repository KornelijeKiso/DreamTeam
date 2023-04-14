using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.View.GuideView;
using ProjectTourism.View.OwnerView;
using ProjectTourism.View.Guest1View;
using ProjectTourism.WPF.View.Guest2View;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.Domain.IRepositories;
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
        public UserService Service;
        public IdentifyUserWindow()
        {
            InitializeComponent();
            DataContext = this;
            //UserVM = new UserVM();
            Service = new UserService(new UserRepository());
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void LogInClick(object sender, RoutedEventArgs e)
        {
            UserVM = new UserVM(new User(txtUsername.Text, txtPassword.Password));
            UserVM newUser = Service.Identify(UserVM);
            if (newUser != null)
            {
                switch (UserVM.Type)
                {
                    case USERTYPE.OWNER: 
                        {
                            MainOwnerWindow mainOwnerWindow = new MainOwnerWindow(UserVM.Username);
                            mainOwnerWindow.ShowDialog();
                            break; 
                        }
                    case USERTYPE.GUIDE:
                        {
                            MainGuideWindow mainGuideWindow = new MainGuideWindow(UserVM.Username);
                            mainGuideWindow.ShowDialog();
                            break;
                        }
                    case USERTYPE.GUEST1:
                        {
                            Guest1MainWindow guest1mainWindow = new Guest1MainWindow(UserVM.Username);
                            guest1mainWindow.ShowDialog();
                            
                            break;
                        }
                    case USERTYPE.GUEST2:
                        {
                            //MainGuest2Window mainGuest2Window = new MainGuest2Window(User.Username);
                            Guest2MainWindow mainGuest2Window = new Guest2MainWindow(UserVM.Username);
                            mainGuest2Window.ShowDialog();
                            break;
                        }

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
