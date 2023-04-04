using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
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

namespace ProjectTourism.View.OwnerView
{
    /// <summary>
    /// Interaction logic for Guest1ReviewWindow.xaml
    /// </summary>
    public partial class Guest1ReviewWindow : Window, INotifyPropertyChanged
    {
        public AccommodationGradeVM Review { get; set; }
        public int Hospitality { get; set; }
        public int Cleanness { get; set; }
        public int Location { get; set; }
        public int PriceQuality { get; set; }
        public int Comfort { get; set; }
        private int _i;
        public int i
        {
            get => _i;
            set
            {
                if(value != _i)
                {
                    _i = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Picture;
        public string Picture
        {
            get => _Picture;
            set
            {
                if(value != _Picture)
                {
                    _Picture = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Average;
        public string Average
        {
            get => _Average;
            set
            {
                if (value != _Average)
                {
                    _Average = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guest1ReviewWindow(AccommodationGradeVM review)
        {
            InitializeComponent();
            DataContext = this;
            Review = review;
            Hospitality = Review.Grades["Hospitality"];
            Cleanness = Review.Grades["Cleanness"];
            Location = Review.Grades["Location"];
            Comfort = Review.Grades["Comfort"];
            PriceQuality = Review.Grades["Price and quality ratio"];
            i = 0;
            Picture = Review.Pictures[i];
            Average = String.Format("{0:0.0}", Review.AverageGrade);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void BackwardsClick(object sender, RoutedEventArgs e)
        {
            if (i > 0)
            {
                i--;
            }
            else
            {
                i = Review.Pictures.Length - 1;
            }
            Picture = Review.Pictures[i];
        }
        public void ForwardClick(object sender, RoutedEventArgs e)
        {
            if (i < Review.Pictures.Length-1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
            Picture = Review.Pictures[i];
        }
    }
}
