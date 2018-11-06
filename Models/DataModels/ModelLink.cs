using RestSharp.Deserializers;

namespace SciFiReviews.Models.DataModels
{
    public class ModelLink
    {
        [DeserializeAs(Name = "Href")]
        public string Href { get; set; }

        [DeserializeAs(Name = "Rel")]
        public string Rel { get; set; }

        [DeserializeAs(Name = "Method")]
        public string Method { get; set; }
    }
}
