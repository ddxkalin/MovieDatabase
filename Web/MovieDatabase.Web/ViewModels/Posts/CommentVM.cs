namespace MovieDatabase.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;

    public class CommentVM
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserVM Author { get; set; }
    }
}
