using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class TicketsVM : ViewModelBase
    {
        public Guest2VM? Guest2 { get; set; }

        public void SetGuest2(Guest2VM guest2)
        {
            Guest2 = guest2;
        }
    }
}
