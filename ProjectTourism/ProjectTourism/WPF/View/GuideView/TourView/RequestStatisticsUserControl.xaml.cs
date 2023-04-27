using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    public partial class RequestStatisticsUserControl : UserControl
    {
        public struct Stat
        {
            public int Year { get; set; }
            public int StatsForThatYear { get; set; }
        }
        public RequestVM Request { get; set; }
        public List<int> nizovi { get; set; } 
        public ObservableCollection<Stat> Stats { get; set; }

        public RequestStatisticsUserControl()
        {
            InitializeComponent();
            DataContext = this;

            Request = new RequestVM();
            Stats = new ObservableCollection<Stat>();

            foreach(var year in Request.Years())
            {
                Stat stat = new Stat();
                stat.Year = year;
                stat.StatsForThatYear = Request.StatForYear(year);
                Stats.Add(stat);
            }
        }
        private void MonthStatsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
