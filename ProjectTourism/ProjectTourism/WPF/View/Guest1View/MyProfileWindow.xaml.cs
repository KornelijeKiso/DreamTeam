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
    /// Interaction logic for MyProfileWindow.xaml
    /// </summary>
    public partial class MyProfileWindow : Window
    {
        public Guest1VM Guest1VM { get; set; }
        //public float AverageGrade { get; set; }
        public MyProfileWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1VM = new Guest1VM(username);
            //AverageGrade = Guest1VM.CalculateGrade();
        }
    }
}
