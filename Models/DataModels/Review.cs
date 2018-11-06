using RestSharp.Deserializers;
using System;
using System.Collections.Generic;

namespace SciFiReviews.Models.DataModels
{
    public class Review
    {
        [DeserializeAs(Name = "Id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "Title")]
        public string Title { get; set; }

        [DeserializeAs(Name = "Body")]
        public string Body { get; set; }

        [DeserializeAs(Name = "Rating")]
        public float Rating { get; set; }

        [DeserializeAs(Name = "SubmitTime")]
        public DateTime SubmitTime { get; set; }

        [DeserializeAs(Name = "Movie")]
        public Movie Movie { get; set; }

        [DeserializeAs(Name = "Reviewer")]
        public Reviewer Reviewer { get; set; }

        [DeserializeAs(Name = "Comments")]
        public List<Comment> Comments { get; set; }

        [DeserializeAs(Name = "PeopleWhoLikedReview")]
        public List<string> PeopleWhoLikedReview { get; set; }

        [DeserializeAs(Name = "Links")]
        public List<ModelLink> Links { get; set; }
    }
}
