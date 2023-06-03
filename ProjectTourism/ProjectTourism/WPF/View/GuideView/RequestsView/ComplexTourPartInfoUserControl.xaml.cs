using System.Collections.ObjectModel;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.View.TourView;

namespace ProjectTourism.WPF.View.GuideView.RequestsView
{
    public partial class ComplexTourPartInfoUserControl : UserControl
    {
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }
        public TourRequestDTO SelectedTourRequest { get; set; }

        public GuideDTO Guide { get; set; }
        public ComplexTourPartInfoUserControl(ComplexTourDTO complexRequest, GuideDTO guide)
        {
            InitializeComponent();
            DataContext = this;
            TourRequests = new ObservableCollection<TourRequestDTO>(complexRequest.TourRequests);
            Guide = guide;
        }
        private void AcceptComplexTourPart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AcceptedRequestUserControl acceptedRequestUserControl = new AcceptedRequestUserControl(Guide, SelectedTourRequest);
            ContentArea.Content = acceptedRequestUserControl;
        }
    }
}
