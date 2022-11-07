namespace MovieDatabase.Web.ViewModels.Posts
{
    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class UserWishlistVM : IMapFrom<UserWishlist>
    {
        public UserVM User { get; set; }
    }
}
