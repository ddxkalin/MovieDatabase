namespace MovieDatabase.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserRating
    {
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public Rating Rating { get; set; }

        public string RatingId { get; set; }
    }
}
