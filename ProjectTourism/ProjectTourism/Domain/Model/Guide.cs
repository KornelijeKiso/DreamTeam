using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Guide : Serializable, INotifyPropertyChanged
    {
        public bool? IsSuperGuide;
        public bool HasTourStarted;
        public string? Username;
        public string? Name;
        public string? Surname;
        public string? Biography;
        public string? Language;
        
        public Guide()
        {
            IsSuperGuide = false;
            Username = "";
            Name = "";
            Surname = "";
            Biography = "";
            Language = "";
            HasTourStarted = false;
        }
        public Guide(string username, string name, string surname, string biography, string language)
        {
            IsSuperGuide = false;
            Username = Username;
            Name = name;
            Surname = surname;
            Biography = biography;
            Language = language;
            HasTourStarted = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string?[] ToCSV()
        {
            string?[] csvValues =
            {
                Username, Name, Surname, Biography, Language, HasTourStarted.ToString()
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
            HasTourStarted = bool.Parse(values[5]);
        }
    }
}
