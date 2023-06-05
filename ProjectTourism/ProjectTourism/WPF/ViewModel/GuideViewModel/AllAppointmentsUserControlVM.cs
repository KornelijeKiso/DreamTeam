using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ProjectTourism.DTO;
using ProjectTourism.Localization;
using ProjectTourism.PDF.GuidePDF;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.ViewModel.GuideViewModel
{
    public class AllAppointmentsUserControlVM: ViewBase, INotifyPropertyChanged
    {
        public TourAppointmentDTO SelectedAppointment { get; set; }
        public GuideDTO Guide { get; set; }

        public AllAppointmentsUserControlVM(String username)
        {
            Guide = new GuideDTO(username);
        }
        private void PdfReportButton_Click(object parameter)
        {
            Guide.Timer.Stop();
            GuestsOnATourPDFGenerator generatePDFDocument = new GuestsOnATourPDFGenerator(SelectedAppointment);
            MessageBox.Show(GetLocalizedErrorMessage("PDFLocation") + " ../../PDF/GuidePDFs\n\n" +
                GetLocalizedErrorMessage("PDFLocation2") + SelectedAppointment.Id.ToString() + ".pdf");
            Guide.SetTimer();
        }
        private void QuitButton_Click(object parameter)
        {
            Guide.Timer.Stop();
            string name = new string(SelectedAppointment.Tour.Name);
            MessageBoxResult result = MessageBox.Show(GetLocalizedErrorMessage("CancelAppQuestion"), GetLocalizedErrorMessage("DeleteApp"), MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Guide.CancelAppointment(SelectedAppointment);
                MessageBox.Show(name + GetLocalizedErrorMessage("SucecssfulDeletation"));
                Guide.CanceledApps.Add(SelectedAppointment);
                Guide.ReadyApps.Remove(SelectedAppointment);
            }
            Guide.SetTimer();
        }
        string GetLocalizedErrorMessage(string resourceKey)
        {
            TextBlock Templabel = new TextBlock();
            LocExtension locExtension = new LocExtension(resourceKey);
            BindingOperations.SetBinding(Templabel, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
            return Templabel.Text;
        }


        public ICommand PdfReportButton_ClickCommand
        {
            get => new RelayCommand(PdfReportButton_Click);
        }

        public ICommand QuitButton_ClickCommand
        {
            get => new RelayCommand(QuitButton_Click);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
