using ProjectTourism.DTO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Colors;

namespace ProjectTourism.PDF.GuidePDF
{
    public class GuestsOnATourPDFGenerator
    {
        private PdfWriter pdfWriter { get; set; }
        private PdfDocument pdfDocument { get; set; }
        private Document document { get; set; }

        public GuestsOnATourPDFGenerator(TourAppointmentDTO tourApp)
        {
            pdfWriter = new PdfWriter("../../../PDF/GuidePDF/tourAppointmentReposrt_" + tourApp.Id.ToString() + ".pdf");
            pdfDocument = new PdfDocument(pdfWriter);
            document = new Document(pdfDocument);

            Paragraph info = new Paragraph("Report for " + tourApp.Tour.Name)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(info);

        }
    }
}
