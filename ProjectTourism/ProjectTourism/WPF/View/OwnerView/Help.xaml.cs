using ProjectTourism.DTO;
using ProjectTourism.Services;
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

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : UserControl
    {
        public OwnerDTO Owner { get; set; }
        public Help(string username)
        {
            InitializeComponent();
            DataContext = this;
            Owner = new OwnerDTO(username);
            if (!Owner.HelpOn) but.Content = "Off";
        }

        public void SwitchHelp(object sender, RoutedEventArgs e)
        {
            if(Owner.HelpOn) 
            { 
                Owner.HelpOn = false;
                but.Content = "Off";
                new OwnerService().TurnHelpOff(Owner.GetOwner());
            }
            else
            {
                Owner.HelpOn = true;
                but.Content = "On";
                new OwnerService().TurnHelpOn(Owner.GetOwner());

            }
        }
    }
}
