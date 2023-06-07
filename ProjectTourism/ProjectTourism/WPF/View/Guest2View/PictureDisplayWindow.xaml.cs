using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

namespace ProjectTourism.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for PictureDisplayWindow.xaml
    /// </summary>
    public partial class PictureDisplayWindow : Window
    {
        public PictureDisplayWindow() 
        {
            InitializeComponent();
        }
        public PictureDisplayWindow(TourDTO tour)
        {
            InitializeComponent();
            DataContext = new PictureDisplayVM(tour);
            
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
        
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
