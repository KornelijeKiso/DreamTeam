using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProjectTourism.View.GuideView
{
    public partial class CreateGuideWindow : Window, INotifyPropertyChanged
    {
        public GuideDTO Guide { get; set; }
        public UserDTO UserVM { get; set; }
        public CreateGuideWindow(UserDTO userVM)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideDTO(new Guide(userVM.GetUser()));
            UserVM = userVM;
        }
        private void CreateGuideButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameSurnameNotNull())
            {
                UserVM.Add(UserVM);
                Guide.Add(Guide.GetGuide());
                Close();
            }
            else
                MessageBox.Show("You have not entered the data correctly.");
        }
        private bool NameSurnameNotNull()
        {
            return Guide.GetGuide().FirstName != null
               && Guide.GetGuide().LastName != null;
        }
        public void Update() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
