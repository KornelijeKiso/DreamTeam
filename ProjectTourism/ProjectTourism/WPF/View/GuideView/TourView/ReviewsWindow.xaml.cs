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
using System.Windows.Shapes;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.WPF.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : Window
    {
        public TicketGradeVM SelectedTicketGrade { get; set; }
        public ObservableCollection<TicketGradeVM> Tickets { get; set; }
        public TicketGradeService TicketGradeService { get; set; }
        public ReviewsWindow()
        {
            InitializeComponent();
            DataContext = this;
            TicketGradeService = new TicketGradeService(new TicketGradeRepository());
            List<TicketGradeVM> tickets = TicketGradeService.GetAll();
            Tickets = new ObservableCollection<TicketGradeVM>(tickets);
        }
    }
}
