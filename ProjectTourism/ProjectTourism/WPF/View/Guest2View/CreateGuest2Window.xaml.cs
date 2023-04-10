using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
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

namespace ProjectTourism.View.Guest2View
{
    /// <summary>
    /// Interaction logic for CreateGuest2Window.xaml
    /// </summary>
    public partial class CreateGuest2Window : Window, INotifyPropertyChanged, IObserver
    {
        public Guest2VM Guest2VM { get; set; }
        public UserVM UserVM { get; set; }
        public Guest2Service Guest2Service { get; set; }
        public UserService UserService { get; set; }

        public CreateGuest2Window(UserVM userVM)
        {
            InitializeComponent();
            DataContext = this;
            Guest2Service = new Guest2Service(new Guest2Repository());
            UserService = new UserService(new UserRepository());
            Guest2VM = new Guest2VM( new Guest2());
            UserVM = userVM;
            Guest2VM.Username = userVM.Username;
            Guest2Service.Subscribe(this);
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
            if (Guest2VM.FirstName != null && Guest2VM.LastName != null)
            {
                UserService.Add(UserVM);
                Guest2Service.Add(Guest2VM);
                Close();
            }
            else
            {
                MessageBox.Show("You have not entered the data correctly.");
            }

        }
    }
}
