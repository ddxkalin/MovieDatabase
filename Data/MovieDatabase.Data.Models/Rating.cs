namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Data.Common.Models;

    public class Rating : BaseDeletableModel<string>
    {
        public Rating()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationUser RatedBy { get; set; }

        public string RatedById { get; set; }

        public DateTime RatedOn { get; set; }

        [Range(1, 5)]
        public double Score { get; set; }

        public virtual ICollection<UserRating> UserRatings { get; set; } = new HashSet<UserRating>();

        public virtual ICollection<MovieRating> MovieRatings { get; set; } = new HashSet<MovieRating>();
    }
}
