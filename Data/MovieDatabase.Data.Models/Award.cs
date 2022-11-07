namespace MovieDatabase.Data.Models
{
    using System;
    using MovieDatabase.Data.Common.Models;

    public class Award: BaseDeletableModel<string>
    {
        public Award()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}