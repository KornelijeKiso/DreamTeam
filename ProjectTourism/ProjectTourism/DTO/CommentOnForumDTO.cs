using ProjectTourism.Domain.Model;
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
    public class CommentOnForumDTO : INotifyPropertyChanged
    {
        private CommentOnForum _commentOnForum;
        public CommentOnForumDTO(CommentOnForum comment)
        {
            _commentOnForum = comment;
        }
        public CommentOnForum GetCommentOnForum()
        {
            return _commentOnForum;
        }
        public int Id
        {
            get => _commentOnForum.Id;
            set
            {
                if (value != _commentOnForum.Id)
                {
                    _commentOnForum.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ForumId
        {
            get => _commentOnForum.ForumId;
            set
            {
                if (value != _commentOnForum.ForumId)
                {
                    _commentOnForum.ForumId = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Reports
        {
            get => _commentOnForum.Reports;
            set
            {
                if (value != _commentOnForum.Reports)
                {
                    _commentOnForum.Reports = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Text
        {
            get => _commentOnForum.Text;
            set
            {
                if (value != _commentOnForum.Text)
                {
                    _commentOnForum.Text = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Username
        {
            get => _commentOnForum.Username;
            set
            {
                if (value != _commentOnForum.Username)
                {
                    _commentOnForum.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsByOwner
        {
            get => _commentOnForum.IsByOwner;
            set
            {
                if (value != _commentOnForum.IsByOwner)
                {
                    _commentOnForum.IsByOwner = value;
                    OnPropertyChanged();
                }
            }
        }
        public ForumDTO Forum
        {
            get => new ForumDTO(_commentOnForum.Forum);
            set
            {
                if (value.GetForum() != _commentOnForum.Forum)
                {
                    _commentOnForum.Forum = value.GetForum();
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Published
        {
            get => _commentOnForum.Published;
            set
            {
                if (value != _commentOnForum.Published)
                {
                    _commentOnForum.Published = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool MaybeInvalid
        {
            get {
                if (!IsByOwner)
                {
                    try {
                        return (new ReservationService().GetAll().Where(r => r.Guest1Username.Equals(Username) && r.EndDate <= DateOnly.FromDateTime(Published) &&
                            new AccommodationService().GetAll().Where(a => a.LocationId == Forum.LocationId && r.AccommodationId == a.Id).Count() > 0).Count() > 0);

                    }
                    catch(Exception ex)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
