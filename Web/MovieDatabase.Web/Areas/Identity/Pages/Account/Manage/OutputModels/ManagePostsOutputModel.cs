namespace MovieDatabase.Web.Areas.Identity.Pages.Account.Manage.OutputModels
{
    using System;

    using MovieDatabase.Common.Mapping;
    using MovieDatabase.Data.Models;

    public class ManagePostsOutputModel : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ManagePostsMovieOutputModel Movie { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
