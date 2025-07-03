using System.Diagnostics;
using CV_Rater.Models;
using Microsoft.AspNetCore.Mvc;
using Mscc.GenerativeAI;
using CV_Rater.Services;

namespace CV_Rater.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upload([FromForm]IFormFile file)
        {
            var response = new CVReviewViewModel();
            try
            {     
                response.ReviewResponse = await _homeService.ProcessFileAsync(file);
            }
            catch (Exception ex)
            {
                response.ReviewResponse = "Error generating response.";
                _logger.LogError(ex, "Error generating response from Gemini AI.");
            }

            return View("CvReview", response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
