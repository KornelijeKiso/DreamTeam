using ProjectTourism.DTO;
using System;
using System.Windows;

namespace ProjectTourism.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for PDFDisplayWindow.xaml
    /// </summary>
    public partial class PDFDisplayWindow : Window
    {
        private string fullPathToPDF { get; set; }
        public PDFDisplayWindow(TicketDTO ticket)
        {
            InitializeComponent();
            DataContext = this;

            string fullPathToDebug = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            fullPathToPDF = fullPathToDebug.Substring(0, fullPathToDebug.Length - 25);
            fullPathToPDF = @"file:///" + fullPathToPDF + "/PDF/Guest2PDFs/ticket_report_" + ticket.Guest2Username + "_" + ticket.Id.ToString() + ".pdf";
            PDFDisplayWebBrowser.Navigate(fullPathToPDF);
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
