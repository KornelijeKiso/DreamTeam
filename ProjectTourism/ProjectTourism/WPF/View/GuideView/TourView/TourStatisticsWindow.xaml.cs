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
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TourStatistics.xaml
    /// </summary>
    public partial class TourStatisticsWindow : UserControl
    {
        public ObservableCollection<TourVM> Tours { get; set; }
        public GuideService GuideService { get; set; }

        public TourStatisticsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            GuideService = new GuideService(new GuideRepository());
            List<TourVM> ToursVM = new List<TourVM>();
            foreach (var tour in GuideService.GetGuidesTours(username))
            {
                ToursVM.Add(tour);
            }
            Tours = new ObservableCollection<TourVM>(ToursVM);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {

        }
    }
}
