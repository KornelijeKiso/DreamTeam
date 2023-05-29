using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ProjectTourism.DTO;
using ProjectTourism.Model;

namespace ProjectTourism.WPF.View.GuideView.RequestsView
{
    public partial class ComplexToursUserControl : UserControl
    {
        public GuideDTO Guide { get; set; }
        public ObservableCollection<ComplexTourDTO> ComplexTours { get; set; }
        public ComplexTourDTO SelectedComplexTour { get; set; }
        public ComplexToursUserControl(String username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideDTO(username);
            ComplexTours = Guide.ComplexTours;
        }
    }
}
