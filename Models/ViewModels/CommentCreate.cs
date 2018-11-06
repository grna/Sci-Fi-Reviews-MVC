using System.ComponentModel.DataAnnotations;

namespace SciFiReviews.Models.ViewModels
{
    public class CommentCreate
    {
        [Required]
        public string ReviewerName { get; set; }

        [Required]
        public int ReviewId { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
