using ProjectTourism.Model;
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

namespace ProjectTourism.WPF.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for GradeTicketWindow.xaml
    /// </summary>
    public partial class GradeTicketWindow : Window
    {
        public Guest2VM Guest2 { get; set; }
        public TicketGradeVM TicketGrade { get; set; }
        public GradeTicketWindow(TicketVM ticket, Guest2VM guest2)
        {
            InitializeComponent();
            DataContext = this;
            Guest2 = guest2;
            TicketGrade = new TicketGradeVM(new TicketGrade()); 
            TicketGrade.TicketId = ticket.Id;
            TicketGrade.Ticket = ticket;
        }

        private void GradeTicketClick(object sender, RoutedEventArgs e)
        {
            GradeAllCategories();
            foreach (var category in ProjectTourism.Model.TicketGrade.CategoryNames)
            {
                if (TicketGrade.Grades[category] == 0)
                {
                    MessageBox.Show("You have to grade each of listed categories.");
                    return;
                }
            }
            Guest2.GradeATicket(TicketGrade);
            Close();
        }

        private void GradeAllCategories()
        {
            GradeGuideKnoweledge();
            GradeGuideLanguage();
            GradeInteresting();
        }

        private void GradeGuideKnoweledge()
        {
            foreach (RadioButton radioButton in Knoweledge.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    TicketGrade.Grades["GuidesKnowledge"] = Convert.ToInt32(radioButton.Content);
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
                    TicketGrade.Grades["GuidesLanguage"] = Convert.ToInt32(radioButton.Content);
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
