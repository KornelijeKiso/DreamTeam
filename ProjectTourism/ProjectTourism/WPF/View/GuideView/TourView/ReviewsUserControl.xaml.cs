using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.GuideViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class ReviewsUserControl : UserControl
    {
        public ReviewsUserControl(TourAppointmentDTO tourApp)
        {
            InitializeComponent();
            DataContext = new ReviewsUserControlVM(tourApp);
        }
    }
}
