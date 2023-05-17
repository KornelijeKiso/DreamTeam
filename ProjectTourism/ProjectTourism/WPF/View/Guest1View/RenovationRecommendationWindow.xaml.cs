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

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for RenovationRecommendationWindow.xaml
    /// </summary>
    public partial class RenovationRecommendationWindow : Window
    {
        public RenovationRecommendationVM renovationRecommendationVM { get; set; } 
        public RenovationRecommendationWindow()
        {
            InitializeComponent();
            renovationRecommendationVM = new RenovationRecommendationVM(new RenovationRecommendation());
        }

        private void RegisterLevel()
        {
            foreach (RadioButton radioButton in Level.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    if (radioButton.Name.Equals("l1")) 
                        renovationRecommendationVM.Level = 1;
                    if (radioButton.Name.Equals("l2"))
                        renovationRecommendationVM.Level = 2;
                    if (radioButton.Name.Equals("l3"))
                        renovationRecommendationVM.Level = 3;
                    if (radioButton.Name.Equals("l4"))
                        renovationRecommendationVM.Level = 4;
                    if (radioButton.Name.Equals("l5"))
                        renovationRecommendationVM.Level = 5; 
                    break;
                }
            }
        }
        private void SubmitRecommendationClick(object sender, RoutedEventArgs e)
        {
            RegisterLevel();
            
            Close();
        }

    }
}
