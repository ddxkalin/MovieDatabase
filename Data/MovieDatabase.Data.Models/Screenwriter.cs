namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MovieDatabase.Data.Common.Models;

    public class Screenwriter : BaseDeletableModel<string>
    {
        public Screenwriter()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<Movie> Written { get; set; }
    }
}