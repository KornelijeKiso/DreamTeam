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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView
{
    /// <summary>
    /// Interaction logic for CreateGuideWindow.xaml
    /// </summary>
    public partial class CreateGuideWindow : Window, INotifyPropertyChanged, IObserver
    {
        public GuideVM Guide { get; set; }
        public User User { get; set; }
        public GuideService GuideController { get; set; }
        public UserController UserController { get; set; }
        public CreateGuideWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new Guide();
            User = user;
            Guide.Username = user.Username;
            GuideController = new GuideController();
            UserController = new UserController();
            GuideController.Subscribe(this);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {

        }

        private void CreateGuideButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameSurnameNotNull())
            {
                UserController.Add(User);
                GuideController.Add(Guide);
                Close();
            }
            else
            {
                MessageBox.Show("You have not entered the data correctly.");
            }
        }

        private bool NameSurnameNotNull()
        {
            return Guide.Name != null && Guide.Surname != null;
        }
    }
}
