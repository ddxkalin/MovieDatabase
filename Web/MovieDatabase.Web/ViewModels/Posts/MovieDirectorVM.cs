namespace MovieDatabase.Web.ViewModels.Posts
{
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieDirectorVM : IMapFrom<Director>
    {
        public string Name { get; set; }
    }
}
