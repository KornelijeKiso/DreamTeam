using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Repositories;
using System.Runtime.CompilerServices;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.Localization;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class AllAppointmentsUserControl : UserControl, INotifyPropertyChanged
    {
        public TourAppointmentVM SelectedAppointment { get; set; }
        public GuideVM Guide { get; set; }
        

        public AllAppointmentsUserControl(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            AllAppsLabel.Visibility = Visibility.Hidden;
            TabControl.Visibility = Visibility.Hidden;
            ContentArea.Content = new ReviewsUserControl(SelectedAppointment);
        }
        
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            string name = new string(SelectedAppointment.Tour.Name);
            MessageBoxResult result = MessageBox.Show(GetLocalizedErrorMessage("CancelAppQuestion"), GetLocalizedErrorMessage("DeleteApp"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Guide.CancelAppointment(SelectedAppointment);
                MessageBox.Show(name + GetLocalizedErrorMessage("SucecssfulDeletation"));
                Guide.CanceledApps.Add(SelectedAppointment);
                Guide.ReadyApps.Remove(SelectedAppointment);
            }
        }
        string GetLocalizedErrorMessage(string resourceKey)
        {
            TextBlock Templabel = new TextBlock();
            LocExtension locExtension = new LocExtension(resourceKey);
            BindingOperations.SetBinding(Templabel, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
            return Templabel.Text;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
