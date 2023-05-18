using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class AccommodationStatisticsDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AccommodationStatisticsDTO(AccommodationDTO accommodation, int year)
        {
            Year = year;
            Month = -1;
            StatsByMonths = new ObservableCollection<AccommodationStatisticsDTO>();
            CalculateStatistics(accommodation);
            CalculateOccupancy(accommodation);

        }
        public AccommodationStatisticsDTO(int year)
        {
            Year = year;
            Month = -1;
            Occupancy = 0;
            Reservations = 0;
        }

        public AccommodationStatisticsDTO(int year, int month)
        {
            Year = year;
            Month = month;
        }
        private int _Year;
        public int Year
        {
            get => _Year;
            set
            {
                if (value != _Year)
                {
                    _Year = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _Month;
        public int Month
        {
            get => _Month;
            set
            {
                if (value != _Month)
                {
                    _Month = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _Reservations;
        public int Reservations
        {
            get => _Reservations;
            set
            {
                if (value != _Reservations)
                {
                    _Reservations = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _PostponedReservations;
        public int PostponedReservations
        {
            get => _PostponedReservations;
            set
            {
                if (value != _PostponedReservations)
                {
                    _PostponedReservations = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _CanceledReservations;
        public int CanceledReservations
        {
            get => _CanceledReservations;
            set
            {
                if (value != _CanceledReservations)
                {
                    _CanceledReservations = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _RenovationReccommendations;
        public int RenovationReccommendations
        {
            get => _RenovationReccommendations;
            set
            {
                if (value != _RenovationReccommendations)
                {
                    _RenovationReccommendations = value;
                    OnPropertyChanged();
                }
            }
        }

        
        public void CalculateStatistics(AccommodationDTO accommodation)
        {
            Reservations = accommodation.Reservations.Where(r => r.StartDate.Year == Year).Count();
            PostponedReservations = accommodation.Reservations.Where(r => new PostponeRequestService().GetOneAcceptedByReservation(r.Id) != null && r.StartDate.Year == Year).Count();
            CanceledReservations = new CanceledReservationService().GetAllByAccommodation(accommodation.Id).Where(c => c.StartDate.Year == Year).Count();
            RenovationReccommendations = accommodation.Reservations.Where(r => r.StartDate.Year == Year && r.AccommodationGrade.GetAccommodationGrade() != null).Where(r => r.AccommodationGrade.RenovationRecommended).Count(); ;
            if (Month == -1) CalculateStatisticsByMonths(accommodation, Year);
            CalculateOccupancy(accommodation);
        }
        public void CalculateStatisticsByMonths(AccommodationDTO accommodation, int Year)
        {
            for (int i = 1; i < 13; i++)
            {
                AccommodationStatisticsDTO accommodationStatisticsDTO = new AccommodationStatisticsDTO(Year, i);
                accommodationStatisticsDTO.Reservations = accommodation.Reservations.Where(r => r.StartDate.Year == accommodationStatisticsDTO.Year && r.StartDate.Month == i).Count();
                accommodationStatisticsDTO.PostponedReservations = accommodation.Reservations.Where(r => new PostponeRequestService().GetOneAcceptedByReservation(r.Id) != null && r.StartDate.Year == accommodationStatisticsDTO.Year && r.StartDate.Month == i).Count();
                accommodationStatisticsDTO.CanceledReservations = new CanceledReservationService().GetAllByAccommodation(accommodation.Id).Where(c => c.StartDate.Year == accommodationStatisticsDTO.Year && c.StartDate.Month == i).Count();
                accommodationStatisticsDTO.RenovationReccommendations = accommodation.Reservations.Where(r=> r.StartDate.Year == accommodationStatisticsDTO.Year && r.StartDate.Month == i && r.AccommodationGrade.GetAccommodationGrade()!=null).Where(r=>r.AccommodationGrade.RenovationRecommended).Count();
                accommodationStatisticsDTO.CalculateOccupancy(accommodation);
                StatsByMonths.Add(accommodationStatisticsDTO);
            }
        }
        public ObservableCollection<AccommodationStatisticsDTO> StatsByMonths { get; set; }

        public AccommodationStatisticsDTO BestMonth
        {
            get => StatsByMonths.ToList().MaxBy(s => s.Occupancy);
        }
        public string MonthString
        {
            get
            {
                switch (Month)
                {
                    case 1: return "January";
                    case 2: return "February";
                    case 3: return "March";
                    case 4: return "April";
                    case 5: return "May";
                    case 6: return "June";
                    case 7: return "July";
                    case 8: return "August";
                    case 9: return "September";
                    case 10: return "October";
                    case 11: return "November";
                    case 12: return "December";
                    default: return "";
                }
            }
        }
        public int Occupancy { get; set; }
        public void CalculateOccupancy(AccommodationDTO accommodation)
        {
            int countedDays = 0;
            int totalDays = 0;
            if (Month != -1)
            {
                DateOnly month = new DateOnly(Year, Month, 1);
                for (DateOnly m = month; m.Month == month.Month; m = m.AddDays(1))
                {
                    totalDays++;
                    if (accommodation.IsReserved(m)) countedDays++;
                }
                Occupancy = Convert.ToInt32(100 * (double)countedDays / (double)totalDays);
            }
            else
            {
                DateOnly year = new DateOnly(Year, 1, 1);
                for (DateOnly m = year; m.Year == year.Year; m = m.AddDays(1))
                {
                    totalDays++;
                    if (accommodation.IsReserved(m)) countedDays++;
                }
                Occupancy = Convert.ToInt32(100 * (double)countedDays / (double)totalDays);
            }
        }
    }
}
