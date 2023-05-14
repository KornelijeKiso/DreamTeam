using ProjectTourism.Domain.Model;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class PostponeRequestDTO : INotifyPropertyChanged
    {
        private PostponeRequest _postponeRequest;

        public PostponeRequestDTO(PostponeRequest postponeRequest)
        {
            _postponeRequest = postponeRequest;
        }
        public PostponeRequest GetPostponeRequest()
        {
            return _postponeRequest;
        }

        public void Update()
        {
            PostponeRequestService postponeRequestService = new PostponeRequestService();
            postponeRequestService.Update(this.GetPostponeRequest());
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

        public ReservationDTO Reservation
        {
            get => new ReservationDTO(_postponeRequest.Reservation);
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
