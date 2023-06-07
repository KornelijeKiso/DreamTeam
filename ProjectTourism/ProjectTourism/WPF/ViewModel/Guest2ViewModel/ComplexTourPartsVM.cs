using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class ComplexTourPartsVM : ViewModelBase
    {
        public ComplexTourDTO ComplexTour { get; set; }
        public TourRequestDTO SelectedTourRequest { get; set; }
        public ComplexTourPartsVM() { }

        public ComplexTourPartsVM(ComplexTourDTO complexTour) 
        {
            ComplexTour = complexTour;
        }
    }
}
