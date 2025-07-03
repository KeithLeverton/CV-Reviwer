using CV_Rater.Util;
using System.Net.Mime;
using System.Runtime.InteropServices;

namespace CV_Rater.Services
{
    public class HomeService : IHomeService
    {
        IGeminiClient _geminiClient;
        public HomeService(IGeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }
        public async Task<string> ProcessFileAsync(IFormFile file)
        {
            CheckFile(file);
            string fileContent = await readFileContent(file);
            string prompt = GeneratePrompt(fileContent);
            try
            {
                var response = await _geminiClient.GenerateResponse(prompt);
                return !string.IsNullOrEmpty(response.Text) ? response.Text : "Error generating response from Gemini AI.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating response from Gemini AI.", ex);
            }
        }
        private async Task<string> readFileContent(IFormFile file)
        {
            if(file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return await FileReader.ReadPdfAsync(file);
            }
            else if(file.Name.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            else
            {
                throw new ArgumentException("Unsupported file type. Only text and PDF files are allowed.");
            }
        }
        private void CheckFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty or not provided.");
            }
            if (file.Length > 10 * 1024 * 1024) // 10 MB limit
            {
                throw new ArgumentException("File size exceeds the limit of 10 MB.");
            }
            if (file.ContentType != "text/plain" && file.ContentType != "application/pdf")
            {
                throw new ArgumentException("Unsupported file type. Only text and PDF files are allowed.");
            }
        }
        private string GeneratePrompt(string fileContent)
        {
            var prompt = Prompts.CVPrompt.Replace("{0}", fileContent);
            return prompt;
        }
    }
}
