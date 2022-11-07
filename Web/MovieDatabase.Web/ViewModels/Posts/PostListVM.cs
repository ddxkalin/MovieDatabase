namespace MovieDatabase.Web.ViewModels.Posts
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class PostListVM : PaginatedWithMappingVM<Post>
    {
        public List<PostVM> Posts { get; set; }
    }
}