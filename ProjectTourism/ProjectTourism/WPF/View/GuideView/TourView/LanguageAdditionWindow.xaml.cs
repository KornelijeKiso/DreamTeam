using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class LanguageAdditionWindow : Window, INotifyPropertyChanged
    {
        public bool LanguageAdded { get; set; }
        public ObservableCollection<string> ObserverLanguages { get; set; }
        public LanguageAdditionWindow(ObservableCollection<string> observerLanguages)
        {
            InitializeComponent();
            DataContext = this;
            ObserverLanguages = new ObservableCollection<string>();
            ObserverLanguages = observerLanguages;
        }
        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            ObserverLanguages.Add(LanguageTextBox.Text);
            LanguageAdded = true;
            Close();
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
