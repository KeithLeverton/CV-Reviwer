using Mscc.GenerativeAI;

namespace CV_Rater.Services
{
    public class GeminiClient : IGeminiClient
    {
        private GoogleAI _googleAI;
        private GenerativeModel _model;
        private IConfiguration _configuration;
        public GeminiClient(IConfiguration configuration)
        {
           _configuration = configuration;
           _googleAI = new GoogleAI(configuration["GeminiApiKey"]);
           _model = _googleAI.GenerativeModel(model: Model.Gemini20FlashLite);
           
        }

        public async Task<GenerateContentResponse> GenerateResponse(string prompt)
        {
            var request = new GenerateContentRequest(prompt);
            var response = await _model.GenerateContent(request);
            return response;
        }
    }
}
