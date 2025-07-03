namespace CV_Rater.Services
{
    public interface IHomeService
    {
        public Task<string> ProcessFileAsync(IFormFile file);
    }
}
