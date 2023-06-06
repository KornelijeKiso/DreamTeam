using ProjectTourism.DTO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Colors;
using ProjectTourism.Localization;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectTourism.WPF.View.GuideView;

namespace ProjectTourism.PDF.GuidePDF
{
    public class GuestsOnATourPDFGenerator
    {
        private PdfWriter pdfWriter { get; set; }
        private PdfDocument pdfDocument { get; set; }
        private Document document { get; set; }
        private string path;
        string GetLocalizedErrorMessage(string resourceKey)
        {
            TextBlock Templabel = new TextBlock();
            LocExtension locExtension = new LocExtension(resourceKey);
            BindingOperations.SetBinding(Templabel, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
            return Templabel.Text;
        }
        public GuestsOnATourPDFGenerator(TourAppointmentDTO tourApp)
        {
            path = GetLocalizedErrorMessage("PDFName") + tourApp.Id.ToString() + ".pdf";
            pdfWriter = new PdfWriter(path);
            pdfDocument = new PdfDocument(pdfWriter);
            document = new Document(pdfDocument);


            document.Add(new Paragraph(tourApp.Tour.Name + GetLocalizedErrorMessage("PDF1")).SetTextAlignment(TextAlignment.CENTER).SetFontSize(20));

            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            Paragraph emptySpace = new Paragraph("\n")
                .SetFontSize(12);
            document.Add(emptySpace);

            Paragraph location = new Paragraph()
            .Add(new Text(GetLocalizedErrorMessage("TourLocation")).SetBold())
            .Add(tourApp.Tour.Location?.City + ", " + tourApp.Tour.Location?.Country)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetFontSize(10);
            document.Add(location);

            Paragraph time = new Paragraph()
                .Add(new Text(GetLocalizedErrorMessage("PDF2")).SetBold())
                .Add(tourApp.TourDateTime.ToString())
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(time);

            Paragraph language = new Paragraph()
                .Add(new Text(GetLocalizedErrorMessage("PDF3")).SetBold())
                .Add(tourApp.Tour.Language)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(language);

            Paragraph tourStops = new Paragraph()
                .Add(new Text(GetLocalizedErrorMessage("PDF3")).SetBold())
                .Add(tourApp.Tour.Start + ", ")
                .Add(tourApp.Tour.Stops + ", ")
                .Add(tourApp.Tour.Finish)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(10);
            document.Add(tourStops);

            document.Add(emptySpace);

            Table table = new Table(3).UseAllAvailableWidth();
            table.AddCell(new Cell().Add(new Paragraph(GetLocalizedErrorMessage("Guest")).SetBackgroundColor(ColorConstants.LIGHT_GRAY)));
            table.AddCell(new Cell().Add(new Paragraph(GetLocalizedErrorMessage("TourStop")).SetBackgroundColor(ColorConstants.LIGHT_GRAY)));
            table.AddCell(new Cell().Add(new Paragraph(GetLocalizedErrorMessage("NumberOfTickets")).SetBackgroundColor(ColorConstants.LIGHT_GRAY)));

            foreach (TicketDTO ticket in tourApp.Tickets)
            {
                Cell guestCell = new Cell().Add(new Paragraph(ticket.Guest2.FirstName + " " + ticket.Guest2.LastName));
                guestCell.Add(new Paragraph("(" + ticket.Guest2Username + ")").SetFontColor(ColorConstants.GRAY)).SetTextAlignment(TextAlignment.CENTER);

                table.AddCell(guestCell);
                table.AddCell(ticket.TourStop).SetTextAlignment(TextAlignment.CENTER);
                table.AddCell(ticket.NumberOfGuests.ToString()).SetTextAlignment(TextAlignment.CENTER);
            }

            document.Add(table);
            document.Close();
            PDFViewer pDFViewer = new PDFViewer(path);
            pDFViewer.Show();
        }
    }
}
