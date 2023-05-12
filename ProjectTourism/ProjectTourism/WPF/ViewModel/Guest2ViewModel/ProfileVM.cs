using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class ProfileVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }

        public ProfileVM() { }
        public ProfileVM(Guest2VM guest2)
        {
            Guest2 = guest2;

        }
    }
}
