namespace SciFiReviews.Helpers
{
    public class MovieGenres
    {
        public string[] Genres
        {
            get
            {
                return new string[] 
                {
                    "Thriller",
                    "Horror",
                    "SciFi"
                };
            }
            set
            {
                Genres = value;
            }
        }

        public string CurrentGenre { get; set; }

        public MovieGenres(string currentGenre)
        {
            CurrentGenre = currentGenre;
        }
    }
}
