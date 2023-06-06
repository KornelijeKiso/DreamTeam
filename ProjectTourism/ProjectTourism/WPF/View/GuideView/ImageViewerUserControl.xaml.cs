using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.GuideViewModels;

namespace ProjectTourism.WPF.View.GuideView
{
    public partial class ImageViewerUserControl : UserControl
    {
        public ImageViewerUserControl(TourDTO tour)
        {
            InitializeComponent();
            DataContext = new ImageViewerUserControlVM(tour);
        }
        private void BlackBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseOverImageBorder(e))
            {
                BlackBackground.Visibility = Visibility.Hidden;
            }
        }
        private bool IsMouseOverImageBorder(MouseButtonEventArgs e)
        {
            var position = e.GetPosition(ImageBorder);
            var rect = new Rect(0, 0, ImageBorder.ActualWidth, ImageBorder.ActualHeight);
            return rect.Contains(position);
        }
    }
}
