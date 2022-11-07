namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Movie : BaseDeletableModel<string>
    {
        public Movie()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Slug { get; set; }

        public virtual ICollection<MovieCategory> Categories { get; set; } = new HashSet<MovieCategory>();

        public DateTime ReleaseDate { get; set; }

        public string Studio { get; set; }

        public string Description { get; set; }

        public string PosterImageLink { get; set; }

        public string TrailerLink { get; set; }

        public string Duration { get; set; }

        public string Country { get; set; }

        public double ImdbRating { get; set; }

        public string Language { get; set; }

        public string DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public string ScreenwriterId { get; set; }

        public virtual Screenwriter Screenwriter { get; set; }

        public string ComposerId { get; set; }

        public virtual Composer Composer { get; set; }

        public virtual ICollection<Award> Awards { get; set; } = new HashSet<Award>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<Keyword> Keywords { get; set; } = new HashSet<Keyword>();

        public virtual ICollection<MovieActor> Actors { get; set; } = new HashSet<MovieActor>();

        public virtual ICollection<MovieRating> Ratings { get; set; } = new HashSet<MovieRating>();

        public virtual ICollection<UserWishlist> UserWishlists { get; set; } = new HashSet<UserWishlist>();

        public virtual ICollection<UserWatchedMovie> UserWatchedMovies { get; set; } = new HashSet<UserWatchedMovie>();

        public virtual ICollection<UserOwnedMovie> UserOwnedMovies { get; set; } = new HashSet<UserOwnedMovie>();
    }
}