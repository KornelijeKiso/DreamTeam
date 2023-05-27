using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
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
    public class ForumDTO : INotifyPropertyChanged
    {
        private Forum _forum;
        public ForumDTO(Forum forum)
        {
            _forum = forum;
        }
        public Forum GetForum()
        {
            return _forum;
        }
        public int Id
        {
            get => _forum.Id;
            set
            {
                if (value != _forum.Id)
                {
                    _forum.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int LocationId
        {
            get => _forum.LocationId;
            set
            {
                if (value != _forum.LocationId)
                {
                    _forum.LocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public LocationDTO Location
        {
            get => new LocationDTO(_forum.Location);
            set
            {
                if (value.GetLocation() != _forum.Location)
                {
                    _forum.Location = value.GetLocation();
                    OnPropertyChanged();
                }
            }
        }
        public DateTime OpenedOn
        {
            get => _forum.OpenedOn;
            set
            {
                if (value != _forum.OpenedOn)
                {
                    _forum.OpenedOn = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _IsVeryUseful;
        public bool IsVeryUseful
        {
            get => _IsVeryUseful=CheckIfVeryUseful();
            set
            {
                if(value!=_IsVeryUseful)
                {
                    _IsVeryUseful=value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CheckIfVeryUseful()
        {
            OnPropertyChanged();
            return CommentsByGuests >= 4 && CommentsByOwner >= 2;
        }

        public int CommentsByGuests
        {
            get => Comments.ToList().Where(c => !c.IsByOwner).Count();
        }
        private int _CommentsByOwner;
        public int CommentsByOwner
        {
            get => _CommentsByOwner = Comments.ToList().Where(c => c.IsByOwner).Count();
            set
            {
                if (value != _CommentsByOwner)
                {
                    _CommentsByOwner = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<CommentOnForumDTO> _Comments;
        public ObservableCollection<CommentOnForumDTO> Comments
        {
            get => _Comments;
            set
            {
                if (value != _Comments)
                {
                    _Comments = value;
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
