namespace MovieDatabase.Services.Models
{
    using System.Collections.Generic;

    public class ImdbServiceMovieModel
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string Rated { get; set; }

        public string Released { get; set; }

        public string Runtime { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Writer { get; set; }

        public string Actors { get; set; }

        public string Plot { get; set; }

        public string Language { get; set; }

        public string Country { get; set; }

        public string Awards { get; set; }

        public string Poster { get; set; }

        public List<ImdbServiceRatingModel> Ratings { get; set; }

        public string Metascore { get; set; }

        public double ImdbRating { get; set; }

        public string ImdbVotes { get; set; }

        public string ImdbID { get; set; }

        public string Type { get; set; }

        public string Production { get; set; }

        public bool Response { get; set; }

        public string Error { get; set; }
    }
}
