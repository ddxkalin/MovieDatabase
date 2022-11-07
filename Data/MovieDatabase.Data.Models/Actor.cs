namespace MovieDatabase.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MovieDatabase.Data.Common.Models;

    public class Actor : BaseDeletableModel<string>
    {
        public Actor()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        //[Range(0, 150, ErrorMessage = "Invalid Age")]
        //public int Age { get; set; }

        //public string Nationality { get; set; }

        //public Gender Gender { get; set; }

        public virtual ICollection<MovieActor> StaredIn { get; set; }
    }
}