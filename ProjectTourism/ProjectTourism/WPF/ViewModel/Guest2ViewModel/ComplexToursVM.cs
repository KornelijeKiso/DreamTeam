using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class ComplexToursVM
    {
        public Guest2DTO Guest2 { get; set; }


        public ComplexToursVM() { }
        public ComplexToursVM(Guest2DTO guest2) 
        {
            Guest2 = guest2;
        }
    }
}
