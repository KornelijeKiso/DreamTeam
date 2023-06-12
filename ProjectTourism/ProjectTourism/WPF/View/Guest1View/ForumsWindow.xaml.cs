using ProjectTourism.DTO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for ForumsWindow.xaml
    /// </summary>
    public partial class ForumsWindow : Window
    {
        public Guest1DTO Guest1 { get; set; }
        public ForumsWindow(string username)
        {
            Guest1 = new Guest1DTO(username);

            InitializeComponent();
        }
    }
}
