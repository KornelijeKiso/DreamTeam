using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class RenovationRecommendationDTO : INotifyPropertyChanged
    {
        private RenovationRecommendation _renovationRecommendation;
        public RenovationRecommendationDTO(RenovationRecommendation renovationRecommendation)
        {
            _renovationRecommendation = renovationRecommendation;
        }
        public RenovationRecommendation GetRenovationRecommendation()
        {
            return _renovationRecommendation;
        }
        public int Id
        {
            get => _renovationRecommendation.Id;
            set
            {
                if (value != _renovationRecommendation.Id)
                {
                    _renovationRecommendation.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int AccommodationGradeId
        {
            get => _renovationRecommendation.AccommodationGradeId;
            set
            {
                if (value != _renovationRecommendation.AccommodationGradeId)
                {
                    _renovationRecommendation.AccommodationGradeId = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Comment
        {
            get => _renovationRecommendation.Comment;
            set
            {
                if (value != _renovationRecommendation.Comment)
                {
                    _renovationRecommendation.Comment = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Level
        {
            get => _renovationRecommendation.Level;
            set
            {
                if (value != _renovationRecommendation.Level)
                {
                    _renovationRecommendation.Level = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
