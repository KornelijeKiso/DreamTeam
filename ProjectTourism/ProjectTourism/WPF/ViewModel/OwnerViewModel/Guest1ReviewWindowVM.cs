using ProjectTourism.DTO;
using ProjectTourism.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class Guest1ReviewWindowVM:INotifyPropertyChanged
    {
        public ReservationDTO Reservation { get; set; }
        public int Hospitality { get; set; }
        public int Cleanness { get; set; }
        public int Location { get; set; }
        public int PriceQuality { get; set; }
        public int Comfort { get; set; }
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
        private string _Average;
        public string Average
        {
            get => _Average;
            set
            {
                if (value != _Average)
                {
                    _Average = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guest1ReviewWindowVM(ReservationDTO reservation)
        {
            Reservation = reservation;
            Hospitality = reservation.AccommodationGrade.Grades["Hospitality"];
            Cleanness = reservation.AccommodationGrade.Grades["Cleanness"];
            Location = reservation.AccommodationGrade.Grades["Location"];
            Comfort = reservation.AccommodationGrade.Grades["Comfort"];
            PriceQuality = reservation.AccommodationGrade.Grades["Price and quality ratio"];
            i = 0;
            Picture = reservation.AccommodationGrade.Pictures[i];
            Average = String.Format("{0:0.0}", reservation.AccommodationGrade.AverageGrade);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void BackwardsClick(object parameter)
        {
            if (i > 0) i--;
            else i = Reservation.AccommodationGrade.Pictures.Length - 1;
            Picture = Reservation.AccommodationGrade.Pictures[i];
        }
        public void ForwardClick(object parameter)
        {
            if (i < Reservation.AccommodationGrade.Pictures.Length - 1) i++;
            else i = 0;
            Picture = Reservation.AccommodationGrade.Pictures[i];
        }
        public ICommand ForwardClickCommand
        {
            get => new RelayCommand(ForwardClick);
        }
        public ICommand BackwardsClickCommand
        {
            get => new RelayCommand(BackwardsClick);
        }
    }
}
