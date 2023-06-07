using System.Windows.Input;
using ProjectTourism.DTO;
using ProjectTourism.Utilities;


namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class PictureDisplayVM : ViewModelBase
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

        public PictureDisplayVM() { }
        public PictureDisplayVM(TourDTO tour) 
        {
            Tour = tour;
            i = 0;
            if (Tour.Pictures != null)
                Picture = Tour.Pictures[i];
        }

        public ICommand RightClickCommand
        {
            get => new RelayCommand(RightClick);
        }
        private void RightClick(object sender)
        {
            if (i < Tour.Pictures.Length - 1) i++;
            else i = 0;
            Picture = Tour.Pictures[i];
        }
        public ICommand LeftClickCommand
        {
            get => new RelayCommand(LeftClick);
        }
        private void LeftClick(object sender)
        {
            if (i > 0) i--;
            else i = Tour.Pictures.Length - 1;
            Picture = Tour.Pictures[i];
        }
    }
}
