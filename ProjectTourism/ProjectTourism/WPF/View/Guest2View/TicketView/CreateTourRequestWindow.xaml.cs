using System;
using System.Collections.Generic;
using System.Linq;
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
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for CreateTourRequestWindow.xaml
    /// </summary>
    public partial class CreateTourRequestWindow : Window, INotifyPropertyChanged
    {
        public Guest2VM Guest2 { get; set; }
        public List<string> LanguageList { get; set; }
        public RequestVM TourRequest { get; set; }


        public CreateTourRequestWindow(Guest2VM guest2)
        {
            InitializeComponent();
            DataContext = this;

            Guest2 = guest2;
            SetTourRequest();
            LanguageList = new List<string> { "English", "Spanish", "French", "German", "Italian", "Portuguese", "Russian", "Japanese", "Korean", "Chinese", "Dutch", "Swedish", "Norwegian", "Danish", "Finnish", "Turkish", "Greek", "Polish", "Arabic", "Hebrew", };
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

        private void SetTourRequest()
        {
            TourRequest = new RequestVM(new Request());
            TourRequest.Guest2Username = Guest2.Username;
            TourRequest.CreationDateTime = DateTime.Now;
            TourRequest.Location = new LocationVM(new Location()); //TourRequest.Location.Country, TourRequest.Location.City
        }

        private void CreateTourRequest(object sender, RoutedEventArgs e)
        {
            if (/*TourRequest.IsValid*/ TourRequest != null)
            {
                Guest2.CreateTourRequest(TourRequest);
                Close();
            }
            else
                MessageBox.Show("Tour Request can't be made because the fields were not entered correctly.");
        }
    }
}
