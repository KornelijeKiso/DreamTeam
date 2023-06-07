using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.PDF.Guest1PDFs;
using ProjectTourism.WPF.ViewModel;
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

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for MyProfileWindow.xaml
    /// </summary>
    
    public partial class MyProfileWindow : UserControl
    {
        DateOnly From { get; set; }
        DateOnly To { get; set; }
        bool Canceled { get; set; }
        public Guest1DTO Guest1 { get; set; }
        //public float AverageGrade { get; set; }
        public MyProfileWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1 = new Guest1DTO(username);
            //AverageGrade = Guest1VM.CalculateGrade();
            //From = DateOnly.FromDateTime(DateTime.Now);
            //To = DateOnly.FromDateTime(DateTime.Now);
               
        }
        public void ReportClick(object sender, RoutedEventArgs e)
        {
            ReportPDF.Visibility = Visibility.Visible;
        }
        public void CloseReportClick(object sender, RoutedEventArgs e)
        {
            ReportPDF.Visibility = Visibility.Collapsed;
        }
        public void CloseReviewsClick(object sender, RoutedEventArgs e)
        {
            Reviews.Visibility = Visibility.Collapsed;
        }
        public void OpenReviewsClick(object sender, RoutedEventArgs e)
        {
            Reviews.Visibility = Visibility.Visible;
        }

        public void GeneratePDFClick(object sender, RoutedEventArgs e)
        {
            //From = DateOnly.FromDateTime((DateTime)StartDatePicker.SelectedDate);
            //To = DateOnly.FromDateTime((DateTime)EndDatePicker.SelectedDate);
            Canceled = ReportType.SelectedIndex == 0;

            ReportGenerator reportGenerator = new ReportGenerator(DateOnly.FromDateTime(DateTime.Now).AddYears(-2), DateOnly.FromDateTime(DateTime.Now), Guest1, Canceled);

        }

    }
}
