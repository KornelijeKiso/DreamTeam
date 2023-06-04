using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Runtime.CompilerServices;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.Localization;
using ProjectTourism.DTO;
using ProjectTourism.PDF.GuidePDF;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class AllAppointmentsUserControl : UserControl, INotifyPropertyChanged
    {
        public TourAppointmentDTO SelectedAppointment { get; set; }
        public GuideDTO Guide { get; set; }
        

        public AllAppointmentsUserControl(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideDTO(username);
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            AllAppsLabel.Visibility = Visibility.Hidden;
            TabControl.Visibility = Visibility.Hidden;
            ContentArea.Content = new ReviewsUserControl(SelectedAppointment);
        }
        private void PdfReportButton_Click(object sender, RoutedEventArgs e)
        {
            Guide.Timer.Stop();
            GuestsOnATourPDFGenerator generatePDFDocument = new GuestsOnATourPDFGenerator(SelectedAppointment);
            MessageBox.Show(GetLocalizedErrorMessage("PDFLocation") + " ../../PDF/GuidePDFs\n\n" +
                GetLocalizedErrorMessage("PDFLocation2") + SelectedAppointment.Id.ToString() + ".pdf");
            Guide.SetTimer();
        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Guide.Timer.Stop();
            string name = new string(SelectedAppointment.Tour.Name);
            MessageBoxResult result = MessageBox.Show(GetLocalizedErrorMessage("CancelAppQuestion"), GetLocalizedErrorMessage("DeleteApp"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Guide.CancelAppointment(SelectedAppointment);
                MessageBox.Show(name + GetLocalizedErrorMessage("SucecssfulDeletation"));
                Guide.CanceledApps.Add(SelectedAppointment);
                Guide.ReadyApps.Remove(SelectedAppointment);
            }
            Guide.SetTimer();
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
