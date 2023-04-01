using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TrackToursWindow.xaml
    /// </summary>
    public partial class ViewAllAppointmentsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Guide Guide { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public GuideController GuideController { get; set; }

        public ViewAllAppointmentsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            GuideController = new GuideController();
            Guide = GuideController.GetOne(username);
            Tours = new ObservableCollection<Tour>(GuideController.GetGuidesTours(username));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {

        }
    }
}
