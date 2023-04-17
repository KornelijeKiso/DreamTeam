using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.View.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class HomeWindow : UserControl, INotifyPropertyChanged, IObserver
    {
        public GuideVM Guide { get; set; }
        public HomeWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void AddNewTourButton_Click(object sender, RoutedEventArgs e)
        {
            HideHomeContent();
            ContentArea.Content = new CreateTourWindow(Guide);
        }
        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            HideHomeContent();
            ContentArea.Content = new ViewAllToursWindow(Guide.Username);
        }
        private void HideHomeContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { HomeLabel, WelcomeLabel, UpcomingLabel, AddNewTourButton, AllToursButton };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
