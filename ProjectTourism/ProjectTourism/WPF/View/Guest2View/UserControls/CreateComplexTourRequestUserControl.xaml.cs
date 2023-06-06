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
    /// Interaction logic for CreateComplexTourRequestUserControl.xaml
    /// </summary>
    public partial class CreateComplexTourRequestUserControl : UserControl
    {
        public CreateComplexTourRequestUserControl()
        {
            InitializeComponent();
        }

        public CreateComplexTourRequestUserControl(Guest2DTO guest2)
        {
            InitializeComponent();
            DataContext = new CreateComplexTourRequestVM(guest2);
        }
    }
}
