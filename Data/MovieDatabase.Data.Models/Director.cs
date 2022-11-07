namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Director : BaseDeletableModel<string>
    {
        public Director()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<Movie> Directed { get; set; }
    }
}