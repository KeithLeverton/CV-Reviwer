namespace CV_Rater.Models
{
    public class CVReviewViewModel
    {
        public string ?ReviewResponse { get; set; }
        public string ReviewResponseHtml
        {
            get
            {
                if (string.IsNullOrEmpty(ReviewResponse))
                    return string.Empty;

                string html = ReviewResponse;

                // Convert **text** to bullet points
                html = System.Text.RegularExpressions.Regex.Replace(
                    html,
                    @"\*\*(.*?)\*\*",
                    "<li>$1</li>");

                // Convert *text* to bold
                html = System.Text.RegularExpressions.Regex.Replace(
                    html,
                    @"\*(.*?)\*",
                    "<b>$1</b>");

                // Replace newlines with <br/>
                html = html.Replace("\n", "<br/>");

                // Optionally, wrap bullet points in <ul> if any <li> exists
                if (html.Contains("<li>"))
                {
                    html = "<ul>" + html + "</ul>";
                }

                return html;
            }
        }
    }
}
