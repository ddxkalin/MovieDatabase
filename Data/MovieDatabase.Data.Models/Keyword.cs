namespace MovieDatabase.Data.Models
{
    using System;
    using MovieDatabase.Data.Common.Models;

    public class Keyword: BaseDeletableModel<string>
    {
        public Keyword()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}