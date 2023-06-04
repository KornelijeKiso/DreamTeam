using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for PictureDisplayUserControl.xaml
    /// </summary>
    public partial class PictureDisplayUserControl : UserControl, INotifyPropertyChanged
    {
        public TourDTO Tour { get; set; }
        private int _i;
        public int i
        {
            get => _i;
            set
            {
                if (value != _i)
                {
                    _i = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Picture;
        public string Picture
        {
            get => _Picture;
            set
            {
                if (value != _Picture)
                {
                    _Picture = value;
                    OnPropertyChanged();
                }
            }
        }
        public PictureDisplayUserControl(TourDTO tour)
        {
            InitializeComponent();
            DataContext = this;
            Tour = tour;
            i = 0;
            if (Tour.Pictures != null)
                Picture = Tour.Pictures[i];
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void HidePictureDisplay()
        {
            BlackBackground.Visibility = Visibility.Hidden;
        }
        private void BlackBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseOverImageBorder(e))
            {
                HidePictureDisplay();
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
            HidePictureDisplay();
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            if (i > 0) i--;
            else i = Tour.Pictures.Length - 1;
            Picture = Tour.Pictures[i];
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            if (i < Tour.Pictures.Length - 1) i++;
            else i = 0;
            Picture = Tour.Pictures[i];
        }
    }
}
