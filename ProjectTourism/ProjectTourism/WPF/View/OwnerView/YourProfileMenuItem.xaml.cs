using ProjectTourism.WPF.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for YourProfileMenuItem.xaml
    /// </summary>
    public partial class YourProfileMenuItem : UserControl, INotifyPropertyChanged
    {
        public OwnerVM Owner { get; set; }
        private string _Average;
        public string Average
        {
            get => _Average;
            set
            {
                if (value != _Average)
                {
                    _Average = value;
                    OnPropertyChanged();
                }
            }
        }
        public YourProfileMenuItem(OwnerVM owner)
        {
            InitializeComponent();
            DataContext = this;
            Owner = owner;
            Average = owner.AverageGrade.ToString("0.0");
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
