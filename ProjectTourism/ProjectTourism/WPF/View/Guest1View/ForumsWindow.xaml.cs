using ProjectTourism.DTO;
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

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for ForumsWindow.xaml
    /// </summary>
    public partial class ForumsWindow : Window, INotifyPropertyChanged
    {
        private ForumDTO _SelectedForum;
        public ForumDTO SelectedForum
        {
            get => _SelectedForum;
            set
            {
                if (value != _SelectedForum)
                {
                    _SelectedForum = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guest1DTO Guest1 { get; set; }
        //public ForumDTO SelectedForum { get; set; }
        public ForumsWindow(string username)
        {
            Guest1 = new Guest1DTO(username);
            if (Guest1.Forums.Any())
            {
                SelectedForum = Guest1.Forums.First();
            }
            int a = 1;
            InitializeComponent();
            DataContext = this;
        }

        public void AddNewForum(object sender, RoutedEventArgs e)
        {
            NewForumWindow nf = new NewForumWindow(Guest1.Forums, Guest1.Username);
            nf.ShowDialog();
            Guest1.Forums.Add(nf.noviforum);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
