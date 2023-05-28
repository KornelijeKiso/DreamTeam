using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
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

namespace ProjectTourism.WPF.View.Guest2View.Menu
{
    /// <summary>
    /// Interaction logic for G2ComplexTours.xaml
    /// </summary>
    public partial class G2ComplexTours : UserControl
    {
        public G2ComplexTours()
        {
            InitializeComponent();
        }
        public G2ComplexTours(Guest2DTO guest2)
        {
            InitializeComponent();
            DataContext = new ComplexToursVM(guest2);
        }
    }
}
