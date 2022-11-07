namespace MovieDatabase.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;

    public class MovieVM
    {
        public string Name { get; set; }

        public List<MovieCategoryVM> Categories { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Studio { get; set; }

        public string Description { get; set; }

        public string PosterImageLink { get; set; }

        public string PosterImageRelativeLink { get; set; }

        public string TrailerLink { get; set; }

        public string Duration { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }

        public List<MovieActorVM> Actors { get; set; }

        public MovieDirectorVM Director { get; set; }
        
        public MovieScreenwriterVM Screenwriter { get; set; }

        public MovieComposerVM Composer { get; set; }

        public List<MovieAwardVM> Awards { get; set; }

        public double ImdbRating { get; set; }

        public List<MovieRatingVM> Ratings { get; set; }

        public double OverallRating { get; set; }

        public double GivenUserRating { get; set; }

        public bool IsInWishlist { get; set; }

        public List<UserWishlistVM> UserWishlists { get; set; }

        public List<MovieKeywordVM> Keywords { get; set; }

        public List<CommentVM> Comments { get; set; }
    }
}
