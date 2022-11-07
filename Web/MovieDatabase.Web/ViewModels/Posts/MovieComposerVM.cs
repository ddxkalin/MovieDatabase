namespace MovieDatabase.Web.ViewModels.Posts
{
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieComposerVM : IMapFrom<Composer>
    {
        public string Name { get; set; }
    }
}
