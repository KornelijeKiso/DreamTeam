using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for CreateComplexTourWindow.xaml
    /// </summary>
    public partial class CreateComplexTourWindow : Window, INotifyPropertyChanged
    {
        public List<string> LanguageList { get; set; }
        public Guest2DTO Guest2 { get; set; }
        private TourRequestDTO _NewTourRequestPart;
        public TourRequestDTO NewTourRequestPart 
        { 
            get => _NewTourRequestPart;
            set
            {
                if (value != _NewTourRequestPart)
                {
                    _NewTourRequestPart = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<TourRequestDTO> _TourRequests;
        public ObservableCollection<TourRequestDTO> TourRequests 
        {
            get => _TourRequests;
            set
            {
                if (value != _TourRequests)
                {
                    _TourRequests = value;
                    OnPropertyChanged();
                }
            }
        }
        public TourRequestDTO SelectedTourRequest { get; set; }
        public CreateComplexTourWindow(Guest2DTO guest2)
        {
            InitializeComponent();
            DataContext = this;
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };
            Guest2 = guest2;
            TourRequests = new ObservableCollection<TourRequestDTO>();
            //NewTourRequestPart = new TourRequestDTO(new TourRequest());
            //SelectedTourRequest = new TourRequestDTO(new TourRequest());
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Update()
        {
            throw new NotImplementedException();
        }
        private void EndDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // TO DO
        }

        private void StartDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // TO DO
        }



        private ICommand _AddTourRequestCommand;
        public ICommand AddTourRequestCommand
        {
            get
            {
                return _AddTourRequestCommand ?? (_AddTourRequestCommand = new CommandHandler(() => AddTourRequestPartClick(), () => CanAddTourRequest));
            }
        }
        private bool CanAddTourRequest
        {
            get
            {
                return (Guest2 != null /*&& NewTourRequestPart.IsValid*/);
            }
        }

        public void AddTourRequestPartClick()
        {
            TourRequests.Add(NewTourRequestPart);
            // TO DO -> clear text boxes, set up datePickers
            //NewTourRequestPart = new TourRequestDTO(new TourRequest());
        }

        private ICommand _MakeComplexTourCommand;
        public ICommand MakeComplexTourCommand
        {
            get
            {
                return _MakeComplexTourCommand ?? (_MakeComplexTourCommand = new CommandHandler(() => MakeComplexTour_Click(), () => CanMakeComplexTour));
            }
        }
        private bool CanMakeComplexTour
        {
            get
            {
                // TO DO
                return (Guest2 != null);
            }
        }

        public void MakeComplexTour_Click()
        {
            // TO DO -> save created Complex Tour
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
