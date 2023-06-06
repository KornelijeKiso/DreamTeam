using System;
using System.Windows;
using System.Windows.Controls;
using Syncfusion.Windows.PdfViewer;

namespace ProjectTourism.WPF.View.GuideView
{
    public partial class PDFViewer : Window
    {
        public PDFViewer(string path)
        {
            InitializeComponent();
            pdfViewerControl.Load(path);
        }
    }
}
