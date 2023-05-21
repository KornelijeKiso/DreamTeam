using ProjectTourism.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for GradeTicketWindow.xaml
    /// </summary>
    public partial class GradeTicketWindow : Window
    {
        public Guest2DTO Guest2 { get; set; }
        public TicketGradeDTO TicketGrade { get; set; }
        public GradeTicketWindow(TicketDTO ticket, Guest2DTO guest2)
        {
            InitializeComponent();
            DataContext = this;
            Guest2 = guest2;
            TicketGrade = new TicketGradeDTO(new TicketGrade()); 
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
