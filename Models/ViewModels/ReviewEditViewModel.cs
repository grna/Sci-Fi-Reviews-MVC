using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SciFiReviews.Models.ViewModels
{
    public class ReviewEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public float Rating { get; set; }
    }
}
