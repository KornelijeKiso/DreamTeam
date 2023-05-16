using System.Windows;
using ProjectTourism.WPF.ViewModel.GuideViewModels;

namespace ProjectTourism.View.GuideView
{
    public partial class MainGuideWindow : Window
    {
        public MainGuideWindow(string username)
        {
            InitializeComponent();
            DataContext = new MainGuideWindowVM(username);
        }
    }
}
