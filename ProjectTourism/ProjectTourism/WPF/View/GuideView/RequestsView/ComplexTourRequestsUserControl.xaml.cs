using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.Model;

namespace ProjectTourism.WPF.View.GuideView.RequestsView
{
    public partial class ComplexTourRequestsUserControl : UserControl, INotifyPropertyChanged
    {
        public GuideDTO Guide { get; set; }
        public ObservableCollection<ComplexTourDTO> ComplexTourRequests { get; set; }
        public ComplexTourDTO SelectedComplexTour { get; set; }
        public ComplexTourRequestsUserControl(String username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideDTO(username);
            ComplexTourRequests = Guide.ComplexTours;
        }

        private void AcceptComplexTourPart_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void InfoComplexTourPart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ComplexTourPartInfoUserControl complexTourPartInfoUserControl = new ComplexTourPartInfoUserControl(SelectedComplexTour);
            ContentArea.Content = complexTourPartInfoUserControl;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
