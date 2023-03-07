using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Guide: Serializable, INotifyPropertyChanged
    {
        private string _Username;
        public string Username
        {
            get => _Username;
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Surname;
        public string Surname
        {
            get => _Surname;
            set
            {
                if(_Surname != value)
                {
                    _Surname = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Biography;
        public string Biography
        {
            get => _Biography;
            set
            {
                if (_Biography != value)
                {
                    _Biography = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Language;

        public Guide()
        {
            Username = "";
            Name = "";
            Surname = "";
            Biography = "";
            Language = "";
        }
        public Guide(string username, string name, string surname, string biography, string language)
        {
            Username = Username;
            Name = name;
            Surname = surname;
            Biography = biography;
            Language = language;
        }

        public string Language
        {
            get=> _Language;
            set
            {
                if (_Language != value)
                {
                    _Language= value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Username, Name, Surname, Biography, Language
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Username = values[0];
            Name = values[1];
            Surname = values[2];
            Biography = values[3];
            Language = values[4];
        }
    }
}
