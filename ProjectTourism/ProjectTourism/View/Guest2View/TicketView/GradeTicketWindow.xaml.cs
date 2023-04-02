using ProjectTourism.Controller;
using ProjectTourism.Model;
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

namespace ProjectTourism.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for GradeTicketWindow.xaml
    /// </summary>
    public partial class GradeTicketWindow : Window
    {
        public TicketGradeController TicketGradeController { get; set; }
        public List<TicketGrade> ticketGrades { get; set; }
        public TicketGrade TicketGrade { get; set; }
        public TicketController TicketController { get; set; }
        bool Graded;        
        public GradeTicketWindow(int ticketId)
        {
            InitializeComponent();
            DataContext = this;
            TicketGradeController = new TicketGradeController();
            ticketGrades = TicketGradeController.GetAll();
            TicketGrade = new TicketGrade();
            TicketController = new TicketController();

            TicketGrade.TicketId = ticketId;
        }

        private void GradeTicketClick(object sender, RoutedEventArgs e)
        {
            GradeGuideKnoweledge();
            GradeGuideLanguage();
            GradeInteresting();
            TicketGradeController.Add(TicketGrade);
            Graded = true;
            Close();
            }

        private void GradeGuideKnoweledge()
        {
            foreach (RadioButton radioButton in Knoweledge.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    TicketGrade.Grades["Guide's knoweledge"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }
        private void GradeGuideLanguage()
        {
            foreach (RadioButton radioButton in Language.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    TicketGrade.Grades["Guide's language"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }
        private void GradeInteresting()
        {
            foreach (RadioButton radioButton in Interesting.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    TicketGrade.Grades["Interesting"] = Convert.ToInt32(radioButton.Content);
                    break;
                }
            }
        }
    }
}
