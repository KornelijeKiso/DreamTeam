using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.GuideView.RequestsView
{
    public partial class ComplexTourPartInfoUserControl : UserControl
    {
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }
        public TourRequestDTO SelectedTourRequest { get; set; }
        public ComplexTourPartInfoUserControl(ComplexTourDTO complexRequest)
        {
            InitializeComponent();
            DataContext = this;
            TourRequests = new ObservableCollection<TourRequestDTO>(complexRequest.TourRequests);
        }
        private void AcceptComplexTourPart_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
