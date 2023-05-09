using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RejectingPostponeMessageWindow.xaml
    /// </summary>
    public partial class RejectingPostponeMessageWindow : Window
    {
        public bool Submited { get; set; }
        public string Message { get; set; }
        public RejectingPostponeMessageWindow()
        {
            InitializeComponent();
            Submited = false;
        }
        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            Submited = true;
            Message = textBox.Text;
            MessageBox.Show("You have successfully rejected postpone request.");
            Close();
        }
    }
}
