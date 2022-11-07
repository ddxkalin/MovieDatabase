namespace MovieDatabase.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieRatingVM : IMapFrom<MovieRating>
    {
        public RatingVM Rating { get; set; }
    }
}
