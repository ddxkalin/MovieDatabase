namespace MovieDatabase.Web.ViewModels.Posts
{
    using System;

    public class PostVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public MovieVM Movie { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserVM User { get; set; }
    }
}
