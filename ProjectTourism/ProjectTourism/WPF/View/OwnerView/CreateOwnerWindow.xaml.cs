using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.WPF.ViewModel;
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
        public OwnerVM OwnerVM { get; set; }
        public UserVM UserVM { get; set; }
        public OwnerService OwnerService { get; set; }
        public UserService UserService { get; set; }

        public CreateOwnerWindow(UserVM userVM)
        {
            InitializeComponent();
            DataContext= this;
            OwnerVM = new OwnerVM(userVM.Username);
            UserVM = userVM;
            OwnerVM.Username= UserVM.Username;
            OwnerService = new OwnerService(new OwnerRepository());
            UserService = new UserService(new UserRepository());
            OwnerService.Subscribe(this);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CreateClick(object sender, RoutedEventArgs e)
        {
            if(OwnerVM.Email!=null && OwnerVM.FirstName!=null && OwnerVM.LastName != null)
            {
                UserService.Add(UserVM);
                OwnerService.Add(OwnerVM.GetOwner());
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
