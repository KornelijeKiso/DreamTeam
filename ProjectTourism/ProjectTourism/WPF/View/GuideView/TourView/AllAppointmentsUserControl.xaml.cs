using System.Windows;
using System.Windows.Controls;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.GuideViewModel;
using ProjectTourism.Utilities;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class AllAppointmentsUserControl : UserControl
    {
        public AllAppointmentsUserControlVM ViewModel { get; set; }
        public AllAppointmentsUserControl(string username)
        {
            InitializeComponent();
            ViewModel = new AllAppointmentsUserControlVM(username);
            DataContext = ViewModel;
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            AllAppsLabel.Visibility = Visibility.Hidden;
            TabControl.Visibility = Visibility.Hidden;
            ContentArea.Content = new ReviewsUserControl(ViewModel.SelectedAppointment);
        }
    }
}
