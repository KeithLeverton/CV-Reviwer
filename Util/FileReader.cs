using iText.Kernel.Pdf;
using System.Text;
namespace CV_Rater.Util
{
    public static class FileReader
    {
        public static async Task<string> ReadPdfAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty or not provided.");
            }
            using (var stream = file.OpenReadStream())
            {
                using (var reader = new iText.Kernel.Pdf.PdfReader(stream))
                {
                    using (var pdfDoc = new iText.Kernel.Pdf.PdfDocument(reader))
                    {
                        var text = new StringBuilder();
                        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                        {
                            var page = pdfDoc.GetPage(i);
                            var strategy = new iText.Kernel.Pdf.Canvas.Parser.Listener.SimpleTextExtractionStrategy();
                            var pageText = iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(page, strategy);
                            text.AppendLine(pageText);
                        }
                        return text.ToString();
                    }
                }
            }
        }

    }
}
