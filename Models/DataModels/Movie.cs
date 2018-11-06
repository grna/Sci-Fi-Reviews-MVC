using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SciFiReviews.Models.DataModels
{
    public class Movie
    {
        [DeserializeAs(Name = "Id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "Title")]
        public string Title { get; set; }

        [DeserializeAs(Name = "Author")]
        public string Author { get; set; }

        [DeserializeAs(Name = "ReleaseYear")]
        public int ReleaseYear { get; set; }

        [DeserializeAs(Name = "Genre")]
        public string Genre { get; set; }

        [DeserializeAs(Name = "ImagePath")]
        public string ImagePath { get; set; }

        [DeserializeAs(Name = "AverageRating")]
        public float AverageRating { get; set; }

        [DeserializeAs(Name = "MovieReviews")]
        public IEnumerable<Review> MovieReviews { get; set; } = new List<Review>();
    }
}
