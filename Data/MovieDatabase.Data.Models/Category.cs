namespace MovieDatabase.Data.Models
{
    using MovieDatabase.Data.Common.Models;
    using System.Collections.Generic;

    public class Category : BaseDeletableModel<string>
    {
        public string Slug { get; set; }

        public virtual ICollection<MovieCategory> MoviesList { get; set; }
    }
}