using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace MeBot.Helpers;

public static class PdfTextExtractor
{
    public static string ExtractTextFromPdf(string pdfFilePath)
    {
        try
        {
            var extractedText = new StringBuilder();

            using var pdfDocument = PdfDocument.Open(pdfFilePath);

            foreach (Page page in pdfDocument.GetPages())
            {
                extractedText.AppendLine(page.Text);
            }

            return extractedText.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting text from PDF: {ex.Message}");
            return string.Empty;
        }
    }
}