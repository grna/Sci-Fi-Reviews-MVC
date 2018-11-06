using System.ComponentModel.DataAnnotations;

namespace SciFiReviews.Models.ViewModels
{
    public class MovieViewModel
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(40)]
        public string Author { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}
