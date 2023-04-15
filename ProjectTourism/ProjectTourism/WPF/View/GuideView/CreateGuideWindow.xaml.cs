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

namespace ProjectTourism.View.GuideView
{
    /// <summary>
    /// Interaction logic for CreateGuideWindow.xaml
    /// </summary>
    public partial class CreateGuideWindow : Window, INotifyPropertyChanged, IObserver
    {
        public GuideVM Guide { get; set; }
        public UserVM UserVM { get; set; }
        public GuideService GuideService { get; set; }
        public UserService UserService { get; set; }
        public CreateGuideWindow(UserVM userVM)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(new Guide(userVM.GetUser()));
            UserVM = userVM;
            GuideService = new GuideService(new GuideRepository());
            UserService = new UserService(new UserRepository());
            GuideService.Subscribe(this);
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
                UserService.Add(UserVM);
                GuideService.Add(Guide.GetGuide());
                Close();
            }
            else
            {
                MessageBox.Show("You have not entered the data correctly.");
            }
        }

        private bool NameSurnameNotNull()
        {
            return Guide.GetGuide().FirstName != null
               && Guide.GetGuide().LastName != null;
        }
    }
}
