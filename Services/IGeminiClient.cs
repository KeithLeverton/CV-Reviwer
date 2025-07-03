using Mscc.GenerativeAI;

namespace CV_Rater.Services
{
    public interface IGeminiClient
    {
        Task<GenerateContentResponse> GenerateResponse(string prompt);

    }
}
