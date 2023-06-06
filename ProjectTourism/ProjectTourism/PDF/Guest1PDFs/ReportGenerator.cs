using ProjectTourism.DTO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Colors;
using ProjectTourism.WPF.View.Guest1View;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectTourism.PDF.Guest1PDFs
{
    public class ReportGenerator
    {
        private string path;
        private PdfWriter pdfWriter { get; set; }
        private PdfDocument pdfDocument { get; set; }
        private Document document { get; set; }

        public ReportGenerator(DateOnly startDate, DateOnly endDate, Guest1DTO guest1, bool isCanceled)
        {
            // TO DO -> add where to save doc
            pdfWriter = new PdfWriter("../../../PDF/GuestPDFs/cancelledReservation_report" + startDate.ToString() + endDate.ToString() + ".pdf");
            path = ".. / .. / .. / PDF / GuestPDFs / cancelledReservation_report" + startDate.ToString() + endDate.ToString() + ".pdf";
            pdfDocument = new PdfDocument(pdfWriter);
            document = new Document(pdfDocument);

            Paragraph info = new Paragraph("Reservation report from " + startDate.ToString() + " to " + endDate.ToString())
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(info);

            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            Paragraph emptySpace = new Paragraph("\n")
                .SetFontSize(12);
            document.Add(emptySpace);
            if (isCanceled == true)
            {
                Paragraph tot = new Paragraph("Canceled")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(16);
                document.Add(tot);
            }
            else
            {
                Paragraph tot = new Paragraph("Reserved")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(16);
                document.Add(tot);
            }

            List<ReservationDTO> list = guest1.MyReservations.ToList().Where(r => r.StartDate >= startDate && r.EndDate <= endDate).ToList();

            Table total = new Table(4, false).UseAllAvailableWidth();
            Cell cellT2 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Start Date"));
            Cell cellT3 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("End Date"));
            Cell cellT4 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Accommodation"));
            Cell cellT5 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Location"));

            foreach (var reservation in list)
            {
                if (!isCanceled)
                {
                    Cell cellTR = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(reservation.StartDate.ToString()));
                    Cell cellTP = new Cell(1, 1)
                       .SetTextAlignment(TextAlignment.CENTER)
                       .Add(new Paragraph(reservation.EndDate.ToString()));
                    Cell cellTC = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(reservation.Accommodation.Name.ToString()));
                    Cell cellTRR = new Cell(1, 1)
                       .SetTextAlignment(TextAlignment.CENTER)
                       .Add(new Paragraph(reservation.Accommodation.Location.City.ToString() + reservation.Accommodation.Location.Country.ToString()));
                    total.AddCell(cellTR);
                    total.AddCell(cellTP);
                    total.AddCell(cellTC);
                    total.AddCell(cellTRR);
                }
            }
            
            total.AddCell(cellT2);
            total.AddCell(cellT3);
            total.AddCell(cellT4);
            total.AddCell(cellT5);
            //total.AddCell(cellTR);
            //total.AddCell(cellTP);
            //total.AddCell(cellTC);
            //total.AddCell(cellTRR);
            document.Add(total);

            document.Close();
            pdfDocument.Close();
            pdfWriter.Close();
        }
    }
}
