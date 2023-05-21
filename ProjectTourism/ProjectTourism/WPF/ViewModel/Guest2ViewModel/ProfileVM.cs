using ProjectTourism.Utilities;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class ProfileVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }

        public ProfileVM() { }
        public ProfileVM(Guest2DTO guest2)
        {
            Guest2 = guest2;

        }
    }
}
