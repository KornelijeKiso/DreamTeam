using System.Runtime.CompilerServices;
using ProjectTourism.DTO;
using System.Windows.Input;
using ProjectTourism.Utilities;
using System.ComponentModel;

namespace ProjectTourism.WPF.ViewModel.GuideViewModels
{
    public class ImageViewerUserControlVM: INotifyPropertyChanged
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
        public ImageViewerUserControlVM(TourDTO tour)
        {
            Tour = tour;
            i = 0;
            if (Tour.Pictures != null)
                Picture = Tour.Pictures[i];
        }
        private void Left_Click(object parameter)
        {
            if (i > 0) i--;
            else i = Tour.Pictures.Length - 1;
            Picture = Tour.Pictures[i];
        }
        private void Right_Click(object parameter)
        {
            if (i < Tour.Pictures.Length - 1) i++;
            else i = 0;
            Picture = Tour.Pictures[i];
        }
        public ICommand Right_ClickCommand
        {
            get => new RelayCommand(Right_Click);
        }
        public ICommand Left_ClickCommand
        {
            get => new RelayCommand(Left_Click);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
