namespace MovieDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MovieRating
    {
        public Movie Movie { get; set; }

        public string MovieId { get; set; }

        public Rating Rating { get; set; }

        public string RatingId { get; set; }
    }
}
