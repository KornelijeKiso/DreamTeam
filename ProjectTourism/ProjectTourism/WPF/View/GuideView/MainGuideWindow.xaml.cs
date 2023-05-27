using System.Windows;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.GuideViewModels;

namespace ProjectTourism.View.GuideView
{
    public partial class MainGuideWindow : Window
    {
        public MainGuideWindow(string username)
        {
            InitializeComponent();
            GuideDTO Guide = new GuideDTO(username);
            DataContext = new MainGuideWindowVM(Guide);
        }
    }
}
