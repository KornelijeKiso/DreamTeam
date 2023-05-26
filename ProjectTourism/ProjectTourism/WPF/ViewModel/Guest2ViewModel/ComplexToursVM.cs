using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectTourism.DTO;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.View.Guest2View.TicketView;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class ComplexToursVM
    {
        public Guest2DTO Guest2 { get; set; }
        public ObservableCollection<ComplexTourDTO> YourComplexTours { get; set; }
        public ComplexTourDTO SelectedComplexTour { get; set; }

        public ComplexToursVM() { }
        public ComplexToursVM(Guest2DTO guest2) 
        {
            Guest2 = guest2;
            YourComplexTours = Guest2.ComplexTours;
        }



        private ICommand _MakeComplexTourCommand;
        public ICommand MakeComplexTourCommand
        {
            get
            {
                return _MakeComplexTourCommand ?? (_MakeComplexTourCommand = new CommandHandler(() => MakeComplexTour_Click(), () => true));
            }
        }

        public void MakeComplexTour_Click()
        {
            CreateComplexTourWindow createComplexTourWindow = new CreateComplexTourWindow(Guest2);
            createComplexTourWindow.ShowDialog();
        }
    }
}
