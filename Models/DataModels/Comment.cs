using RestSharp.Deserializers;
using System;

namespace SciFiReviews.Models.DataModels
{
    public class Comment
    {
        [DeserializeAs(Name = "Id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "ReviewerId")]
        public int ReviewerId { get; set; }

        [DeserializeAs(Name = "ReviewerName")]
        public string ReviewerName { get; set; }

        [DeserializeAs(Name = "Body")]
        public string Body { get; set; }

        [DeserializeAs(Name = "TimeStamp")]
        public DateTime TimeStamp { get; set; }
    }
}
