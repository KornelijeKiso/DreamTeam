using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.DTO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace ProjectTourism.View.Guest2View
{
    /// <summary>
    /// Interaction logic for CreateGuest2Window.xaml
    /// </summary>
    public partial class CreateGuest2Window : Window, INotifyPropertyChanged
    {
        public Guest2DTO Guest2 { get; set; }
        public UserVM UserVM { get; set; }
        public Guest2Service Guest2Service { get; set; }
        public UserService UserService { get; set; }

        public CreateGuest2Window(UserVM userVM)
        {
            InitializeComponent();
            DataContext = this;
            Guest2Service = new Guest2Service();
            UserService = new UserService();
            Guest2 = new Guest2DTO(new Guest2(userVM.GetUser()));
            UserVM = userVM;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (Guest2.GetGuest2().Email != null &&
                Guest2.GetGuest2().FirstName != null &&
                Guest2.GetGuest2().LastName != null)
            {
                UserService.Add(UserVM);
                Guest2Service.Add(Guest2.GetGuest2());
                Close();
            }
            else
            {
                MessageBox.Show("You have not entered the data correctly.");
            }

        }
    }
}