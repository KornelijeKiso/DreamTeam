using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ProjectTourism.Model
{
    public class TicketGrade : Serializable, INotifyPropertyChanged
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _TicketId;
        public int TicketId
        {
            get => _TicketId;
            set
            {
                if (value != _TicketId)
                {
                    _TicketId = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private Ticket _Ticket;
        public Ticket Ticket
        {
            get => _Ticket;
            set
            {
                if (value != _Ticket)
                {
                    _Ticket = value;
                    OnPropertyChanged();
                }
            }
        }

        public static readonly string[] CategoryNames = { "Guide's knoweledge", "Guide's language", "Interesting" };

        private Dictionary<string, int> _Grades;
        public Dictionary<string, int> Grades
        {
            get => _Grades;
            set
            {
                if (value != _Grades)
                {
                    _Grades = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Comment;
        public string Comment
        {
            get => _Comment;
            set
            {
                if (value != _Comment)
                {
                    _Comment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _PictureURLs;
        public string? PictureURLs
        {
            get => _PictureURLs;
            set
            {
                _PictureURLs = value;
                OnPropertyChanged();
            }
        }

        private string[] _Pictures;
        public string[] Pictures
        {
            get => _Pictures;
            set
            {
                if (value != _Pictures)
                {
                    _Pictures = value;
                    OnPropertyChanged();
                }
            }
        }

        public TicketGrade()
        {
            Grades = new Dictionary<string, int>();
            foreach (var category in CategoryNames)
            {
                Grades.Add(category, 0);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Ticket FindTicket(int ticketId)
        {
            TicketDAO ticketDAO = new TicketDAO();
            Ticket ticket = ticketDAO.GetOne(ticketId);
            return ticket;
        }

        public string[] GetPictureURLsFromCSV()
        {
            string[] pictures = PictureURLs.Split(',');
            foreach (var picture in pictures)
            {
                picture.Trim();
            }
            return pictures;
        }

        public string[] ToCSV()
        {
            List<string> csv = new List<string>();
            csv.Add(Id.ToString());
            csv.Add(TicketId.ToString());
            foreach (var category in CategoryNames)
            {
                csv.Add(Grades[category].ToString());
            }
            csv.Add(Comment);
            csv.Add(PictureURLs);
            string[] csvValues = csv.ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TicketId = int.Parse(values[1]);
            Ticket = FindTicket(TicketId);
            for (int i = 2; i < 5; i++)
            {
                Grades[CategoryNames[i - 2]] = int.Parse(values[i]);
            }
            Comment = values[5];
            PictureURLs = values[6];
            Pictures = GetPictureURLsFromCSV();
        }
    }
}
