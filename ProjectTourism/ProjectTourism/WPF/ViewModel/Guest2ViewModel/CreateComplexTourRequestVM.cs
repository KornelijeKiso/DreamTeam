using System.Windows;
using System.Windows.Input;
using ProjectTourism.Domain.Model;
using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.Utilities;


namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class CreateComplexTourRequestVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public ComplexTourDTO ComplexTour { get; set; }
        
        // Contents
        private object _Content;
        public object Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }

        private object _PartsContent;
        public object PartsContent
        {
            get { return _PartsContent; }
            set { _PartsContent = value; OnPropertyChanged(); }
        }
        private object _PartsFormContent;
        public object PartsFormContent
        {
            get { return _PartsFormContent; }
            set { _PartsFormContent = value; OnPropertyChanged(); }
        }

        public CreateComplexTourRequestVM() { }
        public CreateComplexTourRequestVM(Guest2DTO guest2)
        {
            Guest2 = guest2;
            ComplexTour = new ComplexTourDTO(new ComplexTour(Guest2.Username, "-1"));

            // Commands
            ContentCommand = new RelayCommand(ReturnToComplexTourRequests);
            PartsContentCommand = new RelayCommand(DisplayParts);
            PartsFormContentCommand = new RelayCommand(DisplayForm);

            PartsContentCommand.Execute(DisplayParts);
            PartsFormContentCommand.Execute(DisplayForm);
        }


        // Commands 
        private ICommand _MakeComplexTourCommand;
        public ICommand MakeComplexTourCommand
        {
            get
            {
                return _MakeComplexTourCommand ?? (_MakeComplexTourCommand = new CommandHandler(() => CreateComplexTour(), () => true));
            }
        }
        public void CreateComplexTour()
        {
            if (ComplexTour.IsValid)
            {
                if (ComplexTour.TourRequests.Count >= 2)
                {
                    Guest2.CreateComplexTour(ComplexTour);
                    MessageBox.Show("Complex Tour Request created! ");
                    Content = new ComplexToursVM(Guest2);
                }
                else
                    MessageBox.Show("Complex Tour Request must have at least 2 parts!");
            }
            else
            {
                MessageBox.Show("Complex Tour Request can't be made because the data were not entered correctly.");
            }
        }
        public ICommand ContentCommand { get; set; }
        private void ReturnToComplexTourRequests(object obj)
        {
            Content = new ComplexToursVM(Guest2);
        }
        public ICommand PartsContentCommand { get; set; }
        private void DisplayParts(object obj)
        {
            PartsContent = new ComplexTourPartsVM(ComplexTour);
        }
        public ICommand PartsFormContentCommand { get; set; }
        private void DisplayForm(object obj)
        {
            PartsFormContent = new CreateComplexTourPartVM(Guest2, ComplexTour);
        }
    }
}
