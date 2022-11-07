namespace MovieDatabase.Web.Areas.Identity.Pages.Account.Manage.OutputModels
{
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class ManagePostsMovieOutputModel : IMapFrom<Movie>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }
    }
}
