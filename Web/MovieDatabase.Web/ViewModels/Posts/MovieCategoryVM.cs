namespace MovieDatabase.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieCategoryVM : IMapFrom<MovieCategory>
    {
        [Required(ErrorMessage = "Field is required")]
        public string CategoryId { get; set; }

        public CategoryVM Category { get; set; }
    }
}
