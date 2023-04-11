using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class PostponeRequestVM : INotifyPropertyChanged
    {
        private PostponeRequest _postponeRequest;

        public PostponeRequestVM(PostponeRequest postponeRequest)
        {
            _postponeRequest = postponeRequest;
        }
        public PostponeRequest GetPostponeRequest()
        {
            return _postponeRequest;
        }
        public int Id
        {
            get => _postponeRequest.Id;
            set
            {
                if (value != _postponeRequest.Id)
                {
                    _postponeRequest.Id = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReservationVM Reservation
        {
            get => new ReservationVM(_postponeRequest.Reservation);
        }
        public int ReservationId
        {
            get => _postponeRequest.ReservationId;
            set
            {
                if (value != _postponeRequest.ReservationId)
                {
                    _postponeRequest.ReservationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly NewStartDate
        {
            get => _postponeRequest.NewStartDate;
            set
            {
                if (value != _postponeRequest.NewStartDate)
                {
                    _postponeRequest.NewStartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly NewEndDate
        {
            get => _postponeRequest.NewEndDate;
            set
            {
                if (value != _postponeRequest.NewEndDate)
                {
                    _postponeRequest.NewEndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Accepted
        {
            get => _postponeRequest.Accepted;
            set
            {
                if (value != _postponeRequest.Accepted)
                {
                    _postponeRequest.Accepted = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Rejected
        {
            get => _postponeRequest.Rejected;
            set
            {
                if (value != _postponeRequest.Rejected)
                {
                    _postponeRequest.Rejected = value;
                    OnPropertyChanged();
                }
            }
        }
        public string AdditionalComment
        {
            get => _postponeRequest.AdditionalComment;
            set
            {
                if (value != _postponeRequest.AdditionalComment)
                {
                    _postponeRequest.AdditionalComment = value;
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
