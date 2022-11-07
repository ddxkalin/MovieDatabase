namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Composer : BaseDeletableModel<string>
    {
        public Composer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<Movie> Composed { get; set; }
    }
}