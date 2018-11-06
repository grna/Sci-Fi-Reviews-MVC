namespace SciFiReviews.Models
{
    public class ErrorViewModel
    {
        private string _defaultMessage = "Oops... Something went wrong. Please try again later.";

        public ErrorViewModel(string errorMessage, string errorDetails)
        {
            ErrorMessage = errorMessage ?? _defaultMessage;
            ErrorDetails = errorDetails ?? ErrorMessage;
        }

        public string ErrorMessage { get; set; }

        public string ErrorDetails { get; set; }
    }
}