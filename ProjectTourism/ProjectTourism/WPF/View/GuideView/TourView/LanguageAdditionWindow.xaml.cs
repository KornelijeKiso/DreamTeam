using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
