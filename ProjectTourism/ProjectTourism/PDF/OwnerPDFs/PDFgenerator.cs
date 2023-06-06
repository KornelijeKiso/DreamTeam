using ProjectTourism.DTO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Element;
using iText.Kernel.Colors;
using ProjectTourism.WPF.View.OwnerView;

namespace ProjectTourism.PDF.OwnerPDFs
{
    public class PDFgenerator
    {
        private string path;
        private PdfWriter pdfWriter { get; set; }
        private PdfDocument pdfDocument { get; set; }
        private Document document { get; set; }

        public PDFgenerator(AccommodationStatisticsDTO stats, AccommodationDTO acc)
        {
            // TO DO -> add where to save doc
            pdfWriter = new PdfWriter("../../../PDF/OwnerPDFs/statistics_report" + acc.Id.ToString() + ".pdf");
            path = "../../../PDF/OwnerPDFs/statistics_report" + acc.Id.ToString() + ".pdf";
            pdfDocument = new PdfDocument(pdfWriter);
            document = new Document(pdfDocument);

            Paragraph info = new Paragraph("Statistics for " + acc.Name + " in " + stats.Year.ToString())
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(info);

            Paragraph header = new Paragraph("Location: " + acc.Location.City +", "+ acc.Location.Country + " \n "
                                                + "Occupancy for " + stats.Year.ToString() + " : " + stats.Occupancy +"%" + "\n"
                                                + "Best month: " + stats.BestMonth.MonthString + " (" + stats.BestMonth.Occupancy + "% occupancy)")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(16);
            document.Add(header);

            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            Paragraph emptySpace = new Paragraph("\n")
                .SetFontSize(12);
            document.Add(emptySpace);
            Paragraph tot = new Paragraph("Total stats")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16);
            document.Add(tot);

            Table total = new Table(4, false).UseAllAvailableWidth();
            Cell cellT2 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Reservations"));
            Cell cellT3 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Postponed"));
            Cell cellT4 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Canceled"));
            Cell cellT5 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Renovation recommendations"));
            Cell cellTR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.Reservations.ToString()));
            Cell cellTP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.PostponedReservations.ToString()));
            Cell cellTC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.CanceledReservations.ToString()));
            Cell cellTRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.RenovationReccommendations.ToString()));

            total.AddCell(cellT2);
            total.AddCell(cellT3);
            total.AddCell(cellT4);
            total.AddCell(cellT5);
            total.AddCell(cellTR);
            total.AddCell(cellTP);
            total.AddCell(cellTC);
            total.AddCell(cellTRR);
            document.Add(total);

            Paragraph emptySpace2 = new Paragraph("\n")
                .SetFontSize(12);
            document.Add(emptySpace2);

            Paragraph mon = new Paragraph("Monthly stats")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16);
            document.Add(mon);
            Table table = new Table(5, false).UseAllAvailableWidth();
            //headers
            Cell cellH1 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Month"));
            Cell cellH2 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Reservations"));
            Cell cellH3 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Postponed"));
            Cell cellH4 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Canceled"));
            Cell cellH5 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Renovation recommendations"));


            //january
            Cell cellJan = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("January"));
            Cell cellJanR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[0].Reservations.ToString()));
            Cell cellJanP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[0].PostponedReservations.ToString()));
            Cell cellJanC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[0].CanceledReservations.ToString()));
            Cell cellJanRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[0].RenovationReccommendations.ToString()));

            //february
            Cell cellFeb = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("February"));
            Cell cellFebR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[1].Reservations.ToString()));
            Cell cellFebP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[1].PostponedReservations.ToString()));
            Cell cellFebC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[1].CanceledReservations.ToString()));
            Cell cellFebRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[1].RenovationReccommendations.ToString()));

            //march
            Cell cellMar = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("March"));
            Cell cellMarR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[2].Reservations.ToString()));
            Cell cellMarP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[2].PostponedReservations.ToString()));
            Cell cellMarC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[2].CanceledReservations.ToString()));
            Cell cellMarRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[2].RenovationReccommendations.ToString()));

            //april
            Cell cellApr = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("April"));
            Cell cellAprR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[3].Reservations.ToString()));
            Cell cellAprP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[3].PostponedReservations.ToString()));
            Cell cellAprC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[3].CanceledReservations.ToString()));
            Cell cellAprRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[3].RenovationReccommendations.ToString()));

            //may
            Cell cellMay = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("May"));
            Cell cellMayR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[4].Reservations.ToString()));
            Cell cellMayP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[4].PostponedReservations.ToString()));
            Cell cellMayC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[4].CanceledReservations.ToString()));
            Cell cellMayRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[4].RenovationReccommendations.ToString()));

            //june
            Cell cellJun = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("June"));
            Cell cellJunR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[5].Reservations.ToString()));
            Cell cellJunP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[5].PostponedReservations.ToString()));
            Cell cellJunC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[5].CanceledReservations.ToString()));
            Cell cellJunRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[5].RenovationReccommendations.ToString()));

            //july
            Cell cellJul = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("July"));
            Cell cellJulR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[6].Reservations.ToString()));
            Cell cellJulP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[6].PostponedReservations.ToString()));
            Cell cellJulC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[6].CanceledReservations.ToString()));
            Cell cellJulRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[6].RenovationReccommendations.ToString()));

            //august
            Cell cellAug = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("August"));
            Cell cellAugR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[7].Reservations.ToString()));
            Cell cellAugP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[7].PostponedReservations.ToString()));
            Cell cellAugC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[7].CanceledReservations.ToString()));
            Cell cellAugRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[7].RenovationReccommendations.ToString()));

            //september
            Cell cellSep = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("September"));
            Cell cellSepR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[8].Reservations.ToString()));
            Cell cellSepP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[8].PostponedReservations.ToString()));
            Cell cellSepC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[8].CanceledReservations.ToString()));
            Cell cellSepRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[8].RenovationReccommendations.ToString()));

            //october
            Cell cellOct = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("October"));
            Cell cellOctR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[9].Reservations.ToString()));
            Cell cellOctP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[9].PostponedReservations.ToString()));
            Cell cellOctC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[9].CanceledReservations.ToString()));
            Cell cellOctRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[9].RenovationReccommendations.ToString()));
            
            //november
            Cell cellNov = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("November"));
            Cell cellNovR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[10].Reservations.ToString()));
            Cell cellNovP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[10].PostponedReservations.ToString()));
            Cell cellNovC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[10].CanceledReservations.ToString()));
            Cell cellNovRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[10].RenovationReccommendations.ToString()));

            //december
            Cell cellDec = new Cell(1, 1)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("December"));
            Cell cellDecR = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[11].Reservations.ToString()));
            Cell cellDecP = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[11].PostponedReservations.ToString()));
            Cell cellDecC = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(stats.StatsByMonths[11].CanceledReservations.ToString()));
            Cell cellDecRR = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph(stats.StatsByMonths[11].RenovationReccommendations.ToString()));

            table.AddCell(cellH1);
            table.AddCell(cellH2);
            table.AddCell(cellH3);
            table.AddCell(cellH4);
            table.AddCell(cellH5);

            table.AddCell(cellJan);
            table.AddCell(cellJanR);
            table.AddCell(cellJanP);
            table.AddCell(cellJanC);
            table.AddCell(cellJanRR);

            table.AddCell(cellFeb);
            table.AddCell(cellFebR);
            table.AddCell(cellFebP);
            table.AddCell(cellFebC);
            table.AddCell(cellFebRR);

            table.AddCell(cellMar);
            table.AddCell(cellMarR);
            table.AddCell(cellMarP);
            table.AddCell(cellMarC);
            table.AddCell(cellMarRR);

            table.AddCell(cellApr);
            table.AddCell(cellAprR);
            table.AddCell(cellAprP);
            table.AddCell(cellAprC);
            table.AddCell(cellAprRR);

            table.AddCell(cellMay);
            table.AddCell(cellMayR);
            table.AddCell(cellMayP);
            table.AddCell(cellMayC);
            table.AddCell(cellMayRR);

            table.AddCell(cellJun);
            table.AddCell(cellJunR);
            table.AddCell(cellJunP);
            table.AddCell(cellJunC);
            table.AddCell(cellJunRR);

            table.AddCell(cellJul);
            table.AddCell(cellJulR);
            table.AddCell(cellJulP);
            table.AddCell(cellJulC);
            table.AddCell(cellJulRR);

            table.AddCell(cellAug);
            table.AddCell(cellAugR);
            table.AddCell(cellAugP);
            table.AddCell(cellAugC);
            table.AddCell(cellAugRR);

            table.AddCell(cellSep);
            table.AddCell(cellSepR);
            table.AddCell(cellSepP);
            table.AddCell(cellSepC);
            table.AddCell(cellSepRR);

            table.AddCell(cellOct);
            table.AddCell(cellOctR);
            table.AddCell(cellOctP);
            table.AddCell(cellOctC);
            table.AddCell(cellOctRR);

            table.AddCell(cellNov);
            table.AddCell(cellNovR);
            table.AddCell(cellNovP);
            table.AddCell(cellNovC);
            table.AddCell(cellNovRR);

            table.AddCell(cellDec);
            table.AddCell(cellDecR);
            table.AddCell(cellDecP);
            table.AddCell(cellDecC);
            table.AddCell(cellDecRR);

            document.Add(table);

            document.Close();
            pdfDocument.Close();
            pdfWriter.Close();
            PdfViewer pdf = new PdfViewer(path);
            pdf.Show();
        }
    }
}
