using ProjectTourism.Model;
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
    public partial class CreateGuideWindow : Window, INotifyPropertyChanged
    {
        public GuideVM Guide { get; set; }
        public UserVM UserVM { get; set; }
        public CreateGuideWindow(UserVM userVM)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(new Guide(userVM.GetUser()));
            UserVM = userVM;
        }
        private void CreateGuideButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameSurnameNotNull())
            {
                UserVM.Add(UserVM);
                Guide.Add(Guide.GetGuide());
                Close();
            }
            else
                MessageBox.Show("You have not entered the data correctly.");
        }
        private bool NameSurnameNotNull()
        {
            return Guide.GetGuide().FirstName != null
               && Guide.GetGuide().LastName != null;
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
