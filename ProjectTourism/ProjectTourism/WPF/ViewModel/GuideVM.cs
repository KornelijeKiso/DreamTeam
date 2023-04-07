using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.WPF.ViewModel
{
    public class GuideVM : INotifyPropertyChanged
    {
        private Guide _guide;
        public GuideVM(Guide guide)
        {
            _guide = guide;
        }

        public Guide GetGuide()
        {
            return _guide;
        }
        public bool? IsSuperGuide
        {
            get => _guide.IsSuperGuide;
            set
            {
                if (_guide.IsSuperGuide != value)
                {
                    _guide.IsSuperGuide = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool HasTourStarted
        {
            get => _guide.HasTourStarted;
            set
            {
                if (_guide.HasTourStarted != value)
                {
                    _guide.HasTourStarted = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Username
        {
            get => _guide.Username;
            set
            {
                if (_guide.Username != value)
                {
                    _guide.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Name
        {
            get => _guide.Name;
            set
            {
                if (_guide.Name != value)
                {
                    _guide.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Surname
        {
            get => _guide.Surname;
            set
            {
                if (_guide.Surname != value)
                {
                    _guide.Surname = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Biography
        {
            get => _guide.Biography;
            set
            {
                if (_guide.Biography != value)
                {
                    _guide.Biography = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Language
        {
            get => _guide.Language;
            set
            {
                if (_guide.Language != value)
                {
                    _guide.Language = value;
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
