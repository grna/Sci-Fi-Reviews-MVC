using RestSharp.Deserializers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SciFiReviews.Models.DataModels
{
    public class Reviewer
    {
        [Required]
        [DeserializeAs(Name = "Id")]
        public int Id { get; set; }

        [MaxLength(50)]
        [DeserializeAs(Name = "Username")]
        public string Username { get; set; }

        [DeserializeAs(Name = "NumberOfReviewsSubmitted")]
        public int NumberOfReviewsSubmitted { get; set; }

        [DeserializeAs(Name = "NumberOfCommentsSubmitted")]
        public int NumberOfCommentsSubmitted { get; set; }

        [DeserializeAs(Name = "LikedReviews")]
        public List<int> LikedReviews { get; set; }
    }
}
