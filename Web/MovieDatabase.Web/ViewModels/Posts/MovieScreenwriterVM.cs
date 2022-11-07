namespace MovieDatabase.Web.ViewModels.Posts
{
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class MovieScreenwriterVM : IMapFrom<Screenwriter>
    {
        public string Name { get; set; }
    }
}
