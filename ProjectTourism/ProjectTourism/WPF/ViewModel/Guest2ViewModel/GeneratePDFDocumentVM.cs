using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.DTO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Colors;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class GeneratePDFDocumentVM
    {
        private PdfWriter pdfWriter { get; set; }
        private PdfDocument pdfDocument { get; set; }
        private Document document { get; set; }

        public GeneratePDFDocumentVM(TicketDTO ticket)
        {
            // TO DO -> add where to save doc
            pdfWriter = new PdfWriter("../../../PDF/Guest2PDFs/ticket_report_" + ticket.Guest2Username + "_" + ticket.Id.ToString() + ".pdf");
            pdfDocument = new PdfDocument(pdfWriter);
            document = new Document(pdfDocument);


            Paragraph header = new Paragraph("TICKET REPORT")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(header);

            Paragraph subHeader = new Paragraph("Ticket ID: " + ticket.Id.ToString())
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(15);
            document.Add(subHeader);

            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);
            
            Paragraph emptySpace = new Paragraph("\n")
                .SetFontSize(12);
            document.Add(emptySpace);

            // Ticket data table
            Table table = new Table(3, false).UseAllAvailableWidth();

            // Ticket info
            Cell cellTC = new Cell(3, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Ticket information "));
            
            Cell cellTC12 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Number of Tickets "));

            Cell cellTC13 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.NumberOfGuests.ToString()));

            Cell cellTC22 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Joining stop "));

            Cell cellTC23 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourStop));

            Cell cellTC32 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("ATTENDANCE "));

            Paragraph AtendanceParagraph = new Paragraph("(can't confirm yet, appointment is not in appropriate state)");
            if (ticket.HasGuideChecked && ticket.HasGuestConfirmed)
                AtendanceParagraph = new Paragraph("attended");
            else if (ticket.HasGuideChecked && !ticket.HasGuestConfirmed)
                AtendanceParagraph = new Paragraph("skipped");

            Cell cellTC33 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(AtendanceParagraph);


            // Guest2 info
            Cell cellG = new Cell(5, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Guest information "));

            Cell cellG12 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("First Name "));

            Cell cellG13 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.Guest2.FirstName));

            Cell cellG22 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Last Name "));

            Cell cellG23 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.Guest2.LastName));
            
            Cell cellG32 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Birthday "));

            Cell cellG33 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.Guest2.Birthday.ToString("dd.MM.yyyy HH:mm")));

            Cell cellG42 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Email "));

            Cell cellG43 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.Guest2.Email));

            Cell cellG52 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Phone Number "));

            Cell cellG53 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.Guest2.PhoneNumber));

            // TourAppointment info
            Cell cellT = new Cell(10, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Tour Appointment information "));

            Cell cellT12 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Tour name "));

            Cell cellT13 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.Tour.Name));

            Cell cellT22 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Description "));

            Cell cellT23 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.Tour.Description));

            Cell cellT32 = new Cell(1, 1) 
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Stops "));

            Paragraph StopsParagraph = new Paragraph(ticket.TourAppointment.Tour.Start + ", " + ticket.TourAppointment.Tour.Finish);
            if (ticket.TourAppointment.Tour.Stops != "")
                StopsParagraph = new Paragraph(ticket.TourAppointment.Tour.Start + ", " + ticket.TourAppointment.Tour.Stops + ", " + ticket.TourAppointment.Tour.Finish);
            
            Cell cellT33 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(StopsParagraph);

            Cell cellT42 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Guide "));

            Cell cellT43 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.Tour.Guide.FirstName + " " + ticket.TourAppointment.Tour.Guide.LastName + " ("+ ticket.TourAppointment.Tour.GuideUsername + ")"));

            Cell cellT52 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Date and Time od Departure "));

            Cell cellT53 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.TourDateTime.ToString("dd.MM.yyyy HH:mm")));

            Cell cellT62 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Location "));

            Cell cellT63 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.Tour.Location.Country + ", " + ticket.TourAppointment.Tour.Location.City));

            Cell cellT72 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Language "));

            Cell cellT73 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.Tour.Language));

            Cell cellT82 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Duration "));

            Cell cellT83 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.Tour.Duration.ToString() + "h "));

            Cell cellT92 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Number of seats "));

            Cell cellT93 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.Tour.MaxNumberOfGuests.ToString()));

            Cell cellT102 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("STATE "));

            Cell cellT103 = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(ticket.TourAppointment.State.ToString()));

            // Ticket data
            table.AddCell(cellTC);
            table.AddCell(cellTC12);
            table.AddCell(cellTC13);
            table.AddCell(cellTC22);
            table.AddCell(cellTC23);
            table.AddCell(cellTC32);
            table.AddCell(cellTC33);

            // Guest2 data
            table.AddCell(cellG);
            table.AddCell(cellG12); 
            table.AddCell(cellG13);
            table.AddCell(cellG22);
            table.AddCell(cellG23);
            table.AddCell(cellG32);
            table.AddCell(cellG33);
            table.AddCell(cellG42);
            table.AddCell(cellG43);
            table.AddCell(cellG52);
            table.AddCell(cellG53);

            // Tour data
            table.AddCell(cellT);
            table.AddCell(cellT12);
            table.AddCell(cellT13);
            table.AddCell(cellT22);
            table.AddCell(cellT23);
            table.AddCell(cellT32);
            table.AddCell(cellT33); 
            table.AddCell(cellT42);
            table.AddCell(cellT43);
            table.AddCell(cellT52);
            table.AddCell(cellT53);
            table.AddCell(cellT62);
            table.AddCell(cellT63);
            table.AddCell(cellT72);
            table.AddCell(cellT73);
            table.AddCell(cellT82);
            table.AddCell(cellT83);
            table.AddCell(cellT92);
            table.AddCell(cellT93);
            table.AddCell(cellT102);
            table.AddCell(cellT103);

            document.Add(table);

            document.Close();
            pdfDocument.Close();
            pdfWriter.Close();
        }
    }
}
