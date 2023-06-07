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
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

namespace ProjectTourism.WPF.View.Guest2View.UserControls
{
    /// <summary>
    /// Interaction logic for ComplexTourPartsUserControl.xaml
    /// </summary>
    public partial class ComplexTourPartsUserControl : UserControl
    {
        public ComplexTourPartsUserControl()
        {
            InitializeComponent();
        }

        public ComplexTourPartsUserControl(ComplexTourDTO complexTour)
        {
            InitializeComponent();
            DataContext = new ComplexTourPartsVM(complexTour);
        }
    }
}
