namespace MovieDatabase.Data.Models
{
    using System;

    using MovieDatabase.Data.Common.Models;

    public class Comment: BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}