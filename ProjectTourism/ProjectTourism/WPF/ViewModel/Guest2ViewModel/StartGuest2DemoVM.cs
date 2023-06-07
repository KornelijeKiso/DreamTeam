using System.Windows.Controls;
using System.Windows.Input;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class StartGuest2DemoVM
    {
        public bool On { get; set; }
        public UserControl PopUp { get; set; }

        public ICommand GoCommand { get; set; }
        private void GoAction(object obj) 
        {
            On = true;
            PopUp.Visibility = System.Windows.Visibility.Hidden;
        }
        public ICommand CancelCommand { get; set; }
        private void CancelAction(object obj)
        {
            On = false;
            PopUp.Visibility = System.Windows.Visibility.Hidden;
        }
        
        public StartGuest2DemoVM(UserControl control)
        {
            this.On = false;
            this.PopUp = control;
            GoCommand = new RelayCommand(GoAction);
            CancelCommand = new RelayCommand(CancelAction);
        }
    }
}
