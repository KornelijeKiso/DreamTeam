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
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.GuideView.RequestsView
{
    /// <summary>
    /// Interaction logic for ComplexToursUserControl.xaml
    /// </summary>
    public partial class ComplexToursUserControl : UserControl
    {
        public GuideDTO Guide { get; set; }
        public ComplexToursUserControl(String username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideDTO(username);
        }
    }
}
