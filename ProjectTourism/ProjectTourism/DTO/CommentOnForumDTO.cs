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
    public class CommentOnForumDTO : INotifyPropertyChanged
    {
        private CommentOnForum _commentOnForum;
        public CommentOnForumDTO(CommentOnForum comment)
        {
            _commentOnForum = comment;
            User = new UserDTO(new UserService().GetOne(comment.Username));
        }
        public CommentOnForumDTO()
        {
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
        public UserDTO User { get; set; }
        public bool IsByOwner
        {
            get => User.Type==USERTYPE.OWNER;
        }
        public bool IsByGuest
        {
            get => !IsByOwner;
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

        public bool Suspicious
        {
            get {
                if (!IsByOwner)
                {
                    try {
                        List<Reservation> reservations = new ReservationService().GetAll().Where(r => r.EndDate <= DateOnly.FromDateTime(Published) && r.Guest1Username.Equals(Username)).ToList();
                        foreach(var r in reservations)
                        {
                            r.Accommodation = new AccommodationService().GetOne(r.AccommodationId);
                            Location l = new LocationService().GetOne(r.Accommodation.LocationId);
                            if(l.City == Forum.Location.City && l.Country.Equals(Forum.Location.Country))
                            {
                                return false;
                            }
                        }
                        return true;

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
