namespace MovieDatabase.Web.Areas.Admin.Models.Posts
{
    using System.Collections.Generic;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Web.Areas.Admin.Models.Base;

    public class PostListVM : PaginatedWithMappingVM<PostVM>
    {
        public List<PostVM> Posts { get; set; }
    }
}
