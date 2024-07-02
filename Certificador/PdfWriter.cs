using FontChanging;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Writing;

public class PdfWriter
{
    public static void ReplaceText(string nomeNoCertificado, string caminhoCertificado, string caminhoCertificadoCompleto)
    {
        string outputPath = caminhoCertificado + "\\" + nomeNoCertificado + ".pdf";

        GlobalFontSettings.FontResolver = new PoppinsFontResolver();


        PdfDocument document = PdfReader.Open(caminhoCertificadoCompleto, PdfDocumentOpenMode.Modify);

        PdfPage page = document.Pages[0];

        XGraphics gfx = XGraphics.FromPdfPage(page);
        XFont font = new XFont("Poppins", 24, XFontStyleEx.Regular);

        XSize textSize = gfx.MeasureString(nomeNoCertificado, font);

        double xPos = (592 - textSize.Width) / 2;
        double yPos = 290;

        gfx.DrawString(nomeNoCertificado, font, XBrushes.Black, new XPoint(xPos, yPos));

        document.Save(outputPath);
    }
}