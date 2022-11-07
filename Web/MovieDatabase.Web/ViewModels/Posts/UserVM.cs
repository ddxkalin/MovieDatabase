namespace MovieDatabase.Web.ViewModels.Posts
{
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class UserVM : IMapFrom<ApplicationUser>
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Id { get; set; }
    }
}
